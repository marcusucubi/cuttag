namespace PluginModelUnitTests.Quote
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class QuoteHeaderTest
    {
        [Test]
        public void TestMethod()
        {
            DateTime now = DateTime.Now;
            
            Model.Quote.Header header = new Model.Quote.Header(
                1, "test", "test", "test", now, now, 2);
            
            Assert.That(header.IsQuote, Is.True);
            Assert.That(header.DisplayName, Is.EqualTo("Quote 1"));
            Model.Common.Detail detail = header.NewDetail(new Model.Product());
            Assert.That(detail, Is.Not.Null);
            
            header.SetPublicPrimaryProperties(new Model.Common.PrimaryProperties());
            header.SetPublicComputationProperties(new Model.Common.ComputationProperties());
            header.SetPublicOtherProperties(new Model.Common.OtherProperties());
            header.SetPublicNoteProperties(new Model.Common.NoteProperties());
            
            Model.Quote.Header header2 = new Model.Quote.Header();
        }
    }
}