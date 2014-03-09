namespace Model.Template
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// A class that contains properties for the notes 
    /// of the quote.
    /// </summary>
    public sealed class NoteProperties : Common.NoteProperties
    {
        /// <summary>
        /// The note string.
        /// </summary>
        private string note;

        /// <summary>
        /// The note for the customer.
        /// </summary>
        private string note2Customer;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteProperties" /> class.
        /// </summary>
        public NoteProperties()
        {
        }

        /// <summary>
        /// Gets or sets the text for the note.
        /// </summary>
        /// <value>The text of the internal note.</value>
        [DisplayName("Note-Internal"), CategoryAttribute("Notes")]
        public string Note 
        {
            get 
            { 
                return this.note; 
            }
            
            set 
            {
                if (this.note != value) 
                {
                    this.note = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the note to the customer.
        /// </summary>
        /// <value>The text of the note to the customer.</value>
        [DisplayName("Note-To Customer"), CategoryAttribute("Notes")]
        public string Note2Customer 
        {
            get 
            { 
                return this.note2Customer; 
            }
            
            set 
            {
                if (this.note2Customer != value) 
                {
                    this.note2Customer = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
