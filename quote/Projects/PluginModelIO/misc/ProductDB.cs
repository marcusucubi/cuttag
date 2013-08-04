namespace Model.IO.Misc
{
    using System;
    using System.Linq;

    public sealed class ProductDB
    {
        private ProductDB()
        {
        }

        public static Product Load(
            string code, 
            decimal unitCost, 
            string gage, 
            bool wire, 
            DB.QuoteDataBase.WireSourceRow wireRow, 
            DB.QuoteDataBase.WireComponentSourceRow partRow, 
            string unitOfMeasure, 
            decimal copperWeightPer1000Feet)
        {
            ProductBuildData data = new ProductBuildData();

            data.Code = code;
            data.UnitCost = unitCost;
            data.Gage = gage;
            data.IsWire = wire;
            data.UnitOfMeasure = unitOfMeasure;

            if (wireRow != null) 
            {
                data.CopperWeightPer1000Feet = copperWeightPer1000Feet;
            }

            if (partRow != null) 
            {
                data.Description = partRow.Description;

                if (!partRow.IsLeadTimeNull()) 
                {
                    data.LeadTime = partRow.LeadTime;
                }

                if (!partRow.IsVendorNull()) 
                {
                    data.Vendor = partRow.Vendor;
                }

                if (!partRow.IsMachineTimeNull()) 
                {
                    data.MachineTime = partRow.MachineTime;
                }

                if (!partRow.IsMinimumQtyNull()) 
                {
                    data.MinimumQty = partRow.MinimumQty;
                }

                if (!partRow.IsMinimumDollarNull()) 
                {
                    data.MinimumDollar = partRow.MinimumDollar;
                }
            }

            return new Product(data);
        }
        
        public static Product Load(
            Guid sourceId, 
            bool wire, 
            DB.QuoteDataBase partLookupDataSource)
        {
            ProductBuildData data = new ProductBuildData();
            
            string gage = string.Empty;
            decimal cost = 0;
            string unitOfMeasure = string.Empty;
            DB.QuoteDataBase._UnitOfMeasureRow unitOfMeasureRow = null;
            
            if (wire) 
            {
                DB.QuoteDataBase.WireSourceRow source = partLookupDataSource.WireSource.FindByWireSourceID(sourceId);
                var with1 = source;
                data.Code = with1.PartNumber;
                data.Description = with1.Description;
                unitOfMeasure = "Decimeter";
                
                DB.QuoteDataBase.GageRow gageRow = null;
                gageRow = partLookupDataSource.Gage.FindByOrganizationIDGageID(10, with1.GageID);
                
                if (gage != null)
                {
                    gage = gageRow.Gage;
                }
                
                if (!source.IsQuotePriceNull()) 
                {
                    cost = with1.QuotePrice;
                }
                
                // dd_ToDo 12/31/11 
                // change sp GetWirePoundsPer1000Ft to return how wt
                // computed and present to as wireproperty
                DB.QuoteDataBaseTableAdapters.WireSourceTableAdapter weight = 
                    new DB.QuoteDataBaseTableAdapters.WireSourceTableAdapter();
                string message = string.Empty;
                
                data.CopperWeightPer1000Feet = 
                    (decimal)weight.GetWirePoundsPer1000Ft(with1.WireSourceID, ref message);
            } 
            else 
            {
                DB.QuoteDataBase.WireComponentSourceRow source = 
                    partLookupDataSource.WireComponentSource.FindByWireComponentSourceID(sourceId);
                var with2 = source;
                data.Code = with2.PartNumber;
                data.Description = with2.Description;
                unitOfMeasureRow = partLookupDataSource._UnitOfMeasure.FindByID(source.UnitOfMeasureID);
                
                if (unitOfMeasureRow != null)
                {
                    unitOfMeasure = unitOfMeasureRow.Name;
                }
                
                if (!source.IsQuotePriceNull()) 
                {
                    cost = with2.QuotePrice;
                }
                
                gage = string.Empty;
                if (!with2.IsMachineTimeNull()) 
                {
                    data.MachineTime = with2.MachineTime;
                }
                
                if (!with2.IsLeadTimeNull()) 
                {
                    data.LeadTime = with2.LeadTime;
                }
                
                if (!with2.IsVendorNull()) 
                {
                    data.Vendor = with2.Vendor;
                }
                
                if (!with2.IsMinimumQtyNull()) 
                {
                    data.MinimumQty = with2.MinimumQty;
                }
                
                if (!with2.IsMinimumDollarNull()) 
                {
                    data.MinimumDollar = with2.MinimumDollar;
                }
            }
            
            data.Gage = gage;
            data.UnitCost = cost;
            data.IsWire = wire;
            data.UnitOfMeasure = unitOfMeasure;
            
            return new Product(data);
        }
    }
}
