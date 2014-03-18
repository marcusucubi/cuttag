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
        /// Gets or sets the product code.
        /// </summary>
        /// <value>The product code of the product.</value>
        public string Code 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the wire gage.
        /// </summary>
        /// <value>The gage of the product.</value>
        public string Gage 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the copper weight per 1000 feet.
        /// </summary>
        /// <value>The copper weight per 1000 feet of the product.</value>
        public decimal CopperWeightPer1000Feet 
        {
            get;
            set;
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
        /// Gets or sets the machine time.
        /// </summary>
        /// <value>The machine time of the product.</value>
        public decimal MachineTime 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the product is a wire.
        /// </summary>
        /// <value>True if the product is a wire.</value>
        public bool IsWire 
        {
            get;
            set;
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
        /// Gets or sets the minimum quantity.
        /// </summary>
        /// <value>The minimum quantity.</value>
        public decimal MinimumQty 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the minimum dollar amount.
        /// </summary>
        /// <value>The minimum dollar amount.</value>
        public decimal MinimumDollar 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the unit of measure.
        /// </summary>
        /// <value>The unit of measure for the quantity.</value>
        public string UnitOfMeasure 
        {
            get;
            set;
        }
    }
}