using System;
using Model.Template;

namespace SampleProperties
{
    public class SampleWireProperties : WireProperties
    {
        private string m_ProductCode;

        public SampleWireProperties(Detail detail)
            : base(detail)
        {
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
                base.MakeDirty();
            }
        }
    }
}
