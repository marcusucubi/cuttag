using System;
using System.Collections.Generic;
using System.Windows.Forms;

using PluginHost;

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
                App.DockPanel.SuspendLayout(true);
                _Output.Show(App.DockPanel, DockState.DockLeft);
                App.DockPanel.ResumeLayout(true, true);
            }
            if (_Output.IsHidden || _Output.IsDisposed) 
            {
                _Output = new FrmOutputView();
                InitChild(_Output, DockState.DockLeft);
            }
        }

        private static void InitChild(DockContent frm)
        {
            App.DockPanel.SuspendLayout(true);
            frm.Show(App.DockPanel, DockState.DockRight);
            App.DockPanel.ResumeLayout(true, true);
        }

        private static void InitChild(DockContent frm, DockState state)
        {
            App.DockPanel.SuspendLayout(true);
            frm.Show(App.DockPanel, state);
            App.DockPanel.ResumeLayout(true, true);
        }
        
    }
}
