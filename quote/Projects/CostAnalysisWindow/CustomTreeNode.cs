using System;
using System.Windows.Forms;

using CostAnalysisWindow.Elements;

namespace CostAnalysisWindow
{
    public class CustomTreeNode : TreeNode
    {
        private CodeElement m_CodeElement;
        
        public CustomTreeNode(string text)
            : base(text)
        {
            UpdateImage();
        }
        
        public CodeElement CodeElement 
        { 
            get { return m_CodeElement; }
            set { m_CodeElement = value; UpdateImage(); }
        }
        
        public void ResetText()
        {
            PropertyElement prop = m_CodeElement as PropertyElement;
            if (prop != null)
            {
                this.Text = prop.Name;
            }
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
