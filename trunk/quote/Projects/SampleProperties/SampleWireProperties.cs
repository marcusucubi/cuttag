using System;
using Model.Template;

namespace SampleProperties
{
    public class SampleWireProperties 
        : Model.Template.WireProperties
    {
        private string m_ProductCode;
        private Detail m_Detail;

        public SampleWireProperties(Detail detail)
            : base(detail)
        {
            m_Detail = detail;
        }

        public string Sample
        {
            get { return "Sample Wire"; }
        }

        public string ProductCode
        {
            get { return m_ProductCode; }
            set
            {
                m_ProductCode = value;
                SendEvents();
            }
        }

        public new void SendEvents()
        {
            base.SendEvents();
            m_Detail.Header.ComputationProperties.SendEvents();
        }

    }
}
