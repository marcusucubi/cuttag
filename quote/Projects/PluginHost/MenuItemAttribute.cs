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
        /// The menu item's text.
        /// </summary> 
        public string Text
        {
            get;
            set;
        }

        /// <summary> 
        /// Name of the parent menu item. exmaple: View
        /// </summary> 
        public string Parent
        {
            get;
            set;
        }

        /// <summary> 
        /// If true, adds the item to the tool bar also.
        /// </summary> 
        public bool ShowInToolBar
        {
            get;
            set;
        }
    }
}
