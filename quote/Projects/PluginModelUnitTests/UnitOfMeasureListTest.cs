using System;
using NUnit.Framework;

namespace PluginModelUnitTests
{
    [TestFixture]
    public class UnitOfMeasureListTest
    {
        [Test]
        public void TestMethod()
        {
            Model.UnitOfMeasureList.Init(new string[]{"test"} );
            
            int count = Model.UnitOfMeasureList.Collection.Count;
            Assert.AreEqual(count, 1);
            
            Model.UnitOfMeasureConverter converter = new Model.UnitOfMeasureConverter();
            
            converter.GetStandardValues(new TestContext());
            converter.GetStandardValuesExclusive(new TestContext());
            converter.GetStandardValuesSupported(new TestContext());
        }
    }
}
