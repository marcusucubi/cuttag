using System;
using Mono.Cecil;

namespace CostAnalysisWindow.Elements
{
    public class FieldElement : CodeElement, IEquatable<FieldElement>
    {
        private readonly FieldDefinition m_Field;
        
        public FieldElement(FieldDefinition field)
        {
            this.m_Field = field;
        }
        
        public override string Name 
        {
            get { return this.m_Field.Name; }
        }
        
        public FieldDefinition Field
        {
            get { return this.m_Field; }
        }
        
        public bool Equals(FieldElement other)
        {
            return (this.m_Field.Name == other.Name);
        }
        
        public override string ToString()
        {
            return this.Name;
        }

    }
}
