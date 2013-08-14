namespace Model.IO
{
    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    
    using Microsoft.VisualBasic;

    public class ObjectGenerator
    {
        private List<ObjectGeneratorPropertyInfo> infoList = new List<ObjectGeneratorPropertyInfo>();

        private List<string> nameList = new List<string>();
        
        public ObjectGenerator()
        {
            this.ClassName = "GeneratedProperties";
        }
        
        public string ClassName { get; set; }
        
        public string BaseTypeName { get; set; }
        
        public string InterfaceToImplement { get; set; }
        
        public object InitObject { get; set; }
        
        public ReadOnlyCollection<string> NameList 
        {
            get { return new ReadOnlyCollection<string>(this.nameList); }
        }

        public void Add(ObjectGeneratorPropertyInfo node)
        {
            if (!this.nameList.Contains(node.Name)) 
            {
                this.infoList.Add(node);
                this.nameList.Add(node.Name);
            }
        }

        internal object Generate()
        {
            CodeCompileUnit compileUnit = new CodeCompileUnit();
            CodeNamespace samples = new CodeNamespace("Generated");

            samples.Imports.Add(new CodeNamespaceImport("System"));
            samples.Imports.Add(new CodeNamespaceImport("System.ComponentModel"));
            
            samples.Imports.Add(new CodeNamespaceImport("Model.Common"));
            
            compileUnit.Namespaces.Add(samples);
            CodeTypeDeclaration class1 = new CodeTypeDeclaration(this.ClassName);
            samples.Types.Add(class1);
            
            if (this.BaseTypeName.Length > 0) 
            {
                class1.BaseTypes.Add(this.BaseTypeName);
                
                if (this.InterfaceToImplement != null)
                {
                    class1.BaseTypes.Add(this.InterfaceToImplement);
                }
            }

            foreach (ObjectGeneratorPropertyInfo node in this.infoList) 
            {
                if (node.Value == null & node.CodeSnippet == null) 
                {
                    this.AddProperty(class1, node.Name);
                } 
                else
                {
                    this.AddProperty(
                        class1, 
                        node.Name, 
                        node.TypeName, 
                        node.Value, 
                        node.CodeSnippet, 
                        node.Category, 
                        node.Description);
                }
            }

            string code = GenerateCode(compileUnit);
            return this.CompileCode(code);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Justification = "Ok to ignore")]
        private static string GenerateCode(CodeCompileUnit compileunit)
        {
            VBCodeProvider provider = new VBCodeProvider();
            string sourceFile = null;

            using (StringWriter sw = new StringWriter(CultureInfo.CurrentCulture)) 
            {
                IndentedTextWriter tw = new IndentedTextWriter(sw, "    ");
                provider.GenerateCodeFromCompileUnit(compileunit, tw, new CodeGeneratorOptions());
                tw.Close();
                sourceFile = sw.ToString();
            }

            return sourceFile;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Justification = "Ok to ignore")]
        private object CompileCode(string sourceFile)
        {
            Common.SavableProperties result = null;
            
            VBCodeProvider provider = new VBCodeProvider();
            CompilerParameters cp = new CompilerParameters();

            cp.ReferencedAssemblies.Add("System.dll");
            cp.ReferencedAssemblies.Add("PluginModel.dll");

            cp.GenerateExecutable = false;
            cp.GenerateInMemory = true;
            CompilerResults cr = provider.CompileAssemblyFromSource(cp, sourceFile);

            if (cr.Errors.Count > 0) 
            {
                StringWriter writer = new StringWriter(CultureInfo.CurrentCulture);
                foreach (CompilerError ce in cr.Errors) 
                {
                    writer.WriteLine("{0}", ce.ToString());
                    writer.WriteLine();
                }
                
                throw new ObjectGeneratorException(writer.ToString());
            }

            Assembly ca = cr.CompiledAssembly;
            Type[] classes = ca.GetTypes();
            foreach (Type t in classes) 
            {
                if (t.Name == this.ClassName) 
                {
                    result = Activator.CreateInstance(t) as Common.SavableProperties;
                    if (this.InitObject != null) 
                    {
                        // TODO
                        //result.Subject = this.InitObject;
                    }
                }
            }

            if (cr.Errors.Count > 0) 
            {
                throw new DB.DatabaseException();
            }

            return result;
        }

        private void AddProperty(
            CodeTypeDeclaration class1, 
            string name, 
            string typeName = "System.String", 
            object value = null, 
            string snippet = null, 
            string category = "", 
            string desc = "")
        {
            CodeMemberProperty property1 = new CodeMemberProperty();
            property1.Name = name;
            property1.Type = new CodeTypeReference(typeName);
            property1.Attributes = MemberAttributes.Public;
            property1.HasSet = false;
            
            if (value == null) 
            {
                if (typeName == "System.Decimal") 
                {
                    value = 0;
                }
                
                if (typeName == "System.Int32") 
                {
                    value = 0;
                }
                
                if (typeName == "System.String") 
                {
                    value = string.Empty;
                }
                
                if (typeName == "System.Boolean") 
                {
                    value = string.Empty;
                }
            }

            if (snippet == null) 
            {
                property1.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(value)));
            } 
            else 
            {
                if (this.InitObject != null) 
                {
                    foreach (System.Reflection.PropertyInfo node in this.InitObject.GetType().GetProperties()) 
                    {
                        CodePropertyReferenceExpression variableRef1 = 
                            new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Subject");
                        CodePropertyReferenceExpression propertyRef = 
                            new CodePropertyReferenceExpression(variableRef1, node.Name);
                        CodeVariableDeclarationStatement variableDeclaration = 
                            new CodeVariableDeclarationStatement(node.PropertyType, node.Name, propertyRef);
                        
                        property1.GetStatements.Add(variableDeclaration);
                    }
                }

                property1.GetStatements.Add(new CodeMethodReturnStatement(new CodeSnippetExpression(snippet)));
            }

            var arg = new CodeAttributeArgument(new CodePrimitiveExpression(category));
            property1.CustomAttributes.Add(new CodeAttributeDeclaration("CategoryAttribute", arg));
            var arg2 = new CodeAttributeArgument(new CodePrimitiveExpression(desc));
            property1.CustomAttributes.Add(new CodeAttributeDeclaration("DescriptionAttribute", arg2));
            class1.Members.Add(property1);
        }
    }
}
