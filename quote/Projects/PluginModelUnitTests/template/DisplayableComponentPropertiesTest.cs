namespace PluginModelUnitTests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class DisplayableComponentPropertiesTest
    {
        [Test]
        public void TestMethod()
        {
            Host.App.RegisteredClasses.Clear();
                
            Model.Template.Header header = new Model.Template.Header();
            Model.Product product = new Model.Product();
            Model.Template.Detail detail = new Model.Template.Detail(header, product);
            
            Model.Template.DisplayableComponentProperties displayable = 
                detail.QuoteDetailProperties as Model.Template.DisplayableComponentProperties;
            Assert.IsNotNull(displayable);
            
            displayable.MachineTime = 1;
            displayable.Quantity = 1;
            displayable.UnitCost = 1;
            
            Assert.AreEqual(displayable.TotalMachineTime, 1);
            Assert.AreEqual(displayable.MachineTime, 1);
            Assert.AreEqual(displayable.Quantity, 1);
            Assert.AreEqual(displayable.UnitCost, 1);
            
            displayable.Description = "test";
            displayable.Vendor = "test";
            displayable.UnitOfMeasure = "test";
            
            Assert.AreEqual(displayable.Description, "test");
            Assert.AreEqual(displayable.Vendor, "test");
            Assert.AreEqual(displayable.UnitOfMeasure, "test");
        }
    }
}
