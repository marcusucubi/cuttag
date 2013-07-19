using System;
using System.Collections.Generic;

using Mono.Cecil;

namespace CostAnalysisWindow
{
    public class PropertyNodeDataCollection : List<PropertyNode2Data>
    {
        public PropertyNode2Data Find(PropertyDefinition property)
        {
            foreach (PropertyNode2Data n in this)
            {
                if (property.Name == n.Property.Name)
                {
                    return n;
                }
            }

            return null;
        }

        public PropertyNode2Data Find(string property)
        {
            foreach (PropertyNode2Data n in this)
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

            foreach (PropertyNode2Data n in this)
            {
                bool found = false;

                foreach (PropertyNode2Data child in n.NodesBelow)
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

            foreach (PropertyNode2Data child in this)
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
