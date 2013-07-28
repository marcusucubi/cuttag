using System;
using System.Collections.Specialized;
using System.Configuration;

using Host;
using PluginOutputView;

namespace DebugPlugin
{
    [
    MenuItem(Text = "Configuration Settings", Parent = "Debug"),
    ]
    public class PrintConfigurationMenuItem : IMenuAction
    {
        public void Execute()
        {
            OutputPlugin.ShowOutputView();

            ReadAppSettings();
        }

        public static void ReadAppSettings()
        {
            try
            {
                // Get the AppSettings section.
                NameValueCollection appSettings =
                    ConfigurationManager.AppSettings;

                // Get the AppSettings section elements.
                Console.WriteLine();
                Console.WriteLine("Application settings:");

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("[ReadAppSettings: {0}]",
                    "AppSettings is empty Use GetSection command first.");
                }
                for (int i = 0; i < appSettings.Count; i++)
                {
                    Console.WriteLine("#{0} Key: {1} Value: {2}",
                    i, appSettings.GetKey(i), appSettings[i]);
                }
            }
            catch (ConfigurationErrorsException e)
            {
                Console.WriteLine("[ReadAppSettings: {0}]",
                    e.ToString());
            }
        }
    }
}
