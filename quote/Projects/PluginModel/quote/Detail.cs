namespace Model.Quote
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    using Model.Common;

    /// <summary>
    /// A detail object for a quote.  It can not be changed.
    /// </summary>
    public class Detail : Common.Detail
    {
        /// <summary>
        /// The properties for the quote.
        /// </summary>
        private Common.ISavableProperties properties = new QuoteSavableProperties();

        /// <summary>
        /// Initializes a new instance of the <see cref="Detail" /> class.
        /// </summary>
        /// <param name="header">The parent header.</param>
        /// <param name="product">The product.</param>
        internal Detail(Common.Header header, Product product) : base(product, string.Empty, 1)
        {
            this.QuoteHeader = header as Quote.Header;
        }

        /// <summary>
        /// Gets or sets the header for the detail.
        /// </summary>
        /// <value>The quote header.</value>
        [BrowsableAttribute(false)]
        public Header QuoteHeader { get; set; }

        /// <summary>
        /// Gets or sets the detail properties.
        /// </summary>
        /// <value>The detail properties.</value>
        [BrowsableAttribute(false)]
        public override Model.Common.ISavableProperties QuoteDetailProperties 
        {
            get { return this.properties; }
        }

        /// <summary>
        /// Gets or sets the quantity for the detail item.
        /// </summary>
        /// <value>The quantity for the detail item.</value>
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
        
        /// <summary>
        /// Sets the properties.
        /// </summary>
        /// <param name="props">The new properties.</param>
        public void SetProperties(Common.ISavableProperties props)
        {
            this.properties = props;
        }
    }
}