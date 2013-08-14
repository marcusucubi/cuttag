namespace Model.Common
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    
    using Model.Template;

    public class PrimaryProperties : SavableProperties
    {
        private int id;
        
        private DateTime commonCreatedDate;
        
        private DateTime commonLastModified;
        
        public PrimaryProperties()
        {
            this.commonCreatedDate = DateTime.Now;
            this.commonLastModified = DateTime.Now;
        }
        
        [Browsable(false)]
        public Customer CommonCustomer { get; set; }

        [Browsable(false)]
        public string CommonRequestForQuoteNumber { get; set; }

        [Browsable(false)]
        public string CommonPartNumber { get; set; }

        [Browsable(false)]
        public DateTime CommonCreatedDate 
        {
            get { return this.commonCreatedDate; }
            set { this.commonCreatedDate = value; }
        }

        [Browsable(false)]
        public DateTime CommonLastModified 
        { 
            get { return this.commonLastModified; }
            set { this.commonLastModified = value; }
        }

        [Browsable(false)]
        public string CommonInitials { get; set; }

        [Browsable(false)]
        public int CommonId 
        {
            get { return this.id; }
        }

        public void SetId(int id)
        {
            this.id = id;
            this.OnPropertyChanged();
        }
    }
}
