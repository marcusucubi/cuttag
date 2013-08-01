namespace Model.Quote
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    
    using Model.Template;

    public class PrimaryProperties : Common.PrimaryProperties
    {
        private string requestForQuoteNumber;
        
        private string partNumber;
        
        private int templateID;
        
        private string initials;
        
        private DateTime createdDate;

        private DateTime lastModified;
        
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

        [CategoryAttribute(Spaces.SortedSpaces1 + "Date"), DisplayName("CreatedDate"), DescriptionAttribute("Created Date")]
        public DateTime CreatedDate 
        {
            get { return this.createdDate; }
        }

        [CategoryAttribute(Spaces.SortedSpaces1 + "Date"), DisplayName("LastModified"), DescriptionAttribute("Last Modified Date")]
        public DateTime LastModified 
        {
            get { return this.lastModified; }
        }

        [CategoryAttribute(Spaces.SortedSpaces2 + "Misc"), DisplayName("Quote Number"), DescriptionAttribute("Quote Number")]
        public int QuoteNumber 
        {
            get { return this.CommonId; }
        }

        [CategoryAttribute(Spaces.SortedSpaces2 + "Misc"), DisplayName("Initials"), DescriptionAttribute("Initials of creator")]
        public string Initials 
        {
            get { return this.initials; }
        }

        [CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), DisplayName("Customer"), DescriptionAttribute("The customer"), TypeConverter(typeof(CustomerConverter))]
        public Customer Customer 
        {
            get { return this.CommonCustomer; }
        }

        [CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), DisplayName("Part Number"), DescriptionAttribute("Part Number")]
        public string PartNumber 
        {
            get { return this.partNumber; }
        }

        [CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), DisplayName("RFQ"), DescriptionAttribute("Request For Quote")]
        public string RequestForQuoteNumber 
        {
            get { return this.requestForQuoteNumber; }
        }

        [CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), DisplayName("TemplateNumber"), DescriptionAttribute("Created from template")]
        public int TemplateNumber 
        {
            get { return this.templateID; }
        }

        public void SetTemplateId(int id)
        {
            this.templateID = id;
        }
    }
}
