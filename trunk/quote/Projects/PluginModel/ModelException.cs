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
        /// The default constructor
        /// </summary>
        public ModelException()
        {
        }

        /// <summary>
        /// A Constructor with a message
        /// </summary>
        /// <param name="message">A short description</param>
        public ModelException(string message) : base(message)
        {
        }

        /// <summary>
        /// A wrapping constructor
        /// </summary>
        /// <param name="message">A short description</param>
        /// <param name="innerException">The exception to wrap</param>
        public ModelException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Used for serialization
        /// </summary>
        /// <param name="info">The info</param>
        /// <param name="context">The context</param>
        protected ModelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}