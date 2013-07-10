using System;
using Model.Template;
using PluginHost;

namespace SampleProperties 
{
    [Register(Key = typeof(Model.Template.IComputationProperiesFactory))]
    public class ComputationProperiesFactory : Model.Template.IComputationProperiesFactory
    {
        public ComputationProperiesFactory()
        {
        }

        public Model.Common.SaveableProperties CreateComputationProperties(
            Header header, int id)
        {
            return new SampleComputationProperties(header);
        }

    }
}
