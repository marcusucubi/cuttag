namespace PluginModelUnitTests.Template
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class TemplateHeaderTest
    {
        [Test]
        public void TestMethod()
        {
            Model.Template.Header header = new Model.Template.Header();
            
            header.Id = 1;
            Assert.AreEqual(header.Id, 1);
            
            Assert.AreEqual(header.ID, 0);
            
            Model.Product product = new Model.Product();
            Model.Common.Detail detail = header.NewDetail(product);
            
            header.Remove(detail as Model.Template.Detail);
        }
    }
}