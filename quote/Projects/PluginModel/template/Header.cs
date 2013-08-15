namespace Model.Template
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    
    using Model.Common;
    using Model.Template.Ext;

    public class Header : Common.Header
    {
        private int nextSequenceNumber = 1;
        
        public Header() : this(0)
        {
        }

        public Header(int id)
        {
            this.AddChildProperty(this.Details);

            this.SetPrimaryProperties(new PrimaryProperties(id));

            this.SetOtherProperties(PropertyFactory.CreateOtherProperties(this, id));
            this.SetComputationProperties(PropertyFactory.CreateComputationProperties(this, id));
            this.SetNoteProperties(new NoteProperties());

            this.AddChildProperty(this.ComputationProperties);
            this.AddChildProperty(this.OtherProperties);
            this.AddChildProperty(this.PrimaryProperties);
            this.AddChildProperty(this.NoteProperties);
        }

        public override bool IsQuote
        { 
            get { return false; }
        }

        public override string DisplayName 
        {
            get 
            {
                string s = "Template";
                
                if (this.PrimaryProperties.CommonId == 0) 
                {
                    return "New " + s;
                }
                
                return s + " " + this.PrimaryProperties.CommonId;
            }
        }

        public int NextSequenceNumber 
        {
            get 
            { 
                return this.nextSequenceNumber++; 
            }
            
            set 
            { 
                if (value > this.nextSequenceNumber)
                {
                    this.nextSequenceNumber = value; 
                }
            }
        }

        public override Common.Detail NewDetail(Product product)
        {
            Detail oo = new Detail(this, product);
            this.Details.Add(oo);
            return oo;
        }

        public void Remove(Detail detail)
        {
            if (detail != null) 
            {
                this.Details.Remove(detail);
            }
        }
    }
}
