namespace Model.Template.Ext
{
    using System;
    using System.Linq;

    /// <summary>
    /// Provides a way for clients to customize the wire properties.
    /// For example:
    /// <code>
    /// [Register(Key = typeof(Model.Template.Ext.IWirePropertiesFactory))]
    /// public class WirePropertiesFactory : Model.Template.Ext.IWirePropertiesFactory 
    /// </code>
    /// </summary>
    public interface IWirePropertiesFactory
    {
        /// <summary>
        /// Creates a custom wire properties object.
        /// </summary>
        /// <param name="detail">The detail object.</param>
        /// <returns>The custom wire properties object.</returns>
        Common.WireProperties CreateWireProperties(Detail detail);
    }
}