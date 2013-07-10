using System;

namespace PluginHost
{
    public class RegisterAttribute : Attribute
    {
        public Type Key
        {
            get;
            set;
        }
    }
}
