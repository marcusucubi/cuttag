using System;

using Model.Common;
using Host;

namespace SampleProperties
{
    [Register(Key = typeof(Model.Template.Ext.IWirePropertiesFactory))]
    public class WirePropertiesFactory : Model.Template.Ext.IWirePropertiesFactory 
    {
        public Model.Common.WireProperties CreateWireProperties(Model.Template.Detail detail)
        {
            return new DisplayableWireProperties(new SampleWireProperties(detail));
        }
    }
}
