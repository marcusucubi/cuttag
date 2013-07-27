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

namespace CostAnalysisWindow
{
    public partial class FormTree : DockContent
    {
        private DecompileHelper _Helper = new DecompileHelper();
        private Model.Common.Header _Header;
        private UIUpdater2 _Updater;
        
        public FormTree()
        {
            this.InitializeComponent();

            Model.ActiveHeader.ActiveHeader.PropertyChanged += this.ActiveHeader_PropertyChanged;
            this.Display();
            this._Updater.DisplayValues(this.treeView1);
            this.WatchProperties();
        }

        private void ActiveHeader_PropertyChanged(object source, EventArgs args)
        {
            if (this._Updater != null)
            {
                this._Updater.DisplayValues(this.treeView1);
            }
            
            this.WatchProperties();
        }

        void FrmTreeFormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._Header != null)
            {
                this._Header.ComputationProperties.PropertyChanged -= this.Header_PropertyChanged;
                this._Header = null;
            }
        }
        
        void WatchProperties()
        {
            if (this._Header != null)
            {
                this._Header.ComputationProperties.PropertyChanged -= this.Header_PropertyChanged;
            }

            this._Header = Model.ActiveHeader.ActiveHeader.Header;
            if (this._Header != null) 
            {
                this._Header.ComputationProperties.PropertyChanged += this.Header_PropertyChanged;
            }
        }

        private void Header_PropertyChanged(object source, EventArgs args)
        {
            if (this._Updater != null)
            {
                this._Updater.DisplayValues(this.treeView1);
            }
        }
        
        private void Display()
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

            PropertyAnalyzer2 analyzer2 = new PropertyAnalyzer2();
            analyzer2.Init();
            this._Updater = new UIUpdater2(analyzer2.Nodes);
            this._Updater.UpdateTree(this.treeView1);
            
            this._Helper.Init3();

            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }
        
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
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
            if (this._Helper.Dictionary.ContainsKey(propNode.Property.GetMethod.ToString()))
            {
                StringWriter w = this._Helper.Dictionary[propNode.Property.GetMethod.ToString()];
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
