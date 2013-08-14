namespace CostAnalysisWindow
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    
    using CostAnalysisWindow.Elements;
    
    using Mono.Cecil;
    using Mono.Cecil.Cil;
    
    public class PropertyAnalyzer2
    {
        private PropertyCollection nodes = new PropertyCollection();

        public ReadOnlyCollection<PropertyElement> Nodes
        {
            get { return new ReadOnlyCollection<PropertyElement>(this.nodes); }
        }
        
        public static Model.Common.ISavableProperties BuildComputationProperties()
        {
            Model.Template.Header header = new Model.Template.Header();
            Model.Common.ISavableProperties computationProperties =
                Model.Template.Ext.PropertyFactory.CreateComputationProperties(header, 1);
            Model.Template.Ext.IComputationWrapper wrapper =
                computationProperties as Model.Template.Ext.IComputationWrapper;

            if (wrapper != null)
            {
                computationProperties = wrapper.ComputationProperties;
            }

            return computationProperties;
        }
        
        public static TypeDefinition LoadTypeDef(
            Type findType,
            ModuleDefinition module)
        {
            TypeDefinition result = null;
            
            foreach (TypeDefinition type in module.Types) 
            {
                if (!type.IsPublic)
                {
                    continue;
                }
        
                if (type.FullName != findType.FullName)
                {
                    continue;
                }
                
                result = type;
            }
            
            return result;
        }

        public void Init()
        {
            Model.Common.ISavableProperties computationProperties = 
                BuildComputationProperties();

            Type computationPropertiesType = computationProperties.GetType();
            
            ModuleDefinition module = 
                Mono.Cecil.ModuleDefinition.ReadModule(
                    computationPropertiesType.Assembly.Location);
            
            TypeDefinition typeDefinition = 
                LoadTypeDef(computationPropertiesType, module);
            
            ElementBuilder builder = new Elements.ElementBuilder(typeDefinition);
            builder.Build();
            this.nodes = builder.Elements;
        }
    }
}
