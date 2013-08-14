namespace Model.Template
{
    using System;
    using System.ComponentModel;

    public interface IHasSubject
    {
        [Browsable(false)]
        Model.Common.SavableProperties Subject
        {
            get;
        }
    }
}