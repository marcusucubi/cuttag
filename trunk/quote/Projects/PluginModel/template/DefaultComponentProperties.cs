namespace Model.Template
{
    using System;
    using System.Linq;

    public sealed class DefaultComponentProperties : Template.ComponentProperties
    {
        private Template.Detail quoteDetail;
        
        public DefaultComponentProperties(Template.Detail quoteDetail) : base(quoteDetail)
        {
            this.quoteDetail = quoteDetail;
        }

        public override void SendEvents()
        {
            base.SendEvents();
            this.quoteDetail.Header.ComputationProperties.SendEvents();
        }
    }
}
