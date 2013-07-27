using System;

using WeifenLuo.WinFormsUI.Docking;

namespace CostAnalysisWindow
{
    public class ViewController : IDisposable
    {
        private static ViewController m_Instance = new ViewController();

        private FormTree m_Tree;

        private ViewController() { }

        public static ViewController Instance
        {
            get { return m_Instance; }
        }

        public void ShowTree()
        {
            if (this.m_Tree == null)
            {
                this.m_Tree = new FormTree();
                InitChild(this.m_Tree);
            }

            if (this.m_Tree.IsHidden || this.m_Tree.IsDisposed) 
            {
                this.m_Tree = new FormTree();
                InitChild(this.m_Tree);
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
            this.m_Tree.Close();
            this.m_Tree = null;
        }
        
    }
}
