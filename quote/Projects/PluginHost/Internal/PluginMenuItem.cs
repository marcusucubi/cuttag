using System;
using System.Collections.Generic;
using System.Text;

using PluginHost;

namespace PluginHost.Internal
{
    internal sealed class PluginMenuItem 
    {
        private readonly string m_Text;
        private readonly string m_MenuName;
        private readonly int m_MenuSeporatorNumber;
        private readonly MenuPosition m_MenuPosition;
        private readonly IPluginMenuAction m_Action;

        internal PluginMenuItem(
            string text, 
            string menuName,
            int menuSeporatorNumber,
            MenuPosition menuPosition,
            IPluginMenuAction action)
        {
            m_Text = text;
            m_MenuName = menuName;
            m_MenuSeporatorNumber = menuSeporatorNumber;
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

        internal MenuPosition MenuPosition
        {
            get { return m_MenuPosition; }
        }

        internal int MenuSeporatorNumber
        {
            get { return m_MenuSeporatorNumber; }
        }

        internal IPluginMenuAction Action
        {
            get { return m_Action; }
        }
    }
}
