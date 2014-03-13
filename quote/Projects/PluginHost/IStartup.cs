// <summary>
// Contains the <c>IInit</c> interface.
// </summary>
// <copyright file="IInit.cs" company="Davis Computer Services">
//  No copyright information.
// </copyright>
namespace Host
{
    /// <summary> 
    /// This will be called on start.  Any class can implement.
    /// See <see cref="IStartup2.InitializeUI"/> for user interface
    /// initialization.
    /// </summary> 
    public interface IStartup
    {
        /// <summary>
        /// Called on startup by <see cref="App.Initialize" />.
        /// Use to setup database code.
        /// </summary> 
        void Initialize();
    }
}
