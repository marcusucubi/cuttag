using System;
using System.Windows.Forms;

namespace Host
{
    public interface IMenuInit
    {
        void InitMenu(ToolStripItem menu);

        void InitButton(ToolStripButton button);
    }
}
