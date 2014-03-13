namespace Host
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// The class contains the data needed to build
    /// a <see cref="PlugInProxy" />.
    /// </summary>
    public class PlugInProxyBuildData
    {
        /// <summary>
        /// A collection of setup object.
        /// </summary>
        private ICollection<IStartup> initList;
        
        /// <summary>
        /// Gets or sets the assembly.
        /// </summary>
        /// <value>A assembly object.</value>
        public Assembly Assembly { get; set; }
        
        /// <summary>
        /// Gets a collection of setup objects.
        /// </summary>
        /// <value>A collection of setup objects.</value>
        public ICollection<IStartup> InitList 
        {
            get { return this.initList; }
        }
        
        /// <summary>
        /// Sets the list of setup objects.
        /// </summary>
        /// <param name="initList">The new list.</param>
        public void SetInitList(ICollection<IStartup> initList)
        {
            this.initList = initList;
        }
   }
}
