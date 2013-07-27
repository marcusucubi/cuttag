using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using ICSharpCode.Decompiler;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CostAnalysisWindow.Elements
{
    public class ElementBuilder
    {
        private readonly TypeDefinition m_Target;
        private readonly PropertyCollection m_Collection;
        
        public ElementBuilder(TypeDefinition target)
        {
            this.m_Target = target;
            this.m_Collection = new PropertyCollection();
        }
        
        public PropertyCollection Elements
        {
            get { return this.m_Collection; }
        }
        
        public void Build()
        {
            List<PropertyDefinition> props = this.LoadProperties();
            this.PopulateNodes(props);
            
            foreach (PropertyDefinition p in props)
            {
                PropertyElement cursor = this.m_Collection.Find(p);
                
                this.PopulateNodesBelowUsingMethodCalls(p, cursor);
                this.PopulateFields(p, cursor);
                this.PopulateNodesBelowUsingFields(p, cursor);
            }

            foreach (PropertyElement propNode in this.m_Collection)
            {
                PropertyCollection col =
                    this.m_Collection.FindNodeWithDependent(
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
            foreach (PropertyElement prop in this.m_Collection)
            {
                CleanupFields(prop);
            }
        }
        
        private static void CleanupFields(PropertyElement prop)
        {
            FieldCollection usedFields = prop.FieldsInNodesBellow;
            foreach (FieldElement element in prop.Fields)
            {
                if (!usedFields.Contains(element))
                {
                    if (!prop.OrphanedFields.Contains(element))
                    {
                        prop.OrphanedFields.Add(element);
                    }
                }
            }
            
        }
        
        private List<PropertyDefinition> LoadProperties()
        {
            List<PropertyDefinition> props = new List<PropertyDefinition>();
            foreach (PropertyDefinition p in this.m_Target.Properties)
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
                PropertyElement node = new PropertyElement(p);
                this.m_Collection.Add(node);
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
                
                PropertyElement other = this.m_Collection.Find(methodRef.Name);
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
                
                string name = fieldRef.FieldType.FullName;
                bool isSystem = (name.Contains("System"));
                bool isInt = (name.Contains("int"));
                bool isString = (name.Contains("string"));
                if (!isSystem && !isInt && !isString)
                {
                    continue;
                }
                
                bool found = false;
                PropertyCollection col = this.m_Collection.FindNodePrimaryProperty(fieldRef.Name);
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
                
                if (propertyNode.Fields.Find(fieldRef) == null)
                {
                    FieldElement fieldElement = new FieldElement(fieldRef);
                    propertyNode.Fields.Add(fieldElement);
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
                
                PropertyCollection primaryNodeCollection = 
                    this.m_Collection.FindNodePrimaryProperty(fieldRef.Name);
                foreach (PropertyElement primaryNode in primaryNodeCollection)
                {
                    propertyNode.NodesBelow.Add(primaryNode);
                }
            }
        }
        
    }
}
