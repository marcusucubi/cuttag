namespace Model.Common
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    
    using Model.Template;

    /// <summary>
    /// The primary properties for the quote or template.
    /// The primary properties are not customizable and
    /// are mapped to database fields.
    /// </summary>
    public class PrimaryProperties : DefaultSavableProperties
    {
        /// <summary>
        /// The id of the quote or template.
        /// </summary>
        private int id;
        
        /// <summary>
        /// The create date from the database.
        /// </summary>
        private DateTime commonCreatedDate;
        
        /// <summary>
        /// The last modified date from the database.
        /// </summary>
        private DateTime commonLastModified;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PrimaryProperties" /> class.
        /// </summary>
        public PrimaryProperties()
        {
            this.commonCreatedDate = DateTime.Now;
            this.commonLastModified = DateTime.Now;
        }
        
        /// <summary>
        /// Gets or sets the customer object.
        /// </summary>
        /// <value>The customer object.</value>
        [Browsable(false)]
        public Customer CommonCustomer { get; set; }

        /// <summary>
        /// Gets or sets the request for quote number.
        /// </summary>
        /// <value>The request for quote number.</value>
        [Browsable(false)]
        public string CommonRequestForQuoteNumber { get; set; }

        /// <summary>
        /// Gets or sets the part number.
        /// </summary>
        /// <value>The part number.</value>
        [Browsable(false)]
        public string CommonPartNumber { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>The create date.</value>
        [Browsable(false)]
        public DateTime CommonCreatedDate 
        {
            get { return this.commonCreatedDate; }
            set { this.commonCreatedDate = value; }
        }

        /// <summary>
        /// Gets or sets the last modified date.
        /// </summary>
        /// <value>The last modified date.</value>
        [Browsable(false)]
        public DateTime CommonLastModified 
        { 
            get { return this.commonLastModified; }
            set { this.commonLastModified = value; }
        }

        /// <summary>
        /// Gets or sets the initials of the person creating the quote.
        /// </summary>
        /// <value>The initials of the person creating the quote.</value>
        [Browsable(false)]
        public string CommonInitials { get; set; }

        /// <summary>
        /// Gets the id of the quote.
        /// </summary>
        /// <value>The id of the quote.</value>
        [Browsable(false)]
        public int CommonId 
        {
            get 
            { 
                return this.id; 
            }
            
            internal set
            { 
                this.id = value; 
                this.OnPropertyChanged(); 
            }
        }

        /// <summary>
        /// Sets the id of the quote.
        /// </summary>
        /// <param name="id">The quote id.</param>
        public void SetId(int id)
        {
            this.id = id;
            this.OnPropertyChanged();
        }
    }
}
