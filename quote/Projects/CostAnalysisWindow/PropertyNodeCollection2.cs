using System;
using System.Collections.Generic;

using Mono.Cecil;

namespace CostAnalysisWindow
{
    public class PropertyNodeCollection2 : List<PropertyNode2>
    {
        public PropertyNode2 Find(PropertyDefinition property)
        {
            foreach (PropertyNode2 n in this)
            {
                if (property.Name == n.Property.Name)
                {
                    return n;
                }
            }

            return null;
        }

        public PropertyNode2 Find(string property)
        {
            foreach (PropertyNode2 n in this)
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

        public PropertyNodeCollection2 FindNodeWithDependent(string property)
        {
            PropertyNodeCollection2 result = new PropertyNodeCollection2();

            foreach (PropertyNode2 n in this)
            {
                bool found = false;

                foreach (PropertyNode2 child in n.DependentProperties)
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

        public PropertyNodeCollection2 FindNodePrimaryProperty(string property)
        {
            PropertyNodeCollection2 result = new PropertyNodeCollection2();

            foreach (PropertyNode2 child in this)
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
