namespace CostAnalysisWindow
{
    using System;
    
    using Host.UI;

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
