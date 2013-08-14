namespace Model.Template
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    public sealed class PrimaryProperties : Common.PrimaryProperties
    {
        public PrimaryProperties(int id)
        {
            this.SetId(id);
            this.Customer = new Customer();
        }

        [CategoryAttribute(Spaces.SortedSpaces1 + "Date"), DisplayName("CreatedDate"), DescriptionAttribute("Created Date")]
        public DateTime CreatedDate 
        {
            get { return this.CommonCreatedDate; }
        }

        [CategoryAttribute(Spaces.SortedSpaces1 + "Date"), DisplayName("LastModified"), DescriptionAttribute("Last Modified Date")]
        public DateTime LastModified 
        {
            get { return this.CommonLastModified; }
        }

        [CategoryAttribute(Spaces.SortedSpaces2 + "Misc"), DisplayName("Quote Number"), DescriptionAttribute("Quote Number")]
        public int QuoteNumber 
        {
            get { return this.CommonId; }
        }

        [CategoryAttribute(Spaces.SortedSpaces2 + "Misc"), DisplayName("Initials"), DescriptionAttribute("Initials of creator")]
        public string Initials 
        {
            get { return this.CommonInitials; }
        }

        [CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), DisplayName("Customer"), DescriptionAttribute("The customer"), TypeConverter(typeof(CustomerConverter))]
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

        [CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), DisplayName("Part Number"), DescriptionAttribute("Part Number")]
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

        [CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), DisplayName("RFQ"), DescriptionAttribute("Request For Quote")]
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
