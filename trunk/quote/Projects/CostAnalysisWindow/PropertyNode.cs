using System;
using System.Collections.Generic;

using Dile.Disassemble;
using Dile.Disassemble.ILCodes;

namespace SampleWindow
{
    public class PropertyNode : IComparable<PropertyNode>
    {
        private readonly List<PropertyNode> m_DependentProperties = new List<PropertyNode>();
        private readonly List<PropertyNode> m_DependingProperties = new List<PropertyNode>();
        private readonly List<FieldDefinition> m_FieldDefs = new List<FieldDefinition>();
        private readonly List<CodeLine> m_CodeLines = new List<CodeLine>();
        private readonly string m_Getter;
        private readonly Property m_Property;
        private readonly bool m_ReadonlyProperty;

        public PropertyNode(
            Property property, 
            string getter,
            bool readonlyProperty)
        {
            m_Getter = getter;
            m_Property = property;
            m_ReadonlyProperty = readonlyProperty;
        }

        public string Getter
        {
            get { return m_Getter; }
        }

        public Property Property
        {
            get { return m_Property; }
        }

        public bool ReadonlyProperty
        {
            get { return m_ReadonlyProperty; }
        }

        public List<PropertyNode> DependentProperties
        {
            get { return m_DependentProperties; }
        }

        public List<PropertyNode> DependingProperties
        {
            get { return m_DependingProperties; }
        }

        public List<FieldDefinition> FieldDefs
        {
            get { return m_FieldDefs; }
        }

        public List<CodeLine> CodeLines
        {
            get { return m_CodeLines; }
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

        public int CompareTo(PropertyNode other)
        {
            return this.Property.Name.CompareTo(other.Property.Name);
        }

        public override string ToString()
        {
            return this.Property.Name;
        }
    }

}
