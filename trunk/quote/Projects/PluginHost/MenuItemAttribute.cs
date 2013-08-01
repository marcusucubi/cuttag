// <summary>
// Contains the MenuItemAttribute class.
// </summary>
// <copyright file="MenuItemAttribute.cs" company="Davis Computer Services">
//  No copyright information.
// </copyright>
namespace Host
{
    using System;
    using NDepend.Attributes;
    
    /// <summary> 
    /// Used to add a new menu item to the main form.
    /// </summary> 
    [AttributeUsageAttribute(AttributeTargets.Class), CannotDecreaseVisibility]
    public sealed class MenuItemAttribute : Attribute
    {
        /// <summary> 
        /// Gets or sets the menu item's text.
        /// </summary> 
        /// <value>The menu item's text.</value>
        public string Text
        {
            get;
            set;
        }

        /// <summary> 
        /// Gets or sets the name of the parent menu item. For example: View.
        /// </summary> 
        /// <value>The name of the parent menu item.</value>
        public string Parent
        {
            get;
            set;
        }

        /// <summary> 
        /// Gets or sets a value indicating whether the item is added to the tool bar.
        /// </summary> 
        /// <value>The item is added to the tool bar.</value>
        public bool ShowInToolBar
        {
            get;
            set;
        }
    }
}
