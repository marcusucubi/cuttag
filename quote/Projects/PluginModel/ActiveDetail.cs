namespace Model
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    
    using Model.Common;

    /// <summary>
    /// Holds the <c>detail</c> that is currently active. 
    /// This is a singleton class.
    /// </summary>
    public class ActiveDetail : INotifyPropertyChanged
    {
        /// <summary>
        /// The only instance.
        /// </summary>
        private static ActiveDetail instance = new ActiveDetail();

        /// <summary>
        /// The active detail.
        /// </summary>
        private Detail detail;

        /// <summary>
        /// Event that signals the detail has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        /// <value>The instance.</value>
        public static ActiveDetail Instance 
        {
            get { return instance; }
        }

        /// <summary>
        /// Gets or sets the active <c>detail</c>.
        /// </summary>
        /// <value>The detail.</value>
        public Detail Detail 
        {
            get 
            { 
                return this.detail; 
            }
            
            set 
            {
                if (!object.ReferenceEquals(this.detail, value))
                {
                    this.detail = value;
                    if (this.PropertyChanged != null) 
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Detail"));
                    }
                }
            }
        }
    }
}
