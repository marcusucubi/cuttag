using Model.Template;
namespace PluginModelUnitTests
{
    using System;

    public class CustomComputationProperties : Model.Template.ComputationProperties
    {
        private Header _Header;
        
        public CustomComputationProperties(Header header)
        {
            _Header = header;
            
            header.Details.Dirty += delegate { base.OnPropertyChanged(); };
        }
        
        public decimal ComponentMaterialCost
        {
            get
            {
                return SumCost(false);
            }
        }
        
        public decimal SumCost(bool IsWire)
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
    }
}
