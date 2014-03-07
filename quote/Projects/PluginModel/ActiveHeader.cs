namespace Model
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    
    using Model.Common;

    /// <summary>
    /// Holds the <c>header</c> that is currently active.
    /// The class is a singleton.
    /// </summary>
    public class ActiveHeader : INotifyPropertyChanged
    {
        /// <summary>
        /// The only instance.
        /// </summary>
        private static ActiveHeader instance = new ActiveHeader();
        
        /// <summary>
        /// The active header.
        /// </summary>
        private Header header;

        /// <summary>
        /// Event used to signal the header has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the singleton.
        /// </summary>
        /// <value>The active header.</value>
        public static ActiveHeader Instance 
        {
            get { return instance; }
        }

        /// <summary>
        /// Gets or sets the active <c>header</c>.
        /// </summary>
        /// <value>The header.</value>
        public Header Header 
        {
            get 
            { 
                return this.header; 
            }
            
            set 
            {
                if (!object.ReferenceEquals(this.header, value))
                {
                    this.header = value;
                    
                    if (this.PropertyChanged != null) 
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Header"));
                    }
                }
            }
        }
    }
}