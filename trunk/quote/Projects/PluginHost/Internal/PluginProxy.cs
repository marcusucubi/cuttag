using System;
using System.Collections.Generic;
using System.Reflection;

using PluginHost;

namespace PluginHost.Internal
{
    sealed class PluginProxy
    {
        private readonly List<PluginMenuItem> m_PluginMenuItems = new List<PluginMenuItem>();
        private readonly List<IPluginInit> m_Plugins = new List<IPluginInit>();

        internal PluginProxy(
            List<PluginMenuItem> pluginMenuItems,
            List<IPluginInit> plugins)
        {
            m_PluginMenuItems = pluginMenuItems;
            m_Plugins = plugins;
        }

        internal PluginMenuItem[] PluginMenuItems 
        {
            get { return m_PluginMenuItems.ToArray(); }
        }

        internal IPluginInit[] Plugins
        {
            get { return m_Plugins.ToArray(); }
        }

    }
}
