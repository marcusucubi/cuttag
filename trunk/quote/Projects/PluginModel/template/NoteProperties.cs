namespace Model.Template
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    public sealed class NoteProperties : Common.NoteProperties
    {
        private string note;

        private string note2Customer;
        
        public NoteProperties()
        {
        }

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
