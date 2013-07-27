namespace CostAnalysisWindow.Elements
{
    using System;
    using System.Collections.Generic;
    
    using CostAnalysisWindow.Elements;
    
    using Mono.Cecil;
    using Mono.Cecil.Cil;

    public class PropertyElement : CodeElement, IEquatable<PropertyElement>
    {
        private readonly FieldCollection fields = new FieldCollection();
        private readonly FieldCollection orphanedFields = new FieldCollection();
        private readonly PropertyDefinition property;

        public PropertyElement(PropertyDefinition property)
        {
            this.property = property;
        }
        
        public override string Name
        {
            get { return this.property.Name; }
        }

        public MethodDefinition Getter
        {
            get { return this.property.GetMethod; }
        }

        public PropertyDefinition Property
        {
            get { return this.property; }
        }

        public bool IsReadOnly
        {
            get { return (this.property.SetMethod == null); }
        }

        public FieldCollection Fields
        {
            get { return this.fields; }
        }

        public FieldCollection OrphanedFields
        {
            get { return this.orphanedFields; }
        }
        
        public FieldElement PrimaryFieldDefinition
        {
            get
            {
                if (this.fields.Count != 1)
                {
                    return null;
                }

                return this.fields[0];
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
