namespace PluginModelUnitTests.Common
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class DetailCollectionTest
    {
        [Test]
        public void TestMethod()
        {
            Model.Template.Header header = new Model.Template.Header();
            Model.Common.DetailCollection<Model.Template.Detail> details = 
                new Model.Common.DetailCollection<Model.Template.Detail>(header);
            
            Model.ProductBuildData data = new Model.ProductBuildData() { IsWire = true };
            Model.Product product = new Model.Product(data);
            Model.Template.Detail detail = new Model.Template.Detail(header, product);
            
            details.Add(detail);
            
            details.IsDirty = false;
            
            bool flagDirty = false;
            details.Dirty += delegate { flagDirty = true; };
            
            bool flagClean = false;
            details.Clean += delegate { flagClean = true; };
            
            detail.ProductCode = "test";
            Assert.IsTrue(flagDirty);
            Assert.IsTrue(details.IsDirty);
            
            details.IsDirty = false;
            Assert.IsTrue(flagClean);
            Assert.IsFalse(details.IsDirty);
            
            details.Remove(detail);
            Assert.IsTrue(flagDirty);
            Assert.IsTrue(details.IsDirty);
            
            Assert.That(details.Header, Is.EqualTo(header));
            
        }
    }
}
