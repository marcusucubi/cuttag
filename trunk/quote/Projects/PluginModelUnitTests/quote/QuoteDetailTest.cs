namespace PluginModelUnitTests.Quote
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class QuoteDetailTest
    {
        [Test]
        public void TestMethod()
        {
            Model.Quote.Header header = new Model.Quote.Header();
            
            Model.ProductBuildData data = new Model.ProductBuildData() { IsWire=true };
            Model.Product product = new Model.Product(data);
            
            Model.Quote.Detail detail = (Model.Quote.Detail)header.NewDetail(product);
            
            Model.Quote.QuoteSavableProperties props = new Model.Quote.QuoteSavableProperties();
            detail.SetProperties(props);
            Assert.That(detail.QuoteDetailProperties, Is.EqualTo(props));
            
            detail.Qty = 10;
            Assert.That(detail.Qty, Is.EqualTo(10));
        }
    }
}