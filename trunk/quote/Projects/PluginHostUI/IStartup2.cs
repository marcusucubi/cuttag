// <summary>
// Contains the IStartup2 class.
// </summary>
// <copyright file="IStartup2.cs" company="Davis Computer Services">
//  No copyright information.
// </copyright>
namespace Host.UI
{
    /// <summary> 
    /// This will be called on start.  Any class can implement.
    /// </summary> 
    public interface IStartup2
    {
        /// <summary>
        /// Called on startup by <see cref="UIApp.Initialize" />.
        /// Use to setup user interface code.
        /// </summary> 
        void InitializeUI();
    }
}
