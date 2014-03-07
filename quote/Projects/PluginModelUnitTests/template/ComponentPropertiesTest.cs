using PluginModelUnitTests.Template.Ext;
namespace PluginModelUnitTests.Template
{
    using System;
    using Model.Common;
    using NUnit.Framework;

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
            
            CustomComponentProperties component = 
                detail.QuoteDetailProperties as CustomComponentProperties;
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
        public void TestChangeEvent()
        {
            Host.App.RegisteredClasses.Clear();
                
            Host.App.RegisteredClasses.Add(
                typeof(Model.Template.Ext.IWirePropertiesFactory), 
                typeof(CustomWirePropertiesFactory));
            
            Host.App.RegisteredClasses.Add(
                typeof(Model.Template.Ext.IComputationPropertiesFactory), 
                typeof(CustomComputationPropertiesFactory));
            
            Model.Template.Header header = new Model.Template.Header();
            
            CustomComputationProperties computation = 
                header.ComputationProperties as CustomComputationProperties;
            
            Model.ProductBuildData data = new Model.ProductBuildData();
            data.IsWire = false;
            Model.Product product = new Model.Product(data);
            
            Model.Template.Detail detail = new Model.Template.Detail(header, product);
            header.Details.Add(detail);
            
            Model.Template.DisplayableComponentProperties wire = 
                detail.QuoteDetailProperties as Model.Template.DisplayableComponentProperties;
            Assert.IsNotNull(wire);
            
            wire.Quantity = 1;
            wire.UnitCost = 1;
            
            decimal cost = computation.ComponentMaterialCost;
            Assert.AreEqual(cost, 1);
            
            bool flag = false;
            computation.Dirty += delegate { flag = true; };
            
            Assert.IsFalse(flag);
            
            wire.Quantity = 2;
            wire.UnitCost = 1;
            
            cost = computation.ComponentMaterialCost;
            Assert.AreEqual(cost, 2);
            
            Assert.IsTrue(flag);
        }
    }
} 