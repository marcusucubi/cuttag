namespace Model.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Provides default implementation for <code>ISavableProperties</code>.
    /// </summary>
    public abstract class DefaultSavableProperties : ISavableProperties
    {
        /// <summary>
        /// Indicates if the properties have been changed.
        /// </summary>
        private bool dirty;
        
        /// <summary>
        /// A list of child properties.
        /// </summary>
        private List<ISavableProperties> children = new List<ISavableProperties>();
        
        /// <summary>
        /// Default constructor
        /// </summary>
        protected DefaultSavableProperties()
        {
        }
        
        /// <summary>
        /// Fires when a property changes.
        /// </summary>
        public virtual event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Fires the first time a property is changed.
        /// </summary>
        public virtual event EventHandler Dirty;

        /// <summary>
        /// Fires when the properties are saved.
        /// </summary>
        public virtual event EventHandler Clean;
        
        /// <summary>
        /// Returns true if the properties have been changed.
        /// </summary>
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
                    
                    foreach (ISavableProperties dependant in this.children)
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

        /// <summary>
        /// Adds a child property.
        /// </summary>
        /// <param name="child">The child properties.</param>
        protected void AddChildProperty(ISavableProperties child)
        {
            child.Dirty += this.OnChildDirty;
            this.children.Add(child);
        }

        /// <summary>
        /// Removes the child property.
        /// </summary>
        /// <param name="child">The child property.</param>
        protected void RemoveChildProperty(ISavableProperties child)
        {
            child.Dirty -= this.OnChildDirty;
            this.children.Remove(child);
        }

        /// <summary>
        /// Called when a child property is changed.
        /// </summary>
        protected virtual void OnPropertyChanged()
        {
            if (this.PropertyChanged != null) 
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("sp"));
            }
            
            this.IsDirty = true;
        }
        
        /// <summary>
        /// Called when a child is dirty.
        /// </summary>
        /// <param name="source">The child property.</param>
        /// <param name="args">The arguments.</param>
        private void OnChildDirty(object source, EventArgs args)
        {
            this.IsDirty = true;
        }
    }
}