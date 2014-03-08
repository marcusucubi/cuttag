namespace Model.Template
{
    using System;
    using System.Linq;

    /// <summary>
    /// The default wire properties.
    /// </summary>
    public sealed class DefaultWireProperties : Template.WireProperties
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultWireProperties" /> class.
        /// </summary>
        /// <param name="quoteDetail">A quote detail object.</param>
        public DefaultWireProperties(Model.Template.Detail quoteDetail) : base(quoteDetail)
        {
            // Base class
        }
    }
}