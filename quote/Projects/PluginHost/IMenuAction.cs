namespace Host
{
    using System;
    using NDepend.Attributes;

    [CannotDecreaseVisibility]
    public interface IMenuAction
    {
        void Execute();
    }
}
