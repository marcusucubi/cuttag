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
    public partial class frmTree : DockContent
    {
        private DecompileHelper _Helper = new DecompileHelper();
        private Model.Common.Header _Header;
        private UIUpdater2 _Updater;
        
        public frmTree()
        {
            InitializeComponent();

            Model.ActiveHeader.ActiveHeader.PropertyChanged += ActiveHeader_PropertyChanged;
            Display();
            _Updater.DisplayValues(treeView1);
            WatchProperties();
        }

        private void ActiveHeader_PropertyChanged(object source, EventArgs args)
        {
            if (_Updater != null)
            {
                _Updater.DisplayValues(treeView1);
            }
            
            WatchProperties();
        }

        void FrmTreeFormClosing(object sender, FormClosingEventArgs e)
        {
            if (_Header != null)
            {
                _Header.ComputationProperties.PropertyChanged -= Header_PropertyChanged;
                _Header = null;
            }
        }
        
        void WatchProperties()
        {
            if (_Header != null)
            {
                _Header.ComputationProperties.PropertyChanged -= Header_PropertyChanged;
            }

            _Header = Model.ActiveHeader.ActiveHeader.Header;
            if (_Header != null) 
            {
                _Header.ComputationProperties.PropertyChanged += Header_PropertyChanged;
            }
        }

        private void Header_PropertyChanged(object source, EventArgs args)
        {
            if (_Updater != null)
            {
                _Updater.DisplayValues(treeView1);
            }
        }
        
        private void Display()
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

            PropertyAnalyzer2 analyzer2 = new PropertyAnalyzer2();
            analyzer2.Init();
            _Updater = new UIUpdater2(analyzer2.Nodes);
            _Updater.UpdateTree(this.treeView1);
            
            _Helper.Init3();

            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }
        
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CustomTreeNode node = e.Node as CustomTreeNode;
            PropertyElement propNode = node.CodeElement as PropertyElement;
            if (propNode == null)
            {
                this.richTextBox1.Text = "";
                return;
            }

            this.richTextBox1.Text = Decompile(propNode);
        }
        
        private string Decompile(PropertyElement propNode)
        {
            try
            {
                if (_Helper.Dictionary.ContainsKey(propNode.Property.GetMethod.ToString()))
                {
                    StringWriter w = _Helper.Dictionary[propNode.Property.GetMethod.ToString()];
                    return w.ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message;
            }
            
            return "";
        }

        
        void TreeView1DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Bounds.Left == -1)
            {
                return;
            }
            
            string text = "Costs";
            string numValue = "";
            CustomTreeNode node = e.Node as CustomTreeNode;
            if (node != null)
            {
                text = node.CodeElement.Name;
                
                if (node.PropertyAttached)
                {
                    numValue = node.PropertyValue.ToString("#,##0.0000");
                }
            }
            
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
        }
    }
}
