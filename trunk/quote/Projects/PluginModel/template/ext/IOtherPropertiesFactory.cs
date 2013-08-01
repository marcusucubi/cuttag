namespace Model.Template.Ext
{
    using System;
    using System.Linq;

    public interface IOtherPropertiesFactory
    {
        Common.OtherProperties CreateOtherProperties(Header header, int id);
    }
}