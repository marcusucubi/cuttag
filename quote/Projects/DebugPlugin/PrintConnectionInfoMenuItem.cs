using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PluginHost;
using System.Configuration;
using PluginOutputView;

namespace DebugPlugin
{
    [
    PluginMenuItem(Text = "Connection Information", Parent = "Debug"),
    ]
    public class PrintConnectionInfoMenuItem : IPluginMenuAction
    {

        public void Execute()
        {
            OutputPlugin.ShowOutputView();

            Console.WriteLine();
            ReadConnectionStrings();
        }

        // Get the ConnectionStrings section.         
        // This function uses the ConnectionStrings  
        // property to read the connectionStrings 
        // configuration section. 
        public static void ReadConnectionStrings()
        {

            // Get the ConnectionStrings collection.
            ConnectionStringSettingsCollection connections =
                ConfigurationManager.ConnectionStrings;

            if (connections.Count != 0)
            {
                Console.WriteLine();
                Console.WriteLine("Using ConnectionStrings property.");
                Console.WriteLine("Connection strings:");

                // Get the collection elements. 
                foreach (ConnectionStringSettings connection in
                  connections)
                {
                    string name = connection.Name;
                    string provider = connection.ProviderName;
                    string connectionString = connection.ConnectionString;

                    Console.WriteLine();
                    Console.WriteLine("Name:               {0}",
                      name);
                    Console.WriteLine("Connection string:  {0}",
                      connectionString);
                    Console.WriteLine("Provider:            {0}",
                       provider);
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("No connection string is defined.");
                Console.WriteLine();
            }
        }
    }
}
