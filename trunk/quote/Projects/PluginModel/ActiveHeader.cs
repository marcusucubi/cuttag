namespace Model
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    
    using Model.Common;

    public class ActiveHeader : INotifyPropertyChanged
    {
        private static ActiveHeader instance = new ActiveHeader();
        
        private Header header;

        public event PropertyChangedEventHandler PropertyChanged;

        public static ActiveHeader Instance 
        {
            get { return instance; }
        }

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