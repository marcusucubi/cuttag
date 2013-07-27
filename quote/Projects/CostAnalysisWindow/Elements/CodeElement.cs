﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CostAnalysisWindow.Elements
{
    public abstract class CodeElement : IComparable<CodeElement>
    {
        private readonly Collection<CodeElement> m_NodesBelow = new Collection<CodeElement>();
        private readonly Collection<CodeElement> m_NodesAbove = new Collection<CodeElement>();
        
        protected CodeElement()
        {
        }
        
        public abstract string Name
        {
            get;
        }
        
        public Collection<CodeElement> NodesBelow
        {
            get { return this.m_NodesBelow; }
        }

        public Collection<CodeElement> NodesAbove
        {
            get { return this.m_NodesAbove; }
        }

        public int CompareTo(CodeElement other)
        {
            return string.Compare(this.Name, other.Name, true, CultureInfo.CurrentCulture);
        }
        
        public override bool Equals(object obj)
        {
            CodeElement other = obj as CodeElement;
            if (other == null)
            {
                return false;
            }
            
            return this.Name == other.Name;
        }
        
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
        
        public static bool operator ==(CodeElement left, CodeElement right)
        {
            if (ReferenceEquals(left, right))
                return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;
            return left.Equals(right);
        }
        
        public static bool operator !=(CodeElement left, CodeElement right)
        {
            return !(left == right);
        }

        public static bool operator <(CodeElement element1, CodeElement element2) {

            return element1.CompareTo(element2) > 0;
        }

        public static bool operator >(CodeElement element1, CodeElement element2) {

            return element1.CompareTo(element2) < 0;
        }
        
    }
}
