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
        private readonly IPluginMenuAction m_Action;

        internal PluginMenuItem(
            string text, 
            string menuName,
            IPluginMenuAction action)
        {
            m_Text = text;
            m_MenuName = menuName;
            m_Action = action;
        }

        internal string Text
        {
            get { return m_Text; }
        }

        internal string MenuName
        {
            get { return m_MenuName; }
        }

        internal IPluginMenuAction Action
        {
            get { return m_Action; }
        }
    }
}
