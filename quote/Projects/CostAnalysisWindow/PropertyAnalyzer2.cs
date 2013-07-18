using System;
using System.Collections.Generic;

using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CostAnalysisWindow
{
    public class PropertyAnalyzer2
    {
        private PropertyNodeCollection2 m_Nodes = new PropertyNodeCollection2();

        public PropertyNodeCollection2 Nodes
        {
            get { return m_Nodes; }
        }
        
        private void PopulateNodes(
            TypeDefinition typeDefinition, 
            List<PropertyDefinition> props)
        {
            foreach (PropertyDefinition p in props)
            {
                MethodDefinition def = p.GetMethod;
                bool isReadonly = ( p.SetMethod == null );

                PropertyNode2 node = new PropertyNode2(p, def, isReadonly);
                m_Nodes.Add(node);
            }
        }
        
        public void Init()
        {
            Model.Common.SaveableProperties computationProperties = 
                BuildComputationProperties();

            Type computationPropertiesType = computationProperties.GetType();
            
            ModuleDefinition module = 
                Mono.Cecil.ModuleDefinition.ReadModule(
                    computationPropertiesType.Assembly.Location);
            
            TypeDefinition typeDefinition = 
                LoadTypeDef(computationPropertiesType, module);
            List<PropertyDefinition> props = LoadProperties(typeDefinition);

            PopulateNodes(typeDefinition, props);
            m_Nodes.Sort();

            foreach (PropertyDefinition p in props)
            {
                PropertyNode2 cursor = m_Nodes.Find(p);

                ProcessPropteryDef(p, cursor);
                ProcessPropteryDef2(p, cursor);
                ProcessPropteryDef3(p, cursor);
            }

            foreach (PropertyNode2 propNode in m_Nodes)
            {
                PropertyNodeCollection2 col =
                    m_Nodes.FindNodeWithDependent(
                        propNode.Property.Name);

                foreach (PropertyNode2 childNode in col)
                {
                    propNode.DependingProperties.Add(childNode);
                }
            }
        }
        
        private void ProcessPropteryDef(
            PropertyDefinition property,
            PropertyNode2 propertyNode)
        {
            MethodDefinition def = property.GetMethod;

            foreach (Instruction line in def.Body.Instructions)
            {
                MethodReference methodRef = (line.Operand as MethodReference);
                if (methodRef == null)
                {
                    continue;
                }
    
                PropertyNode2 other = m_Nodes.Find(methodRef.Name);
                if (other == null)
                {
                    continue;
                }
                
                if (propertyNode.Property.Name == other.Property.Name)
                {
                    continue;
                }
                
                if (!propertyNode.DependentProperties.Contains(other))
                {
                    propertyNode.DependentProperties.Add(other);
                }
            }
        }
        
        private void ProcessPropteryDef2(
            PropertyDefinition property,
            PropertyNode2 propertyNode)
        {
            MethodDefinition def = property.GetMethod;

            foreach (Instruction line in def.Body.Instructions)
            {
                FieldDefinition fieldRef = (line.Operand as FieldDefinition);
                if (fieldRef == null)
                {
                    continue;
                }
    
                PropertyNode2 other = m_Nodes.Find(fieldRef.Name);
                if (other != null)
                {
                    if (propertyNode.Property.Name == other.Property.Name)
                    {
                        continue;
                    }
                }
                
                bool isSystem = (fieldRef.FieldType.FullName.IndexOf("System") != -1);
                bool isInt = (fieldRef.FieldType.FullName.IndexOf("int") != -1);
                bool isString = (fieldRef.FieldType.FullName.IndexOf("string") != -1);
                if (!isSystem && !isInt && !isString)
                {
                    continue;
                }
                
                if (!propertyNode.FieldDefs.Contains(fieldRef))
                {
                    propertyNode.FieldDefs.Add(fieldRef);
                }
            }
        }
        
        private void ProcessPropteryDef3(
            PropertyDefinition property,
            PropertyNode2 propertyNode)
        {
            MethodDefinition def = property.GetMethod;

            foreach (Instruction line in def.Body.Instructions)
            {
                FieldDefinition fieldRef = (line.Operand as FieldDefinition);
                if (fieldRef == null)
                {
                    continue;
                }

                PropertyNodeCollection2 primaryNodeCollection = 
                    m_Nodes.FindNodePrimaryProperty(fieldRef.Name);
                foreach (PropertyNode2 primaryNode in primaryNodeCollection)
                {
                    propertyNode.DependentProperties.Add(primaryNode);
                }
            }
        }
        
        private static List<PropertyDefinition> LoadProperties(
            TypeDefinition typeDef)
        {
            List<PropertyDefinition> props = new List<PropertyDefinition>();
            foreach (PropertyDefinition p in typeDef.Properties)
            {
                props.Add(p);
            }

            return props;
        }
        
        private static Model.Common.SaveableProperties BuildComputationProperties()
        {
            Model.Template.Header header = new Model.Template.Header();
            Model.Common.SaveableProperties computationProperties =
                Model.Template.PropertyFactory.Instance.CreateComputationProperties(header, 1);
            Model.Template.IComputationWrapper wrapper =
                computationProperties as Model.Template.IComputationWrapper;

            if (wrapper != null)
            {
                computationProperties = wrapper.GetComputationProperties();
            }

            return computationProperties;
        }
        
        private static TypeDefinition LoadTypeDef(
            Type t,
            ModuleDefinition module)
        {
            TypeDefinition result = null;
            
            foreach (TypeDefinition type in module.Types) 
            {
                if (!type.IsPublic)
                {
                    continue;
                }
        
                if (type.FullName != t.FullName)
                {
                    continue;
                }
                
                result = type;
            }
            
            return result;
        }

    }
}
