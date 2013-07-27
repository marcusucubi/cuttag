namespace CostAnalysisWindow.Elements
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    
    using ICSharpCode.Decompiler;
    
    using Mono.Cecil;
    using Mono.Cecil.Cil;

    public class ElementBuilder
    {
        private readonly TypeDefinition target;
        private readonly PropertyCollection collection;
        
        public ElementBuilder(TypeDefinition target)
        {
            this.target = target;
            this.collection = new PropertyCollection();
        }
        
        public PropertyCollection Elements
        {
            get { return this.collection; }
        }
        
        public void Build()
        {
            List<PropertyDefinition> props = this.LoadProperties();
            this.PopulateNodes(props);
            
            foreach (PropertyDefinition p in props)
            {
                PropertyElement cursor = this.collection.Find(p);
                
                this.PopulateNodesBelowUsingMethodCalls(p, cursor);
                this.PopulateFields(p, cursor);
                this.PopulateNodesBelowUsingFields(p, cursor);
            }

            foreach (PropertyElement propNode in this.collection)
            {
                PropertyCollection col =
                    this.collection.FindNodeWithDependent(
                        propNode.Property.Name);

                foreach (PropertyElement childNode in col)
                {
                    propNode.NodesAbove.Add(childNode);
                }
            }
            
            CleanupFields();
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
        
        private void CleanupFields()
        {
            foreach (PropertyElement prop in this.collection)
            {
                CleanupFields(prop);
            }
        }
        
        private List<PropertyDefinition> LoadProperties()
        {
            List<PropertyDefinition> props = new List<PropertyDefinition>();
            foreach (PropertyDefinition p in this.target.Properties)
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
                this.collection.Add(node);
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
                
                PropertyElement other = this.collection.Find(methodRef.Name);
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
                bool hasSystem = (name.Contains("System"));
                bool hasInt = (name.Contains("int"));
                bool hasString = (name.Contains("string"));
                if (!hasSystem && !hasInt && !hasString)
                {
                    continue;
                }
                
                bool found = false;
                PropertyCollection col = this.collection.FindNodePrimaryProperty(fieldRef.Name);
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
                    this.collection.FindNodePrimaryProperty(fieldRef.Name);
                foreach (PropertyElement primaryNode in primaryNodeCollection)
                {
                    propertyNode.NodesBelow.Add(primaryNode);
                }
            }
        }
        
    }
}
