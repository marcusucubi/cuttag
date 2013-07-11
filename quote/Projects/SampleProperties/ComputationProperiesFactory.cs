using System;
using Model.Template;
using PluginHost;

namespace SampleProperties 
{
    [Register(Key = typeof(Model.Template.Ext.IComputationProperiesFactory))]
    public class ComputationProperiesFactory : Model.Template.Ext.IComputationProperiesFactory
    {

        public Model.Common.SaveableProperties CreateComputationProperties(
            Header header, int id)
        {
            return new SampleComputationProperties(header);
        }

    }
}
