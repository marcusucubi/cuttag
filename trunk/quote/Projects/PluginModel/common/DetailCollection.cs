namespace Model.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public class DetailCollection<T> : 
        System.Collections.ObjectModel.ObservableCollection<T>,
        ISavableProperties
        where T : Model.Common.Detail
    {
        private bool dirty;
        
        private Common.Header header;
        
        public DetailCollection(Common.Header header)
        {
            this.header = header;
        }

        public event EventHandler Dirty;

        public event EventHandler Clean;
        
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
                    
                    foreach (ISavableProperties detail in this)
                    {
                        detail.IsDirty = false;
                    }
                    
                    if (this.Clean != null) 
                    {
                        this.Clean(this, new EventArgs());
                    }
                }
            }
        }
        
        public Common.Header Header
        {
            get { return this.header; }
        }
        
        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            item.Dirty += this.OnDependentDirty;
            this.IsDirty = true;
        }
        
        protected override void RemoveItem(int index)
        {
            ISavableProperties item = this.Items[index];
            base.RemoveItem(index);
            item.Dirty -= this.OnDependentDirty;
            this.IsDirty = true;
        }
        
        private void OnDependentDirty(object source, EventArgs args)
        {
            this.IsDirty = true;
        }
    }
}
