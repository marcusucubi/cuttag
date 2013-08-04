namespace Model.IO
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    [Serializable]
    public class ObjectGeneratorException : Exception
    {
        public ObjectGeneratorException()
        {
        }
        
        public ObjectGeneratorException(
            string message, 
            Exception inner)
            : base(message, inner)
        {
        }
        
        public ObjectGeneratorException(string message) : base(message)
        {
        }
        
        protected ObjectGeneratorException(
            SerializationInfo info, 
            StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
