namespace PluginModelUnitTests
{
    using System;
    using NUnit.Framework;
    
    [TestFixture]
    public class GlobalOptionsTest
    {
        [Test]
        public void TestMethod()
        {
            bool changed = false;
            
            Model.Common.GlobalOptions.Instance.Changed += delegate { changed=true; };
            
            Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay = 1;
            
            Assert.IsTrue(changed);
            Assert.AreEqual(Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay, 1);
        }
    }
}
