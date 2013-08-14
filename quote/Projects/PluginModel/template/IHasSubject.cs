namespace Model.Template
{
    using System;
    using System.ComponentModel;

    public interface IHasSubject
    {
        [Browsable(false)]
        Model.Common.ISavableProperties Subject
        {
            get;
        }
    }
}