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
            
            Assert.That(header.IsQuote, Is.False);
        }
        
        [Test]
        public void TestNextSequence()
        {
            Model.Template.Header quote = new Model.Template.Header();
            
            quote.NextSequenceNumber = 10;
            Assert.That(quote.NextSequenceNumber, Is.EqualTo(10));
            Assert.That(quote.NextSequenceNumber, Is.EqualTo(11));
        }
    }
}