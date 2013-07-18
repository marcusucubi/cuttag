using System;
using WeifenLuo.WinFormsUI.Docking;

namespace CostAnalysisWindow
{
    public class ViewController
    {
        private static ViewController m_Instance = new ViewController();

        private frmTree m_Tree;

        private ViewController() {}

        public static ViewController Instance
        {
            get { return m_Instance; }
        }

        public void ShowTree()
        {
            if (m_Tree == null)
            {
                m_Tree = new frmTree();
                InitChild(m_Tree);
            }

            if (m_Tree.IsHidden || m_Tree.IsDisposed) 
            {
                m_Tree = new frmTree();
                InitChild(m_Tree);
            }
        }
        
        private void InitChild(DockContent frm)
        {
            PluginHost.App.DockPanel.SuspendLayout(true);
            frm.Show(PluginHost.App.DockPanel, DockState.DockRight);
            PluginHost.App.DockPanel.ResumeLayout(true, true);
        }

    }
}
