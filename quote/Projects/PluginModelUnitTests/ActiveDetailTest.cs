using System;
using NUnit.Framework;

namespace PluginModelUnitTests
{
    [TestFixture]
    public class ActiveDetailTest
    {
        [Test]
        public void TestMethod()
        {
            Model.Template.Header header = new Model.Template.Header();
            
            Model.ProductBuildData data = new Model.ProductBuildData();
            data.Code = "test";
            data.IsWire = false;
            Model.Product product = new Model.Product(data);
            
            Model.Template.Detail detail = new Model.Template.Detail(header, product);
            header.Details.Add(detail);
            
            bool changed = false;
            
            Model.ActiveDetail.Instance.PropertyChanged += delegate { changed = true; };
            
            Model.ActiveDetail.Instance.Detail = detail;
            
            Assert.IsTrue(changed);
            
            Assert.AreEqual(detail, Model.ActiveDetail.Instance.Detail);
        }
    }
}
