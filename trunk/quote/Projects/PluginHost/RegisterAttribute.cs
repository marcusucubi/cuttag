using System;

namespace Host
{
    public sealed class RegisterAttribute : Attribute
    {
        public Type Key
        {
            get;
            set;
        }
    }
}
