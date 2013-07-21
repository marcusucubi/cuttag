using System;
using Mono.Cecil;

namespace CostAnalysisWindow.Elements
{
    public class FieldElement : CodeElement, IEquatable<FieldElement>
    {
        private readonly FieldDefinition m_Field;
        
        public FieldElement(FieldDefinition field)
        {
            m_Field = field;
        }
        
        public override string Name 
        {
            get { return m_Field.Name; }
        }
        
        public FieldDefinition Field
        {
            get { return m_Field; }
        }
        
        public bool Equals(FieldElement other)
        {
            return (m_Field.Name == other.Name);
        }
        
        public override string ToString()
        {
            return Name;
        }

    }
}
