namespace Host
{
    using System;
    using System.Windows.Forms;

    public interface IMenuInit
    {
        void InitMenu(ToolStripItem menu);

        void InitButton(ToolStripButton button);
    }
}
