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
        /// Classes to setup on startup.
        /// </summary> 
        private readonly List<IInit> classesToInit = new List<IInit>();
        
        /// <summary> 
        /// The assemble.
        /// </summary> 
        private readonly Assembly assembly;

        /// <summary> 
        /// Initializes a new instance of the <see cref="PluginProxy" /> class..
        /// </summary> 
        /// <param name="pluginMenuItems">The menu items for the plugin.</param>
        /// <param name="classesToInit">The classes to setup.</param>
        /// <param name="assembly">The assembly of the plugin.</param>
        internal PluginProxy(
            List<PluginMenuItem> pluginMenuItems,
            List<IInit> classesToInit,
            Assembly assembly)
        {
            this.pluginMenuItems = pluginMenuItems;
            this.classesToInit = classesToInit;
            this.assembly = assembly;
        }

        /// <summary> 
        /// Gets the menu items.
        /// </summary> 
        internal PluginMenuItem[] PluginMenuItems 
        {
            get { return this.pluginMenuItems.ToArray(); }
        }

        /// <summary> 
        /// Gets the classes to setup on startup.
        /// </summary> 
        internal IInit[] ClassesToInit
        {
            get { return this.classesToInit.ToArray(); }
        }

        /// <summary> 
        /// Gets the assembly of the plugin.
        /// </summary> 
        internal Assembly Assembly
        {
            get { return this.assembly; }
        }
    }
}
