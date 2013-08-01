// <summary>
// Contains the IMenuInit class.
// </summary>
// <copyright file="IMenuInit.cs" company="Davis Computer Services">
//  No copyright information.
// </copyright>
namespace Host
{
    using System;
    using System.Windows.Forms;

    /// <summary> 
    /// Used by classes with the MenuItemAttribute.
    /// </summary> 
    public interface IMenuInit
    {
        /// <summary> 
        /// Provides custom setup of the menu.
        /// </summary> 
        /// <param name="menu" >The menu to setup.</param>
        void InitMenu(ToolStripItem menu);

        /// <summary> 
        /// Provides custom setup of the tool button.
        /// </summary> 
        /// <param name="button" >The button to setup.</param>
        void InitButton(ToolStripButton button);
    }
}
