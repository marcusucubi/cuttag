namespace CostAnalysisWindow.Elements
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    
    using Mono.Cecil;

    public class FieldCollection : Collection<FieldElement>
    {
        
        public FieldElement Find(MemberReference field)
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
