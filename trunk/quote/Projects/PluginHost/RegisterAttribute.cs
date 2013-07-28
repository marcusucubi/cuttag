namespace Host
{
    using System;

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
