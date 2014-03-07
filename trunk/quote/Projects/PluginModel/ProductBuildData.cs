namespace Model
{
    using System;
    using System.Linq;

    /// <summary>
    /// A mutable object used to create a product.
    /// </summary>
    public class ProductBuildData
    {
        /// <summary>
        /// The product code.
        /// </summary>
        private string code;
        
        /// <summary>
        /// The wire gage.
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
        /// True if is a wire.
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
        /// The minimum dollar.
        /// </summary>
        private decimal minimumDollar;
        
        /// <summary>
        /// The unit or measure.
        /// </summary>
        private string unitOfMeasure;
        
        /// <summary>
        /// Gets or sets the product code.
        /// </summary>
        /// <value>The product code of the product.</value>
        public string Code 
        {
            get { return this.code; }
            set { this.code = value; }
        }

        /// <summary>
        /// Gets or sets the wire gage.
        /// </summary>
        /// <value>The gage of the product.</value>
        public string Gage 
        {
            get { return this.gage; }
            set { this.gage = value; }
        }

        /// <summary>
        /// Gets or sets the copper weight per 1000 feet.
        /// </summary>
        /// <value>The copper weight per 1000 feet of the product.</value>
        public decimal CopperWeightPer1000Feet 
        {
            get { return this.copperWeightPer1000Feet; }
            set { this.copperWeightPer1000Feet = value; }
        }

        /// <summary>
        /// Gets or sets the unit cost.
        /// </summary>
        /// <value>The unit cost of the product.</value>
        public decimal UnitCost 
        {
            get { return this.unitCost; }
            set { this.unitCost = value; }
        }

        /// <summary>
        /// Gets or sets the machine time.
        /// </summary>
        /// <value>The machine time of the product.</value>
        public decimal MachineTime 
        {
            get { return this.machineTime; }
            set { this.machineTime = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the product is a wire.
        /// </summary>
        /// <value>True if the product is a wire.</value>
        public bool IsWire 
        {
            get { return this.wire; }
            set { this.wire = value; }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description of the product.</value>
        public string Description 
        {
            get { return this.description; }
            set { this.description = value; }
        }

        /// <summary>
        /// Gets or sets the lead time.
        /// </summary>
        /// <value>The lead time of the product.</value>
        public int LeadTime 
        {
            get { return this.leadTime; }
            set { this.leadTime = value; }
        }

        /// <summary>
        /// Gets or sets the vendor name.
        /// </summary>
        /// <value>The vendor of the product.</value>
        public string Vendor 
        {
            get { return this.vendor; }
            set { this.vendor = value; }
        }

        /// <summary>
        /// Gets or sets the minimum quantity.
        /// </summary>
        /// <value>The minimum quantity.</value>
        public decimal MinimumQty 
        {
            get { return this.minimumQty; }
            set { this.minimumQty = value; }
        }

        /// <summary>
        /// Gets or sets the minimum dollar amount.
        /// </summary>
        /// <value>The minimum dollar amount.</value>
        public decimal MinimumDollar 
        {
            get { return this.minimumDollar; }
            set { this.minimumDollar = value; }
        }

        /// <summary>
        /// Gets or sets the unit of measure.
        /// </summary>
        /// <value>The unit of measure for the quantity.</value>
        public string UnitOfMeasure 
        {
            get { return this.unitOfMeasure; }
            set { this.unitOfMeasure = value; }
        }
    }
}