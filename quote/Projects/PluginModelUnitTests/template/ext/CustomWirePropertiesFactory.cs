namespace PluginModelUnitTests
{
    using System;
    using Model.Template.Ext;

    public class CustomWirePropertiesFactory : IWirePropertiesFactory
    {
        public Model.Common.WireProperties CreateWireProperties(Model.Template.Detail detail)
        {
            return new CustomWireProperties(detail);
        }
    }
}
