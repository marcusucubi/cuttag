namespace Model.Quote
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    using Model.Common;

    public class Detail : Common.Detail
    {
        private Common.ISavableProperties properties = new QuoteSavableProperties();

        internal Detail(Common.Header header, Product product) : base(product, string.Empty, 1)
        {
            this.QuoteHeader = header as Quote.Header;
        }

        [BrowsableAttribute(false)]
        public Header QuoteHeader { get; set; }

        [BrowsableAttribute(false)]
        public override Model.Common.ISavableProperties QuoteDetailProperties 
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
                    this.OnPropertyChanged();
                }
            }
        }
        
        public void SetProperties(Common.ISavableProperties props)
        {
            this.properties = props;
        }
    }
}