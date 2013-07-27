namespace CostAnalysisWindow.Elements
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    
    using Mono.Cecil;

    public class PropertyCollection : Collection<PropertyElement>
    {
        public PropertyCollection()
        {
        }
        
        public PropertyCollection(ReadOnlyCollection<PropertyElement> nodes)
            : base(nodes)
        {
        }
        
        public PropertyElement Find(MemberReference property)
        {
            foreach (PropertyElement n in this)
            {
                if (property.Name == n.Property.Name)
                {
                    return n;
                }
            }

            return null;
        }

        public PropertyElement Find(string property)
        {
            foreach (PropertyElement n in this)
            {
                if (property == n.Property.Name)
                {
                    return n;
                }

                if (n.Getter != null && property == n.Getter.Name)
                {
                    return n;
                }
            }

            return null;
        }

        public PropertyCollection FindNodeWithDependent(string property)
        {
            PropertyCollection result = new PropertyCollection();

            foreach (PropertyElement n in this)
            {
                bool found = false;

                foreach (PropertyElement child in n.NodesBelow)
                {
                    if (child.Property.Name == property)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    result.Add(n);
                }
            }

            return result;
        }

        public PropertyCollection FindNodePrimaryProperty(string property)
        {
            PropertyCollection result = new PropertyCollection();

            foreach (PropertyElement child in this)
            {
                if (child.PrimaryFieldDefinition == null)
                {
                    continue;
                }

                if (child.PrimaryFieldDefinition.Name == property)
                {
                    result.Add(child);
                }
            }

            return result;
        }
    }
}
