namespace PluginModelUnitTests
{
    using System;
    
    using Model;
    
    using NUnit.Framework;

    [TestFixture]
    public class CustomerTest
    {
        [Test]
        public void TestDefaultConstructor()
        {
            Customer customer = new Customer();
            
            customer.SetName("test");
            customer.SetId(100);
            
            Assert.AreEqual(customer.Name, "test");
            Assert.AreEqual(customer.Id, 100);
            
            Assert.GreaterOrEqual(customer.ToString().Length, 0);
        }
        
        [Test]
        public void TestCreateFromString()
        {
            Customer customer = Customer.CreateFromString("100 test");
            
            Assert.AreEqual(customer.Name, "test");
            Assert.AreEqual(customer.Id, 100);
        }
        
        [Test]
        public void TestCreateFromString2()
        {
            Customer customer = Customer.CreateFromString("test");
            
            Assert.AreEqual(customer.Name, "test");
            Assert.AreEqual(customer.Id, 0);
            
            Assert.GreaterOrEqual(customer.ToString().Length, 0);
        }
        
        [Test]
        public void TestCreateFromString3()
        {
            Customer customer = Customer.CreateFromString("test");
            
            Assert.AreEqual(customer.Name, "test");
            Assert.AreEqual(customer.Id, 0);
        }
        
        [Test]
        public void TestCreateFromString4()
        {
            Customer customer = Customer.CreateFromString("");
            
            Assert.AreEqual(customer.Name, "");
            Assert.AreEqual(customer.Id, 0);
        }
        
        [Test]
        public void TestCreateFromString5()
        {
            Customer customer = Customer.CreateFromString("aa test");
            
            Assert.AreEqual(customer.Name, "test");
            Assert.AreEqual(customer.Id, 0);
        }
        
        [Test]
        public void TestCreateFromString6()
        {
            Customer customer = Customer.CreateFromString("  ");
            
            Assert.AreEqual(customer.Name, "  ");
            Assert.AreEqual(customer.Id, 0);
        }
        
        [Test]
        public void TestCreateFromString7()
        {
            Customer customer1 = Customer.CreateFromString("100 test");
            Customer customer2 = Customer.CreateFromString("100 test");
            
            Assert.AreEqual(customer1, customer2);
            Assert.AreEqual(customer1.GetHashCode(), customer2.GetHashCode());
        }
    }
}
