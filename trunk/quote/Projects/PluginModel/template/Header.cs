namespace Model.Template
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    
    using Model.Common;

    public class Header : Common.Header
    {
        private DetailCollection<Common.Detail> withEventsFieldDetails;
        
        public Header() : this(0)
        {
            this._Details = this.Details;
        }

        public Header(int id) : base(false)
        {
            this._Details = this.Details;

            this.SetPrimaryProperties(new PrimaryProperties(id));

            this.SetOtherProperties(PropertyFactory.CreateOtherProperties(this, id));
            this.SetComputationProperties(PropertyFactory.CreateComputationProperties(this, id));
            this.SetNoteProperties(new NoteProperties());

            this.AddDependent(this.ComputationProperties);
            this.AddDependent(this.OtherProperties);
            this.AddDependent(this.PrimaryProperties);
            this.AddDependent(this.NoteProperties);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID", Justification = "Uppercase is alright")]
        public int ID 
        {
            get { return PrimaryProperties.CommonId; }
        }

        private DetailCollection<Common.Detail> _Details 
        {
            set 
            {
                if (this.withEventsFieldDetails != null) 
                {
                    this.withEventsFieldDetails.ListChanged -= this._col_ListChanged;
                }
                
                this.withEventsFieldDetails = value;
                
                if (this.withEventsFieldDetails != null) 
                {
                    this.withEventsFieldDetails.ListChanged += this._col_ListChanged;
                }
            }
        }
        
        public override Common.Detail NewDetail(Product product)
        {
            Detail oo = new Detail(this, product);

            this.Details.Add(oo);
            this.AddDependent(oo);
            this.SendEvents();

            return oo;
        }

        public void Remove(Detail detail)
        {
            if (detail != null) 
            {
                this.Details.Remove(detail);

                this.RemoveDependent(detail);
                this.SendEvents();
            }
        }

        public override void SendEvents()
        {
            this.ComputationProperties.SendEvents();
        }
        
        private void _col_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.SendEvents();
        }
    }
}
