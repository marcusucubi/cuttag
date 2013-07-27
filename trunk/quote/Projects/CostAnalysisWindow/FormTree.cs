namespace CostAnalysisWindow
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;
    
    using CostAnalysisWindow;
    using CostAnalysisWindow.Decompile;
    using CostAnalysisWindow.Elements;
    
    using ICSharpCode.Decompiler;
    using ICSharpCode.Decompiler.Ast;
    
    using Model.Template;
    
    using Mono.Cecil;
    using Mono.Cecil.Cil;
    
    using WeifenLuo.WinFormsUI.Docking;

    public partial class FormTree : DockContent
    {
        private DecompileHelper helper = new DecompileHelper();
        private Model.Common.Header header;
        private UIUpdater2 updater;
        
        public FormTree()
        {
            this.InitializeComponent();

            Model.ActiveHeader.ActiveHeader.PropertyChanged += this.ActiveHeader_PropertyChanged;
            this.Display();
            this.updater.DisplayValues(this.treeView1);
            this.WatchProperties();
        }

        private void ActiveHeader_PropertyChanged(object source, EventArgs args)
        {
            if (this.updater != null)
            {
                this.updater.DisplayValues(this.treeView1);
            }
            
            this.WatchProperties();
        }

        void FrmTreeFormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.header != null)
            {
                this.header.ComputationProperties.PropertyChanged -= this.Header_PropertyChanged;
                this.header = null;
            }
        }
        
        void WatchProperties()
        {
            if (this.header != null)
            {
                this.header.ComputationProperties.PropertyChanged -= this.Header_PropertyChanged;
            }

            this.header = Model.ActiveHeader.ActiveHeader.Header;
            if (this.header != null) 
            {
                this.header.ComputationProperties.PropertyChanged += this.Header_PropertyChanged;
            }
        }

        private void Header_PropertyChanged(object source, EventArgs args)
        {
            if (this.updater != null)
            {
                this.updater.DisplayValues(this.treeView1);
            }
        }
        
        private void Display()
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

            PropertyAnalyzer2 analyzer2 = new PropertyAnalyzer2();
            analyzer2.Init();
            this.updater = new UIUpdater2(analyzer2.Nodes);
            this.updater.UpdateTree(this.treeView1);
            
            this.helper.Init3();

            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }
        
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CustomTreeNode node = e.Node as CustomTreeNode;
            PropertyElement propNode = node.CodeElement as PropertyElement;
            if (propNode == null)
            {
                this.richTextBox1.Text = string.Empty;
                return;
            }

            this.richTextBox1.Text = this.Decompile(propNode);
        }
        
        private string Decompile(PropertyElement propNode)
        {
            if (this.helper.CodeDictionary.ContainsKey(propNode.Property.GetMethod.ToString()))
            {
                StringWriter w = this.helper.CodeDictionary[propNode.Property.GetMethod.ToString()];
                return w.ToString();
            }
            
            return string.Empty;
        }

        void TreeView1DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            CustomTreeNode node = e.Node as CustomTreeNode;
            if (node != null)
            {
                node.OnDraw(sender, e);
            }
        }
    }
}
