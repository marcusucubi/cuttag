// <summary>
// Contains the IMenuAction interface.
// </summary>
// <copyright file="IMenuAction.cs" company="Davis Computer Services">
//  No copyright information.
// </copyright>
namespace Host.UI
{
    using System;

    /// <summary> 
    /// Used by classes with the MenuItemAttribute.
    /// </summary> 
    public interface IMenuAction
    {
        /// <summary> 
        /// Called on menu click.
        /// </summary> 
        void Execute();
    }
}
