namespace PluginModelUnitTests
{
    using System;

    public class CustomWireProperties : Model.Template.WireProperties
    {
        public CustomWireProperties(Model.Template.Detail quoteDetail)
            : base(quoteDetail)
        {
        }
        
        public int CustomProperty
        {
            get;
            set;
        }
    }
}
