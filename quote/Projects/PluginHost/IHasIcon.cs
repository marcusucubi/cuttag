namespace Host
{
    using System;
    using System.Drawing;

    /// <summary> 
    /// For classes the have the MenuItemAttribute.
    /// </summary> 
    public interface IHasIcon
    {
        /// <summary> 
        /// Gets the image to display.
        /// </summary> 
        /// <value>The image to display.</value>
        Image Image
        {
            get;
        }
    }
}
