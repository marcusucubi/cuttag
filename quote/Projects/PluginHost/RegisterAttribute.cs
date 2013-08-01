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
        /// The type for which the class is keyed.
        /// </summary> 
        /// <remarks>
        /// Custom properties are registered with IComputationPropertiesFactory
        /// </remarks>
        public Type Key
        {
            get;
            set;
        }
    }
}
