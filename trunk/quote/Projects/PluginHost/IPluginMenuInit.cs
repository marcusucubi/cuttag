using System;
using System.Windows.Forms;

namespace PluginHost
{
    public interface IPluginMenuInit
    {
        void InitMenu(ToolStripItem menu);

        void InitButton(ToolStripButton button);
    }
}
