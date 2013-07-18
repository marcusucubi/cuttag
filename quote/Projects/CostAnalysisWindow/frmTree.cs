using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Mono.Cecil.Cil;

using CostAnalysisWindow;

using WeifenLuo.WinFormsUI.Docking;

namespace SampleProperties
{
    public partial class frmTree : DockContent
    {
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
            PropertyNode2 propNode = e.Node.Tag as PropertyNode2;
            if (propNode == null)
            {
                return;
            }

            if (!propNode.ReadonlyProperty &&
                propNode.PrimaryFieldDefinition != null)
            {
                this.textBox1.Text = propNode.PrimaryFieldDefinition.Name;
            }
            else
            {
                string text = "";
                foreach (Instruction line in propNode.Property.GetMethod.Body.Instructions)
                {
                    text += line.ToString();
                    text += "\r\n";
                }

                this.textBox1.Text = text;
            }

        }

    }
}
