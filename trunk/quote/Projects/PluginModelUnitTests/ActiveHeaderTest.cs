using System;
using NUnit.Framework;

namespace PluginModelUnitTests
{
    [TestFixture]
    public class ActiveHeaderTest
    {
        [Test]
        public void TestChanged()
        {
            Model.Template.Header header = new Model.Template.Header();
            
            bool changed = false;
            
            Model.ActiveHeader.Instance.PropertyChanged += delegate { changed = true; };
            
            Model.ActiveHeader.Instance.Header = header;
            
            Assert.IsTrue(changed);
            
            Assert.AreEqual(header, Model.ActiveHeader.Instance.Header);
        }
    }
}
