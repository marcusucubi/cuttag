using System;

using NUnit.Framework;

namespace PluginModelUnitTests
{
    [TestFixture]
    public class ProductTest
    {
        [Test]
        public void TestBuildData()
        {
            Model.ProductBuildData data = new Model.ProductBuildData();
            
            data.IsWire = true;
            data.Code = "code";
            data.UnitOfMeasure = "unitOfMeasure";
            data.Gage = "gage";
            data.Description = "description";
            data.Vendor = "vendor";
            data.LeadTime = 1;
            data.MinimumQty = 2;
            data.MinimumDollar = 3;
            data.CopperWeightPer1000Feet = 4;
            data.UnitCost = 5;
            data.MachineTime = 6;
            
            Model.Product product = new Model.Product(data);
            
            Assert.AreEqual(data.IsWire, product.IsWire);
            Assert.AreEqual(data.Code, product.Code);
            Assert.AreEqual(data.UnitOfMeasure, product.UnitOfMeasure);
            Assert.AreEqual(data.Gage, product.Gage);
            Assert.AreEqual(data.Description, product.Description);
            Assert.AreEqual(data.Vendor, product.Vendor);
            Assert.AreEqual(data.LeadTime, product.LeadTime);
            Assert.AreEqual(data.MinimumQty, product.MinimumQty);
            Assert.AreEqual(data.MinimumDollar, product.MinimumDollar);
            Assert.AreEqual(data.CopperWeightPer1000Feet, product.CopperWeightPer1000Feet);
            Assert.AreEqual(data.UnitCost, product.UnitCost);
            Assert.AreEqual(data.MachineTime, product.MachineTime);
        }
        
        [Test]
        public void TestSetMethods()
        {
            
            Model.ProductBuildData data = new Model.ProductBuildData();
            data.IsWire = true;
            data.Code = "code";
            data.UnitOfMeasure = "unitOfMeasure";
            data.Gage = "gage";
            data.Description = "description";
            data.Vendor = "vendor";
            data.LeadTime = 1;
            data.MinimumQty = 2;
            data.MinimumDollar = 3;
            data.CopperWeightPer1000Feet = 4;
            data.UnitCost = 5;
            data.MachineTime = 6;
            
            Model.Product product = new Model.Product();
            product.Code = data.Code;
            product.UnitOfMeasure = data.UnitOfMeasure;
            product.Description = data.Description;
            product.Vendor = data.Vendor;
            product.LeadTime = data.LeadTime;
            product.MinimumQty = data.MinimumQty;
            product.MinimumDollar = data.MinimumDollar;
            product.CopperWeightPer1000Feet = data.CopperWeightPer1000Feet;
            product.UnitCost = data.UnitCost;
            product.MachineTime = data.MachineTime;
            
            Assert.AreEqual(data.Code, product.Code);
            Assert.AreEqual(data.UnitOfMeasure, product.UnitOfMeasure);
            Assert.AreEqual(data.Description, product.Description);
            Assert.AreEqual(data.Vendor, product.Vendor);
            Assert.AreEqual(data.LeadTime, product.LeadTime);
            Assert.AreEqual(data.MinimumQty, product.MinimumQty);
            Assert.AreEqual(data.MinimumDollar, product.MinimumDollar);
            Assert.AreEqual(data.CopperWeightPer1000Feet, product.CopperWeightPer1000Feet);
            Assert.AreEqual(data.UnitCost, product.UnitCost);
            Assert.AreEqual(data.MachineTime, product.MachineTime);
        }
    }
}
