using System;
using System.Collections.Specialized;
using System.Configuration;

using PluginHost;
using SampleProperties;

namespace DebugPlugin
{
    [
    PluginMenuItem(Text = "Cost Analysis", Parent = "View"),
    ]
    public class PrintConfigurationMenuItem : IPluginMenuAction
    {
        public void Execute()
        {
            ViewController.Instance.ShowTree();
        }

    }
}
