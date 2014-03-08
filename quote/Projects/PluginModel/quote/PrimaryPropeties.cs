namespace Model.Quote
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    
    using Model.Template;

    /// <summary>
    /// The primary properties of a quote.
    /// </summary>
    public class PrimaryProperties : Common.PrimaryProperties
    {
        /// <summary>
        /// The request for quote number.
        /// </summary>
        private string requestForQuoteNumber;

        /// <summary>
        /// The part number of the quote.
        /// </summary>
        private string partNumber;

        /// <summary>
        /// The template id.
        /// </summary>
        private int templateID;
        
        /// <summary>
        /// The initials of the person who created the quote.
        /// </summary>
        private string initials;
        
        /// <summary>
        /// The create date of the quote.
        /// </summary>
        private DateTime createdDate;

        /// <summary>
        /// The last modified date of the quote.
        /// </summary>
        private DateTime lastModified;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PrimaryProperties" /> class.
        /// </summary>
        /// <param name="id">The quote id.</param>
        /// <param name="requestForQuoteNumber">The request for quote number.</param>
        /// <param name="partNumber">The part number.</param>
        /// <param name="initials">The initials.</param>
        /// <param name="createdDate">The create date.</param>
        /// <param name="lastModified">The last modified date.</param>
        /// <param name="templateId">The template id.</param>
        public PrimaryProperties(
            int id, 
            string requestForQuoteNumber, 
            string partNumber, 
            string initials, 
            DateTime createdDate, 
            DateTime lastModified,
            int templateId)
        {
            this.SetId(id);
            this.requestForQuoteNumber = requestForQuoteNumber;
            this.partNumber = partNumber;
            this.initials = initials;
            this.createdDate = createdDate;
            this.lastModified = lastModified;
            this.templateID = templateId;
        }

        /// <summary>
        /// Gets the create date.
        /// </summary>
        /// <value>The create date.</value>
        [CategoryAttribute(Spaces.SortedSpaces1 + "Date"), DisplayName("CreatedDate"), DescriptionAttribute("Created Date")]
        public DateTime CreatedDate 
        {
            get { return this.createdDate; }
        }

        /// <summary>
        /// Gets the last modified date.
        /// </summary>
        /// <value>The last modified date.</value>
        [CategoryAttribute(Spaces.SortedSpaces1 + "Date"), DisplayName("LastModified"), DescriptionAttribute("Last Modified Date")]
        public DateTime LastModified 
        {
            get { return this.lastModified; }
        }

        /// <summary>
        /// Gets the quote number.
        /// </summary>
        /// <value>The quote number.</value>
        [CategoryAttribute(Spaces.SortedSpaces2 + "Misc"), DisplayName("Quote Number"), DescriptionAttribute("Quote Number")]
        public int QuoteNumber 
        {
            get { return this.CommonId; }
        }

        /// <summary>
        /// Gets the initials of the person who created the quote.
        /// </summary>
        /// <value>The initials.</value>
        [CategoryAttribute(Spaces.SortedSpaces2 + "Misc"), DisplayName("Initials"), DescriptionAttribute("Initials of creator")]
        public string Initials 
        {
            get { return this.initials; }
        }

        /// <summary>
        /// Gets the customer object.
        /// </summary>
        /// <value>The customer object.</value>
        [CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), DisplayName("Customer"), DescriptionAttribute("The customer"), TypeConverter(typeof(CustomerConverter))]
        public Customer Customer 
        {
            get { return this.CommonCustomer; }
        }

        /// <summary>
        /// Gets the part number.
        /// </summary>
        /// <value>The part number.</value>
        [CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), DisplayName("Part Number"), DescriptionAttribute("Part Number")]
        public string PartNumber 
        {
            get { return this.partNumber; }
        }

        /// <summary>
        /// Gets the request for quote number.
        /// </summary>
        /// <value>The request for quote number.</value>
        [CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), DisplayName("RFQ"), DescriptionAttribute("Request For Quote")]
        public string RequestForQuoteNumber 
        {
            get { return this.requestForQuoteNumber; }
        }

        /// <summary>
        /// Gets the template number.
        /// </summary>
        /// <value>The template number.</value>
        [CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), DisplayName("TemplateNumber"), DescriptionAttribute("Created from template")]
        public int TemplateNumber 
        {
            get { return this.templateID; }
        }

        /// <summary>
        /// Sets the template id.
        /// </summary>
        /// <param name="id">The template id.</param>
        public void SetTemplateId(int id)
        {
            this.templateID = id;
        }
    }
}
