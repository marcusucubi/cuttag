using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using PluginHost;

namespace PluginHost.Internal
{
    internal sealed class PluginMenuItem 
    {
        internal sealed class BuildData
        {
            internal string Text;
            internal string MenuName;
            internal string MenuAnchor;
            internal string ButtonAnchor;
            internal Image Image;
            internal MenuPosition MenuPosition;
            internal IPluginMenuAction Action;

            public BuildData()
            {
                MenuPosition = MenuPosition.Below;
            }
        }

        private readonly string m_Text;
        private readonly string m_MenuName;
        private readonly string m_MenuAnchor;
        private readonly string m_ButtonAnchor;
        private readonly Image m_Image;
        private readonly MenuPosition m_MenuPosition;
        private readonly IPluginMenuAction m_Action;

        internal PluginMenuItem(BuildData data)
        {
            System.Diagnostics.Debug.Assert(data.MenuName != null);

            m_Text = data.Text;
            m_MenuName = data.MenuName;
            m_MenuAnchor = data.MenuAnchor;
            m_ButtonAnchor = data.ButtonAnchor;
            m_Image = data.Image;
            m_Action = data.Action;
            m_MenuPosition = data.MenuPosition;
        }

        internal string Text
        {
            get { return m_Text; }
        }

        internal string MenuName
        {
            get { return m_MenuName; }
        }

        internal string MenuAnchor
        {
            get { return m_MenuAnchor; }
        }

        internal string ButtonAnchor
        {
            get { return m_ButtonAnchor; }
        }

        internal Image Image
        {
            get { return m_Image; }
        }

        internal IPluginMenuAction Action
        {
            get { return m_Action; }
        }

        internal MenuPosition MenuPosition
        {
            get { return m_MenuPosition; }
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

    }
}
