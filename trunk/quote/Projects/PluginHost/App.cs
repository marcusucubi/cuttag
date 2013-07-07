using System;
using System.Collections.Generic;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;

using PluginHost.Internal;
using NDepend.Attributes;

namespace PluginHost
{
    [CannotDecreaseVisibility]
    public static class App
    {
        private static Form s_MainForm; 
        private static DockPanel s_DockPanel;
        private static MenuStrip s_Menu;

        [CannotDecreaseVisibility]
        public static MenuStrip Menu
        {
            get { return s_Menu; }
        }

        [CannotDecreaseVisibility]
        public static DockPanel DockPanel
        {
            get { return s_DockPanel; }
        }

        [CannotDecreaseVisibility]
        public static Form MainForm
        {
            get { return s_MainForm; }
        }

        [CannotDecreaseVisibility]
        public static void Init(
            Form mainForm, 
            DockPanel dockPanel,
            MenuStrip menu,
            ToolStrip toolStrip)
        {
            s_MainForm = mainForm;
            s_DockPanel = dockPanel;
            s_Menu = menu;

            PluginCollection col = Loader.Load();

            UIBuilder.BuildUI(col, menu, toolStrip);
        }

    }
}
