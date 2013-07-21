﻿using System;

using System.Collections.Generic;

using Mono.Cecil;

namespace CostAnalysisWindow.Elements
{
    public class ElementCollection : List<PropertyElement>
    {
        public PropertyElement Find(PropertyDefinition property)
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

        public ElementCollection FindNodeWithDependent(string property)
        {
            ElementCollection result = new ElementCollection();

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

        public ElementCollection FindNodePrimaryProperty(string property)
        {
            ElementCollection result = new ElementCollection();

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
        
        public ElementCollection TopLevelElements
        {
            get
            {
                ElementCollection result = new ElementCollection();
    
                foreach (PropertyElement child in this)
                {
                    
                    if (child.NodesAbove.Count == 0)
                    {
                        result.Add(child);
                    }
                }
    
                return result;
            }
        }
    }
}
