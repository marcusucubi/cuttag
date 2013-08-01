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
        /// Constructor.
        /// </summary> 
        internal PluginMenuItem(BuildData data)
        {
            this.text = data.Text;
            this.menuName = data.MenuName;
            this.image = data.Image;
            this.action = data.Action;
            this.showInToolbar = data.ShowInToolbar;
        }

        /// <summary> 
        /// The menu text.
        /// </summary> 
        internal string Text
        {
            get { return this.text; }
        }

        /// <summary> 
        /// The menu name.
        /// </summary> 
        internal string MenuName
        {
            get { return this.menuName; }
        }

        /// <summary> 
        /// Show in the tool bar.
        /// </summary> 
        internal bool ShowInToolbar
        {
            get { return this.showInToolbar; }
        }

        /// <summary> 
        /// Menu item image.
        /// </summary> 
        internal Image Image
        {
            get { return this.image; }
        }

        /// <summary> 
        /// Called on click.
        /// </summary> 
        internal IMenuAction Action
        {
            get { return this.action; }
        }

        /// <summary> 
        /// Used for sorting.
        /// </summary> 
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
            /// Menu item text.
            /// </summary> 
            internal string Text { get; set; }
            
            /// <summary> 
            /// Menu item name.
            /// </summary> 
            internal string MenuName { get; set; }
            
            /// <summary> 
            /// Show in toolbar.
            /// </summary> 
            internal bool ShowInToolbar { get; set; }
            
            /// <summary> 
            /// Menu image.
            /// </summary> 
            internal Image Image { get; set; }
            
            /// <summary> 
            /// Menu action.
            /// </summary> 
            internal IMenuAction Action { get; set; }
        }
    }
}
