using System;
using System.Collections.Generic;

using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CostAnalysisWindow
{
    public class PropertyNode2 : IComparable<PropertyNode2>
    {
        private readonly List<PropertyNode2> m_DependentProperties = new List<PropertyNode2>();
        private readonly List<PropertyNode2> m_DependingProperties = new List<PropertyNode2>();
        private readonly List<FieldDefinition> m_FieldDefs = new List<FieldDefinition>();
        private readonly MethodDefinition m_Getter;
        private readonly PropertyDefinition m_Property;
        private readonly bool m_ReadonlyProperty;

        public PropertyNode2(
            PropertyDefinition property, 
            MethodDefinition getter,
            bool readonlyProperty)
        {
            m_Getter = getter;
            m_Property = property;
            m_ReadonlyProperty = readonlyProperty;
        }

        public MethodDefinition Getter
        {
            get { return m_Getter; }
        }

        public PropertyDefinition Property
        {
            get { return m_Property; }
        }

        public bool ReadonlyProperty
        {
            get { return m_ReadonlyProperty; }
        }

        public List<PropertyNode2> DependentProperties
        {
            get { return m_DependentProperties; }
        }

        public List<PropertyNode2> DependingProperties
        {
            get { return m_DependingProperties; }
        }

        public List<FieldDefinition> FieldDefs
        {
            get { return m_FieldDefs; }
        }

        public FieldDefinition PrimaryFieldDefinition
        {
            get
            {
                //if (m_ReadonlyProperty)
                //{
                //    return null;
                //}

                if (m_FieldDefs.Count != 1)
                {
                    return null;
                }

                return m_FieldDefs[0];
            }
        }

        public int CompareTo(PropertyNode2 other)
        {
            return this.Property.Name.CompareTo(other.Property.Name);
        }

        public override string ToString()
        {
            return this.Property.Name;
        }
    }
}
