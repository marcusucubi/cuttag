namespace PluginModelUnitTests
{
    using System;
    using Model.Common;
    
    using NUnit.Framework;
    
    using PluginModelUnitTests.template.ext;

    [TestFixture]
    public class ComponentPropertiesTest
    {
        [Test]
        public void TestDefault()
        {
            Host.App.RegisteredClasses.Clear();
                
            Model.Template.Header header = new Model.Template.Header();
            Model.Product product = new Model.Product();
            Model.Template.Detail detail = new Model.Template.Detail(header, product);
            
            Model.Template.DisplayableComponentProperties displayable = 
                detail.QuoteDetailProperties as Model.Template.DisplayableComponentProperties;
            Assert.IsNotNull(displayable);
            
            Model.Template.ComponentProperties component = 
                displayable.Subject as Model.Template.ComponentProperties;
            Assert.IsNotNull(component);
            
            component.MachineTime = 1;
            component.Quantity = 1;
            component.UnitCost = 1;
            
            Assert.AreEqual(component.TotalMachineTime, 1);
            Assert.AreEqual(component.MachineTime, 1);
            Assert.AreEqual(component.Quantity, 1);
            Assert.AreEqual(component.UnitCost, 1);
            
            component.Description = "test";
            component.Vendor = "test";
            component.UnitOfMeasure = "test";
            
            Assert.AreEqual(component.Description, "test");
            Assert.AreEqual(component.Vendor, "test");
            Assert.AreEqual(component.UnitOfMeasure, "test");
        }
        
        [Test]
        public void TestOneProduct()
        {
            Host.App.RegisteredClasses.Clear();
                
            Host.App.RegisteredClasses.Add(
                typeof(Model.Template.Ext.IComponentPropertiesFactory), 
                typeof(CustomComponentPropertiesFactory));
            
            Model.Template.Header header = new Model.Template.Header();
            Model.Product product = new Model.Product();
            Model.Template.Detail detail = new Model.Template.Detail(header, product);
            
            CustomComponentProperties component = detail.QuoteDetailProperties as CustomComponentProperties;
            Assert.IsNotNull(component);
            
            component.MachineTime = 1;
            component.Quantity = 1;
            component.UnitCost = 1;
            
            Assert.AreEqual(component.TotalMachineTime, 1);
            Assert.AreEqual(component.MachineTime, 1);
            Assert.AreEqual(component.Quantity, 1);
            Assert.AreEqual(component.UnitCost, 1);
            
            component.Description = "test";
            component.Vendor = "test";
            component.UnitOfMeasure = "test";
            
            Assert.AreEqual(component.Description, "test");
            Assert.AreEqual(component.Vendor, "test");
            Assert.AreEqual(component.UnitOfMeasure, "test");
        }
    }
} 