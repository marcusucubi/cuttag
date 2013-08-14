namespace PluginModelUnitTests.Template.Ext
{
    using System;
    using Model.Template.Ext;

    public class CustomComponentPropertiesFactory : IComponentPropertiesFactory
    {
        public Model.Common.ComponentProperties CreateComponentProperties(Model.Template.Detail detail)
        {
            return new CustomComponentProperties(detail);
        }
    }
}
