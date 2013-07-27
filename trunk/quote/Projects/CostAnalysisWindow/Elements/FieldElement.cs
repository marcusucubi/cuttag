namespace CostAnalysisWindow.Elements
{
    using System;
    using Mono.Cecil;

    public class FieldElement : CodeElement, IEquatable<FieldElement>
    {
        private readonly FieldDefinition field;
        
        public FieldElement(FieldDefinition field)
        {
            this.field = field;
        }
        
        public override string Name 
        {
            get { return this.field.Name; }
        }
        
        public FieldDefinition Field
        {
            get { return this.field; }
        }
        
        public bool Equals(FieldElement other)
        {
            return (this.field.Name == other.Name);
        }
        
        public override string ToString()
        {
            return this.Name;
        }

    }
}
