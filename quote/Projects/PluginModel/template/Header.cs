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
                    this.withEventsFieldDetails.CollectionChanged -= this._col_ListChanged;
                }
                
                this.withEventsFieldDetails = value;
                
                if (this.withEventsFieldDetails != null) 
                {
                    this.withEventsFieldDetails.CollectionChanged += this._col_ListChanged;
                }
            }
        }
        
        public override Common.Detail NewDetail(Product product)
        {
            Detail oo = new Detail(this, product);

            this.Details.Add(oo);
            this.AddDependent(oo);
            this.OnPropertyChanged();

            return oo;
        }

        public void Remove(Detail detail)
        {
            if (detail != null) 
            {
                this.Details.Remove(detail);

                this.RemoveDependent(detail);
                this.OnPropertyChanged();
            }
        }

        protected override void OnPropertyChanged()
        {
            base.OnPropertyChanged();
        }
        
        private void _col_ListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnPropertyChanged();
        }
    }
}
