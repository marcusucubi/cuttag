using System;
using System.Collections.Generic;
using System.IO;

using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Ast;
using ICSharpCode.Decompiler.Disassembler;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CostAnalysisWindow
{
    class DecompileHelper
    {
        private Dictionary<string, StringWriter> _Dictionary = 
            new Dictionary<string, StringWriter>();
                
        public Dictionary<string, StringWriter> Dictionary
        {
            get { return _Dictionary; }
        }
        
        public void Init2()
        {
            Model.Common.SaveableProperties computationProperties = 
                PropertyAnalyzer2.BuildComputationProperties();

            Type computationPropertiesType = computationProperties.GetType();
            
            ModuleDefinition module = 
                Mono.Cecil.ModuleDefinition.ReadModule(
                    computationPropertiesType.Assembly.Location);
            
            TypeDefinition type = 
                PropertyAnalyzer2.LoadTypeDef(computationPropertiesType, module);
                
            foreach(PropertyDefinition p in type.Properties)
            {
                MethodDefinition m = p.GetMethod;
                
                DecompilerContext context = new DecompilerContext(type.Module);
                
                context.CurrentMethod = m;
                context.Settings.ExpressionTrees = true;
                context.Settings.FullyQualifyAmbiguousTypeNames = false;
                context.Settings.CSharpFormattingOptions.IndentBlocks = true;
                
                AstBuilder astBuilder = new AstBuilder(context);
                
                astBuilder.AddType(type);
                
                StringWriter stringWriter = new StringWriter();
                PlainTextOutput plain = new PlainTextOutput(stringWriter);
                DebuggerTextOutput output = new DebuggerTextOutput(plain);
                
                _Dictionary = output.Dictionary; 
                
                astBuilder.GenerateCode(output);
                
            }
        }
        
    }
}
