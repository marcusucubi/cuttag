using System;
using System.Collections.Specialized;
using System.Configuration;

using PluginHost;
using SampleProperties;

namespace DebugPlugin
{
    [
    PluginMenuItem(Text = "Calc Tree", Parent = "View"),
    ]
    public class PrintConfigurationMenuItem : IPluginMenuAction
    {
        public void Execute()
        {
            ViewController.Instance.ShowTree();
        }

    }
}
