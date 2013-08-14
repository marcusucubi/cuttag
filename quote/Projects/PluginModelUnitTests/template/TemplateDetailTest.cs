namespace PluginModelUnitTests.Common
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class TemplateDetailTest
    {
        [Test]
        public void TestMethod()
        {
            Model.Template.Header header = new Model.Template.Header();
            
            Model.Product product = new Model.Product();
            Model.Template.Detail detail = header.NewDetail(product) as Model.Template.Detail;
            
            Model.ProductBuildData data = new Model.ProductBuildData();
            data.Code = "test";
            Model.Product product2 = new Model.Product(data);
            
            detail.UpdateComponentProperties(product2);
        }
    }
}
