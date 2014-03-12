// <summary>
// Contains the PluginProxy class.
// </summary>
// <copyright file="PluginProxy.cs" company="Davis Computer Services">
//  No copyright information.
// </copyright>
namespace Host
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reflection;
    
    /// <summary> 
    /// Represents a plugin.
    /// </summary> 
    public class PlugInProxy
    {
        /// <summary> 
        /// Classes to setup on startup.
        /// </summary> 
        private readonly ICollection<IInit> classesToInit = new List<IInit>();
        
        /// <summary> 
        /// The assemble.
        /// </summary> 
        private readonly Assembly assembly;

        /// <summary> 
        /// Initializes a new instance of the <see cref="PlugInProxy" /> class..
        /// </summary> 
        /// <param name="data">The build data.</param>
        public PlugInProxy(PlugInProxyBuildData data)
        {
            this.classesToInit = data.InitList;
            this.assembly = data.Assembly;
        }

        /// <summary> 
        /// Gets the classes to setup on startup.
        /// </summary> 
        /// <value>A collection of objects.</value>
        public ReadOnlyCollection<IInit> ClassesToInit
        {
            get 
            { 
                return new ReadOnlyCollection<IInit>(
                    new List<IInit>(this.classesToInit));
            }
        }

        /// <summary> 
        /// Gets the assembly of the plugin.
        /// </summary> 
        /// <value>A assembly.</value>
        public Assembly Assembly
        {
            get { return this.assembly; }
        }
    }
}
