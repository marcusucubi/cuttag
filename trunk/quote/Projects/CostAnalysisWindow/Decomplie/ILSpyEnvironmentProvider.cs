namespace CostAnalysisWindow.Decompile
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using ICSharpCode.Decompiler;
    using ICSharpCode.Decompiler.Ast;
    using ICSharpCode.NRefactory.TypeSystem;
    using ICSharpCode.NRefactory.VB.Visitors;
    
    using Mono.Cecil;
    
    public class ILSpyEnvironmentProvider : IEnvironmentProvider
    {
        public string RootNamespace 
        {
            get 
            {
                return string.Empty;
            }
        }

        public string GetTypeNameForAttribute(ICSharpCode.NRefactory.CSharp.Attribute attribute)
        {
            return attribute.Type.Annotations
                .OfType<Mono.Cecil.MemberReference>()
                .First()
                .FullName;
        }
        
        public IType ResolveType(
            ICSharpCode.NRefactory.VB.Ast.AstType type, 
            ICSharpCode.NRefactory.VB.Ast.TypeDeclaration entity)
        {
            return SpecialType.UnknownType;
        }
        
        public TypeKind GetTypeKindForAstType(ICSharpCode.NRefactory.CSharp.AstType type)
        {
            var annotation = type.Annotation<TypeReference>();
            if (annotation == null)
            {
                return TypeKind.Unknown;
            }
            
            var definition = annotation.ResolveOrThrow();
            if (definition.IsClass)
            {
                return TypeKind.Class;
            }
            
            if (definition.IsInterface)
            {
                return TypeKind.Interface;
            }
            
            if (definition.IsEnum)
            {
                return TypeKind.Enum;
            }
            
            if (definition.IsFunctionPointer)
            {
                return TypeKind.Delegate;
            }
            
            if (definition.IsValueType)
            {
                return TypeKind.Struct;
            }
            
            return TypeKind.Unknown;
        }
        
        public TypeCode ResolveExpression(ICSharpCode.NRefactory.CSharp.Expression expression)
        {
            var annotation = expression.Annotations.OfType<TypeInformation>().FirstOrDefault();
            
            if (annotation == null)
            {
                return TypeCode.Object;
            }
            
            var definition = annotation.InferredType.Resolve();
            
            if (definition == null)
            {
                return TypeCode.Object;
            }
            
            switch (definition.FullName) 
            {
                case "System.String":
                    return TypeCode.String;
                default:
                    break;
            }
            
            return TypeCode.Object;
        }
        
        public bool? IsReferenceType(ICSharpCode.NRefactory.CSharp.Expression expression)
        {
            if (expression is ICSharpCode.NRefactory.CSharp.NullReferenceExpression)
            {
                return true;
            }
            
            var annotation = expression.Annotations.OfType<TypeInformation>().FirstOrDefault();
            
            if (annotation == null)
            {
                return null;
            }
            
            var definition = annotation.InferredType.Resolve();
            
            if (definition == null)
            {
                return null;
            }
            
            return !definition.IsValueType;
        }
        
        public IEnumerable<ICSharpCode.NRefactory.VB.Ast.InterfaceMemberSpecifier> CreateMemberSpecifiersForInterfaces(IEnumerable<ICSharpCode.NRefactory.VB.Ast.AstType> interfaces)
        {
            foreach (var type in interfaces) 
            {
                var def = type.Annotation<TypeReference>().Resolve();
                if (def == null) 
                {
                    continue;
                }
                
                foreach (var method in def.Methods.Where(m => !m.Name.StartsWith("get_") && !m.Name.StartsWith("set_"))) 
                {
                    yield return new ICSharpCode.NRefactory.VB.Ast.InterfaceMemberSpecifier((ICSharpCode.NRefactory.VB.Ast.AstType)type.Clone(), method.Name);
                }
                
                foreach (var property in def.Properties) 
                {
                    yield return new ICSharpCode.NRefactory.VB.Ast.InterfaceMemberSpecifier((ICSharpCode.NRefactory.VB.Ast.AstType)type.Clone(), property.Name);
                }
            }
        }
    }
}
