namespace Model.Template
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    public class DisplayableComponentProperties 
        : Common.ComponentProperties, Common.IHasTotalMachineTime
    {
        private ComponentProperties subject;
        
        public DisplayableComponentProperties(ComponentProperties subject)
        {
            this.subject = subject;
            this.Subject = subject;
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
                this.SendEvents();
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
                this.SendEvents();
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
                this.SendEvents();
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
                this.SendEvents();
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
                this.SendEvents();
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
                this.SendEvents();
            }
        }
    }
}
