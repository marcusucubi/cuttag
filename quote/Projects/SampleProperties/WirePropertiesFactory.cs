using System;

using Model.Common;
using Host;

namespace SampleProperties
{
    [Register(Key = typeof(Model.Template.Ext.IWireProperiesFactory))]
    public class WirePropertiesFactory : Model.Template.Ext.IWireProperiesFactory 
    {
        public SaveableProperties CreateWireProperties(Model.Template.Detail detail)
        {
            return new DisplayableWireProperties(new SampleWireProperties(detail));
        }
    }
}
