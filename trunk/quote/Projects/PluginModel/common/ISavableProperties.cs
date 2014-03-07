namespace Model.Common
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Extends the <see cref="INotifyPropertyChanged" /> interface to 
    /// provide state information.
    /// </summary>
    public interface ISavableProperties : INotifyPropertyChanged
    {
        /// <summary>
        /// Notifies clients when the properties become dirty.
        /// The event is triggered whenever a change to 
        /// the properties occurs, even if the properties 
        /// are already dirty.
        /// </summary>
        event EventHandler Dirty;

        /// <summary>
        /// Notifies clients when the properties become clean.
        /// The properties are clean when IsDirty is set
        /// to false.
        /// </summary>
        event EventHandler Clean;
        
        /// <summary>
        /// Gets or sets a value indicating whether the properties
        /// have been changed.
        /// </summary>
        /// <value>If true the properties have been changed.</value>
        bool IsDirty { get; set; }
    }
}
