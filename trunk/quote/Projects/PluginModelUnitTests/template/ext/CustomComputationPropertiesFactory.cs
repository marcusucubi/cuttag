namespace PluginModelUnitTests
{
    using System;
    
    using Model.Template.Ext;

    public class CustomComputationPropertiesFactory : IComputationPropertiesFactory
    {
        public Model.Common.ComputationProperties CreateComputationProperties(
            Model.Template.Header header, int id)
        {
            return new CustomComputationProperties(header);
        }
    }
}
