using System;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;

using Host;
using PluginOutputView;

namespace DebugPlugin
{
    [
    MenuItem(Text = "Current Directory", Parent = "Debug"), 
    ]
    public class PrintDirectoryMenuItem : IMenuAction
    {
        public void Execute()
        {
            OutputPlugin.ShowOutputView();

            Console.WriteLine();
            Console.WriteLine(Directory.GetCurrentDirectory());
        }

    }

}
