namespace PluginModelUnitTests
{
    using System;

    public class TestContext : System.ComponentModel.ITypeDescriptorContext
    {
        public System.ComponentModel.IContainer Container {
            get {
                throw new NotImplementedException();
            }
        }
        
        public object Instance {
            get {
                throw new NotImplementedException();
            }
        }
        
        public System.ComponentModel.PropertyDescriptor PropertyDescriptor {
            get {
                throw new NotImplementedException();
            }
        }
        
        public bool OnComponentChanging()
        {
            throw new NotImplementedException();
        }
        
        public void OnComponentChanged()
        {
            throw new NotImplementedException();
        }
        
        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}
