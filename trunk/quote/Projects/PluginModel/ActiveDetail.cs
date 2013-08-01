namespace Model
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    
    using Model.Common;

    public class ActiveDetail : INotifyPropertyChanged
    {
        private static ActiveDetail instance = new ActiveDetail();

        private Detail detail;

        public event PropertyChangedEventHandler PropertyChanged;

        public static object Instance 
        {
            get { return instance; }
        }

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
