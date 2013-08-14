namespace Model.Template
{
    using System;
    using System.Linq;

    public class ComponentProperties 
        : Common.ComponentProperties, Common.IHasTotalMachineTime
    {
        private Model.Template.Detail quoteDetail;
        
        public ComponentProperties(Model.Template.Detail quoteDetail) : base()
        {
            this.quoteDetail = quoteDetail;
        }

        public decimal TotalMachineTime 
        {
            get { return this.quoteDetail.MachineTime * this.quoteDetail.Qty; }
        }

        public decimal MachineTime 
        {
            get 
            { 
                return this.quoteDetail.MachineTime; 
            }
            
            set 
            {
                if (!(value == this.quoteDetail.MachineTime)) 
                {
                    this.quoteDetail.MachineTime = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public decimal Quantity 
        {
            get 
            { 
                return this.quoteDetail.Qty; 
            }
            
            set 
            {
                this.quoteDetail.Qty = value;
                this.OnPropertyChanged();
            }
        }

        public string Description 
        {
            get 
            { 
                return this.quoteDetail.Product.Description; 
            }
            
            set 
            {
                this.quoteDetail.Product.Description = value;
                this.OnPropertyChanged();
            }
        }

        public string Vendor 
        {
            get 
            { 
                return this.quoteDetail.Product.Vendor; 
            }
            
            set 
            {
                this.quoteDetail.Product.Vendor = value;
                this.OnPropertyChanged();
            }
        }

        public string UnitOfMeasure 
        {
            get { return this.quoteDetail.UnitOfMeasure; }
            set { this.quoteDetail.UnitOfMeasure = value; }
        }

        public decimal UnitCost 
        {
            get 
            { 
                return this.quoteDetail.UnitCost; 
            }
            
            set 
            {
                this.quoteDetail.UnitCost = value;
                this.OnPropertyChanged();
            }
        }
    }
}
