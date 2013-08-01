// <summary>
// Contains the IMenuAction interface.
// </summary>
// <copyright file="IMenuAction.cs" company="Davis Computer Services">
//  No copyright information.
// </copyright>
namespace Host
{
    using System;
    using NDepend.Attributes;

    /// <summary> 
    /// Used by classes with the MenuItemAttribute.
    /// </summary> 
    [CannotDecreaseVisibility]
    public interface IMenuAction
    {
        /// <summary> 
        /// Called on menu click.
        /// </summary> 
        void Execute();
    }
}
