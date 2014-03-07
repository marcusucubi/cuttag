namespace Model
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// An exception for the model project.
    /// </summary>
    [Serializable]
    public class ModelException : Exception, ISerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelException" /> class.
        /// </summary>
        public ModelException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelException" /> class.
        /// </summary>
        /// <param name="message">A short description.</param>
        public ModelException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelException" /> class.
        /// </summary>
        /// <param name="message">A short description.</param>
        /// <param name="innerException">The exception to wrap.</param>
        public ModelException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelException" /> class.
        /// </summary>
        /// <param name="info">The info object.</param>
        /// <param name="context">The context object.</param>
        protected ModelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}