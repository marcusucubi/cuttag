using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace CostAnalysisWindow
{
    public class PropertyNode2Data
    {
        private readonly List<PropertyNode2Data> m_NodesBelow = new List<PropertyNode2Data>();
        private readonly List<PropertyNode2Data> m_NodesAbove = new List<PropertyNode2>();
        private readonly List<FieldDefinition> m_FieldDefs = new List<FieldDefinition>();
        
        public PropertyDefinition Property { get; set; }

        public bool ReadonlyProperty
        {
            get { return (Property.SetMethod == null); }
        }

        public List<PropertyNode2Data> NodesBelow
        {
            get { return m_NodesBelow; }
        }

        public List<PropertyNode2> NodesAbove
        {
            get { return m_NodesAbove; }
        }
        
        public List<FieldDefinition> FieldDefs
        {
            get { return m_FieldDefs; }
        }
    }
}
