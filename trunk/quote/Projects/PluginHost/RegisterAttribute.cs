// <summary>
// Contains the RegisterAttribute class.
// </summary>
// <copyright file="RegisterAttribute.cs" company="Davis Computer Services">
//  No copyright information.
// </copyright>
namespace Host
{
    using System;

    /// <summary> 
    /// Adds the class to the App.RegisteredClasses collection.
    /// </summary> 
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class RegisterAttribute : Attribute
    {
        /// <summary> 
        /// Gets or sets the type for which the class is keyed.
        /// </summary> 
        /// <value>The type for which the class is keyed.</value>
        /// <remarks>
        /// Custom properties are registered with IComputationPropertiesFactory.
        /// </remarks>
        public Type Key
        {
            get;
            set;
        }
    }
}
