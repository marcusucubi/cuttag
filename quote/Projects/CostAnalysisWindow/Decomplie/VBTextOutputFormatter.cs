using System;
using System.Collections.Generic;

using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.ILAst;
using ICSharpCode.NRefactory.VB;
using ICSharpCode.NRefactory.VB.Ast;

using Mono.Cecil;

namespace CostAnalysisWindow.Decompile
{
    /// <summary>
    /// Description of VBTextOutputFormatter.
    /// </summary>
    public class VBTextOutputFormatter : IOutputFormatter
    {
        readonly ITextOutput output;
        readonly Stack<AstNode> nodeStack = new Stack<AstNode>();
        
        public VBTextOutputFormatter(ITextOutput output)
        {
            if (output == null)
                throw new ArgumentNullException("output");
            this.output = output;
        }
        
        public void StartNode(AstNode node)
        {
            this.nodeStack.Push(node);
        }
        
        public void EndNode(AstNode node)
        {
            if (this.nodeStack.Pop() != node) 
            {
                throw new InvalidOperationException();
            }
        }
        
        public void WriteIdentifier(string identifier)
        {
            var definition = this.GetCurrentDefinition();
            if (definition != null) 
            {
                this.output.WriteDefinition(identifier, definition);
                return;
            }
            
            object memberRef = this.GetCurrentMemberReference();
            if (memberRef != null) 
            {
                this.output.WriteReference(identifier, memberRef);
                return;
            }

            definition = this.GetCurrentLocalDefinition();
            if (definition != null) 
            {
                this.output.WriteDefinition(identifier, definition);
                return;
            }

            memberRef = this.GetCurrentLocalReference();
            if (memberRef != null) 
            {
                this.output.WriteReference(identifier, memberRef, true);
                return;
            }

            this.output.Write(identifier);
        }

        MemberReference GetCurrentMemberReference()
        {
            AstNode node = this.nodeStack.Peek();
            MemberReference memberRef = node.Annotation<MemberReference>();
            if (memberRef == null && node.Role == AstNode.Roles.TargetExpression && (node.Parent is InvocationExpression || node.Parent is ObjectCreationExpression)) {
                memberRef = node.Parent.Annotation<MemberReference>();
            }
            return memberRef;
        }

        object GetCurrentLocalReference()
        {
            AstNode node = this.nodeStack.Peek();
            ILVariable variable = node.Annotation<ILVariable>();
            if (variable != null) {
                if (variable.OriginalParameter != null)
                {
                    return variable.OriginalParameter;
                }
                return variable;
            }
            return null;
        }

        object GetCurrentLocalDefinition()
        {
            AstNode node = this.nodeStack.Peek();
            var parameterDef = node.Annotation<ParameterDefinition>();
            if (parameterDef != null)
            {
                return parameterDef;
            }

            if (node is VariableInitializer || node is CatchBlock || node is ForEachStatement) 
            {
                var variable = node.Annotation<ILVariable>();
                if (variable != null) 
                {
                    if (variable.OriginalParameter != null)
                    {
                        return variable.OriginalParameter;
                    }
                    
                    return variable;
                } 
                else 
                {

                }
            }

            return null;
        }
        
        object GetCurrentDefinition()
        {
            if (this.nodeStack == null || this.nodeStack.Count == 0)
            {
                return null;
            }
            
            var node = this.nodeStack.Peek();
            if (IsDefinition(node))
            {
                return node.Annotation<MemberReference>();
            }
            
            node = node.Parent;
            if (IsDefinition(node))
            {
                return node.Annotation<MemberReference>();
            }

            return null;
        }
        
        public void WriteKeyword(string keyword)
        {
            this.output.Write(keyword);
        }
        
        public void WriteToken(string token)
        {
            // Attach member reference to token only if there's no identifier in the current node.
            MemberReference memberRef = this.GetCurrentMemberReference();
            if (memberRef != null && this.nodeStack.Peek().GetChildByRole(AstNode.Roles.Identifier).IsNull)
            {
                this.output.WriteReference(token, memberRef);
            }
            else
            {
                this.output.Write(token);
            }
        }
        
        public void Space()
        {
            this.output.Write(' ');
        }
        
        public void Indent()
        {
            this.output.Indent();
        }
        
        public void Unindent()
        {
            this.output.Unindent();
        }
        
        public void NewLine()
        {
            this.output.WriteLine();
        }
        
        public void WriteComment(bool isDocumentation, string content)
        {
            if (isDocumentation)
            {
                this.output.Write("'''");
            }
            else
            {
                this.output.Write("'");
            }
            
            this.output.WriteLine(content);
        }
        
        public void MarkFoldStart()
        {
            this.output.MarkFoldStart();
        }
        
        public void MarkFoldEnd()
        {
            this.output.MarkFoldEnd();
        }
        
        private static bool IsDefinition(AstNode node)
        {
            return
                node is FieldDeclaration ||
                node is ConstructorDeclaration ||
                node is EventDeclaration ||
                node is DelegateDeclaration ||
                node is OperatorDeclaration ||
                node is MemberDeclaration ||
                node is TypeDeclaration;
        }
    }
}
