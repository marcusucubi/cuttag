using System;
using System.Collections.Generic;

using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CostAnalysisWindow
{
    public abstract class CodeElement : IComparable<CodeElement>
    {
        private readonly List<CodeElement> m_NodesBelow = new List<CodeElement>();
        private readonly List<CodeElement> m_NodesAbove = new List<CodeElement>();
        
        public CodeElement()
        {
        }
        
        public abstract string Name
        {
            get;
        }
        
        public List<CodeElement> NodesBelow
        {
            get { return m_NodesBelow; }
        }

        public List<CodeElement> NodesAbove
        {
            get { return m_NodesAbove; }
        }

        public int CompareTo(CodeElement other)
        {
            return this.Name.CompareTo(other.Name);
        }

    }
}
