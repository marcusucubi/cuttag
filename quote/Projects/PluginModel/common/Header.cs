namespace Model.Common
{
    using System;
    using System.Linq;

    public abstract class Header : DefaultSavableProperties
    {
        private Common.PrimaryProperties primaryProperties;
        
        private Common.OtherProperties otherProperties;
        
        private Common.ComputationProperties computationProperties;
        
        private Common.NoteProperties noteProperties = new Common.NoteProperties();

        private DetailCollection<Common.Detail> details;
        
        protected Header()
        {
            this.details = new DetailCollection<Common.Detail>(this);
        }

        public abstract bool IsQuote { get; }

        public abstract string DisplayName { get; }

        public int Id { get; set; }
        
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
