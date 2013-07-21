using System;
using System.Collections.Generic;

using CostAnalysisWindow.Elements;

using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CostAnalysisWindow
{
    public class PropertyElement : CodeElement
    {
        private readonly FieldCollection m_FieldDefs = new FieldCollection();
        private readonly PropertyDefinition m_Property;

        public PropertyElement(PropertyDefinition property)
        {
            m_Property = property;
        }
        
        public override string Name
        {
            get { return m_Property.Name; }
        }

        public MethodDefinition Getter
        {
            get { return m_Property.GetMethod; }
        }

        public PropertyDefinition Property
        {
            get { return m_Property; }
        }

        public bool ReadonlyProperty
        {
            get { return (m_Property.SetMethod == null); }
        }

        public FieldCollection FieldDefs
        {
            get { return m_FieldDefs; }
        }

        public FieldDefinition FieldReturned
        {
            get; set;
        }
        
        public FieldElement PrimaryFieldDefinition
        {
            get
            {
                if (m_FieldDefs.Count != 1)
                {
                    return null;
                }

                return m_FieldDefs[0];
            }
        }

        public override string ToString()
        {
            return this.Property.Name;
        }
    }
}
