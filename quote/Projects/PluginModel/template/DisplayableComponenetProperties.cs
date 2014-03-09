namespace Model.Template
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// A decorator class for the component properties 
    /// that adds rounding. This class should only be 
    /// concerned with display issus.
    /// </summary>
    public class DisplayableComponentProperties
        : Common.ComponentProperties, Common.IHasTotalMachineTime, Model.Template.IHasSubject
    {
        /// <summary>
        /// The object we are decorating.
        /// </summary>
        private ComponentProperties subject;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayableComponentProperties" /> class.
        /// </summary>
        /// <param name="subject">The component properties.</param>
        public DisplayableComponentProperties(ComponentProperties subject)
        {
            this.subject = subject;
        }

        /// <summary>
        /// Gets the object we are decorating.
        /// </summary>
        /// <value>The subject.</value>
        [Browsable(false)]
        public Model.Common.ISavableProperties Subject 
        {
            get { return this.subject; }
        }
        
        /// <summary>
        /// Gets the total machine time.
        /// </summary>
        /// <value>The total machine time.</value>
        [DisplayName("Total Machine Time"), Browsable(false)]
        public decimal TotalMachineTime 
        {
            get 
            { 
                return Math.Round(
                    this.subject.TotalMachineTime, 
                    Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay);
            }
        }

        /// <summary>
        /// Gets or sets the machine time.
        /// </summary>
        /// <value>The machine time.</value>
        [DisplayName("Machine Time"), CategoryAttribute("Detail")]
        public decimal MachineTime 
        {
            get 
            { 
                return Math.Round(
                    this.subject.MachineTime, 
                    Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay);
            }
            
            set 
            {
                this.subject.MachineTime = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>The quantity.</value>
        [CategoryAttribute("Detail")]
        public decimal Quantity 
        {
            get 
            { 
                return Math.Round(
                    this.subject.Quantity, 
                    Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay);
            }
            
            set 
            {
                this.subject.Quantity = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [DisplayName("Description"), CategoryAttribute("Detail")]
        public string Description 
        {
            get 
            { 
                return this.subject.Description; 
            }
            
            set 
            {
                this.subject.Description = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the vendor.
        /// </summary>
        /// <value>The vendor.</value>
        [DisplayName("Vendor"), CategoryAttribute("Vendor")]
        public string Vendor 
        {
            get 
            { 
                return this.subject.Vendor; 
            }
            
            set 
            {
                this.subject.Vendor = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the unit of measure.
        /// </summary>
        /// <value>The unit of measure.</value>
        [DisplayName("Unit Of Measure"), TypeConverter(typeof(UnitOfMeasureConverter)), CategoryAttribute("Detail")]
        public string UnitOfMeasure 
        {
            get 
            { 
                return this.subject.UnitOfMeasure; 
            }
            
            set 
            {
                this.subject.UnitOfMeasure = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the unit cost.
        /// </summary>
        /// <value>The unit of cost.</value>
        [DisplayName("Unit Cost"), CategoryAttribute("Detail")]
        public decimal UnitCost 
        {
            get 
            { 
                return Math.Round(
                    this.subject.UnitCost, 
                    Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay);
            }
            
            set 
            {
                this.subject.UnitCost = value;
                this.OnPropertyChanged();
            }
        }
    }
}
