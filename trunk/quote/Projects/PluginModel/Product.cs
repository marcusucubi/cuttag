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