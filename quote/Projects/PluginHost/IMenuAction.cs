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
