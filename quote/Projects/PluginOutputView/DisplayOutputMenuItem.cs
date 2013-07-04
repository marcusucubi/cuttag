using System;
using System.Windows.Forms;

using PluginHost;

namespace PluginOutputView
{
    [
    PluginMenuItem(Text = "Output", Parent = "View"), 
    ]
    public class DisplayOutputMenuItem : IPluginMenuAction
    {
        public void Execute()
        {
            OutputPlugin.ShowOutputView();
        }
    }
}
