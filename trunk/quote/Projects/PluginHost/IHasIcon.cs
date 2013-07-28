using System;
using System.Drawing;

namespace PluginHost
{
    public interface IHasIcon
    {
        Image Image
        {
            get;
        }
    }
}
