namespace PluginModelUnitTests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class CustomerListTest
    {
        [Test]
        public void TestMethod()
        {
            Model.Customer customer = new Model.Customer();
            Model.Customer[] list = new Model.Customer[] {customer};
            
            Model.CustomerList.Init(list);
            
            bool test = Model.CustomerList.Collection.Contains(customer);
            Assert.IsTrue(test);
        }
    }
}
