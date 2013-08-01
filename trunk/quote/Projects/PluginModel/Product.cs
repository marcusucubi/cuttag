namespace Model
{
    using System;
    using System.Linq;
    
    /// <summary>
    /// Represent a product
    /// </summary>
    /// <remarks></remarks>
    public class Product
    {
        private string code;
        
        private string gage;
        
        private decimal copperWeightPer1000Feet;
        
        private decimal unitCost;
        
        private decimal machineTime;
        
        private bool wire;
        
        private string description;
        
        private int leadTime;
        
        private string vendor;
        
        private decimal minimumQty;
        
        private decimal minimumDollar;

        private string unitOfMeasure;
        
        public Product(
            string code, 
            string gage, 
            decimal unitCost, 
            decimal machineTime, 
            bool wire, 
            string description, 
            int leadTime, 
            string vendor, 
            decimal minimumQty, 
            decimal minimumDollar)
        {
            this.code = code;
            this.gage = gage;
            this.unitCost = unitCost;
            this.machineTime = machineTime;
            this.wire = wire;
            this.description = description;
            this.leadTime = leadTime;
            this.vendor = vendor;
            this.minimumQty = minimumQty;
            this.minimumDollar = minimumDollar;
        }

        public Product()
        {
            this.code = string.Empty;
            this.gage = string.Empty;
        }

        public Product(ProductBuildData data)
        {
            this.code = data.Code;
            this.gage = data.Gage;
            this.copperWeightPer1000Feet = data.CopperWeightPer1000Feet;
            this.unitCost = data.UnitCost;
            this.machineTime = data.MachineTime;
            this.wire = data.IsWire;
            this.description = data.Description;
            this.leadTime = data.LeadTime;
            this.vendor = data.Vendor;
            this.minimumQty = data.MinimumQty;
            this.minimumDollar = data.MinimumDollar;
            this.unitOfMeasure = data.UnitOfMeasure;
        }

        public Product(Guid sourceId, bool wire, DB.QuoteDataBase partLookupDataSource)
        {
            string gage = string.Empty;
            decimal cost = 0;
            string unitOfMeasure = string.Empty;
            DB.QuoteDataBase._UnitOfMeasureRow unitOfMeasureRow = null;
            
            if (wire) 
            {
                DB.QuoteDataBase.WireSourceRow source = partLookupDataSource.WireSource.FindByWireSourceID(sourceId);
                var with1 = source;
                this.code = with1.PartNumber;
                this.description = with1.Description;
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
                
                // dd_ToDo 12/31/11 change sp GetWirePoundsPer1000Ft to return how wt 
                // computed and present to as wireproperty
                DB.QuoteDataBaseTableAdapters.WireSourceTableAdapter weight = new DB.QuoteDataBaseTableAdapters.WireSourceTableAdapter();
                string message = string.Empty;
                
                this.copperWeightPer1000Feet = (decimal)weight.GetWirePoundsPer1000Ft(with1.WireSourceID, ref message);
            } 
            else 
            {
                DB.QuoteDataBase.WireComponentSourceRow source = partLookupDataSource.WireComponentSource.FindByWireComponentSourceID(sourceId);
                var with2 = source;
                this.code = with2.PartNumber;
                this.description = with2.Description;
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
                    this.machineTime = with2.MachineTime;
                }
                
                if (!with2.IsLeadTimeNull()) 
                {
                    this.leadTime = with2.LeadTime;
                }
                
                if (!with2.IsVendorNull()) 
                {
                    this.vendor = with2.Vendor;
                }
                
                if (!with2.IsMinimumQtyNull()) 
                {
                    this.minimumQty = with2.MinimumQty;
                }
                
                if (!with2.IsMinimumDollarNull()) 
                {
                    this.minimumDollar = with2.MinimumDollar;
                }
            }
            
            this.gage = gage;
            this.unitCost = cost;
            this.wire = wire;
            this.unitOfMeasure = unitOfMeasure;
        }

        public string Code 
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public decimal CopperWeightPer1000Feet 
        {
            get { return this.copperWeightPer1000Feet; }
            set { this.copperWeightPer1000Feet = value; }
        }

        public string Gage 
        {
            get { return this.gage; }
        }

        public decimal UnitCost 
        {
            get { return this.unitCost; }
            set { this.unitCost = value; }
        }

        public bool IsWire 
        {
            get { return this.wire; }
        }

        public string Description 
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public int LeadTime 
        {
            get { return this.leadTime; }
            set { this.leadTime = value; }
        }

        public string Vendor 
        {
            get { return this.vendor; }
            set { this.vendor = value; }
        }

        public decimal MachineTime 
        {
            get { return this.machineTime; }
            set { this.machineTime = value; }
        }

        public decimal MinimumQty 
        {
            get { return this.minimumQty; }
            set { this.minimumQty = value; }
        }

        public decimal MinimumDollar 
        {
            get { return this.minimumDollar; }
            set { this.minimumDollar = value; }
        }
        
        public string UnitOfMeasure 
        {
            get { return this.unitOfMeasure; }
            set { this.unitOfMeasure = value; }
        }
    }
}