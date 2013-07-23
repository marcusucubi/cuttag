using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using CostAnalysisWindow.Elements;

namespace CostAnalysisWindow
{
    public class CustomTreeNode : TreeNode
    {
        private CodeElement m_CodeElement;
        
        public CustomTreeNode(string text, INotifyPropertyChanged subject)
            : base(text + "                            ")
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
        
        private string NumValue
        {
            get 
            {
                string result = "";
                if (this.CodeElement != null)
                {
                    if (this.PropertyAttached)
                    {
                        result = this.PropertyValue.ToString("#,##0.0000");
                        
                        int padSize = (Math.Max(0, 10 - result.Length));
                        string padding = "          ".Substring(0, padSize);
                        
                        result = padding + result;
                    }
                }
                
                return result;
            }
        }
        
        private string TextValue
        {
            get 
            {
                string result = "Costs";
                if (this.CodeElement != null)
                {
                    result = this.CodeElement.Name;
                }
                
                return result;
            }
        }
        
        public void OnDraw(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Bounds.Left == -1)
            {
                return;
            }
            
            string text = this.TextValue;
            string numValue = this.NumValue;
            
            Font numFont = new Font(FontFamily.GenericMonospace, 10);
            e.Graphics.DrawString(
                numValue, numFont, Brushes.DarkBlue,
                e.Bounds.Left + 2, 
                e.Bounds.Top + 1);
            
            Font textFont = new Font(FontFamily.GenericSerif, 10);
            SizeF numSize = e.Graphics.MeasureString(numValue, numFont, new SizeF(100, 100));
            e.Graphics.DrawString(
                text, textFont, Brushes.Black, 
                e.Bounds.Left + numSize.Width + 2, 
                e.Bounds.Top + 1);
            
            e.Graphics.DrawLine(
                Pens.Black, 
                e.Bounds.Left,
                (e.Bounds.Top + numSize.Height) - 1,
                e.Bounds.Left + numSize.Width, 
                (e.Bounds.Top + numSize.Height) - 1);
        }
    }
}
