// <summary>
// Contains the PluginMenuItem class.
// </summary>
// <copyright file="PluginMenuItem.cs" company="Davis Computer Services">
//  No copyright information.
// </copyright>
namespace Host.UI
{
    using System;
    using System.Drawing;
    using System.Globalization;
    
    /// <summary> 
    /// Represents a menu item.
    /// </summary> 
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes", Justification = "We can ignore this.")]
    public sealed class PlugInMenuItem : IComparable<PlugInMenuItem>
    {
        /// <summary> 
        /// Menu item text.
        /// </summary> 
        private readonly string text;
        
        /// <summary> 
        /// Menu item name.
        /// </summary> 
        private readonly string menuName;
        
        /// <summary> 
        /// The image of null.
        /// </summary> 
        private readonly Image image;
        
        /// <summary> 
        /// Display in toolbar if true.
        /// </summary> 
        private readonly bool showInToolbar;
        
        /// <summary> 
        /// Called on menu click.
        /// </summary> 
        private readonly IMenuAction action;

        /// <summary> 
        /// Initializes a new instance of the <see cref="PlugInMenuItem" /> class.
        /// </summary> 
        /// <param name="data">The data for the menu item.</param>
        internal PlugInMenuItem(BuildData data)
        {
            this.text = data.Text;
            this.menuName = data.MenuName;
            this.image = data.Image;
            this.action = data.Action;
            this.showInToolbar = data.ShowInToolBar;
        }

        /// <summary> 
        /// Gets the menu text.
        /// </summary>
        /// <value>The menu text.</value>
        public string Text
        {
            get { return this.text; }
        }

        /// <summary> 
        /// Gets the menu name.
        /// </summary>
        /// <value>The menu name.</value>
        public string MenuName
        {
            get { return this.menuName; }
        }

        /// <summary> 
        /// Gets a value indicating whether a tool bar button is created.
        /// </summary> 
        /// <value>A value indicating whether a tool bar button is created.</value>
        public bool ShowInToolBar
        {
            get { return this.showInToolbar; }
        }

        /// <summary> 
        /// Gets the menu item image.
        /// </summary>
        /// <value>The menu item image.</value>
        public Image Image
        {
            get { return this.image; }
        }

        /// <summary> 
        /// Gets the menu item action.
        /// </summary> 
        /// <value>The menu item action.</value>
        public IMenuAction Action
        {
            get { return this.action; }
        }

        /// <summary> 
        /// Used for sorting.
        /// </summary> 
        /// <param name="other">The other <c>PluginMenuItem</c></param>
        /// <returns>A value indicating the result of the compare.</returns>
        public int CompareTo(PlugInMenuItem other)
        {
            return string.Compare(other.Text, this.Text, true, CultureInfo.CurrentCulture);
        }
        
        /// <summary> 
        /// Helper class.
        /// </summary> 
        internal sealed class BuildData
        {
            /// <summary> 
            /// Gets or sets the menu item text.
            /// </summary> 
            public string Text { get; set; }
            
            /// <summary> 
            /// Gets or sets the menu item name.
            /// </summary> 
            public string MenuName { get; set; }
            
            /// <summary> 
            /// Gets or sets a value indicating whether or not to show in toolbar.
            /// </summary> 
            public bool ShowInToolBar { get; set; }
            
            /// <summary> 
            /// Gets or sets the menu image.
            /// </summary> 
            public Image Image { get; set; }
            
            /// <summary> 
            /// Gets or sets the menu action.
            /// </summary> 
            public IMenuAction Action { get; set; }
        }
    }
}
