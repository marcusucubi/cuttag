namespace Model.Common
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Represents a component or a wire.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Design", "CA1012:AbstractTypesShouldNotHaveConstructors", Justification = "Ignore")]
    public abstract class Detail : DefaultSavableProperties
    {
        /// <summary>
        /// The quantity or length.
        /// </summary>
        private decimal quantity = 1;
        
        /// <summary>
        /// The product.
        /// </summary>
        private Model.Product product;
        
        /// <summary>
        /// A unique number for the detail item.
        /// </summary>
        private int sequenceNumber = 1;
        
        /// <summary>
        /// A unique id stored in the database.
        /// </summary>
        private Guid sourceId;
        
        /// <summary>
        /// True if it is a wire.
        /// </summary>
        private bool wire;
        
        /// <summary>
        /// Identifies the unit of measure.
        /// </summary>
        private string unitOfMeasure;

        /// <summary>
        /// Initializes a new instance of the <see cref="Detail" /> class.
        /// </summary>
        public Detail()
        {
            // Needed for grid view
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Detail" /> class.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="unitOfMeasure">The unit of measure.</param>
        /// <param name="quantity">The quantity.</param>
        protected Detail(
            Model.Product product, 
            string unitOfMeasure, 
            decimal quantity)
        {
            this.quantity = quantity;
            this.product = product;
            this.unitOfMeasure = unitOfMeasure;
            this.wire = product.IsWire;
        }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>The quantity of the product.</value>
        public virtual decimal Qty 
        {
            get 
            { 
                return this.quantity; 
            }

            set 
            {
                if (!(value == this.quantity)) 
                {
                    this.quantity = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the product code.
        /// </summary>
        /// <value>The product code for the item.</value>
        public string ProductCode 
        {
            get 
            { 
                return this.Product.Code.Trim(); 
            }
            
            set 
            {
                if (!(value == this.Product.Code)) 
                {
                    this.Product.Code = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the unit of measure.
        /// </summary>
        /// <value>The unit of measure for the quantity.</value>
        public string UnitOfMeasure 
        {
            get 
            { 
                return this.unitOfMeasure; 
            }
            
            set 
            {
                if (!(this.unitOfMeasure == value)) 
                {
                    this.unitOfMeasure = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the sequence number.
        /// </summary>
        /// <value>A unique number assigned to the detail item.</value>
        public int SequenceNumber
        {
            get 
            { 
                return this.sequenceNumber; 
            }
            
            set 
            {
                if (!(value == this.sequenceNumber)) 
                {
                    this.sequenceNumber = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the source id.
        /// </summary>
        /// <value>A unique database id of the source product.</value>
        public Guid SourceId 
        {
            get { return this.sourceId; }
            set { this.sourceId = value; }
        }

        /// <summary>
        /// Gets a value indicating whether the item is a wire.
        /// </summary>
        /// <value>Indicates whether the item is a wire or a product.</value>
        public bool IsWire 
        {
            get { return this.wire; }
        }

        /// <summary>
        /// Gets or sets the machine time.
        /// </summary>
        /// <value>The machine time of the product.</value>
        public decimal MachineTime 
        {
            get 
            { 
                return this.Product.MachineTime; 
            }
            
            set 
            {
                this.Product.MachineTime = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the unit cost.
        /// </summary>
        /// <value>The unit cost of the product.</value>
        public decimal UnitCost 
        {
            get 
            { 
                return this.Product.UnitCost; 
            }
            
            set 
            {
                this.Product.UnitCost = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the length in feet.
        /// </summary>
        /// <value>The length in feet of the wire.</value>
        [Browsable(false)]
        public decimal LengthFeet 
        {
            get { return Math.Round((decimal)this.Qty / (decimal)3.048, 4); }
        }

        /// <summary>
        /// Gets the product.
        /// </summary>
        /// <value>The product of the item.</value>
        [BrowsableAttribute(false)]
        public Model.Product Product 
        {
            get { return this.product; }
        }

        /// <summary>
        /// Gets an object that holds the properties.
        /// </summary>
        /// <value>An object that holds the properties.</value>
        public abstract ISavableProperties QuoteDetailProperties 
        {
            get;
        }

        /// <summary>
        /// Gets a string of 'Wire or 'Component'.
        /// </summary>
        /// <value>A string of wire of component.</value>
        [BrowsableAttribute(true), DisplayName("Type")]
        public string DisplayableProductClass 
        {
            get { return this.Product.IsWire ? "Wire" : "Component"; }
        }

        /// <summary>
        /// Gets the total cost.
        /// </summary>
        /// <value>The total cost of the item.</value>
        public decimal TotalCost 
        {
            get 
            {
                if (this.Product.IsWire) 
                {
                    return Math.Round(this.UnitCost * this.LengthFeet, 4);
                } 
                else 
                {
                    return Math.Round(this.UnitCost * this.Qty, 4);
                }
            }
        }

        /// <summary>
        /// Allows base classes to set the quantity.
        /// </summary>
        /// <param name="value">The value.</param>
        protected void SetPrivateQty(decimal value)
        {
            this.quantity = value;
        }

        /// <summary>
        /// Returns the quantity.
        /// </summary>
        /// <returns>The quantity of the item.</returns>
        protected decimal PrivateQty()
        {
            return this.quantity;
        }

        /// <summary>
        /// Allows base classes to set the unit of measure.
        /// </summary>
        /// <param name="value">The new value.</param>
        protected void SetPrivateUnitOfMeasure(string value)
        {
            this.unitOfMeasure = value;
        }

        /// <summary>
        /// Allows a base class to set the product.
        /// </summary>
        /// <param name="value">The new product.</param>
        protected void SetProduct(Model.Product value)
        {
            this.product = value;
            this.wire = value.IsWire;
        }
    }
}
