namespace Model
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ModelException : Exception, ISerializable
    {
        public ModelException()
        {
        }

        public ModelException(string message) : base(message)
        {
        }

        public ModelException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ModelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}