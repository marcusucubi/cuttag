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
        public Header() : this(0)
        {
        }

        public Header(int id) : base(false)
        {
            this.AddDependent(this.Details);

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
