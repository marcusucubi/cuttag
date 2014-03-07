namespace Model
{
    using System;

    /// <summary>
    /// Used to signal changes to the model.
    /// </summary>
    public class ModelEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the ID of the model object.
        /// </summary>
        /// <value>The id of the object.</value>
        public object Id { get; set; }
    }
}
