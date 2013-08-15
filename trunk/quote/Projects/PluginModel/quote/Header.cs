namespace Model.Quote
{
    using System;
    using System.Linq;

    public class Header : Common.Header
    {
        public Header() 
            : this(0, string.Empty, string.Empty, string.Empty, System.DateTime.Now, System.DateTime.Now, 0)
        {
        }

        public Header(
            int id, 
            string requestForQuoteNumber, 
            string partNumber, 
            string initials, 
            DateTime createdDate, 
            DateTime lastModifiedDate,
            int templateId)
        {
            Quote.PrimaryProperties p = new Quote.PrimaryProperties(
                id, requestForQuoteNumber, partNumber, initials, createdDate, lastModifiedDate, templateId);
            this.SetPrimaryProperties(p);
            this.SetComputationProperties(new Common.ComputationProperties());
            this.SetOtherProperties(new Common.OtherProperties());
            base.Id = id;
        }

        public new int Id 
        {
            get { return PrimaryProperties.CommonId; }
        }

        public override bool IsQuote
        { 
            get { return true; }
        }

        public override string DisplayName 
        {
            get { return "Quote " + this.PrimaryProperties.CommonId; }
        }

        public override Common.Detail NewDetail(Model.Product product)
        {
            Detail oo = new Detail(this, product);
            this.Details.Add(oo);
            return oo;
        }

        public void SetPublicPrimaryProperties(Model.Common.PrimaryProperties value)
        {
            this.SetPrimaryProperties(value);
        }

        public void SetPublicComputationProperties(Model.Common.ComputationProperties value)
        {
            this.SetComputationProperties(value);
        }

        public void SetPublicOtherProperties(Model.Common.OtherProperties value)
        {
            this.SetOtherProperties(value);
        }

        public void SetPublicNoteProperties(Model.Common.NoteProperties value)
        {
            this.SetNoteProperties(value);
        }
    }
}
