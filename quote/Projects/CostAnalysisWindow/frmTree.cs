using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using CostAnalysisWindow;

using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Ast;

using Mono.Cecil;
using Mono.Cecil.Cil;

using WeifenLuo.WinFormsUI.Docking;

namespace CostAnalysisWindow
{
    public partial class frmTree : DockContent
    {
        private DecompileHelper _Helper = new DecompileHelper();
        
        public frmTree()
        {
            InitializeComponent();

            Model.ActiveHeader.ActiveHeader.PropertyChanged += ActiveHeader_PropertyChanged;
            Display();
        }

        private void ActiveHeader_PropertyChanged(object source, EventArgs args)
        {
            Display();
        }

        private void Display()
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

            PropertyAnalyzer2 analyzer2 = new PropertyAnalyzer2();
            analyzer2.Init();
            UIUpdater2 updater2 = new UIUpdater2(analyzer2.Nodes);
            updater2.UpdateTree(this.treeView1);
            
            _Helper.Init3();

            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
        }

        private void treeView1_NodeMouseClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            PropertyElement propNode = e.Node.Tag as PropertyElement;
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

    }
}
