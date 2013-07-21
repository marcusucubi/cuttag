using System;
using System.Collections.Generic;
using System.IO;

using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Ast;
using ICSharpCode.Decompiler.Ast.Transforms;
using ICSharpCode.Decompiler.Disassembler;

using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CostAnalysisWindow.Decompile
{
    class DecompileHelper
    {
        //Predicate<IAstTransform> transformAbortCondition = null;
        
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
                context.Settings.IntroduceIncrementAndDecrement = true;
                
                AstBuilder astBuilder = new AstBuilder(context);
                
                astBuilder.AddType(type);
                
                StringWriter stringWriter = new StringWriter();
                PlainTextOutput plain = new PlainTextOutput(stringWriter);
                DebuggerTextOutput output = new DebuggerTextOutput(plain);
                
                _Dictionary = output.Dictionary; 
                
                astBuilder.GenerateCode(output);
                
            }
        }
        
        public void Init3()
        {
            Model.Common.SaveableProperties computationProperties = 
                PropertyAnalyzer2.BuildComputationProperties();

            Type computationPropertiesType = computationProperties.GetType();
            
            ModuleDefinition module = 
                Mono.Cecil.ModuleDefinition.ReadModule(
                    computationPropertiesType.Assembly.Location);
            
            TypeDefinition type = 
                PropertyAnalyzer2.LoadTypeDef(computationPropertiesType, module);
                
            _Dictionary.Clear();
            foreach(PropertyDefinition p in type.Properties)
            {
                MethodDefinition m = p.GetMethod;
                
//                DecompilerContext context = new DecompilerContext(type.Module);
//                
//                context.CurrentMethod = m;
//                context.Settings.ExpressionTrees = true;
//                context.Settings.FullyQualifyAmbiguousTypeNames = false;
//                context.Settings.CSharpFormattingOptions.IndentBlocks = true;
//                context.Settings.IntroduceIncrementAndDecrement = true;
//                
//                AstBuilder astBuilder = new AstBuilder(context);
//                
//                astBuilder.AddType(type);
//                
//                StringWriter stringWriter = new StringWriter();
//                PlainTextOutput plain = new PlainTextOutput(stringWriter);
//                DebuggerTextOutput output = new DebuggerTextOutput(plain);
//                
//                _Dictionary = output.Dictionary; 
//                
//                astBuilder.GenerateCode(output);
                
                
                StringWriter stringWriter = new StringWriter();
                PlainTextOutput plain = new PlainTextOutput(stringWriter);

                DecompilationOptions options = new DecompilationOptions();
                DecompilerSettings settings = new DecompilerSettings();
                options.DecompilerSettings = settings;
                
                DecompileProperty(p, plain, options);
                
                _Dictionary.Add(p.GetMethod.ToString(), stringWriter);
            }
        }
        
        public void DecompileProperty(
            PropertyDefinition property, 
            ITextOutput output, 
            DecompilationOptions options)
        {
            AstBuilder codeDomBuilder = CreateAstBuilder(
                options, currentType: property.DeclaringType, 
                currentMethod: property.GetMethod, 
                isSingleMember: true);
            codeDomBuilder.AddProperty(property);
            
            RunTransformsAndGenerateCode(codeDomBuilder, output, options);
        }
        
        AstBuilder CreateAstBuilder(
            DecompilationOptions options, 
            ModuleDefinition currentModule = null, 
            TypeDefinition currentType = null, 
            MethodDefinition currentMethod = null,
            bool isSingleMember = true)
        {
            if (currentModule == null)
            {
                currentModule = currentType.Module;
            }
            
            DecompilerSettings settings = options.DecompilerSettings;
            
            if (isSingleMember) 
            {
                settings = settings.Clone();
                settings.UsingDeclarations = false;
            }
           
            return new AstBuilder(
                new DecompilerContext(currentModule) {
                    CancellationToken = options.CancellationToken,
                    CurrentType = currentType,
                    CurrentMethod = currentMethod,
                    Settings = settings
                });
        }
        
        void RunTransformsAndGenerateCode(
            AstBuilder astBuilder, 
            ITextOutput output, 
            DecompilationOptions options)
        {
            //astBuilder.RunTransformations(transformAbortCondition);
            //astBuilder.GenerateCode(output);
            
            
            var csharpUnit = astBuilder.CompilationUnit;
            csharpUnit.AcceptVisitor(new ICSharpCode.NRefactory.CSharp.InsertParenthesesVisitor() { InsertParenthesesForReadability = true });
            var unit = csharpUnit.AcceptVisitor(
                new ICSharpCode.NRefactory.VB.Visitors.CSharpToVBConverterVisitor(
                    new ILSpyEnvironmentProvider()), null);
            var outputFormatter = new VBTextOutputFormatter(output);
            var formattingPolicy = new ICSharpCode.NRefactory.VB.VBFormattingOptions();
            unit.AcceptVisitor(new ICSharpCode.NRefactory.VB.OutputVisitor(outputFormatter, formattingPolicy), null);
            
        }
        
    }
}
