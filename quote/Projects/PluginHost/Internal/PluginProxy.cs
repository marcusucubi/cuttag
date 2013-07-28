﻿using System;
using System.Collections.Generic;
using System.Reflection;

using PluginHost;

namespace PluginHost.Internal
{
    sealed class PluginProxy
    {
        private readonly List<PluginMenuItem> pluginMenuItems = new List<PluginMenuItem>();
        private readonly List<IPluginInit> plugins = new List<IPluginInit>();
        private readonly Assembly assembly;

        internal PluginProxy(
            List<PluginMenuItem> pluginMenuItems,
            List<IPluginInit> plugins,
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

        internal IPluginInit[] Plugins
        {
            get { return this.plugins.ToArray(); }
        }

        internal Assembly Assembly
        {
            get { return this.assembly; }
        }

    }
}
