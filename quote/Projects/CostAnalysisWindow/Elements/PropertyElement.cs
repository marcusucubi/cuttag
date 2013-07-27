using System;
using System.Collections.Generic;

using CostAnalysisWindow.Elements;

using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CostAnalysisWindow.Elements
{
    public class PropertyElement : CodeElement, IEquatable<PropertyElement>
    {
        private readonly FieldCollection m_Fields = new FieldCollection();
        private readonly FieldCollection m_OrphanedFields = new FieldCollection();
        private readonly PropertyDefinition m_Property;

        public PropertyElement(PropertyDefinition property)
        {
            this.m_Property = property;
        }
        
        public override string Name
        {
            get { return this.m_Property.Name; }
        }

        public MethodDefinition Getter
        {
            get { return this.m_Property.GetMethod; }
        }

        public PropertyDefinition Property
        {
            get { return this.m_Property; }
        }

        public bool IsReadOnly
        {
            get { return (this.m_Property.SetMethod == null); }
        }

        public FieldCollection Fields
        {
            get { return this.m_Fields; }
        }

        public FieldCollection OrphanedFields
        {
            get { return this.m_OrphanedFields; }
        }
        
        public FieldElement PrimaryFieldDefinition
        {
            get
            {
                if (this.m_Fields.Count != 1)
                {
                    return null;
                }

                return this.m_Fields[0];
            }
        }
        
        public FieldCollection FieldsInNodesBellow
        {
            get
            {
                FieldCollection result = new FieldCollection();
                
                foreach (PropertyElement prop in this.NodesBelow)
                {
                    foreach (FieldElement field in prop.Fields)
                    {
                        result.Add(field);
                    }
                }
                
                return result;
            }
        }

        public bool Equals(PropertyElement other)
        {
            return (this.Name == other.Name);
        }
        
        public override string ToString()
        {
            return this.Property.Name;
        }
        
    }
}
