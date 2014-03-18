namespace Model
{
    using System;
    using System.Linq;
    
    /// <summary>
    /// Represents a product.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// The gage of the product.
        /// </summary>
        private string gage;
        
        /// <summary>
        /// True if it is a wire.
        /// </summary>
        private bool wire;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Product" /> class.
        /// </summary>
        public Product()
        {
            this.Code = string.Empty;
            this.gage = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product" /> class.
        /// </summary>
        /// <param name="data">The data used to build the product.</param>
        public Product(ProductBuildData data)
        {
            this.Code = data.Code;
            this.gage = data.Gage;
            this.CopperWeightPer1000Feet = data.CopperWeightPer1000Feet;
            this.UnitCost = data.UnitCost;
            this.MachineTime = data.MachineTime;
            this.wire = data.IsWire;
            this.Description = data.Description;
            this.LeadTime = data.LeadTime;
            this.Vendor = data.Vendor;
            this.MinimumQty = data.MinimumQty;
            this.MinimumDollar = data.MinimumDollar;
            this.UnitOfMeasure = data.UnitOfMeasure;
        }

        /// <summary>
        /// Gets or sets the product code.
        /// </summary>
        /// <value>The code of the product.</value>
        public string Code 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the copper weight per 1000 feet.
        /// </summary>
        /// <value>The copper weight per 1000 feet.</value>
        public decimal CopperWeightPer1000Feet 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the gage of the wire.
        /// </summary>
        /// <value>The gage of the wire.</value>
        public string Gage 
        {
            get { return this.gage; }
        }

        /// <summary>
        /// Gets or sets the unit cost.
        /// </summary>
        /// <value>The unit cost of the product.</value>
        public decimal UnitCost 
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets a value indicating whether the product is a wire.
        /// </summary>
        /// <value>True if the product is a wire.</value>
        public bool IsWire 
        {
            get { return this.wire; }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description of the product.</value>
        public string Description 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the lead time.
        /// </summary>
        /// <value>The lead time of the product.</value>
        public int LeadTime 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the vendor name.
        /// </summary>
        /// <value>The vendor of the product.</value>
        public string Vendor 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the machine time.
        /// </summary>
        /// <value>The machine time of the product.</value>
        public decimal MachineTime 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the minimum quantity.
        /// </summary>
        /// <value>The minimum quantity of the product.</value>
        public decimal MinimumQty 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the minimum dollar amount.
        /// </summary>
        /// <value>The minimum dollar amount of the product.</value>
        public decimal MinimumDollar 
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the unit of measure.
        /// </summary>
        /// <value>The unit of measure for the quantity of the product.</value>
        public string UnitOfMeasure 
        {
            get;
            set;
        }
    }
}