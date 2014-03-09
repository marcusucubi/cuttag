namespace Model.Template
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Adds a subject to an object.  Used by some
    /// decorator classes such as <see cref="DisplayableComponentProperties" />
    /// </summary>
    public interface IHasSubject
    {
        /// <summary>
        /// Gets the subject object of the decorator.
        /// </summary>
        /// <value>The subject object.</value>
        [Browsable(false)]
        Model.Common.ISavableProperties Subject
        {
            get;
        }
    }
}