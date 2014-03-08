namespace Model.Template
{
    using System;
    using System.Linq;

    /// <summary>
    /// The component properties for the template.
    /// </summary>
    public class ComponentProperties 
        : Common.ComponentProperties, Common.IHasTotalMachineTime
    {
        /// <summary>
        /// The template detail object.
        /// </summary>
        private Model.Template.Detail quoteDetail;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentProperties" /> class.
        /// </summary>
        /// <param name="quoteDetail">The quote detail.</param>
        public ComponentProperties(Model.Template.Detail quoteDetail) : base()
        {
            this.quoteDetail = quoteDetail;
        }

        /// <summary>
        /// Gets the total machine time.
        /// </summary>
        /// <value>The total machine time.</value>
        public decimal TotalMachineTime 
        {
            get { return this.quoteDetail.MachineTime * this.quoteDetail.Qty; }
        }

        /// <summary>
        /// Gets or sets the machine time.
        /// </summary>
        /// <value>The machine time.</value>
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

        /// <summary>
        /// Gets or sets the quantity for the detail.
        /// </summary>
        /// <value>The quantity for the detail.</value>
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

        /// <summary>
        /// Gets or sets the description for the component.
        /// </summary>
        /// <value>The description for the component.</value>
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

        /// <summary>
        /// Gets or sets the vendor of the product.
        /// </summary>
        /// <value>The name of the vendor.</value>
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

        /// <summary>
        /// Gets or sets the unit of measure.
        /// </summary>
        /// <value>The unit of measure.</value>
        public string UnitOfMeasure 
        {
            get { return this.quoteDetail.UnitOfMeasure; }
            set { this.quoteDetail.UnitOfMeasure = value; }
        }

        /// <summary>
        /// Gets or sets the unit cost.
        /// </summary>
        /// <value>The unit of cost.</value>
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
