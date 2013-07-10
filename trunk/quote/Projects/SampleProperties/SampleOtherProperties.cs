using System;
using System.ComponentModel;
using System.Drawing;

using PluginHost;

using Model.Template;

namespace SampleProperties
{
    public class SampleOtherProperties : Model.Template.OtherProperties
    {
        private string m_Note;
        private DateTime m_DueDate;

        public SampleOtherProperties(Header header)
            : base(header)
        {
            
        }

        [CategoryAttribute("Date"), 
        DisplayName("Due Date"), 
        DescriptionAttribute("Date the quote is to be given to the customer")]
        public DateTime DueDate
        {
            get { return m_DueDate; }
            set
            {
                m_DueDate = value;
                SendEvents();
            }
        }

        [CategoryAttribute("Note"),
        DisplayName("Short Description"),
        DescriptionAttribute("A short description of the quote")]
        public string Note
        {
            get { return m_Note; }
            set
            {
                m_Note = value;
                base.MakeDirty();
            }
        }

    }
}
