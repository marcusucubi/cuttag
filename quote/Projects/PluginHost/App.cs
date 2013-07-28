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
        private static Form mainForm; 
        private static DockPanel dockPanel;
        private static MenuStrip menu;
        private static FlowLayoutPanel statusStripPanel;
        private static ToolTip statusStripToolTip;
        private static Dictionary<Type, Type> registeredClasses = new Dictionary<Type, Type>();

        [CannotDecreaseVisibility]
        public static Dictionary<Type, Type> RegisteredClasses
        {
            get { return registeredClasses; }
        }

        [CannotDecreaseVisibility]
        public static MenuStrip Menu
        {
            get { return menu; }
        }

        [CannotDecreaseVisibility]
        public static DockPanel DockPanel
        {
            get { return dockPanel; }
        }

        [CannotDecreaseVisibility]
        public static Form MainForm
        {
            get { return mainForm; }
        }

        [CannotDecreaseVisibility]
        public static FlowLayoutPanel StatusStripPanel
        {
            get { return statusStripPanel; }
        }

        [CannotDecreaseVisibility]
        public static ToolTip StatusStripToolTip
        {
            get { return statusStripToolTip; }
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
            App.mainForm = mainForm;
            App.dockPanel = dockPanel;
            App.statusStripPanel = statusStripPanel;
            App.statusStripToolTip = statusStripToolTip;
            App.menu = menu;

            PluginCollection col = Loader.Load();

            UIBuilder.BuildUI(col, menu, toolStrip);
        }

    }
}
