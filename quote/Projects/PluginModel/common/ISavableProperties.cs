namespace Model.Common
{
    using System;
    using System.ComponentModel;

    public interface ISavableProperties : INotifyPropertyChanged
    {
        event EventHandler Dirty;

        event EventHandler Clean;
        
        bool IsDirty { get; set; }
    }
}
