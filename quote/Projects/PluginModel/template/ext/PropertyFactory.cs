namespace Model.Template.Ext
{
    using System;
    using System.Linq;
    
    using Host;
    
    using Model.Template.Ext;

    /// <summary>
    /// A utility class used to create each of the property objects.
    /// It checks if the factory interface has been registered,
    /// and if it has, the custom factory is created.
    /// </summary>
    public static class PropertyFactory
    {
        /// <summary>
        /// Creates the property object.
        /// </summary>
        /// <param name="header">The header object.</param>
        /// <param name="id">The quote id.</param>
        /// <returns>The new property object.</returns>
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
                    var o = Activator.CreateInstance(v) as IOtherPropertiesFactory;
                    
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

        /// <summary>
        /// Creates the property object.
        /// </summary>
        /// <param name="header">The header object.</param>
        /// <param name="id">The quote id.</param>
        /// <returns>The new property object.</returns>
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
                    var o = Activator.CreateInstance(v) as IComputationPropertiesFactory;

                    result = o.CreateComputationProperties(header, id);
                }
            }

            if (result == null) 
            {
                result = new DefaultComputationProperties();
            }

            return result;
        }

        /// <summary>
        /// Creates the property object.
        /// </summary>
        /// <param name="detail">The detail object.</param>
        /// <returns>The new property object.</returns>
        public static Model.Common.ComponentProperties CreateComponentProperties(
            Template.Detail detail)
        {
            Model.Common.ComponentProperties result = null;

            if (App.RegisteredClasses.ContainsKey(typeof(IComponentPropertiesFactory))) 
            {
                var v = App.RegisteredClasses[typeof(IComponentPropertiesFactory)];

                if (v != null)
                {
                    var o = Activator.CreateInstance(v) as IComponentPropertiesFactory;

                    result = o.CreateComponentProperties(detail);
                }
            }

            if (result == null) 
            {
                result = new DisplayableComponentProperties(new DefaultComponentProperties(detail));
            }

            return result;
        }

        /// <summary>
        /// Creates the property object.
        /// </summary>
        /// <param name="detail">The detail object.</param>
        /// <returns>The new property object.</returns>
        public static Model.Common.WireProperties CreateWireProperties(
            Template.Detail detail)
        {
            Model.Common.WireProperties result = null;

            if (App.RegisteredClasses.ContainsKey(typeof(IWirePropertiesFactory))) 
            {
                var v = App.RegisteredClasses[typeof(IWirePropertiesFactory)];

                if (v != null)
                {
                    var o = Activator.CreateInstance(v) as IWirePropertiesFactory;
                    
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
