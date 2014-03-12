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
    using System.Collections.ObjectModel;
    
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
        /// Called from the main form's OnLoad.
        /// </summary> 
        /// <returns>A collection of plug in objects.</returns>
        public static ICollection<PlugInProxy> Init()
        {
            return Host.PlugInLoader.Load();
        }
    }
}
