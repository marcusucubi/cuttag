namespace Model.Template.Ext
{
    using System;
    using System.Linq;

    public interface IWirePropertiesFactory
    {
        Common.WireProperties CreateWireProperties(Detail detail);
    }
}