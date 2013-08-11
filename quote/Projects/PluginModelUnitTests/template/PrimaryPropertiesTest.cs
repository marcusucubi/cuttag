namespace PluginModelUnitTests
{
    using System;
    
    using NUnit.Framework;

    [TestFixture]
    public class PrimaryPropertiesTest
    {
        [Test]
        public void TestMethod()
        {
            Host.App.RegisteredClasses.Clear();
                
            Model.Template.Header header = new Model.Template.Header();
            Model.Product product = new Model.Product();
            Model.Template.Detail detail = new Model.Template.Detail(header, product);
            
            Model.Template.PrimaryProperties primary = header.PrimaryProperties as Model.Template.PrimaryProperties;
            
            DateTime now = DateTime.Now;
            
            primary.CommonCreatedDate = now;
            primary.CommonLastModified = now;
            
            primary.CommonInitials = "test";
            
            Assert.NotNull(primary.Customer);
            
            Assert.AreEqual(primary.CreatedDate, now);
            Assert.AreEqual(primary.LastModified, now);
            
            Assert.AreEqual(primary.QuoteNumber, 0);
            
            primary.PartNumber = "test";
            primary.RequestForQuoteNumber = "test";
            
            Assert.AreEqual(primary.Initials, "test");
            Assert.AreEqual(primary.PartNumber, "test");
            Assert.AreEqual(primary.RequestForQuoteNumber, "test");
        }
    }
}
