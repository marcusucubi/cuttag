namespace Model.Template.Ext
{
    using System;
    using System.Linq;

    /// <summary>
    /// Provides a way for clients to customize the other properties.
    /// For example:
    /// <code>
    /// [Register(Key = typeof(Model.Template.Ext.IOtherPropertiesFactory))]
    /// public class OtherPropertiesFactory : Model.Template.Ext.IOtherPropertiesFactory 
    /// </code>
    /// </summary>
    public interface IOtherPropertiesFactory
    {
        /// <summary>
        /// Creates a custom other properties object.
        /// </summary>
        /// <param name="header">The header object.</param>
        /// <param name="id">The quote id.</param>
        /// <returns>The custom other properties object.</returns>
        Common.OtherProperties CreateOtherProperties(Header header, int id);
    }
}