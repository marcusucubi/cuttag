using System;
using NDepend.Attributes;

namespace PluginHost
{
    [AttributeUsageAttribute(AttributeTargets.Class), CannotDecreaseVisibility]
    sealed public class PluginMenuItemAttribute : Attribute
    {
        public string Text
        {
            get;
            set;
        }

        public string Parent
        {
            get;
            set;
        }

        public int MenuSeporatorNumber
        {
            get;
            set;
        }

        public MenuPosition Position
        {
            get;
            set;
        }
    }
}
