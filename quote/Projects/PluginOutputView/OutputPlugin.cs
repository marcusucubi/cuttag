using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Host;

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
                Host.App.DockPanel.SuspendLayout(true);
                _Output.Show(Host.App.DockPanel, DockState.DockLeft);
                Host.App.DockPanel.ResumeLayout(true, true);
            }
            if (_Output.IsHidden || _Output.IsDisposed) 
            {
                _Output = new FrmOutputView();
                InitChild(_Output, DockState.DockLeft);
            }
        }

        private static void InitChild(DockContent frm)
        {
            Host.App.DockPanel.SuspendLayout(true);
            frm.Show(Host.App.DockPanel, DockState.DockRight);
            Host.App.DockPanel.ResumeLayout(true, true);
        }

        private static void InitChild(DockContent frm, DockState state)
        {
            Host.App.DockPanel.SuspendLayout(true);
            frm.Show(Host.App.DockPanel, state);
            Host.App.DockPanel.ResumeLayout(true, true);
        }
        
    }
}
