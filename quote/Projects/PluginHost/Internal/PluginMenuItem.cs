namespace Host.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Text;
    using System.Windows.Forms;
    
    using Host;

    /// <summary> 
    /// Represents a menu item.
    /// </summary> 
    internal sealed class PluginMenuItem : IComparable<PluginMenuItem>
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
        /// Initializes a new instance of the <see cref="PluginMenuItem" /> class.
        /// </summary> 
        /// <param name="data">The data for the menu item.</param>
        internal PluginMenuItem(BuildData data)
        {
            this.text = data.Text;
            this.menuName = data.MenuName;
            this.image = data.Image;
            this.action = data.Action;
            this.showInToolbar = data.ShowInToolbar;
        }

        /// <summary> 
        /// Gets the menu text.
        /// </summary>
        /// <value>The menu text.</value>
        internal string Text
        {
            get { return this.text; }
        }

        /// <summary> 
        /// Gets the menu name.
        /// </summary>
        /// <value>The menu name.</value>
        internal string MenuName
        {
            get { return this.menuName; }
        }

        /// <summary> 
        /// Gets a value indicating whether a tool bar button is created.
        /// </summary> 
        /// <value>A value indicating whether a tool bar button is created.</value>
        internal bool ShowInToolbar
        {
            get { return this.showInToolbar; }
        }

        /// <summary> 
        /// Gets the menu item image.
        /// </summary>
        /// <value>The menu item image.</value>
        internal Image Image
        {
            get { return this.image; }
        }

        /// <summary> 
        /// Gets the menu item action.
        /// </summary> 
        /// <value>The menu item action.</value>
        internal IMenuAction Action
        {
            get { return this.action; }
        }

        /// <summary> 
        /// Used for sorting.
        /// </summary> 
        /// <param name="other">The other <c>PluginMenuItem</c></param>
        /// <returns>A value indicating the result of the compare.</returns>
        public int CompareTo(PluginMenuItem other)
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
            internal string Text { get; set; }
            
            /// <summary> 
            /// Gets or sets the menu item name.
            /// </summary> 
            internal string MenuName { get; set; }
            
            /// <summary> 
            /// Gets or sets a value indicating whether or not to show in toolbar.
            /// </summary> 
            internal bool ShowInToolbar { get; set; }
            
            /// <summary> 
            /// Gets or sets the menu image.
            /// </summary> 
            internal Image Image { get; set; }
            
            /// <summary> 
            /// Gets or sets the menu action.
            /// </summary> 
            internal IMenuAction Action { get; set; }
        }
    }
}
