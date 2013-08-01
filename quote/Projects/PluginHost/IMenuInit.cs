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
        /// Provide custom setup of the menu.
        /// </summary> 
        void InitMenu(ToolStripItem menu);

        /// <summary> 
        /// Provide custom setup of the tool button.
        /// </summary> 
        void InitButton(ToolStripButton button);
    }
}
