using System;
using System.Collections.Generic;
using ICSharpCode.Decompiler;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CostAnalysisWindow.Elements
{
    public class ElementBuilder
    {
        private readonly TypeDefinition m_Target;
        private readonly ElementCollection m_Collection;
        
        public ElementBuilder(TypeDefinition target)
        {
            m_Target = target;
            m_Collection = new ElementCollection();
        }
        
        public ElementCollection Elements
        {
            get { return m_Collection; }
        }
        
        public void Build()
        {
            List<PropertyDefinition> props = LoadProperties();
            PopulateNodes(props);
            
            m_Collection.Sort();
            
            foreach (PropertyDefinition p in props)
            {
                PropertyElement cursor = m_Collection.Find(p);
                
                PopulateNodesBelowUsingMethodCalls(p, cursor);
                PopulateFields(p, cursor);
                PopulateNodesBelowUsingFields(p, cursor);
            }

            foreach (PropertyElement propNode in m_Collection)
            {
                ElementCollection col =
                    m_Collection.FindNodeWithDependent(
                        propNode.Property.Name);

                foreach (PropertyElement childNode in col)
                {
                    propNode.NodesAbove.Add(childNode);
                }
            }
            
            CleanupFields();
        }
        
        private void CleanupFields()
        {
            foreach(PropertyElement prop in m_Collection)
            {
                CleanupFields(prop);
            }
        }
        
        private void CleanupFields(PropertyElement prop)
        {
            FieldCollection orphanedFields = new FieldCollection();
            
            FieldCollection usedFields = prop.FieldsInNodesBellow;
            foreach(FieldElement element in prop.FieldDefs)
            {
                if (!usedFields.Contains(element))
                {
                    if (!prop.OrphanedFieldDefs.Contains(element))
                    {
                        prop.OrphanedFieldDefs.Add(element);
                    }
                }
            }
            
        }
        
        private List<PropertyDefinition> LoadProperties()
        {
            List<PropertyDefinition> props = new List<PropertyDefinition>();
            foreach (PropertyDefinition p in m_Target.Properties)
            {
                props.Add(p);
            }

            return props;
        }
        
        private void PopulateNodes(
            List<PropertyDefinition> props)
        {
            foreach (PropertyDefinition p in props)
            {
                MethodDefinition def = p.GetMethod;
                bool isReadonly = ( p.SetMethod == null );

                PropertyElement node = new PropertyElement(p);
                m_Collection.Add(node);
            }
        }
        
        private void PopulateNodesBelowUsingMethodCalls(
            PropertyDefinition property,
            PropertyElement propertyNode)
        {
            MethodDefinition def = property.GetMethod;

            foreach (Instruction line in def.Body.Instructions)
            {
                MethodReference methodRef = (line.Operand as MethodReference);
                if (methodRef == null)
                {
                    continue;
                }
                
                PropertyElement other = m_Collection.Find(methodRef.Name);
                if (other == null)
                {
                    continue;
                }
                
                if (propertyNode.Property.Name == other.Property.Name)
                {
                    continue;
                }
                
                if (!propertyNode.NodesBelow.Contains(other))
                {
                    propertyNode.NodesBelow.Add(other);
                }
            }
        }
        
        private void PopulateFields(
            PropertyDefinition property,
            PropertyElement propertyNode)
        {
            MethodDefinition def = property.GetMethod;
            
            foreach (Instruction line in def.Body.Instructions)
            {
                FieldDefinition fieldRef = (line.Operand as FieldDefinition);
                if (fieldRef == null)
                {
                    continue;
                }
                
                bool isSystem = (fieldRef.FieldType.FullName.IndexOf("System") != -1);
                bool isInt = (fieldRef.FieldType.FullName.IndexOf("int") != -1);
                bool isString = (fieldRef.FieldType.FullName.IndexOf("string") != -1);
                if (!isSystem && !isInt && !isString)
                {
                    continue;
                }
                
                bool found = false;
                ElementCollection col = m_Collection.FindNodePrimaryProperty(fieldRef.Name);
                foreach (PropertyElement n in col) 
                {
                    if (n.Property.Name == propertyNode.Property.Name)
                    {
                        found = true;
                    }
                }
                if (found)
                {
                    continue;
                }
                
                if (propertyNode.FieldDefs.Find(fieldRef) == null)
                {
                    FieldElement fieldElement = new FieldElement(fieldRef);
                    propertyNode.FieldDefs.Add(fieldElement);
                }
            }
        }
        
        private void PopulateNodesBelowUsingFields(
            PropertyDefinition property,
            PropertyElement propertyNode)
        {
            MethodDefinition def = property.GetMethod;

            foreach (Instruction line in def.Body.Instructions)
            {
                FieldDefinition fieldRef = (line.Operand as FieldDefinition);
                if (fieldRef == null)
                {
                    continue;
                }
                ElementCollection primaryNodeCollection = 
                    m_Collection.FindNodePrimaryProperty(fieldRef.Name);
                foreach (PropertyElement primaryNode in primaryNodeCollection)
                {
                    propertyNode.NodesBelow.Add(primaryNode);
                }
            }
        }
        
    }
}
