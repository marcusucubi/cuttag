using System;
using Model.Common;
using PluginHost;

namespace SampleProperties
{
    [Register(Key = typeof(Model.Template.IWireProperiesFactory))]
    public class WirePropertiesFactory : Model.Template.IWireProperiesFactory 
    {
        public SaveableProperties CreateWireProperties(Model.Template.Detail detail)
        {
            return new SampleWireProperties(detail);
        }
    }
}
