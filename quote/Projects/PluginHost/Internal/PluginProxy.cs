namespace Host.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    
    using Host;

    internal sealed class PluginProxy
    {
        private readonly List<PluginMenuItem> pluginMenuItems = new List<PluginMenuItem>();
        private readonly List<IInit> plugins = new List<IInit>();
        private readonly Assembly assembly;

        internal PluginProxy(
            List<PluginMenuItem> pluginMenuItems,
            List<IInit> plugins,
            Assembly assembly)
        {
            this.pluginMenuItems = pluginMenuItems;
            this.plugins = plugins;
            this.assembly = assembly;
        }

        internal PluginMenuItem[] PluginMenuItems 
        {
            get { return this.pluginMenuItems.ToArray(); }
        }

        internal IInit[] Plugins
        {
            get { return this.plugins.ToArray(); }
        }

        internal Assembly Assembly
        {
            get { return this.assembly; }
        }
    }
}
