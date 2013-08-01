namespace Model.Quote
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    public class Detail : Common.Detail
    {
        private Common.SavableProperties properties = new Common.SavableProperties();

        internal Detail(Common.Header header, Product product) : base(product, string.Empty, 1)
        {
            this.QuoteHeader = header as Quote.Header;
        }

        [BrowsableAttribute(false)]
        public Header QuoteHeader { get; set; }

        [BrowsableAttribute(false)]
        public override object QuoteDetailProperties 
        {
            get { return this.properties; }
        }

        public override decimal Qty 
        {
            get 
            { 
                return this.PrivateQty(); 
            }

            set 
            {
                if (!(value == this.PrivateQty())) 
                {
                    this.SetPrivateQty(value);
                    this.SendEvents();
                }
            }
        }
        
        public void SetProperties(Common.SavableProperties props)
        {
            this.properties = props;
        }
    }
}
