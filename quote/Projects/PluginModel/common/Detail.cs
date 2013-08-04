namespace Model.Common
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1012:AbstractTypesShouldNotHaveConstructors", Justification = "Ignore")]
    public abstract class Detail : SavableProperties
    {
        private decimal quantity = 1;
        
        private Model.Product product;
        
        private int sequenceNumber = 1;
        
        private Guid sourceId;
        
        private bool wire;
        
        private string unitOfMeasure;

        public Detail()
        {
            // Needed for grid view
        }

        protected Detail(Model.Product product, string unitOfMeasure, decimal quantity)
        {
            this.quantity = quantity;
            this.product = product;
            this.unitOfMeasure = unitOfMeasure;
        }

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
                    this.SendEvents();
                }
            }
        }

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
                    this.SendEvents();
                }
            }
        }

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
                    this.SendEvents();
                }
            }
        }

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
                    this.SendEvents();
                }
            }
        }

        public Guid SourceId 
        {
            get { return this.sourceId; }
            set { this.sourceId = value; }
        }

        public bool IsWire 
        {
            get 
            { 
                return this.wire; 
            }
            
            set 
            {
                if (!(value == this.wire)) 
                {
                    this.wire = value;
                }
            }
        }

        public decimal MachineTime 
        {
            get 
            { 
                return this.Product.MachineTime; 
            }
            
            set 
            {
                this.Product.MachineTime = value;
                this.SendEvents();
            }
        }

        public decimal UnitCost 
        {
            get 
            { 
                return this.Product.UnitCost; 
            }
            
            set 
            {
                this.Product.UnitCost = value;
                this.SendEvents();
            }
        }

        [Browsable(false)]
        public decimal LengthFeet 
        {
            get { return Math.Round((decimal)this.Qty / (decimal)3.048, 4); }
        }

        [BrowsableAttribute(false)]
        public Model.Product Product 
        {
            get { return this.product; }
        }

        public abstract SavableProperties QuoteDetailProperties 
        {
            get;
        }

        [BrowsableAttribute(true), DisplayName("Type")]
        public string DisplayableProductClass 
        {
            get { return this.Product.IsWire ? "Wire" : "Component"; }
        }

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

        protected void SetPrivateQty(decimal value)
        {
            this.quantity = value;
        }

        protected decimal PrivateQty()
        {
            return this.quantity;
        }

        protected void SetPrivateUnitOfMeasure(string value)
        {
            this.unitOfMeasure = value;
        }

        protected void SetProduct(Model.Product value)
        {
            this.product = value;
        }
    }
}
