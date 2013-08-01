namespace Model.Template.Ext
{
    using System;
    using System.Linq;

    public interface IComponentPropertiesFactory
    {
        Common.ComponentProperties CreateComponentProperties(Template.Detail detail);
    }
}