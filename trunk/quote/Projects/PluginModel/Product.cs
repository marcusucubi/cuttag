namespace Model
{
    using System;
    using System.Linq;
    
    /// <summary>
    /// Represents a product
    /// </summary>
    /// <remarks></remarks>
    public class Product
    {
        /// <summary>
        /// The product code
        /// </summary>
        private string code;
        
        /// <summary>
        /// The gage.
        /// </summary>
        private string gage;
        
        /// <summary>
        /// The copper weight per 1000 feet.
        /// </summary>
        private decimal copperWeightPer1000Feet;
        
        /// <summary>
        /// The unit cost.
        /// </summary>
        private decimal unitCost;
        
        /// <summary>
        /// The machine time.
        /// </summary>
        private decimal machineTime;
        
        /// <summary>
        /// True if it is a wire.
        /// </summary>
        private bool wire;
        
        /// <summary>
        /// The description.
        /// </summary>
        private string description;
        
        /// <summary>
        /// The lead time.
        /// </summary>
        private int leadTime;
        
        /// <summary>
        /// The vendor.
        /// </summary>
        private string vendor;
        
        /// <summary>
        /// The minimum quantity.
        /// </summary>
        private decimal minimumQty;
        
        /// <summary>
        /// The minimum dollar amount.
        /// </summary>
        private decimal minimumDollar;

        /// <summary>
        /// The unit of measure.
        /// </summary>
        private string unitOfMeasure;
        
        /// <summary>
        /// The default constructor.
        /// </summary>
        public Product()
        {
            this.code = string.Empty;
            this.gage = string.Empty;
        }

        /// <summary>
        /// A constructor with data.
        /// </summary>
        /// <param name="data">The data used to build the product.</param>
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

        /// <summary>
        /// The product code.
        /// </summary>
        public string Code 
        {
            get { return this.code; }
            set { this.code = value; }
        }

        /// <summary>
        /// The copper weight per 1000 feet.
        /// </summary>
        public decimal CopperWeightPer1000Feet 
        {
            get { return this.copperWeightPer1000Feet; }
            set { this.copperWeightPer1000Feet = value; }
        }

        /// <summary>
        /// Gets the gage of the wire.
        /// </summary>
        public string Gage 
        {
            get { return this.gage; }
        }

        /// <summary>
        /// Gets or sets the unit cost.
        /// </summary>
        public decimal UnitCost 
        {
            get { return this.unitCost; }
            set { this.unitCost = value; }
        }

        /// <summary>
        /// Gets the value of the wire.
        /// </summary>
        public bool IsWire 
        {
            get { return this.wire; }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description 
        {
            get { return this.description; }
            set { this.description = value; }
        }

        /// <summary>
        /// Gets or sets the lead time.
        /// </summary>
        public int LeadTime 
        {
            get { return this.leadTime; }
            set { this.leadTime = value; }
        }

        /// <summary>
        /// Gets or sets the vendor name.
        /// </summary>
        public string Vendor 
        {
            get { return this.vendor; }
            set { this.vendor = value; }
        }

        /// <summary>
        /// Gets or sets the machine time.
        /// </summary>
        public decimal MachineTime 
        {
            get { return this.machineTime; }
            set { this.machineTime = value; }
        }

        /// <summary>
        /// Gets or sets the minimum quantity.
        /// </summary>
        public decimal MinimumQty 
        {
            get { return this.minimumQty; }
            set { this.minimumQty = value; }
        }

        /// <summary>
        /// Gets or sets the minimum dollar amount.
        /// </summary>
        public decimal MinimumDollar 
        {
            get { return this.minimumDollar; }
            set { this.minimumDollar = value; }
        }
        
        /// <summary>
        /// Gets or sets the unit of measure.
        /// </summary>
        public string UnitOfMeasure 
        {
            get { return this.unitOfMeasure; }
            set { this.unitOfMeasure = value; }
        }
    }
}