namespace Model.Common
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    public class SavableProperties : INotifyPropertyChanged, ICloneable
    {
        private bool dirty;
        
        public SavableProperties()
        {
            this.SavableChange += this.OnSavableChanged;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler SavableChange;

        [Browsable(false)]
        public object Subject { get; set; }

        [Browsable(false)]
        public bool Dirty 
        {
            get { return this.dirty; }
        }

        public virtual void MakeDirty()
        {
            this.dirty = true;
            
            if (this.SavableChange != null) 
            {
                this.SavableChange(this, new EventArgs());
            }
        }

        public virtual void ClearDirty()
        {
            this.dirty = false;
            if (this.SavableChange != null) 
            {
                this.SavableChange(this, new EventArgs());
            }
        }

        public virtual void SendEvents()
        {
            if (this.PropertyChanged != null) 
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("sp"));
            }
            
            this.MakeDirty();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        
        protected void AddDependent(SavableProperties subject)
        {
            subject.SavableChange += this.OnSavableChanged;
        }

        protected void RemoveDependent(SavableProperties subject)
        {
            EventHandler address = this.OnSavableChanged;
            subject.SavableChange -= address;
        }

        protected void OnSavableChanged(object subject, EventArgs args)
        {
            SavableProperties savable = subject as SavableProperties;
            if (savable.Dirty != this.Dirty) 
            {
                this.dirty = true;
                if (this.SavableChange != null) 
                {
                    this.SavableChange(this, new EventArgs());
                }
            }
        }
    }
}
