namespace Model.Template.Ext
{
    using System;
    using System.Linq;

    /// <summary>
    /// Provides a way for clients to customize the component properties.
    /// For example:
    /// <code>
    /// [Register(Key = typeof(Model.Template.Ext.IComponentPropertiesFactory))]
    /// public class ComponentPropertiesFactory : Model.Template.Ext.IComponentPropertiesFactory
    /// </code>
    /// </summary>
    public interface IComponentPropertiesFactory
    {
        /// <summary>
        /// Creates a custom properties.
        /// </summary>
        /// <param name="detail">The detail object.</param>
        /// <returns>A custom component properties object.</returns>
        Common.ComponentProperties CreateComponentProperties(Template.Detail detail);
    }
}