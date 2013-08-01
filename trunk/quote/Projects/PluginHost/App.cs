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
        /// Classes with the RegisterAttribute.
        /// </summary> 
        private static Dictionary<Type, Type> registeredClasses = new Dictionary<Type, Type>();

        /// <summary> 
        /// Gets classes with the RegisterAttribute.
        /// </summary> 
        /// <value>Classes with the RegisterAttribute.</value>
        [CannotDecreaseVisibility]
        public static Dictionary<Type, Type> RegisteredClasses
        {
            get { return registeredClasses; }
        }

        /// <summary> 
        /// Gets the menu of the main form.
        /// </summary> 
        /// <value>Menu of the main form.</value>
        [CannotDecreaseVisibility]
        public static MenuStrip Menu
        {
            get { return menu; }
        }

        /// <summary>
        /// Gets the dock panel in the main form.
        /// </summary>
        /// <value>The dock panel in the main form.</value>
        [CannotDecreaseVisibility]
        public static DockPanel DockPanel
        {
            get { return dockPanel; }
        }

        /// <summary> 
        /// Gets the main form.
        /// </summary> 
        /// <value>The main form.</value>
        [CannotDecreaseVisibility]
        public static Form MainForm
        {
            get { return mainForm; }
        }

        /// <summary> 
        /// Gets the status strip displayed an the bottom of the main form.
        /// </summary> 
        /// <value>The status strip displayed an the bottom of the main form.</value>
        [CannotDecreaseVisibility]
        public static FlowLayoutPanel StatusStripPanel
        {
            get { return statusStripPanel; }
        }

        /// <summary> 
        /// Gets the tool tip control of the status strip.
        /// </summary> 
        /// <value>The tool tip control of the status strip.</value>
        [CannotDecreaseVisibility]
        public static ToolTip StatusStripToolTip
        {
            get { return statusStripToolTip; }
        }

        /// <summary> 
        /// Called from the main form's OnLoad.
        /// </summary> 
        /// <param name="mainForm">The main form.</param>
        /// <param name="dockPanel">The dock panel.</param>
        /// <param name="menu">The main menu.</param>
        /// <param name="toolStrip">The tool button control.</param>
        /// <param name="statusStripPanel">The status strip panel.</param>
        /// <param name="statusStripToolTip">The status strip tool tip control.</param>
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
