namespace CostAnalysisWindow.Decompile
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    
    using ICSharpCode.Decompiler;
    using ICSharpCode.Decompiler.Ast;
    using ICSharpCode.Decompiler.Ast.Transforms;
    using ICSharpCode.Decompiler.Disassembler;
    
    using Mono.Cecil;
    using Mono.Cecil.Cil;
    
    internal class DecompileHelper
    {
        private Dictionary<string, StringWriter> codeDictionary = 
            new Dictionary<string, StringWriter>();
                
        public Dictionary<string, StringWriter> CodeDictionary
        {
            get { return this.codeDictionary; }
        }
        
        public static void DecompileProperty(
            PropertyDefinition property, 
            ITextOutput output, 
            DisassembleOptions options)
        {
            AstBuilder codeDomBuilder = CreateAstBuilder(
                options, 
                currentType: property.DeclaringType,
                currentMethod: property.GetMethod, 
                singleMember: true);
            codeDomBuilder.AddProperty(property);
            
            RunTransformsAndGenerateCode(codeDomBuilder, output);
        }
        
        public void Init3()
        {
            Model.Common.SavableProperties computationProperties = 
                PropertyAnalyzer2.BuildComputationProperties();

            Type computationPropertiesType = computationProperties.GetType();
            
            ModuleDefinition module = 
                Mono.Cecil.ModuleDefinition.ReadModule(
                    computationPropertiesType.Assembly.Location);
            
            TypeDefinition type = 
                PropertyAnalyzer2.LoadTypeDef(computationPropertiesType, module);
                
            this.codeDictionary.Clear();
            foreach (PropertyDefinition p in type.Properties)
            {
                StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture);
                PlainTextOutput plain = new PlainTextOutput(stringWriter);

                DisassembleOptions options = new DisassembleOptions();
                DecompilerSettings settings = new DecompilerSettings();
                options.DisassembleSettings = settings;
                
                DecompileProperty(p, plain, options);
                
                this.codeDictionary.Add(p.GetMethod.ToString(), stringWriter);
            }
        }
        
        private static AstBuilder CreateAstBuilder(
            DisassembleOptions options, 
            ModuleDefinition currentModule = null, 
            TypeDefinition currentType = null, 
            MethodDefinition currentMethod = null,
            bool singleMember = true)
        {
            if (currentModule == null)
            {
                currentModule = currentType.Module;
            }
            
            DecompilerSettings settings = options.DisassembleSettings;
            
            if (singleMember) 
            {
                settings = settings.Clone();
                settings.UsingDeclarations = false;
            }
           
            return new AstBuilder(
                new DecompilerContext(currentModule) 
                {
                    CancellationToken = options.CancellationToken,
                    CurrentType = currentType,
                    CurrentMethod = currentMethod,
                    Settings = settings
                });
        }
        
        private static void RunTransformsAndGenerateCode(
            AstBuilder astBuilder, 
            ITextOutput output)
        {
            var csharpUnit = astBuilder.CompilationUnit;
            csharpUnit.AcceptVisitor(new ICSharpCode.NRefactory.CSharp.InsertParenthesesVisitor() { InsertParenthesesForReadability = true });
            var unit = csharpUnit.AcceptVisitor(
                new ICSharpCode.NRefactory.VB.Visitors.CSharpToVBConverterVisitor(new ILSpyEnvironmentProvider()), null);
            var outputFormatter = new VBTextOutputFormatter(output);
            var formattingPolicy = new ICSharpCode.NRefactory.VB.VBFormattingOptions();
            unit.AcceptVisitor(new ICSharpCode.NRefactory.VB.OutputVisitor(outputFormatter, formattingPolicy), null);
        }
    }
}
