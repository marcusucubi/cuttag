namespace PluginModelUnitTests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class ModelEventsTest
    {
        [Test]
        public void TestMethod()
        {
            bool changed = false;
            
            Model.ModelEvents.Instance.QuoteViewed += delegate { changed=true; };
            Model.ModelEvents.Instance.NotifyQuoteViewed();
            Assert.IsTrue(changed);
            
            changed = false;
            Model.ModelEvents.Instance.TemplateCreated += delegate { changed=true; };
            Model.ModelEvents.Instance.NotifyTemplateCreated(1);
            Assert.IsTrue(changed);
            
            changed = false;
            Model.ModelEvents.Instance.TemplateViewed += delegate { changed=true; };
            Model.ModelEvents.Instance.NotifyTemplateViewed();
            Assert.IsTrue(changed);
        }
    }
}
