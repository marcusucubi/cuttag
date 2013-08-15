namespace PluginModelUnitTests.Quote
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class QuotePrimaryPropertiesTest
    {
        [Test]
        public void TestMethod()
        {
            DateTime now = DateTime.Now;
            
            Model.Quote.PrimaryProperties props = new Model.Quote.PrimaryProperties(
                1, "test", "test", "test", now, now, 2);
            
            Assert.That(props.CreatedDate, Is.EqualTo(now));
            Assert.That(props.LastModified, Is.EqualTo(now));
            Assert.That(props.QuoteNumber, Is.EqualTo(1));
            Assert.That(props.Initials, Is.EqualTo("test"));
            Assert.That(props.Customer, Is.Null);
            Assert.That(props.PartNumber, Is.EqualTo("test"));
            Assert.That(props.RequestForQuoteNumber, Is.EqualTo("test"));
            Assert.That(props.TemplateNumber, Is.EqualTo(2));
                
            props.SetTemplateId(10);
        }
    }
}