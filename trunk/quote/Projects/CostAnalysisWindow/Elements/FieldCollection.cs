using System;
using System.Collections.Generic;

using Mono.Cecil;

namespace CostAnalysisWindow.Elements
{
    public class FieldCollection : List<FieldElement>
    {
        
        public FieldElement Find(FieldDefinition field)
        {
            foreach (FieldElement n in this)
            {
                if (field.Name == n.Name)
                {
                    return n;
                }
            }

            return null;
        }
        
    }
}
