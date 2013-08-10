using System.Globalization;
namespace PluginModelUnitTests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class CustomerConverterTest
    {
        [Test]
        public void TestMethod()
        {
            Model.Customer customer = new Model.Customer();
            Model.CustomerList.Init(new Model.Customer[] {customer} );
            
            int count = Model.CustomerList.Collection.Count;
            Assert.AreEqual(count, 1);
            
            Model.CustomerConverter converter = new Model.CustomerConverter();
            
            converter.GetStandardValues(new TestContext());
            converter.GetStandardValuesExclusive(new TestContext());
            converter.GetStandardValuesSupported(new TestContext());
            
            bool test = converter.CanConvertFrom(new TestContext(), typeof(string));
            Assert.IsTrue(test);

            test = converter.CanConvertFrom(new TestContext(), typeof(int));
            Assert.IsFalse(test);
            
            object objTest = converter.ConvertFrom(new TestContext(), CultureInfo.CurrentCulture, "nothing");
            Assert.IsNotNull(objTest);
            
            bool error = false;
            try
            {
                converter.ConvertFrom(new TestContext(), CultureInfo.CurrentCulture, customer);
            }
            catch(NotSupportedException)
            {
                error = true;
            }
            Assert.IsTrue(error);
        }
    }
}
