namespace Host
{
    using System;
    using System.Drawing;

    /// <summary> 
    /// For classes the have the MenuItemAttribue.
    /// </summary> 
    public interface IHasIcon
    {
        /// <summary> 
        /// The image to display.
        /// </summary> 
        Image Image
        {
            get;
        }
    }
}
