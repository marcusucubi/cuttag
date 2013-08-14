namespace PluginModelUnitTests.Common
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class DetailTest
    {
        [Test]
        public void TestMethod()
        {
            Model.ProductBuildData data = new Model.ProductBuildData();
            data.IsWire = false;
            Model.Product wire = new Model.Product(data);
            TestCommonDetail detail = new TestCommonDetail(wire);

            detail.ProductCode = "test";
            Assert.AreEqual("test", detail.ProductCode);
            
            Assert.AreEqual("Component", detail.DisplayableProductClass);
            
            detail.SequenceNumber = 1;
            Assert.AreEqual(1, detail.SequenceNumber);
            
            detail.SourceId = Guid.Empty;
            Assert.AreEqual(Guid.Empty, detail.SourceId);
            
            detail.UnitOfMeasure = "test";
            detail.TestSetPrivateUnitOfMeasure("test");
            Assert.AreEqual("test", detail.UnitOfMeasure);
            
            Assert.AreEqual(false, detail.IsWire);
            
            detail.TestSetPrivateQty(1);
            detail.Qty = 1;
            Assert.AreEqual(1, detail.Qty);
            Assert.AreEqual(1, detail.TestPrivateQty());
            detail.Qty = 2;
            Assert.AreEqual(2, detail.Qty);
            
            detail.MachineTime = 1;
            Assert.AreEqual(1, detail.MachineTime);
            
            detail.UnitCost = 1;
            Assert.AreEqual(1, detail.UnitCost);
            
            detail.Qty = 1;
            Assert.AreEqual(0.3281, detail.LengthFeet);
            Assert.AreEqual(1, detail.TotalCost);
                
            Model.Product product = new Model.Product();
            detail.TestSetProduct(product);
            Assert.AreEqual(detail.Product, product);
        }
        
        [Test]
        public void TestWire()
        {
            TestCommonDetail detail2 = new TestCommonDetail();
            
            Model.ProductBuildData data = new Model.ProductBuildData();
            data.IsWire = true;
            Model.Product wire = new Model.Product(data);
            TestCommonDetail detail = new TestCommonDetail(wire);

            detail.ProductCode = "test";
            Assert.AreEqual("test", detail.ProductCode);
            Assert.AreEqual("Wire", detail.DisplayableProductClass);
            
            detail.SequenceNumber = 1;
            Assert.AreEqual(1, detail.SequenceNumber);
            
            detail.SourceId = Guid.Empty;
            Assert.AreEqual(Guid.Empty, detail.SourceId);
            
            detail.UnitOfMeasure = "test2";
            Assert.AreEqual("test2", detail.UnitOfMeasure);
            
            Assert.AreEqual(true, detail.IsWire);
            
            detail.TestSetPrivateQty(1);
            detail.Qty = 1;
            Assert.AreEqual(1, detail.Qty);
            Assert.AreEqual(1, detail.TestPrivateQty());
            detail.Qty = 2;
            Assert.AreEqual(2, detail.Qty);
            
            detail.MachineTime = 1;
            Assert.AreEqual(1, detail.MachineTime);
            
            detail.UnitCost = 1;
            Assert.AreEqual(1, detail.UnitCost);
            
            detail.Qty = 1;
            Assert.AreEqual(0.3281, detail.LengthFeet);
            Assert.AreEqual(0.3281, detail.TotalCost);
                
            Model.Product product = new Model.Product();
            detail.TestSetProduct(product);
            Assert.AreEqual(detail.Product, product);
            
            detail.SequenceNumber = 10;
            Assert.AreEqual(10, detail.SequenceNumber);
        }
    }
}