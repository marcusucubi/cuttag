namespace Model
{
    using System;
    using System.Linq;

    public struct ProductBuildData
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
        
        public string Code 
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public string Gage 
        {
            get { return this.gage; }
            set { this.gage = value; }
        }

        public decimal CopperWeightPer1000Feet 
        {
            get { return this.copperWeightPer1000Feet; }
            set { this.copperWeightPer1000Feet = value; }
        }

        public decimal UnitCost 
        {
            get { return this.unitCost; }
            set { this.unitCost = value; }
        }

        public decimal MachineTime 
        {
            get { return this.machineTime; }
            set { this.machineTime = value; }
        }

        public bool IsWire 
        {
            get { return this.wire; }
            set { this.wire = value; }
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

        public static bool operator ==(ProductBuildData left, ProductBuildData right)
        {
            return object.Equals(left, right);
        }

        public static bool operator !=(ProductBuildData left, ProductBuildData right)
        {
            return !object.Equals(left, right);
        }
        
        public override int GetHashCode()
        {
            return 0;
        }

        public override bool Equals(object obj)
        {
            return true;
        }
    }
}