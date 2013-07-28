namespace Host
{
    using System;
    using NDepend.Attributes;
    
    [AttributeUsageAttribute(AttributeTargets.Class), CannotDecreaseVisibility]
    public sealed class MenuItemAttribute : Attribute
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
