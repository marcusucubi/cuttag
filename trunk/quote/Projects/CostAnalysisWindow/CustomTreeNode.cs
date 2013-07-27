namespace CostAnalysisWindow
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Windows.Forms;
    using CostAnalysisWindow.Elements;

    [Serializable]
    public class CustomTreeNode : TreeNode
    {
        private CodeElement codeElement;
        
        public CustomTreeNode(string text)
            : base(text + "                            ")
        {
            this.UpdateImage();
        }
        
        protected CustomTreeNode(
            SerializationInfo info, 
            StreamingContext context)
            : base(info, context)
        {
        }
        
        public CodeElement CodeElement 
        { 
            get { return this.codeElement; }
            set 
            { 
                this.codeElement = value; 
                this.UpdateImage(); 
            }
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
        
        private string NumValue
        {
            get 
            {
                string result = string.Empty;
                if (this.CodeElement != null)
                {
                    if (this.PropertyAttached)
                    {
                        result = this.PropertyValue.ToString(
                            "###,###,##0.0", CultureInfo.CurrentCulture);
                        
                        int padSize = (Math.Max(0, 12 - result.Length));
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
        
        public void UpdateImage()
        {
            if (this.codeElement == null)
            {
                this.ImageIndex = 1;
            }
            
            PropertyElement prop = this.codeElement as PropertyElement;
            if (prop == null)
            {
                this.ImageIndex = 5;
            }
            else
            {
                if (!prop.IsReadOnly)
                {
                    this.ImageIndex = 7;
                }
                else if (prop.NodesBelow.Count == 0)
                {
                    this.ImageIndex = 6;
                }
                else
                {
                    this.ImageIndex = 2;
                }
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
                numValue, 
                numFont, 
                Brushes.DarkBlue,
                e.Bounds.Left + 2, 
                e.Bounds.Top + 1);
            
            Font textFont = new Font(FontFamily.GenericSerif, 10);
            SizeF numSize = e.Graphics.MeasureString(numValue, numFont, new SizeF(140, 100));
            e.Graphics.DrawString(
                text, 
                textFont, 
                Brushes.Black,
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
