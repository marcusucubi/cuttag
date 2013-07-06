using System;
using System.Windows.Forms;

namespace PluginHost
{
    public interface IPluginMenuInit
    {
        void Init(ToolStripItem item);
    }
}
