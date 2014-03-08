namespace Model.Template
{
    using System;
    using System.Linq;

    /// <summary>
    /// The default component properties.
    /// </summary>
    public sealed class DefaultComponentProperties : Template.ComponentProperties
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultComponentProperties" /> class.
        /// </summary>
        /// <param name="quoteDetail">The detail object.</param>
        public DefaultComponentProperties(Template.Detail quoteDetail) : base(quoteDetail)
        {
        }
    }
}
