using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Host;

namespace Host.Internal
{
    internal sealed class PluginMenuItem : IComparable<PluginMenuItem>
    {
        internal sealed class BuildData
        {
            internal string Text;
            internal string MenuName;
            internal bool ShowInToolbar;
            internal Image Image;
            internal IMenuAction Action;
        }

        private readonly string text;
        private readonly string menuName;
        private readonly Image image;
        private readonly bool showInToolbar;
        private readonly IMenuAction action;

        internal PluginMenuItem(BuildData data)
        {
            System.Diagnostics.Debug.Assert(data.MenuName != null);

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

        internal ToolStripItem ToolStripItem
        {
            get;
            set;
        }

        internal ToolStripButton ToolStripButton
        {
            get;
            set;
        }

        public int CompareTo(PluginMenuItem other)
        {
            return other.Text.CompareTo(this.Text);
        }
    }
}
