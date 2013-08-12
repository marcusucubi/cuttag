namespace Model.Template.Ext
{
    using System;
    using System.Linq;
    
    using Host;
    
    using Model.Template.Ext;

    public static class PropertyFactory
    {
        public static Model.Common.OtherProperties CreateOtherProperties(
            Header header, 
            int id)
        {
            Model.Common.OtherProperties result = null;

            if (App.RegisteredClasses.ContainsKey(typeof(IOtherPropertiesFactory))) 
            {
                Type v = App.RegisteredClasses[typeof(IOtherPropertiesFactory)];

                if (v != null)
                {
                    IOtherPropertiesFactory o = Activator.CreateInstance(v) as IOtherPropertiesFactory;
                    
                    if (o == null)
                    {
                        string text = "Class registered as a IOtherPropertiesFactory, " + 
                            "is not a IOtherPropertiesFactory";
                        throw new ModelException(text);
                    }
                    
                    result = o.CreateOtherProperties(header, id);
                }
            }

            if (result == null) 
            {
                result = new DefaultOtherProperties();
            }

            return result;
        }

        public static Model.Common.ComputationProperties CreateComputationProperties(
            Header header, 
            int id)
        {
            Model.Common.ComputationProperties result = null;

            if (App.RegisteredClasses.ContainsKey(typeof(IComputationPropertiesFactory))) 
            {
                var v = App.RegisteredClasses[typeof(IComputationPropertiesFactory)];

                if (v != null)
                {
                    IComputationPropertiesFactory o = Activator.CreateInstance(v) as IComputationPropertiesFactory;

                    result = o.CreateComputationProperties(header, id);
                }
            }

            if (result == null) 
            {
                result = new DefaultComputationProperties();
            }

            return result;
        }

        public static Model.Common.ComponentProperties CreateComponentProperties(
            Template.Detail detail)
        {
            Model.Common.ComponentProperties result = null;

            if (App.RegisteredClasses.ContainsKey(typeof(IComponentPropertiesFactory))) 
            {
                var v = App.RegisteredClasses[typeof(IComponentPropertiesFactory)];

                if (v != null)
                {
                    IComponentPropertiesFactory o = 
                        Activator.CreateInstance(v) as IComponentPropertiesFactory;

                    result = o.CreateComponentProperties(detail);
                }
            }

            if (result == null) 
            {
                result = new DisplayableComponentProperties(new DefaultComponentProperties(detail));
            }

            return result;
        }

        public static Model.Common.WireProperties CreateWireProperties(
            Template.Detail detail)
        {
            Model.Common.WireProperties result = null;

            if (App.RegisteredClasses.ContainsKey(typeof(IWirePropertiesFactory))) 
            {
                var v = App.RegisteredClasses[typeof(IWirePropertiesFactory)];

                if (v != null)
                {
                    IWirePropertiesFactory o = Activator.CreateInstance(v) as IWirePropertiesFactory;

                    result = o.CreateWireProperties(detail);
                }
            }

            if (result == null) 
            {
                result = new DefaultWireProperties(detail);
            }

            return result;
        }
    }
}
