namespace Model.Common
{
    using System;
    using System.Linq;

    public abstract class Header : SavableProperties
    {
        private Common.PrimaryProperties primaryProperties;
        
        private Common.OtherProperties otherProperties;
        
        private Common.ComputationProperties computationProperties;
        
        private Common.NoteProperties noteProperties = new Common.NoteProperties();

        private DetailCollection<Common.Detail> details;
        
        private int nextSequenceNumber = 1;
        
        private bool quote;
        
        protected Header(bool quote)
        {
            this.details = new DetailCollection<Common.Detail>(this);
            this.quote = quote;
        }

        public int Id { get; set; }
        
        public bool IsQuote
        { 
            get { return this.quote; }
        }

        public Common.ComputationProperties ComputationProperties 
        {
            get { return this.computationProperties; }
        }

        public Common.OtherProperties OtherProperties 
        {
            get { return this.otherProperties; }
        }

        public Common.PrimaryProperties PrimaryProperties 
        {
            get { return this.primaryProperties; }
        }

        public Common.NoteProperties NoteProperties 
        {
            get { return this.noteProperties; }
        }

        public DetailCollection<Common.Detail> Details 
        {
            get { return this.details; }
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

        public string DisplayName 
        {
            get 
            {
                string s = null;
                
                if (this.IsQuote) 
                {
                    s = "Quote";
                } 
                else 
                {
                    s = "Template";
                }
                
                if (this.PrimaryProperties.CommonId == 0) 
                {
                    return "New " + s;
                }
                
                return s + " " + this.PrimaryProperties.CommonId;
            }
        }

        public abstract Detail NewDetail(Model.Product product);

        protected void SetPrimaryProperties(Model.Common.PrimaryProperties value)
        {
            this.primaryProperties = value;
        }

        protected void SetComputationProperties(Model.Common.ComputationProperties value)
        {
            this.computationProperties = value;
        }

        protected void SetOtherProperties(Model.Common.OtherProperties value)
        {
            this.otherProperties = value;
        }

        protected void SetNoteProperties(Model.Common.NoteProperties value)
        {
            this.noteProperties = value;
        }
    }
}
