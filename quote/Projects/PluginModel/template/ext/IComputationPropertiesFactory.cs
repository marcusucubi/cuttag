namespace Model.Template.Ext
{
    using System;
    using System.Linq;

    public interface IComputationPropertiesFactory
    {
        Common.ComputationProperties CreateComputationProperties(Header header, int id);
    }
}
