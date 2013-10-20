namespace Model
{
    using System;

    /// <summary>
    /// Used to signal changes to the model.
    /// </summary>
    public class ModelEventArgs : EventArgs
    {
        /// <summary>
        /// The ID of the model object.
        /// </summary>
        public object Id { get; set; }
    }
}
