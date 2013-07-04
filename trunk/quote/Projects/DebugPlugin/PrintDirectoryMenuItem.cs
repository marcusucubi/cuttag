using System;
using System.Configuration;
using System.Collections.Specialized;

using PluginHost;
using PluginOutputView;
using System.IO;

namespace DebugPlugin
{
    [
    PluginMenuItem(Text = "Current Directory", Parent = "Debug"), 
    ]
    public class PrintDirectoryMenuItem : IPluginMenuAction
    {
        public void Execute()
        {
            OutputPlugin.ShowOutputView();

            Console.WriteLine();
            Console.WriteLine(Directory.GetCurrentDirectory());
        }

    }

}
