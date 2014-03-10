namespace Model.Template.Ext
{
    using System;
    using System.Linq;

    /// <summary>
    /// Provides a way for clients to customize the computation properties.
    /// For example:
    /// <code>
    /// [Register(Key = typeof(Model.Template.Ext.IComputationPropertiesFactory))]
    /// public class ComputationProperiesFactory : IComputationPropertiesFactory
    /// </code>
    /// </summary>
    public interface IComputationPropertiesFactory
    {
        /// <summary>
        /// Creates a custom computation properties object.
        /// </summary>
        /// <param name="header">The header object.</param>
        /// <param name="id">The header id.</param>
        /// <returns>The custom computation object.</returns>
        Common.ComputationProperties CreateComputationProperties(Header header, int id);
    }
}
