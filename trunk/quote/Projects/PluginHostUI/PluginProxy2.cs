// <summary>
// Contains the PluginProxy2 class.
// </summary>
// <copyright file="PluginProxy2.cs" company="Davis Computer Services">
//  No copyright information.
// </copyright>
namespace Host.UI
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reflection;
    
    /// <summary> 
    /// Represents a plugin.
    /// </summary> 
    public class PlugInProxy2 : PlugInProxy
    {
        /// <summary> 
        /// Menu items.
        /// </summary> 
        private readonly ICollection<PlugInMenuItem> pluginMenuItems;
        
        /// <summary> 
        /// Initializes a new instance of the <see cref="PlugInProxy2" /> class..
        /// </summary> 
        /// <param name="data">Data needed to create a proxy.</param>
        /// <param name="menus">A collection of menu objects.</param>
        public PlugInProxy2(
            PlugInProxyBuildData data,
            ICollection<PlugInMenuItem> menus)
         : base(data)
        {
            this.pluginMenuItems = menus;
        }
        
        /// <summary> 
        /// Gets the menu items.
        /// </summary> 
        public ReadOnlyCollection<PlugInMenuItem> PlugInMenuItems 
        {
            get { return new ReadOnlyCollection<PlugInMenuItem>(new List<PlugInMenuItem>(this.pluginMenuItems)); }
        }
    }
}
