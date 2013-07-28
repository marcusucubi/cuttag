using System;
using NDepend.Attributes;

namespace Host
{
    [AttributeUsageAttribute(AttributeTargets.Class), CannotDecreaseVisibility]
    sealed public class MenuItemAttribute : Attribute
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

        public bool ShowInToolBar
        {
            get;
            set;
        }
    }
}
