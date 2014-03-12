// <summary>
// Contains the UIApp class.
// </summary>
// <copyright file="UIApp.cs" company="Davis Computer Services">
//  No copyright information.
// </copyright>
namespace Host.UI
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    
    using WeifenLuo.WinFormsUI.Docking;
    
    /// <summary> 
    /// Central class used to access common application properties.
    /// </summary> 
    public static class UIApp
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
        /// Gets the menu of the main form.
        /// </summary> 
        /// <value>Menu of the main form.</value>
        public static MenuStrip Menu
        {
            get { return menu; }
        }

        /// <summary>
        /// Gets the dock panel in the main form.
        /// </summary>
        /// <value>The dock panel in the main form.</value>
        public static DockPanel DockPanel
        {
            get { return dockPanel; }
        }

        /// <summary> 
        /// Gets the main form.
        /// </summary> 
        /// <value>The main form.</value>
        public static Form MainForm
        {
            get { return mainForm; }
        }

        /// <summary> 
        /// Gets the status strip displayed an the bottom of the main form.
        /// </summary> 
        /// <value>The status strip displayed an the bottom of the main form.</value>
        public static FlowLayoutPanel StatusStripPanel
        {
            get { return statusStripPanel; }
        }

        /// <summary> 
        /// Gets the tool tip control of the status strip.
        /// </summary> 
        /// <value>The tool tip control of the status strip.</value>
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
        public static void Init(
            Form mainForm, 
            DockPanel dockPanel,
            MenuStrip menu,
            ToolStrip toolStrip,
            FlowLayoutPanel statusStripPanel,
            ToolTip statusStripToolTip)
        {
            ICollection<PlugInProxy2> col = PlugInLoader2.Load();
            
            UIApp.mainForm = mainForm;
            UIApp.dockPanel = dockPanel;
            UIApp.statusStripPanel = statusStripPanel;
            UIApp.statusStripToolTip = statusStripToolTip;
            UIApp.menu = menu;

            UIBuilder.BuildUI(col, menu, toolStrip);
        }
    }
}