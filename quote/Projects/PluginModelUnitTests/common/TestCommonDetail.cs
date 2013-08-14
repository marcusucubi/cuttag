namespace PluginModelUnitTests.Common
{
    using System;

    public class TestCommonDetail : Model.Common.Detail
    {
        public TestCommonDetail()
            : base(new Model.Product(), "test", 1)
        {
        }
        
        public TestCommonDetail(Model.Product product)
            : base(product, "test", 1)
        {
        }

        public TestCommonDetail(Model.Product product, string unitOfMeasure, decimal quantity)
            : base(product, unitOfMeasure, quantity)
        {
        }
        
        public override Model.Common.ISavableProperties QuoteDetailProperties 
        {
            get { return null; }
        }
        
        public void TestSetPrivateQty(decimal value)
        {
            base.SetPrivateQty(value);
        }

        public decimal TestPrivateQty()
        {
            return base.PrivateQty();
        }

        public void TestSetPrivateUnitOfMeasure(string value)
        {
            base.SetPrivateUnitOfMeasure(value);
        }

        public void TestSetProduct(Model.Product value)
        {
            base.SetProduct(value);
        }
    }
}