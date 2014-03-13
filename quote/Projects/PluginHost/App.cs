// <summary>
// Contains the App class.
// </summary>
// <copyright file="App.cs" company="Davis Computer Services">
//  No copyright information.
// </copyright>
namespace Host
{
    using System;
    using System.Collections.Generic;
    
    /// <summary> 
    /// Central class used to access common application properties.
    /// </summary> 
    public static class App
    {
        /// <summary> 
        /// Classes with the RegisterAttribute.
        /// </summary> 
        private static readonly Dictionary<Type, Type> ClassDictionary = new Dictionary<Type, Type>();

        /// <summary> 
        /// Gets classes with the RegisterAttribute.
        /// </summary> 
        /// <value>Classes with the RegisterAttribute.</value>
        public static Dictionary<Type, Type> RegisteredClasses
        {
            get { return ClassDictionary; }
        }

        /// <summary> 
        /// Loads and initializes the plugins using plugins.xml.
        /// </summary> 
        /// <returns>A collection of plug in objects.</returns>
        public static ICollection<PlugInProxy> Initialize()
        {
            ICollection<PlugInProxy> result = PlugInLoader.Load();
            
            return result;
        }
    }
}
