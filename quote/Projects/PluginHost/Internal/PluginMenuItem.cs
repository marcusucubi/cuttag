using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using PluginHost;

namespace PluginHost.Internal
{
    internal sealed class PluginMenuItem : IComparable<PluginMenuItem>
    {
        internal sealed class BuildData
        {
            internal string Text;
            internal string MenuName;
            internal bool ShowInToolbar;
            internal Image Image;
            internal IPluginMenuAction Action;
        }

        private readonly string m_Text;
        private readonly string m_MenuName;
        private readonly Image m_Image;
        private readonly bool m_ShowInToolbar;
        private readonly IPluginMenuAction m_Action;

        internal PluginMenuItem(BuildData data)
        {
            System.Diagnostics.Debug.Assert(data.MenuName != null);

            m_Text = data.Text;
            m_MenuName = data.MenuName;
            m_Image = data.Image;
            m_Action = data.Action;
            m_ShowInToolbar = data.ShowInToolbar;
        }

        internal string Text
        {
            get { return m_Text; }
        }

        internal string MenuName
        {
            get { return m_MenuName; }
        }

        internal bool ShowInToolbar
        {
            get { return m_ShowInToolbar; }
        }

        internal Image Image
        {
            get { return m_Image; }
        }

        internal IPluginMenuAction Action
        {
            get { return m_Action; }
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
