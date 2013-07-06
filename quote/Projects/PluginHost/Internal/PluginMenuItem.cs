using System;
using System.Collections.Generic;
using System.Text;

using PluginHost;
using System.Windows.Forms;
using System.Drawing;

namespace PluginHost.Internal
{
    internal sealed class PluginMenuItem 
    {
        private readonly string m_Text;
        private readonly string m_MenuName;
        private readonly string m_Anchor;
        private readonly Image m_Image;
        private readonly MenuPosition m_MenuPosition;
        private readonly IPluginMenuAction m_Action;

        internal PluginMenuItem(
            string text, 
            string menuName,
            string anchor,
            Image image,
            MenuPosition menuPosition,
            IPluginMenuAction action)
        {
            m_Text = text;
            m_MenuName = menuName;
            m_Anchor = anchor;
            m_Image = image;
            m_Action = action;
            m_MenuPosition = menuPosition;
        }

        internal string Text
        {
            get { return m_Text; }
        }

        internal string MenuName
        {
            get { return m_MenuName; }
        }

        internal string Anchor
        {
            get { return m_Anchor; }
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

    }
}
