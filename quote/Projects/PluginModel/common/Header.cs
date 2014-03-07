namespace Model.Common
{
    using System;
    using System.Linq;

    /// <summary>
    /// The header object for the quote.  It is the
    /// main object used to access the properties 
    /// of the quote.
    /// </summary>
    public abstract class Header : DefaultSavableProperties
    {
        /// <summary>
        /// The primary properties map to database fields. 
        /// They are searchable but not customizable.
        /// </summary>
        private Common.PrimaryProperties primaryProperties;
        
        /// <summary>
        /// The other properties are customizable properties.
        /// </summary>
        private Common.OtherProperties otherProperties;
        
        /// <summary>
        /// The computation properties contain all computed values.
        /// </summary>
        private Common.ComputationProperties computationProperties;
        
        /// <summary>
        /// The note properties contain text values.
        /// </summary>
        private Common.NoteProperties noteProperties = new Common.NoteProperties();

        /// <summary>
        /// The details collection contains the wires or components for the quote.
        /// </summary>
        private DetailCollection<Common.Detail> details;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Header" /> class.
        /// </summary>
        protected Header()
        {
            this.details = new DetailCollection<Common.Detail>(this);
        }

        /// <summary>
        /// Gets a value indicating whether the header is for a quote or a template.
        /// Quotes are frozen and can not be changed.
        /// </summary>
        /// <value>True if the header is for a quote.</value>
        public abstract bool IsQuote { get; }

        /// <summary>
        /// Gets the display name which is shown in the window title.
        /// </summary>
        /// <value>The display name of the header.</value>
        public abstract string DisplayName { get; }

        /// <summary>
        /// Gets or sets the id number for the header.
        /// </summary>
        /// <value>The id of the header.</value>
        public int Id 
        {
            get { return this.PrimaryProperties.CommonId; }
            set { this.PrimaryProperties.CommonId = value; }
        }

        /// <summary>
        /// Gets the computation properties for the header.
        /// </summary>
        /// <value>The computation properties.</value>
        public Common.ComputationProperties ComputationProperties 
        {
            get { return this.computationProperties; }
        }

        /// <summary>
        /// Gets the other properties for the header.
        /// </summary>
        /// <value>The other properties.</value>
        public Common.OtherProperties OtherProperties 
        {
            get { return this.otherProperties; }
        }

        /// <summary>
        /// Gets the primary properties for the header.
        /// </summary>
        /// <value>The primary properties.</value>
        public Common.PrimaryProperties PrimaryProperties 
        {
            get { return this.primaryProperties; }
        }

        /// <summary>
        /// Gets the note properties for the header.
        /// </summary>
        /// <value>The note properties.</value>
        public Common.NoteProperties NoteProperties 
        {
            get { return this.noteProperties; }
        }

        /// <summary>
        /// Gets the detail collection.
        /// </summary>
        /// <value>A collection of detail objects which holds the wires or components.</value>
        public DetailCollection<Common.Detail> Details 
        {
            get { return this.details; }
        }

        /// <summary>
        /// A factory method used by the quote or template classes to create a detail object.
        /// </summary>
        /// <param name="product">The wire or component for the detail.</param>
        /// <returns>A new detail object.</returns>
        public abstract Detail NewDetail(Model.Product product);

        /// <summary>
        /// Used by a derived class to set the property.
        /// </summary>
        /// <param name="value">The new property object.</param>
        protected void SetPrimaryProperties(Model.Common.PrimaryProperties value)
        {
            this.primaryProperties = value;
        }

        /// <summary>
        /// Used by a derived class to set the property.
        /// </summary>
        /// <param name="value">The new property object.</param>
        protected void SetComputationProperties(Model.Common.ComputationProperties value)
        {
            this.computationProperties = value;
        }

        /// <summary>
        /// Used by a derived class to set the property.
        /// </summary>
        /// <param name="value">The new property object.</param>
        protected void SetOtherProperties(Model.Common.OtherProperties value)
        {
            this.otherProperties = value;
        }

        /// <summary>
        /// Used by a derived class to set the property.
        /// </summary>
        /// <param name="value">The new property object.</param>
        protected void SetNoteProperties(Model.Common.NoteProperties value)
        {
            this.noteProperties = value;
        }
    }
}
