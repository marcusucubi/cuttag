namespace Model.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// A collection of detail objects.
    /// </summary>
    /// <typeparam name="T" >Must be a detail type.</typeparam>
    public class DetailCollection<T> : 
        System.Collections.ObjectModel.ObservableCollection<T>,
        ISavableProperties
        where T : Model.Common.Detail
    {
        /// <summary>
        /// True when any item in the collection is dirty.
        /// </summary>
        private bool dirty;

        /// <summary>
        /// The header.
        /// </summary>
        private Common.Header header;

        /// <summary>
        /// Initializes a new instance of the <see cref="{DetailCollection}" /> class.
        /// </summary>
        /// <param name="header">The header.</param>
        public DetailCollection(Common.Header header)
        {
            this.header = header;
        }

        /// <summary>
        /// Fires when the collection is dirty.
        /// </summary>
        public event EventHandler Dirty;

        /// <summary>
        /// Indicates when the dirty flag is cleared.
        /// </summary>
        public event EventHandler Clean;
        
        /// <summary>
        /// Gets or sets a value indicating whether the object needs to be saved.
        /// </summary>
        /// <value>Indicates whether the object needs to be saved.</value>
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
        
        /// <summary>
        /// Gets the header object.
        /// </summary>
        /// <value>A header object.</value>
        public Common.Header Header
        {
            get { return this.header; }
        }
        
        /// <summary>
        /// Called when an item is added.
        /// </summary>
        /// <param name="index">The index of the new item.</param>
        /// <param name="item">The item being added.</param>
        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            item.Dirty += this.OnDependentDirty;
            this.IsDirty = true;
        }
        
        /// <summary>
        /// Called when an item is removed.
        /// </summary>
        /// <param name="index">The index of the item being removed.</param>
        protected override void RemoveItem(int index)
        {
            ISavableProperties item = this.Items[index];
            base.RemoveItem(index);
            item.Dirty -= this.OnDependentDirty;
            this.IsDirty = true;
        }
        
        /// <summary>
        /// Called when an object is changed.
        /// </summary>
        /// <param name="source">The item that was changed.</param>
        /// <param name="args">The arguments for the item being changed.</param>
        private void OnDependentDirty(object source, EventArgs args)
        {
            this.IsDirty = true;
        }
    }
}
