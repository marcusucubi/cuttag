using System;
using NDepend.Attributes;

namespace PluginHost
{
    [CannotDecreaseVisibility]
    public interface IPluginMenuAction
    {
        void Execute();
    }
}
