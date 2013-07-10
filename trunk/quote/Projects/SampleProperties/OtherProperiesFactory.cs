using System;
using Model.Template;
using PluginHost;

namespace SampleProperties
{
    [Register(Key = typeof(Model.Template.IOtherProperiesFactory))]
    public class OtherProperiesFactory : Model.Template.IOtherProperiesFactory
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
