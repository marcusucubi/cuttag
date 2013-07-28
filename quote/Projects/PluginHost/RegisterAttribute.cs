using System;

namespace Host
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class RegisterAttribute : Attribute
    {
        public Type Key
        {
            get;
            set;
        }
    }
}
