using System;

using WeifenLuo.WinFormsUI.Docking;

namespace CostAnalysisWindow
{
    public class ViewController : IDisposable
    {
        private static ViewController instance = new ViewController();

        private FormTree tree;

        private ViewController() { }

        public static ViewController Instance
        {
            get { return instance; }
        }

        public void ShowTree()
        {
            if (this.tree == null)
            {
                this.tree = new FormTree();
                InitChild(this.tree);
            }

            if (this.tree.IsHidden || this.tree.IsDisposed) 
            {
                this.tree = new FormTree();
                InitChild(this.tree);
            }
        }
        
        private static void InitChild(DockContent frm)
        {
            PluginHost.App.DockPanel.SuspendLayout(true);
            frm.Show(PluginHost.App.DockPanel, DockState.DockLeft);
            PluginHost.App.DockPanel.ResumeLayout(true, true);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool native)
        {
            this.tree.Close();
            this.tree = null;
        }
        
    }
}
