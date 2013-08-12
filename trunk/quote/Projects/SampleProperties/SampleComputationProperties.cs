using System;
using System.ComponentModel;

using Model;
using Model.Template;

using PluginHost;

namespace SampleProperties
{
    public class SampleComputationProperties : Model.Template.ComputationProperties 
    {
        private Header _Header;
        private string _ShippingBox = "NoBox";

        private decimal _CopperPrice = new decimal(4.09);
        private decimal _PercentCopperScrap = 10;

        private decimal _ShippingCost;
        private int _OrderQuantity;

        private decimal _MaterialMarkup;

        private decimal _ComponentSetupTime;
        private decimal _WireSetupTime;
        private decimal _WireMachineTime;
        private int _NumberOfCuts;

        public SampleComputationProperties(Header header)
        {
            _Header = header;
        }

#region Shipping

        public int OrderQuantity 
        {
            get { return _OrderQuantity; }
            set
            {
                _OrderQuantity = value;
                SendEvents();
            }
        }

        public string ShippingContainer
        {
            get { return _ShippingBox; }
            set
            {
                _ShippingBox = value;
                SendEvents();
            }
        }

        public decimal ShippingContainerCost 
        {
            get 
            {
                if (_ShippingBox == null) 
                {
                    return 0;
                }
                
                return 1;
            }
        }

        public decimal ShippingContainerCostPerOrder 
        {
            get 
            {
                if (this.OrderQuantity == 0)
                {
                    return ShippingContainerCost;
                }

                return ShippingContainerCost / this.OrderQuantity;
            }
        }

        public decimal ShippingCost
        {
            get { return _ShippingCost; }
            set
            {
                _ShippingCost = value;
                SendEvents();
            }
        }

        #endregion

#region Copper

        public decimal CopperWeight
        {
            get { return Weights.CalcWeight(_Header); }
        }

        public decimal PercentCopperScrap
        {
            get { return _PercentCopperScrap; }
            set
            {
                _PercentCopperScrap = value;
                SendEvents();
            }
        }

        public decimal CopperScrapWeight
        {
            get 
            {
                decimal percent = (_PercentCopperScrap / 100);
                return CopperWeight * percent;
            }
        }

        public decimal CopperPrice
        {
            get { return _CopperPrice; }
            set
            {
                _CopperPrice = value;
                SendEvents();
            }
        }

        public decimal CopperScrapCost
        {
            get { return CopperScrapWeight * CopperPrice; }
        }

        #endregion

#region MaterialCost

    public decimal ComponentMaterialCost
    {
        get { return SumCost(false); }
    }

    public decimal WireMaterialCost
    {
        get { return SumCost(true); }
    }

    public decimal TotalMaterialCost
    {
        //get { return ComponentMaterialCost + WireMaterialCost + ShippingContainerCostPerOrder; }
        get { return ShippingContainerCostPerOrder; }
    }

    public decimal MaterialMarkUp 
    {
        get { return _MaterialMarkup; }
        set
        {
            _MaterialMarkup = value;
            SendEvents();
        }
    }

    public decimal AdjustedTotalMaterialCost 
    {
        get { return TotalMaterialCost * _MaterialMarkup; }
    }

    public decimal TotalVariableMaterialCost
    {
        get 
        { 
            return 
                (TotalMaterialCost * _MaterialMarkup) + 
                 CopperScrapCost + 
                 ShippingCost;
        }
    }

    #endregion

#region Methods

    private decimal SumCost(bool IsWire)
    {
        decimal result = 0;
        foreach(Model.Template.Detail detail in _Header.Details)
        {
            if (detail.Product.IsWire == IsWire)
            {
                result += detail.TotalCost;
            }
        }

        return result;
    }

    private decimal SumTime(bool IsWire)
    {
        decimal result = 0;
        foreach(Model.Template.Detail detail in _Header.Details)
        {
            if (detail.Product.IsWire == IsWire)
            {
                result += 0; // detail.QuoteDetailProperties.TotalMachineTime;
            }
        }

        return result;
    }

    private decimal SumQty(bool IsWire)
    {
        decimal result = 0;
        foreach(Model.Template.Detail detail in _Header.Details)
        {
            if (detail.Product.IsWire == IsWire)
            {
                result += detail.Qty;
            }
        }

        return result;
    }

    private decimal Count(bool IsWire)
    {
        int result = 0;
        foreach(Model.Template.Detail detail in _Header.Details)
        {
            if (detail.Product.IsWire == IsWire)
            {
                result += 1;
            }
        }

        return result;
    }

    #endregion

#region SetupTime

        public decimal NumberOfComponents
        {
            get { return Count(false); }
        }

        public decimal ComponentSetupTime
        {
            get { return _ComponentSetupTime; }
            set
            {
                _ComponentSetupTime = value;
                SendEvents();
            }
        }

        public decimal WireSetupTime
        {
            get { return _WireSetupTime; }
            set
            {
                _WireSetupTime = value;
                SendEvents();
            }
        }

        public int NumberOfCuts
        {
            get { return _NumberOfCuts; }
            set
            {
                _NumberOfCuts = value;
                SendEvents();
            }
        }

        public decimal TotalWireSetupTime
        {
            get { return NumberOfCuts * WireSetupTime; }
        }

        public decimal TotalSetupTime
        {
            get
            {
                if (this.OrderQuantity == 0) 
                {
                    return 0;
                }
                return 
                    (TotalWireSetupTime + ComponentSetupTime) 
                    / this.OrderQuantity;
            }
        }

#endregion

#region Wires

        public decimal NumberOfWires
        {
            get { return Count(true); }
        }

        public decimal WireLength
        {
            get { return SumQty(true); }
        }

        public decimal WireLengthFeet
        {
            get { return SumQty(true) / (decimal)3.048; }
        }

#endregion

#region MachineTime

        public decimal TotalComponentMachineTime
        {
            get { return SumTime(false); }
        }

        public decimal TotalWireMachineTime
        {
            get { return WireLengthFeet * WireMachineTime; }
        }

        public decimal WireMachineTime 
        {
            get { return _WireMachineTime; }
            set 
            {
                _WireMachineTime = value;
                SendEvents();
            }
        }

        public decimal TotalMachineTime 
        {
            get 
            {
                return TotalComponentMachineTime + TotalWireMachineTime;
            }
        }

#endregion


    }
}
