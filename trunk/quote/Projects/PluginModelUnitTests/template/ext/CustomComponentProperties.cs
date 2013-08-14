namespace PluginModelUnitTests.Template.Ext
{
    using System;

    public class CustomComponentProperties : Model.Template.ComponentProperties
    {
        public CustomComponentProperties(Model.Template.Detail quoteDetail)
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
