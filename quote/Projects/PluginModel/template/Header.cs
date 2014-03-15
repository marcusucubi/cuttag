namespace Model.Template
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    
    using Model.Common;
    using Model.Template.Ext;

    /// <summary>
    /// The header for a template.
    /// </summary>
    public class Header : Common.Header
    {
        /// <summary>
        /// Used to assign a unique number to the detail.
        /// </summary>
        private int nextSequenceNumber = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Header" /> class.
        /// </summary>
        public Header() : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Header" /> class.
        /// </summary>
        /// <param name="id">A unique id for the quote.</param>
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

        /// <summary>
        /// Gets a value indicating whether the header is for a quote.
        /// </summary>
        /// <value>Indicates whether the header is for a quote.</value>
        public override bool IsQuote
        { 
            get { return false; }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>The display name.</value>
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

        /// <summary>
        /// Gets or sets a number used to assign a unique number to the detail items.
        /// </summary>
        /// <value>A sequence number.</value>
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

        /// <summary>
        /// Factory method used to create a new detail.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>A new detail object.</returns>
        public override Common.Detail NewDetail(Product product)
        {
            var oo = new Detail(this, product);
            this.Details.Add(oo);
            return oo;
        }

        /// <summary>
        /// Removes the input detail from the detail collection.
        /// </summary>
        /// <param name="detail">The detail to remove.</param>
        public void Remove(Detail detail)
        {
            if (detail != null) 
            {
                this.Details.Remove(detail);
            }
        }
    }
}
