namespace Model.Template
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    public class DisplayableComponentProperties 
        : Common.ComponentProperties, Common.IHasTotalMachineTime, Model.Template.IHasSubject
    {
        private ComponentProperties subject;
        
        public DisplayableComponentProperties(ComponentProperties subject)
        {
            this.subject = subject;
        }

        [Browsable(false)]
        public Model.Common.ISavableProperties Subject 
        {
            get { return this.subject; }
        }
        
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
