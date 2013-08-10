namespace PluginModelUnitTests
{
    using System;
    using Model.Common;
    using NUnit.Framework;

    [TestFixture]
    public class ComponentPropertiesTest
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
            
        }
    }
}
