namespace PluginModelUnitTests.Template
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class WirePropertiesTest
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
                typeof(Model.Template.Ext.IWirePropertiesFactory), 
                typeof(CustomWirePropertiesFactory));
            
            Model.Template.Header header = new Model.Template.Header();
            
            Model.ProductBuildData data = new Model.ProductBuildData();
            data.IsWire = true;
            data.Gage = "gage";
            Model.Product product = new Model.Product(data);
            
            Model.Template.Detail detail = new Model.Template.Detail(header, product);
            
            CustomWireProperties wire = detail.QuoteDetailProperties as CustomWireProperties;
            Assert.IsNotNull(wire);
            
            wire.Quantity = 1;
            wire.UnitCost = 1;
            
            Assert.AreEqual(wire.Quantity, 1);
            Assert.AreEqual(wire.UnitCost, 1);
            
            wire.Description = "test";
            wire.UnitOfMeasure = "test";
            
            Assert.AreEqual(wire.Description, "test");
            Assert.AreEqual(wire.UnitOfMeasure, "test");
            
            wire.PoundsPer1000Feet = 1;
            wire.Quantity = 1;
            wire.UnitCost = 1;
            
            Assert.AreEqual(wire.Length, 1);
            Assert.GreaterOrEqual(wire.LengthFeet, 0.01);
            Assert.AreEqual(wire.PoundsPer1000Feet, 1);
            Assert.GreaterOrEqual(0.01, wire.TotalWeight);
            Assert.AreEqual(wire.Quantity, 1);
            Assert.AreEqual(wire.UnitCost, 1);
            
            wire.UnitOfMeasure = "test";
            wire.Description = "test";
            
            Assert.AreEqual(wire.Gage, "gage");
        }
        
        [Test]
        public void TestOneProduct2()
        {
            Host.App.RegisteredClasses.Clear();
                
            Host.App.RegisteredClasses.Add(
                typeof(Model.Template.Ext.IWirePropertiesFactory), 
                typeof(CustomWirePropertiesFactory));
            
            Model.Template.Header header = new Model.Template.Header();
            
            Model.ProductBuildData data = new Model.ProductBuildData();
            data.IsWire = true;
            Model.Product product = new Model.Product(data);
            
            Model.Template.Detail detail = new Model.Template.Detail(header, product);
            
            CustomWireProperties wire = detail.QuoteDetailProperties as CustomWireProperties;
            Assert.IsNotNull(wire);
            
            Assert.AreEqual(wire.Gage, "");
        }
    }
}