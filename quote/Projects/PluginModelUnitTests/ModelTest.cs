namespace PluginModelUnitTests
{
    using System;
    using Model.Common;
    using NUnit.Framework;

    [TestFixture]
    public class ModelTest
    {
        [Test]
        public void TestOneProduct()
        {
            Model.Template.Header header = new Model.Template.Header();
            
            Model.ProductBuildData data = new Model.ProductBuildData();
            data.Code = "test";
            Model.Product product = new Model.Product(data);
            
            Model.Template.Detail detail = new Model.Template.Detail(header, product);
            header.Details.Add(detail);
            
            Assert.AreEqual(1, header.Details.Count);
        }
        
        [Test]
        public void TestWireProperties()
        {
            Model.Template.Header header = new Model.Template.Header();
            
            Model.ProductBuildData data = new Model.ProductBuildData();
            data.Code = "test";
            data.IsWire = true;
            Model.Product product = new Model.Product(data);
            
            Model.Template.Detail detail = new Model.Template.Detail(header, product);
            header.Details.Add(detail);
            
            ISavableProperties props = detail.QuoteDetailProperties;
            WireProperties wire = props as WireProperties;
            
            Assert.NotNull(wire);
        }
        
        [Test]
        public void TestComponentProperties()
        {
            Model.Template.Header header = new Model.Template.Header();
            
            Model.ProductBuildData data = new Model.ProductBuildData();
            data.Code = "test";
            data.IsWire = false;
            Model.Product product = new Model.Product(data);
            
            Model.Template.Detail detail = new Model.Template.Detail(header, product);
            header.Details.Add(detail);
            
            ISavableProperties props = detail.QuoteDetailProperties;
            ComponentProperties component = props as ComponentProperties;
            
            Assert.NotNull(component);
        }
    }
}
