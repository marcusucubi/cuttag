using System;

using Host.UI;

using WeifenLuo.WinFormsUI.Docking;

namespace PluginOutputView
{
    public static class OutputPlugin 
    {
        private static FrmOutputView _Output;

        public static void ShowOutputView()
        {
            if (_Output == null) 
            {
                _Output = new FrmOutputView();
                UIApp.DockPanel.SuspendLayout(true);
                _Output.Show(UIApp.DockPanel, DockState.DockLeft);
                UIApp.DockPanel.ResumeLayout(true, true);
            }
            if (_Output.IsHidden || _Output.IsDisposed) 
            {
                _Output = new FrmOutputView();
                InitChild(_Output, DockState.DockLeft);
            }
        }

        private static void InitChild(DockContent frm)
        {
            UIApp.DockPanel.SuspendLayout(true);
            frm.Show(UIApp.DockPanel, DockState.DockRight);
            UIApp.DockPanel.ResumeLayout(true, true);
        }

        private static void InitChild(DockContent frm, DockState state)
        {
            UIApp.DockPanel.SuspendLayout(true);
            frm.Show(UIApp.DockPanel, state);
            UIApp.DockPanel.ResumeLayout(true, true);
        }
        
    }
}
