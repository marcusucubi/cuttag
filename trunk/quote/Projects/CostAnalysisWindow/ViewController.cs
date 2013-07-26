using System;

using WeifenLuo.WinFormsUI.Docking;

namespace CostAnalysisWindow
{
    public class ViewController : IDisposable
    {
        private static ViewController m_Instance = new ViewController();

        private FormTree m_Tree;

        private ViewController() {}

        public static ViewController Instance
        {
            get { return m_Instance; }
        }

        public void ShowTree()
        {
            if (m_Tree == null)
            {
                m_Tree = new FormTree();
                InitChild(m_Tree);
            }

            if (m_Tree.IsHidden || m_Tree.IsDisposed) 
            {
                m_Tree = new FormTree();
                InitChild(m_Tree);
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool native)
        {
            m_Tree.Close();
            m_Tree = null;
        }
        
    }
}
