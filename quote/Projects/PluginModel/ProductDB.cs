namespace Model
{
    using System;
    using System.Linq;

    public sealed class ProductDB
    {
        private ProductDB()
        {
        }

        public static ProductBuildData Load(
            string code, 
            decimal unitCost, 
            string gage, 
            bool wire, 
            DB.QuoteDataBase.WireSourceRow wireRow, 
            DB.QuoteDataBase.WireComponentSourceRow partRow, 
            string unitOfMeasure, 
            decimal copperWeightPer1000Feet)
        {
            ProductBuildData result = new ProductBuildData();

            result.Code = code;
            result.UnitCost = unitCost;
            result.Gage = gage;
            result.IsWire = wire;
            result.UnitOfMeasure = unitOfMeasure;

            if (wireRow != null) 
            {
                result.CopperWeightPer1000Feet = copperWeightPer1000Feet;
            }

            if (partRow != null) 
            {
                result.Description = partRow.Description;

                if (!partRow.IsLeadTimeNull()) 
                {
                    result.LeadTime = partRow.LeadTime;
                }

                if (!partRow.IsVendorNull()) 
                {
                    result.Vendor = partRow.Vendor;
                }

                if (!partRow.IsMachineTimeNull()) 
                {
                    result.MachineTime = partRow.MachineTime;
                }

                if (!partRow.IsMinimumQtyNull()) 
                {
                    result.MinimumQty = partRow.MinimumQty;
                }

                if (!partRow.IsMinimumDollarNull()) 
                {
                    result.MinimumDollar = partRow.MinimumDollar;
                }
            }

            return result;
        }
    }
}
