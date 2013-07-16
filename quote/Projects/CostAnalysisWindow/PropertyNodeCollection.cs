using System;
using System.Collections.Generic;

using Dile.Disassemble;

namespace SampleWindow
{
    public class PropertyNodeCollection : List<PropertyNode>
    {
        public PropertyNode Find(Property property)
        {
            foreach (PropertyNode n in this)
            {
                if (property.Name == n.Property.Name)
                {
                    return n;
                }
            }

            return null;
        }

        public PropertyNode Find(string property)
        {
            foreach (PropertyNode n in this)
            {
                if (property == n.Property.Name)
                {
                    return n;
                }

                if (property == n.Getter)
                {
                    return n;
                }
            }

            return null;
        }

        public PropertyNodeCollection FindNodeWithDependent(string property)
        {
            PropertyNodeCollection result = new PropertyNodeCollection();

            foreach (PropertyNode n in this)
            {
                bool found = false;

                foreach (PropertyNode child in n.DependentProperties)
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

        public PropertyNodeCollection FindNodePrimaryProperty(string property)
        {
            PropertyNodeCollection result = new PropertyNodeCollection();

            foreach (PropertyNode child in this)
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
