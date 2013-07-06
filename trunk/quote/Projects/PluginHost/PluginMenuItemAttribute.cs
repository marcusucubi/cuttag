﻿using System;
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

        public string Anchor
        {
            get;
            set;
        }

        public MenuPosition MenuPosition
        {
            get;
            set;
        }

   }
}
