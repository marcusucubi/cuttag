using System;

using Model.Template;
using Host;

namespace SampleProperties
{
    [Register(Key = typeof(Model.Template.Ext.IOtherPropertiesFactory))]
    public class OtherProperiesFactory : Model.Template.Ext.IOtherPropertiesFactory
    {
        public OtherProperiesFactory()
        {
        }

        public Model.Common.SaveableProperties CreateOtherProperties(
            Header header, int id)
        {
            return new SampleOtherProperties(header);
        }

    }
}
