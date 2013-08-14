namespace PluginModelUnitTests.Template
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class OtherPropertiesTest
    {
        [Test]
        public void TestMethod()
        {
            Host.App.RegisteredClasses.Clear();
                
            Host.App.RegisteredClasses.Add(
                typeof(Model.Template.Ext.IOtherPropertiesFactory), 
                typeof(CustomOtherPropertiesFactory));
            
            Model.Template.Header header = new Model.Template.Header();
            Model.Product product = new Model.Product();
            Model.Template.Detail detail = new Model.Template.Detail(header, product);
            
            CustomOtherProperties other = header.OtherProperties as CustomOtherProperties;
            
            Assert.IsNotNull(other);
        }
        
        [Test]
        public void TestException()
        {
            bool error = false;
            try
            {
                Host.App.RegisteredClasses.Clear();
                
                Host.App.RegisteredClasses.Add(
                    typeof(Model.Template.Ext.IOtherPropertiesFactory), 
                    typeof(CustomOtherProperties));
                
                Model.Template.Header header = new Model.Template.Header();
            }
            catch(Model.ModelException)
            {
                error = true;
            }
            
            Assert.IsTrue(error);
        }
    }
}
