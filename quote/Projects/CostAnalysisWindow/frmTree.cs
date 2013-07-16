using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using Dile.Disassemble;
using Dile.Disassemble.ILCodes;
using Dile.Metadata;

using WeifenLuo.WinFormsUI.Docking;
using SampleWindow;

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

            PropertyAnalyzer analyzer = new PropertyAnalyzer();
            analyzer.Init();

            UIUpdater updater = new UIUpdater(analyzer.Nodes);
            updater.UpdateTree(this.treeView1);

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
            PropertyNode propNode = e.Node.Tag as PropertyNode;
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
                foreach (CodeLine line in propNode.CodeLines)
                {
                    text += line.Text;
                    text += "\r\n";
                }

                this.textBox1.Text = text;
            }

        }

    }
}
