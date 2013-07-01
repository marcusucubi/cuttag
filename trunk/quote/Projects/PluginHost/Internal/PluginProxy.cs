using System;
using System.Collections.Generic;
using System.Reflection;

using PluginHost;

namespace PluginHost.Internal
{
    sealed class PluginProxy
    {
        private readonly List<PluginMenuItem> m_PluginMenuItems = new List<PluginMenuItem>();

        internal PluginProxy(
            List<PluginMenuItem> pluginMenuItems)
        {
            m_PluginMenuItems = pluginMenuItems;
        }

        internal PluginMenuItem[] PluginMenuItems 
        {
            get { return m_PluginMenuItems.ToArray(); }
        }

    }
}
