using System;
using System.Reflection;
using System.ComponentModel;

using Model;

namespace SampleProperties
{
    public class DisplayableComputationProperties 
        : Model.Common.ComputationProperties, Model.Template.Ext.IComputationWrapper 
    {

        private Model.Common.GlobalOptions _Options = Model.Common.GlobalOptions.Instance;
        private readonly SampleComputationProperties _Subject;

        public DisplayableComputationProperties(SampleComputationProperties subject)
        {
            _Subject = subject;
            _Options.Changed += new EventHandler(_Options_Changed);
        }

        private void _Options_Changed(object sender, EventArgs args)
        {
            OnPropertyChanged();
        }

        [BrowsableAttribute(false)]
        public Model.Template.ComputationProperties ComputationProperties
        {
            get
            {
                return _Subject;
            }
        }

#region Shipping

        [DisplayName("Order Quantity"), 
        CategoryAttribute(Spaces.SortedSpaces9 + "Shipping")]
        public int OrderQuantity
        {
            get { return _Subject.OrderQuantity; }
            set
            {
                _Subject.OrderQuantity = value;
                base.OnPropertyChanged();
            }
        }

        [DescriptionAttribute("Cost of the Shipping Container\n(Dollars)"), 
        DisplayName("Shipping Container Cost"), 
        CategoryAttribute(Spaces.SortedSpaces9 + "Shipping")]
        public  decimal ShippingContainerCost
        {
            get { return Math.Round(_Subject.ShippingContainerCost, 
                Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); }
        }

        [DescriptionAttribute("ShippingContainerCost / FunctionalQuantity\n(Dollars)"), 
        DisplayName("Shipping Container Cost Per Order"), 
        CategoryAttribute(Spaces.SortedSpaces9 + "Shipping")]
        public decimal ShippingContainerCostPerOrder 
        {
            get { return Math.Round(_Subject.ShippingContainerCostPerOrder, 
                Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); }
        }

        [DescriptionAttribute("Shipping Cost\n(Dollars)"), 
        DisplayName("Shipping Cost"), 
        CategoryAttribute(Spaces.SortedSpaces9 + "Shipping")]
        public decimal ShippingCost
        {
            get { return Math.Round(_Subject.ShippingCost, 
                Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); }
            set
            {
                _Subject.ShippingCost = value;
                base.OnPropertyChanged();
            }
        }

        #endregion

#region Copper

        [DescriptionAttribute("Weight of Copper. \n(Pounds)"), 
        DisplayName("Copper Weight"), 
        CategoryAttribute(Spaces.SortedSpaces1 + "Copper")]
        public decimal CopperWeight 
        {
            get { return Math.Round(_Subject.CopperWeight, Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); }
        }

        [DescriptionAttribute("Percent of Scrap Copper. \n(Percent)"), 
        DisplayName("Percent Copper Scrap"), 
        CategoryAttribute(Spaces.SortedSpaces1 + "Copper")]
        public decimal PercentCopperScrap 
        {
            get { return Math.Round(_Subject.PercentCopperScrap, 
                Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); }
            set
            {
                _Subject.PercentCopperScrap = value;
                base.OnPropertyChanged();
            }
        }

        [DescriptionAttribute("CopperWeight * (PercentCopperScrap / 100)\n(Pounds)"), 
        DisplayName("Copper Scrap Weight"), 
        CategoryAttribute(Spaces.SortedSpaces1 + "Copper")]
        public decimal CopperScrapWeight
        {
            get { return Math.Round(_Subject.CopperScrapWeight, 
                Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); }
        }

        [DescriptionAttribute("Copper Price\n(Dollars Per Pounds)"), 
        DisplayName("Copper Price"), 
        CategoryAttribute(Spaces.SortedSpaces1 + "Copper")]
        public decimal CopperPrice
        {
            get { return Math.Round(_Subject.CopperPrice, Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); }
            set 
            {
                _Subject.CopperPrice = value;
                base.OnPropertyChanged();
            }
        }

        [DescriptionAttribute("CopperScrapWeight * CopperPrice. \n(Dollars Per Pounds)"), 
        DisplayName("Copper Scrap Cost"), 
        CategoryAttribute(Spaces.SortedSpaces1 + "Copper")]
        public decimal CopperScrapCost 
        {
            get { return Math.Round(_Subject.CopperScrapCost, 
                Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); }
        }

        #endregion

#region MaterialCost

        [DescriptionAttribute("Sum(UnitCost * Quantity)\n(Dollar)"), 
        DisplayName("Component Material Cost"), 
        CategoryAttribute(Spaces.SortedSpaces3 + "Material Cost")]
        public decimal ComponentMaterialCost
        {
            get { return Math.Round(_Subject.ComponentMaterialCost, 
                Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); }
        }

        [DescriptionAttribute("Sum(UnitCost * Quantity)\n(Dollar)"), 
        DisplayName("Wire Material Cost"), 
        CategoryAttribute(Spaces.SortedSpaces3 + "Material Cost")]
        public decimal WireMaterialCost
        {
            get { return Math.Round(_Subject.WireMaterialCost, 
                Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); } 
        }

        [DescriptionAttribute("ComponentMaterialCost + WireMaterialCost + ShippingContainerCostPerOrder\n(Dollar)"), 
        DisplayName("Total Material Cost"), 
        CategoryAttribute(Spaces.SortedSpaces3 + "Material Cost")]
        public decimal TotalMaterialCost
        {
            get { return Math.Round(_Subject.TotalMaterialCost, 
                Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); }
        }

        [DescriptionAttribute("Material Markup"), 
        DisplayName("Material Markup"), 
        CategoryAttribute(Spaces.SortedSpaces3 + "Material Cost")]
        public decimal MaterialMarkUp 
        {
            get { return Math.Round(_Subject.MaterialMarkUp, 
                Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); }
            set
            {
                _Subject.MaterialMarkUp = value;
                base.OnPropertyChanged();
            }
        }

        [DescriptionAttribute("TotalMaterialCost * MaterialMarkup \n(Dollars)"), 
        DisplayName("Adjusted Total Material Cost"), 
        CategoryAttribute(Spaces.SortedSpaces3 + "Material Cost")]
        public decimal AdjustedTotalMaterialCost 
        {
            get { return Math.Round(_Subject.AdjustedTotalMaterialCost, 
                Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); }
        }

        [DescriptionAttribute("(TotalMaterialCost * MaterialMarkup)" + 
            " + CopperCost + ShippingCost \n(Dollar)"), 
        DisplayName("Total Variable Material Cost"), 
        CategoryAttribute(Spaces.SortedSpaces3 + "Material Cost")]
        public decimal TotalVariableMaterialCost
        {
            get { return Math.Round(_Subject.TotalVariableMaterialCost, 
                Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); }
        }

#endregion

#region SetupTime

        [DescriptionAttribute("Number of Components \n(Count)"), 
        DisplayName("Number of Components"), 
        CategoryAttribute(Spaces.SortedSpaces5 + "Setup Time")]
        public decimal NumberOfComponents
        {
            get { return _Subject.NumberOfComponents; }
        }

        [DescriptionAttribute("Component Setup Time \n(Seconds)"), 
        DisplayName("Component Setup Time"), 
        CategoryAttribute(Spaces.SortedSpaces5 + "Setup Time")]
        public decimal ComponentSetupTime
        {
            get { return Math.Round(_Subject.ComponentSetupTime, 
                Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); }
            set 
            {
                _Subject.ComponentSetupTime = value;
                base.OnPropertyChanged();
            }
        }

        [DescriptionAttribute("Setup time to cut a particular length of wire. (Cut time) \n(Seconds Per Cut)"), 
        DisplayName("Wire Setup Time Multiplier"), 
        CategoryAttribute(Spaces.SortedSpaces5 + "Setup Time")]
        public decimal WireSetupTime
        {
            get { return Math.Round(_Subject.WireSetupTime, 
                Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); }
            set
            {
                _Subject.WireSetupTime = value;
                OnPropertyChanged();
            }
        }

        [DescriptionAttribute("Number of lines in 'CIRCUIT DATA TABLE'" + 
            " in Engineering Print. \n(Count)"), 
        DisplayName("Number of Cuts"), 
        CategoryAttribute(Spaces.SortedSpaces5 + "Setup Time")]
        public int NumberOfCuts
        {
            get { return _Subject.NumberOfCuts; }
            set
            {
                _Subject.NumberOfCuts = value;
                OnPropertyChanged();
            }
        }

        [DescriptionAttribute("NumberOfCuts * WireSetupTime \n(Seconds)"), 
        DisplayName("Total Wire Setup Time"), 
        CategoryAttribute(Spaces.SortedSpaces5 + "Setup Time")]
        public decimal TotalWireSetupTime 
        {
            get { return Math.Round(_Subject.TotalWireSetupTime, 
                Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); }
        }

        [DescriptionAttribute("(TotalWireSetupTime + ComponentSetupTime) " + 
            "/ MinimumOrderQuantity \n(Seconds)"), 
        DisplayName("Adjusted Total Setup Time"), 
        CategoryAttribute(Spaces.SortedSpaces5 + "Setup Time")]
        public decimal TotalSetupTime
        {
            get 
            {
                decimal c = (TotalWireSetupTime + ComponentSetupTime)
                    / OrderQuantity;
                return Math.Round(c, Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); 
            }
        }

#endregion

#region MachineTime

        [DescriptionAttribute("Sum(ComponentMachineTime) \n(Seconds)"), 
        DisplayName("Total Component Run Time"), 
        CategoryAttribute(Spaces.SortedSpaces6 + "Run Time")]
        public decimal TotalComponentMachineTime
        {
            get { return Math.Round(_Subject.TotalComponentMachineTime, 
                Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); }
        }

        [DescriptionAttribute("WireLengthFeet * WireMachineTime \n(Seconds)"),
        DisplayName("Total Wire Run Time"), 
        CategoryAttribute(Spaces.SortedSpaces6 + "Run Time")]
        public decimal TotalWireMachineTime 
        {
            get { return Math.Round(_Subject.TotalWireMachineTime, 
                Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); }
        }

        [DescriptionAttribute("Used with TotalWireMachineTime \n(Seconds)"), 
        DisplayName("Wire Run Time Multiplier"), 
        CategoryAttribute(Spaces.SortedSpaces6 + "Run Time")]
        public decimal WireMachineTime
        {
            get { return Math.Round(_Subject.WireMachineTime, 
                Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); }
            set
            {
                _Subject.WireMachineTime = value;
                OnPropertyChanged();
            }
        }

        [DescriptionAttribute("TotalWireMachineTime + " + 
            "TotalComponentMachineTime + TwistedPairsMachineTime \n(Seconds)"), 
        DisplayName("Total Run Time"), 
        CategoryAttribute(Spaces.SortedSpaces6 + "Run Time")]
        public decimal TotalMachineTime 
        {
            get { return Math.Round(_Subject.TotalMachineTime, 
                Model.Common.GlobalOptions.Instance.DecimalPointsToDisplay); }
        }

#endregion

    }
}
