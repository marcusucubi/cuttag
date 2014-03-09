namespace Model.Template
{
    using System;
    using System.Linq;

    /// <summary>
    /// Contains the wire properties for a detail object.
    /// </summary>
    public abstract class WireProperties : Common.WireProperties
    {
        /// <summary>
        /// The cooper weight per 1000 feet.
        /// </summary>
        private decimal copperWeightPer1000Ft;
        
        /// <summary>
        /// The detail object.
        /// </summary>
        private Model.Template.Detail withEventsFieldQuoteDetail;

        /// <summary>
        /// Initializes a new instance of the <see cref="WireProperties" /> class.
        /// </summary>
        /// <param name="detail">The detail object.</param>
        protected WireProperties(Model.Template.Detail detail)
        {
            this._QuoteDetail = detail;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="WireProperties" /> class.
        /// </summary>
        ~WireProperties()
        {
            this._QuoteDetail = null;
        }

        /// <summary>
        /// Gets the gage.
        /// </summary>
        /// <value>The gage of the wire.</value>
        public virtual string Gage 
        {
            get 
            {
                if (this._QuoteDetail.Product.Gage == null)
                {
                    return string.Empty;
                }
                
                return this._QuoteDetail.Product.Gage.Trim(); 
            }
        }

        /// <summary>
        /// Gets the wire length.
        /// </summary>
        /// <value>The wire length.</value>
        public virtual decimal Length 
        {
            get { return this._QuoteDetail.Qty; }
        }

        /// <summary>
        /// Gets or sets the wire description.
        /// </summary>
        /// <value>The wire description.</value>
        public string Description 
        {
            get 
            { 
                return this._QuoteDetail.Product.Description; 
            }
            
            set 
            {
                this._QuoteDetail.Product.Description = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the wire length in feet.
        /// </summary>
        /// <value>The wire length in feet.</value>
        public virtual decimal LengthFeet 
        {
            get { return (decimal)this._QuoteDetail.Qty / (decimal)3.048; }
        }

        /// <summary>
        /// Gets or sets the wire weight for 1000 feet.
        /// </summary>
        /// <value>The wire weight.</value>
        public virtual decimal PoundsPer1000Feet 
        {
            get 
            { 
                return this.copperWeightPer1000Ft; 
            }
            
            set 
            {
                this.copperWeightPer1000Ft = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the total wire weight.
        /// </summary>
        /// <value>The total wire weight.</value>
        public virtual decimal TotalWeight 
        {
            get { return this.PoundsPer1000Feet / 1000 * this.LengthFeet; }
        }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>The quantity.</value>
        public virtual decimal Quantity 
        {
            get 
            { 
                return this._QuoteDetail.Qty; 
            }
            
            set 
            {
                this._QuoteDetail.Qty = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the unit cost.
        /// </summary>
        /// <value>The unit cost.</value>
        public virtual decimal UnitCost 
        {
            get 
            { 
                return this._QuoteDetail.UnitCost; 
            }
            
            set 
            {
                this._QuoteDetail.UnitCost = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the unit of measure.
        /// </summary>
        /// <value>The unit of measure.</value>
        public virtual string UnitOfMeasure 
        {
            get { return this._QuoteDetail.UnitOfMeasure; }
            set { this._QuoteDetail.UnitOfMeasure = value; }
        }

        /// <summary>
        /// Gets or sets the detail.
        /// </summary>
        /// <value>The detail object.</value>
        private Model.Template.Detail _QuoteDetail 
        {
            get 
            { 
                return this.withEventsFieldQuoteDetail; 
            }
            
            set 
            {
                if (this.withEventsFieldQuoteDetail != null) 
                {
                    this.withEventsFieldQuoteDetail.PropertyChanged -= this._QuoteDetail_PropertyChanged;
                }
                
                this.withEventsFieldQuoteDetail = value;
                
                if (this.withEventsFieldQuoteDetail != null) 
                {
                    this.withEventsFieldQuoteDetail.PropertyChanged += this._QuoteDetail_PropertyChanged;
                }
            }
        }
        
        /// <summary>
        /// Called when a property is changed on the detail object.
        /// </summary>
        protected override void OnPropertyChanged()
        {
            base.OnPropertyChanged();
        }
        
        /// <summary>
        /// Called when a property is changed on the detail object.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The arguments for the event.</param>
        private void _QuoteDetail_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.OnPropertyChanged();
        }
    }
}