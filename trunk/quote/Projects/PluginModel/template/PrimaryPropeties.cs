namespace Model.Template
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// The class contains the primary properties.
    /// The primary properties are saved to fields 
    /// in the database and are not customized.
    /// </summary>
    public sealed class PrimaryProperties : Common.PrimaryProperties
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrimaryProperties" /> class.
        /// </summary>
        /// <param name="id">The id of the primary properties.</param>
        public PrimaryProperties(int id)
        {
            this.SetId(id);
            this.Customer = new Customer();
        }

        /// <summary>
        /// Gets the creation date.
        /// </summary>
        /// <value>The creation date.</value>
        [CategoryAttribute(Spaces.SortedSpaces1 + "Date"), 
         DisplayName("CreatedDate"), 
         DescriptionAttribute("Created Date")]
        public DateTime CreatedDate 
        {
            get { return this.CommonCreatedDate; }
        }

        /// <summary>
        /// Gets the last modified date.
        /// </summary>
        /// <value>The last modified date.</value>
        [CategoryAttribute(Spaces.SortedSpaces1 + "Date"), 
         DisplayName("LastModified"), 
         DescriptionAttribute("Last Modified Date")]
        public DateTime LastModified 
        {
            get { return this.CommonLastModified; }
        }

        /// <summary>
        /// Gets the quote number.
        /// </summary>
        /// <value>The quote number.</value>
        [CategoryAttribute(Spaces.SortedSpaces2 + "Misc"), 
         DisplayName("Quote Number"), 
         DescriptionAttribute("Quote Number")]
        public int QuoteNumber 
        {
            get { return this.CommonId; }
        }

        /// <summary>
        /// Gets the initials of the person who created the quote.
        /// </summary>
        /// <value>The initials of the person who created the quote.</value>
        [CategoryAttribute(Spaces.SortedSpaces2 + "Misc"), 
         DisplayName("Initials"), 
         DescriptionAttribute("Initials of creator")]
        public string Initials 
        {
            get { return this.CommonInitials; }
        }

        /// <summary>
        /// Gets or sets the customer object.
        /// </summary>
        /// <value>The customer object.</value>
        [CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), 
         DisplayName("Customer"), 
         DescriptionAttribute("The customer"), 
         TypeConverter(typeof(CustomerConverter))]
        public Customer Customer 
        {
            get 
            { 
                return this.CommonCustomer; 
            }
            
            set 
            {
                this.CommonCustomer = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the part number.
        /// </summary>
        /// <value>The part number.</value>
        [CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), 
         DisplayName("Part Number"), 
         DescriptionAttribute("Part Number")]
        public string PartNumber 
        {
            get 
            { 
                return this.CommonPartNumber; 
            }
            
            set 
            {
                this.CommonPartNumber = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the request for quote number.
        /// </summary>
        /// <value>The request for quote number.</value>
        [CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), 
         DisplayName("RFQ"), 
         DescriptionAttribute("Request For Quote")]
        public string RequestForQuoteNumber 
        {
            get 
            { 
                return this.CommonRequestForQuoteNumber; 
            }
            
            set 
            {
                this.CommonRequestForQuoteNumber = value;
                this.OnPropertyChanged();
            }
        }
    }
}
