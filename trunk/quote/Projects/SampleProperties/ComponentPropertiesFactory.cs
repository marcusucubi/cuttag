using System;

using Model.Common;
using Host;

namespace SampleProperties
{
    [Register(Key = typeof(Model.Template.Ext.IComponentPropertiesFactory))]
    public class ComponentPropertiesFactory : Model.Template.Ext.IComponentPropertiesFactory
    {
        public SavableProperties CreateComponentProperties(Model.Template.Detail detail)
        {
            return new SampleComponentProperties(detail);
        }
    }
}
