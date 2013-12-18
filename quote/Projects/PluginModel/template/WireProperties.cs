namespace Model.Template
{
    using System;
    using System.Linq;

    public abstract class WireProperties : Common.WireProperties
    {
        private decimal copperWeightPer1000Ft;
        
        private Model.Template.Detail withEventsFieldQuoteDetail;
        
        protected WireProperties(Model.Template.Detail detail)
        {
            this._QuoteDetail = detail;
        }

        ~WireProperties()
        {
            this._QuoteDetail = null;
        }

        public virtual string Gage 
        {
            get 
            {
                if (this._QuoteDetail.Product.Gage == null)
                {
                    return "";
                }
                
                return this._QuoteDetail.Product.Gage.Trim(); 
            }
        }

        public virtual decimal Length 
        {
            get { return this._QuoteDetail.Qty; }
        }

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

        public virtual decimal LengthFeet 
        {
            get { return (decimal)this._QuoteDetail.Qty / (decimal)3.048; }
        }

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

        public virtual decimal TotalWeight 
        {
            get { return this.PoundsPer1000Feet / 1000 * this.LengthFeet; }
        }

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

        public virtual string UnitOfMeasure 
        {
            get { return this._QuoteDetail.UnitOfMeasure; }
            set { this._QuoteDetail.UnitOfMeasure = value; }
        }

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
        
        protected override void OnPropertyChanged()
        {
            base.OnPropertyChanged();
        }
        
        private void _QuoteDetail_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.OnPropertyChanged();
        }
    }
}