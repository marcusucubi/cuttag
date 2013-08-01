namespace Host.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    
    using Host;

    /// <summary> 
    /// Represents a plugin.
    /// </summary> 
    internal sealed class PluginProxy
    {
        /// <summary> 
        /// Menu items.
        /// </summary> 
        private readonly List<PluginMenuItem> pluginMenuItems = new List<PluginMenuItem>();
        
        /// <summary> 
        /// Classes to init on startup.
        /// </summary> 
        private readonly List<IInit> plugins = new List<IInit>();
        
        /// <summary> 
        /// The assemble.
        /// </summary> 
        private readonly Assembly assembly;

        /// <summary> 
        /// Constructor.
        /// </summary> 
        internal PluginProxy(
            List<PluginMenuItem> pluginMenuItems,
            List<IInit> plugins,
            Assembly assembly)
        {
            this.pluginMenuItems = pluginMenuItems;
            this.plugins = plugins;
            this.assembly = assembly;
        }

        /// <summary> 
        /// The menu items.
        /// </summary> 
        internal PluginMenuItem[] PluginMenuItems 
        {
            get { return this.pluginMenuItems.ToArray(); }
        }

        /// <summary> 
        /// Classes to init on startup.
        /// </summary> 
        internal IInit[] Plugins
        {
            get { return this.plugins.ToArray(); }
        }

        /// <summary> 
        /// The assembly.
        /// </summary> 
        internal Assembly Assembly
        {
            get { return this.assembly; }
        }
    }
}
