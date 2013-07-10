using System;
using Model.Common;
using PluginHost;

namespace SampleProperties
{
    [Register(Key = typeof(Model.Template.IComponentPropertiesFactory))]
    public class ComponentPropertiesFactory : Model.Template.IComponentPropertiesFactory
    {
        public SaveableProperties CreateComponentProperties(Model.Template.Detail detail)
        {
            return new SampleComponentProperties(detail);
        }
    }
}
