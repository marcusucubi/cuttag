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
        private Decimal _ShippingContainerCost;
        private Decimal _ShippingCost;
        private string _ShippingBox = "NoBox";

        public SampleComputationProperties(Header header)
            : base(header)
        {
            _Header = header;
       }

        [DescriptionAttribute("Description of the Shipping Container"), 
        DisplayName("Shipping Container"), 
        CategoryAttribute("Shipping"), 
        TypeConverter(typeof(ShippingList))]
        public string ShippingContainer
        {
            get { return _ShippingBox; }
            set 
            {
                _ShippingBox = value;
                SendEvents();
            }
        }

    }
}
