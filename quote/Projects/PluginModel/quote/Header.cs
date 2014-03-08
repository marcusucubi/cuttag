namespace Model.Quote
{
    using System;
    using System.Linq;

    /// <summary>
    /// The header for a quote object.
    /// </summary>
    public class Header : Common.Header
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Header" /> class.
        /// </summary>
        public Header() 
            : this(0, string.Empty, string.Empty, string.Empty, System.DateTime.Now, System.DateTime.Now, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Header" /> class.
        /// </summary>
        /// <param name="id">The id of the quote.</param>
        /// <param name="requestForQuoteNumber">The request for quote number.</param>
        /// <param name="partNumber">The part number.</param>
        /// <param name="initials">The initials of the person who created initials.</param>
        /// <param name="createdDate">The create date of the quote.</param>
        /// <param name="lastModifiedDate">The last modified date.</param>
        /// <param name="templateId">The template id.</param>
        public Header(
            int id, 
            string requestForQuoteNumber, 
            string partNumber, 
            string initials, 
            DateTime createdDate, 
            DateTime lastModifiedDate,
            int templateId)
        {
            Quote.PrimaryProperties p = new Quote.PrimaryProperties(
                id, requestForQuoteNumber, partNumber, initials, createdDate, lastModifiedDate, templateId);
            this.SetPrimaryProperties(p);
            this.SetComputationProperties(new Common.ComputationProperties());
            this.SetOtherProperties(new Common.OtherProperties());
        }

        /// <summary>
        /// Gets a value indicating whether the header is a quote.
        /// </summary>
        /// <value>True is always returned.</value>
        public override bool IsQuote
        { 
            get { return true; }
        }

        /// <summary>
        /// Gets the display name for the quote.
        /// </summary>
        /// <value>Quote plus the id.</value>
        public override string DisplayName 
        {
            get { return "Quote " + this.PrimaryProperties.CommonId; }
        }

        /// <summary>
        /// Creates a new detail item.
        /// </summary>
        /// <param name="product">The product for the new detail object.</param>
        /// <returns>The new detail object.</returns>
        public override Common.Detail NewDetail(Model.Product product)
        {
            Detail oo = new Detail(this, product);
            this.Details.Add(oo);
            return oo;
        }

        /// <summary>
        /// Sets the primary properties object.
        /// </summary>
        /// <param name="value">The new properties object.</param>
        public void SetPublicPrimaryProperties(Model.Common.PrimaryProperties value)
        {
            this.SetPrimaryProperties(value);
        }

        /// <summary>
        /// Sets the properties object.
        /// </summary>
        /// <param name="value">The new properties object.</param>
        public void SetPublicComputationProperties(Model.Common.ComputationProperties value)
        {
            this.SetComputationProperties(value);
        }

        /// <summary>
        /// Sets the properties object.
        /// </summary>
        /// <param name="value">The new properties object.</param>
        public void SetPublicOtherProperties(Model.Common.OtherProperties value)
        {
            this.SetOtherProperties(value);
        }

        /// <summary>
        /// Sets the properties object.
        /// </summary>
        /// <param name="value">The new properties object.</param>
        public void SetPublicNoteProperties(Model.Common.NoteProperties value)
        {
            this.SetNoteProperties(value);
        }
    }
}
