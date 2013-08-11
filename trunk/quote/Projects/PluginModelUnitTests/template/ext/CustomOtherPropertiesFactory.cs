namespace PluginModelUnitTests
{
    using System;

    public class CustomOtherPropertiesFactory 
        : Model.Template.Ext.IOtherPropertiesFactory
    {
        public Model.Common.OtherProperties CreateOtherProperties(Model.Template.Header header, int id)
        {
            return new CustomOtherProperties();
        }
    }
}
