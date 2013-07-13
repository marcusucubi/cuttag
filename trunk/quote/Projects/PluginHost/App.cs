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
        private static FlowLayoutPanel s_StatusStripPanel;
        private static ToolTip s_StatusStripToolTip;
        private static Dictionary<Type, Type> s_RegisteredClasses = new Dictionary<Type, Type>();

        [CannotDecreaseVisibility]
        public static Dictionary<Type, Type> RegisteredClasses
        {
            get { return s_RegisteredClasses; }
        }

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
        public static FlowLayoutPanel StatusStripPanel
        {
            get { return s_StatusStripPanel; }
        }

        [CannotDecreaseVisibility]
        public static ToolTip StatusStripToolTip
        {
            get { return s_StatusStripToolTip; }
        }

        [CannotDecreaseVisibility]
        public static void Init(
            Form mainForm, 
            DockPanel dockPanel,
            MenuStrip menu,
            ToolStrip toolStrip,
            FlowLayoutPanel statusStripPanel,
            ToolTip statusStripToolTip)
        {
            s_MainForm = mainForm;
            s_DockPanel = dockPanel;
            s_StatusStripPanel = statusStripPanel;
            s_StatusStripToolTip = statusStripToolTip;
            s_Menu = menu;

            PluginCollection col = Loader.Load();

            UIBuilder.BuildUI(col, menu, toolStrip);
        }

    }
}
