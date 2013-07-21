using System;
using Mono.Cecil;

namespace CostAnalysisWindow.Elements
{
    public class FieldElement : CodeElement
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
    }
}
