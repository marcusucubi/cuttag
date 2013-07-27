namespace CostAnalysisWindow
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    
    using PluginHost;

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
