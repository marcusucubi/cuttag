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
        /// A collection of <see cref="IStartup2" /> classes.
        /// </summary>
        private readonly ICollection<IStartup2> initializeUIItems;
        
        /// <summary> 
        /// Initializes a new instance of the <see cref="PlugInProxy2" /> class..
        /// </summary> 
        /// <param name="data">Data needed to create a proxy.</param>
        /// <param name="menus">A collection of menu objects.</param>
        /// <param name="initializeUIItems">A collection of <see cref="IStartup2" /> classes.</param>
        public PlugInProxy2(
            PlugInProxyBuildData data,
            ICollection<PlugInMenuItem> menus,
            ICollection<IStartup2> initializeUIItems)
         : base(data)
        {
            this.pluginMenuItems = menus;
            this.initializeUIItems = initializeUIItems;
        }
        
        /// <summary> 
        /// Gets the menu items.
        /// </summary> 
        public ReadOnlyCollection<PlugInMenuItem> PlugInMenuItems 
        {
            get { return new ReadOnlyCollection<PlugInMenuItem>(new List<PlugInMenuItem>(this.pluginMenuItems)); }
        }
        
        /// <summary>
        /// Gets a collection a objects that need initialization.
        /// </summary>
        public ReadOnlyCollection<IStartup2> InitializeUIItems 
        {
            get { return new ReadOnlyCollection<IStartup2>(new List<IStartup2>(this.initializeUIItems)); }
        }
    }
}
