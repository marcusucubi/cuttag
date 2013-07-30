using System.ComponentModel;
using System.Reflection;
using System;

using Model;
using Model.Quote;

namespace SampleProperties
{
    class DisplayableWireProperties : Model.Common.WireProperties
    {
        private Model.Common.GlobalOptions _Options = Model.Common.GlobalOptions.Instance;
        private readonly SampleWireProperties _Subject;

        public DisplayableWireProperties(SampleWireProperties subject)
        {
            _Subject = subject;
            
        }

        [CategoryAttribute("Detail")]
        public string Gage
        {
            get { return _Subject.Gage; }
        }

        [DisplayName("Description"), 
        CategoryAttribute("Detail")]
        public string Description
        {
            get { return _Subject.Description; }
            set
            {
                _Subject.Description = value;
                SendEvents();
            }
        }

        [DescriptionAttribute("Length in Decimeters"), 
        DisplayName("Length in Decimeters"), 
        CategoryAttribute("Detail")]
        public decimal Length
        {
            get { return Math.Round(_Subject.Length, Model.Common.GlobalOptions.DecimalPointsToDisplay); }
        }
    
        [DescriptionAttribute("Length / 3.048"),
        DisplayName("Length in Feet"),
        CategoryAttribute("Detail")]
        public decimal LengthFeet
        {
            get { return Math.Round(_Subject.LengthFeet, Model.Common.GlobalOptions.DecimalPointsToDisplay); }
        }

        [DescriptionAttribute("Pounds per 1000 feet"), 
        CategoryAttribute("Copper Weight")]
        public decimal PoundsPer1000Feet
        {
            get { return Math.Round(_Subject.PoundsPer1000Feet, Model.Common.GlobalOptions.DecimalPointsToDisplay); }
            set
            {
                _Subject.PoundsPer1000Feet = value;
                SendEvents();
            }
        }

        [DescriptionAttribute("WeightPerFoot * Length \r(Pounds)"), 
        CategoryAttribute("Copper Weight")]
        public decimal TotalWeight 
        {
            get { return Math.Round(_Subject.TotalWeight, Model.Common.GlobalOptions.DecimalPointsToDisplay); }
        }

        [DescriptionAttribute("Number of Decimeters"), 
        DisplayName("Quantity"), 
        CategoryAttribute("Detail")]
        public decimal Quantity 
        {
            get { return Math.Round(_Subject.Quantity, Model.Common.GlobalOptions.DecimalPointsToDisplay); }
            set
            {
                _Subject.Quantity = value;
                SendEvents();
            }
        }

        [DisplayName("Unit Cost"), 
        DescriptionAttribute("Dollars per Decimeter"), 
        CategoryAttribute("Detail")]
        public decimal UnitCost 
        {
            get { return Math.Round(_Subject.UnitCost, Model.Common.GlobalOptions.DecimalPointsToDisplay); }
            set
            {
                _Subject.UnitCost = value;
                SendEvents();
            }
        }

        [DisplayName("Unit Of Measure"), 
        TypeConverter(typeof(UomConverter)), 
        CategoryAttribute("Detail")]
        public string UnitOfMeasure 
        {
            get { return _Subject.UnitOfMeasure; }
            set
            {
                _Subject.UnitOfMeasure = value;
                SendEvents();
            }
        }

    }
}
