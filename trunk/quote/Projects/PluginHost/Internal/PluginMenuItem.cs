namespace Host.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Text;
    using System.Windows.Forms;
    
    using Host;

    internal sealed class PluginMenuItem : IComparable<PluginMenuItem>
    {
        private readonly string text;
        private readonly string menuName;
        private readonly Image image;
        private readonly bool showInToolbar;
        private readonly IMenuAction action;

        internal PluginMenuItem(BuildData data)
        {
            this.text = data.Text;
            this.menuName = data.MenuName;
            this.image = data.Image;
            this.action = data.Action;
            this.showInToolbar = data.ShowInToolbar;
        }

        internal string Text
        {
            get { return this.text; }
        }

        internal string MenuName
        {
            get { return this.menuName; }
        }

        internal bool ShowInToolbar
        {
            get { return this.showInToolbar; }
        }

        internal Image Image
        {
            get { return this.image; }
        }

        internal IMenuAction Action
        {
            get { return this.action; }
        }

        public int CompareTo(PluginMenuItem other)
        {
            return string.Compare(other.Text, this.Text, true, CultureInfo.CurrentCulture);
        }
        
        internal sealed class BuildData
        {
            internal string Text { get; set; }
            
            internal string MenuName { get; set; }
            
            internal bool ShowInToolbar { get; set; }
            
            internal Image Image { get; set; }
            
            internal IMenuAction Action { get; set; }
        }
    }
}
