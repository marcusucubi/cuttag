using System;
using System.Windows.Forms;

using CostAnalysisWindow.Elements;

namespace CostAnalysisWindow
{
    public class CustomTreeNode : TreeNode
    {
        private CodeElement m_CodeElement;
        
        public CustomTreeNode(string text)
            : base("1234567890" + text)
        {
            UpdateImage();
        }
        
        public CodeElement CodeElement 
        { 
            get { return m_CodeElement; }
            set { m_CodeElement = value; UpdateImage(); }
        }
        
        public decimal PropertyValue
        {
            get;
            set;
        }
        
        public bool PropertyAttached
        {
            get;
            set;
        }
        
        public void UpdateImage()
        {
            if (m_CodeElement == null)
            {
                ImageIndex = 1;
            }
            
            PropertyElement prop = m_CodeElement as PropertyElement;
            if (prop == null)
            {
                ImageIndex = 5;
            }
            else
            {
                if (!prop.ReadonlyProperty)
                {
                    ImageIndex = 7;
                }
                else if (prop.NodesBelow.Count == 0)
                {
                    ImageIndex = 6;
                }
                else
                {
                    ImageIndex = 2;
                }
            }
        }
        
    }
}
