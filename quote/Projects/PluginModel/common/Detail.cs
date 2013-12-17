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

        public Detail()
        {
            // Needed for grid view
        }

        /// <summary>
        /// Creates a detail.
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
        public Guid SourceId 
        {
            get { return this.sourceId; }
            set { this.sourceId = value; }
        }

        /// <summary>
        /// Returns true if a wire.
        /// </summary>
        public bool IsWire 
        {
            get { return this.wire; }
        }

        /// <summary>
        /// Gets or sets the machine time.
        /// </summary>
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
        /// Returns the length in feet.
        /// </summary>
        [Browsable(false)]
        public decimal LengthFeet 
        {
            get { return Math.Round((decimal)this.Qty / (decimal)3.048, 4); }
        }

        /// <summary>
        /// Returns the product.
        /// </summary>
        [BrowsableAttribute(false)]
        public Model.Product Product 
        {
            get { return this.product; }
        }

        /// <summary>
        /// Returns an object that holds the properties.
        /// </summary>
        public abstract ISavableProperties QuoteDetailProperties 
        {
            get;
        }

        /// <summary>
        /// Returns 'Wire or 'Component'.
        /// </summary>
        [BrowsableAttribute(true), DisplayName("Type")]
        public string DisplayableProductClass 
        {
            get { return this.Product.IsWire ? "Wire" : "Component"; }
        }

        /// <summary>
        /// Calculates the total cost.
        /// </summary>
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
        /// <returns>Returns the quantity.</returns>
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
