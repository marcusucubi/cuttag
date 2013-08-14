namespace Model.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public abstract class DefaultSavableProperties : ISavableProperties
    {
        private bool dirty;
        
        private List<ISavableProperties> dependents = new List<ISavableProperties>();
        
        protected DefaultSavableProperties()
        {
        }
        
        public virtual event PropertyChangedEventHandler PropertyChanged;

        public virtual event EventHandler Dirty;

        public virtual event EventHandler Clean;
        
        [Browsable(false)]
        public bool IsDirty 
        {
            get 
            { 
                return this.dirty; 
            }
            
            set
            {
                if (value == true)
                {
                    this.dirty = true;
                    
                    if (this.Dirty != null) 
                    {
                        this.Dirty(this, new EventArgs());
                    }
                }
                else
                {
                    this.dirty = false;
                    
                    foreach (ISavableProperties dependant in this.dependents)
                    {
                        dependant.IsDirty = false;
                    }
                    
                    if (this.Clean != null) 
                    {
                        this.Clean(this, new EventArgs());
                    }
                }
            }
        }

        protected void AddDependent(ISavableProperties subject)
        {
            subject.Dirty += this.OnDependentDirty;
            this.dependents.Add(subject);
        }

        protected void RemoveDependent(ISavableProperties subject)
        {
            subject.Dirty -= this.OnDependentDirty;
            this.dependents.Remove(subject);
        }

        protected virtual void OnPropertyChanged()
        {
            if (this.PropertyChanged != null) 
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("sp"));
            }
            
            this.IsDirty = true;
        }
        
        private void OnDependentDirty(object source, EventArgs args)
        {
            this.IsDirty = true;
        }
    }
}