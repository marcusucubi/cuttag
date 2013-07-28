namespace CostAnalysisWindow
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    
    using Host;

    [
    MenuItem(Text = "Cost Analysis", Parent = "View"),
    ]
    public class PrintConfigurationMenuItem : IMenuAction
    {
        public void Execute()
        {
            ViewController.Instance.ShowTree();
        }
    }
}
