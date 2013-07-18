using System;
using System.Collections.Generic;
using System.IO;

using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Ast;

using Mono.Cecil;

namespace CostAnalysisWindow
{
    class DecompileHelper
    {
        private DebuggerTextOutput _DebuggerTextOutput;
                
        public DebuggerTextOutput DebuggerTextOutput
        {
            get { return _DebuggerTextOutput; }
        }
        
        public void Init()
        {
            try
            {
                Model.Common.SaveableProperties computationProperties = 
                    PropertyAnalyzer2.BuildComputationProperties();
    
                Type computationPropertiesType = computationProperties.GetType();
                
                ModuleDefinition module = 
                    Mono.Cecil.ModuleDefinition.ReadModule(
                        computationPropertiesType.Assembly.Location);
                
                TypeDefinition type = 
                    PropertyAnalyzer2.LoadTypeDef(computationPropertiesType, module);
                
                DecompilerContext context = new DecompilerContext(type.Module);
                AstBuilder astBuilder = new AstBuilder(context);
                
                astBuilder.AddType(type);
                
                StringWriter stringWriter = new StringWriter();
                PlainTextOutput plain = new PlainTextOutput(stringWriter);
                _DebuggerTextOutput = new DebuggerTextOutput(plain);
                
                astBuilder.GenerateCode(_DebuggerTextOutput);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
        
    }
}
