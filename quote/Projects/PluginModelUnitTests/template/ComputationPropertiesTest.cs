namespace PluginModelUnitTests.Template
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class ComputationPropertiesTest
    {
        [Test]
        public void TestMethod()
        {
            Host.App.RegisteredClasses.Clear();
            
            Host.App.RegisteredClasses.Add(
                typeof(Model.Template.Ext.IComputationPropertiesFactory), 
                typeof(CustomComputationPropertiesFactory));
            
            Model.Template.Header header = new Model.Template.Header();
            Model.Product product = new Model.Product();
            Model.Template.Detail detail = new Model.Template.Detail(header, product);
            
            CustomComputationProperties computation = header.ComputationProperties as CustomComputationProperties;
            
            Assert.IsNotNull(computation);
        }
    }
}
