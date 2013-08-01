namespace Host
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    
    using Host.Internal;
    
    using NDepend.Attributes;

    using WeifenLuo.WinFormsUI.Docking;
    
    /// <summary> 
    /// Central class used to access common application properties.
    /// </summary> 
    [CannotDecreaseVisibility]
    public static class App
    {
        /// <summary> 
        /// The main form.
        /// </summary> 
        private static Form mainForm; 
        
        /// <summary> 
        /// The only dock panel.
        /// </summary> 
        private static DockPanel dockPanel;
        
        /// <summary> 
        /// The menu of the main form.
        /// </summary> 
        private static MenuStrip menu;
        
        /// <summary> 
        /// The status strip displayed an the bottom of the main form.
        /// </summary> 
        private static FlowLayoutPanel statusStripPanel;
        
        /// <summary> 
        /// Tool tip control of the status strip.
        /// </summary> 
        private static ToolTip statusStripToolTip;
        
        /// <summary> 
        /// Classes with the RegisterAtribute.
        /// </summary> 
        private static Dictionary<Type, Type> registeredClasses = new Dictionary<Type, Type>();

        /// <summary> 
        /// Classes with the RegisterAtribute.
        /// </summary> 
        [CannotDecreaseVisibility]
        public static Dictionary<Type, Type> RegisteredClasses
        {
            get { return registeredClasses; }
        }

        /// <summary> 
        /// The menu of the main form.
        /// </summary> 
        [CannotDecreaseVisibility]
        public static MenuStrip Menu
        {
            get { return menu; }
        }

        /// <summary> 
        /// The only dock panel.
        /// </summary> 
        [CannotDecreaseVisibility]
        public static DockPanel DockPanel
        {
            get { return dockPanel; }
        }

        /// <summary> 
        /// The main form.
        /// </summary> 
        [CannotDecreaseVisibility]
        public static Form MainForm
        {
            get { return mainForm; }
        }

        /// <summary> 
        /// The status strip displayed an the bottom of the main form.
        /// </summary> 
        [CannotDecreaseVisibility]
        public static FlowLayoutPanel StatusStripPanel
        {
            get { return statusStripPanel; }
        }

        /// <summary> 
        /// Tool tip control of the status strip.
        /// </summary> 
        [CannotDecreaseVisibility]
        public static ToolTip StatusStripToolTip
        {
            get { return statusStripToolTip; }
        }

        /// <summary> 
        /// Called from the main form's OnLoad.
        /// </summary> 
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
