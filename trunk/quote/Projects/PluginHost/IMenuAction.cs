using System;
using NDepend.Attributes;

namespace Host
{
    [CannotDecreaseVisibility]
    public interface IMenuAction
    {
        void Execute();
    }
}
