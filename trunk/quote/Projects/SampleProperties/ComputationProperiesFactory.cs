using System;

using Host;
using Model.Template.Ext;

namespace SampleProperties 
{
    [Register(Key = typeof(Model.Template.Ext.IComputationPropertiesFactory))]
    public class ComputationProperiesFactory : IComputationPropertiesFactory
    {

        public Model.Common.SavableProperties CreateComputationProperties(
            Model.Template.Header header, int id)
        {
            return new DisplayableComputationProperties(new SampleComputationProperties(header));
        }

    }
}
