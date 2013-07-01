using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

using PluginHost;
using PluginOutputView;

namespace DebugPlugin
{
    [
    PluginMenuItem(Text = "Print Debug Info", Parent = "Debug"), 
    ]
    public class PrintDebugInfoMenuItem : IPluginMenuAction
    {
        public void Execute()
        {
            OutputPlugin.ShowOutputView();

            Console.WriteLine("Debug Info");
            Console.WriteLine("Current Directory: " + System.IO.Directory.GetCurrentDirectory());
        }
    }
}
