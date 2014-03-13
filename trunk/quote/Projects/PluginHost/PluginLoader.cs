// <summary>
// Contains the Loader class.
// </summary>
// <copyright file="Loader.cs" company="Davis Computer Services">
//  No copyright information.
// </copyright>
namespace Host
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Xml;
    
    /// <summary> 
    /// Responsible for loading plugins that are in plugins.xml.
    /// </summary> 
    public static class PlugInLoader
    {
        /// <summary> 
        /// Loads all plugins.
        /// </summary> 
        /// <returns>A collection of plugins.</returns>
        public static ICollection<PlugInProxy> Load()
        {
            var result = new PlugInCollection();

            ICollection<string> paths = LoadPaths();

            foreach (string path in paths)
            {
                PlugInProxyBuildData data = CreateBuildData(path);
                var proxy = new PlugInProxy(data);
                
                result.Add(proxy);
            }
            
            InitializePlugIns(result);

            return result;
        }

        /// <summary> 
        /// Returns the file names contained in plugins.xml.
        /// </summary> 
        /// <returns>The file names contained in plugins.xml.</returns>
        public static ICollection<string> LoadPaths()
        {
            var paths = new List<string>();
            var doc = new XmlDocument();

            string fullPath = Path.GetFullPath("plugins.xml");

            Console.WriteLine("Opening " + fullPath);

            FileStream stream = File.OpenRead(fullPath);
            doc.Load(stream);

            string select;
            select = "/plugins/*";

            XmlNodeList nodes = doc.SelectNodes(select);
            foreach (XmlNode node in nodes)
            {
                string path = node.Attributes["path"].Value;
                paths.Add(path);
            }

            return paths;
        }

        /// <summary> 
        /// Loads the input plugin.
        /// </summary> 
        /// <param name="path" >The path of the assembly file.</param>
        /// <returns>A build data object.</returns>
        public static PlugInProxyBuildData CreateBuildData(string path)
        {
            var result = new PlugInProxyBuildData();
            
            string fullPath = Path.GetFullPath(path);
            string fileName = Path.GetFileName(path);
            string localFullName = Path.GetFullPath(fileName);

            Console.WriteLine("Loading plugin " + fullPath);

            Assembly a = Assembly.LoadFile(localFullName);

            FindRegisteredClasses(a);
            List<IStartup> inits = FindInits(a);
            
            result.Assembly = a;
            result.SetInitList(inits);
            
            return result;
        }
        
        /// <summary>
        /// Calls the initialize method on each plug in.
        /// </summary>
        /// <param name="plugins">The collection of plug ins.</param>
        public static void InitializePlugIns(ICollection<PlugInProxy> plugins)
        {
            foreach (PlugInProxy proxy in plugins)
            {
                foreach (IStartup init in proxy.ClassesToInit)
                {
                    init.Initialize();
                }
            }
        }
        
        /// <summary> 
        /// Updates App.RegisteredClasses.
        /// </summary> 
        /// <param name="assembly">The plugin's assembly.</param>
        private static void FindRegisteredClasses(Assembly assembly)
        {
            Type[] types = assembly.GetTypes();
            foreach (Type t in types)
            {
                var mias = (RegisterAttribute[])
                    t.GetCustomAttributes(typeof(RegisterAttribute), false);
                if (mias.Length == 0)
                {
                    continue;
                }

                RegisterAttribute r = mias[0];

                App.RegisteredClasses.Add(r.Key, t);
            }
        }

        /// <summary> 
        /// Returns all classes that implement <c>IInit</c>.
        /// </summary> 
        /// <param name="assembly">The plugin's assembly.</param>
        /// <returns>A collection of <c>IInit's</c>.</returns>
        private static List<IStartup> FindInits(Assembly assembly)
        {
            var result = new List<IStartup>();

            Type[] types = assembly.GetTypes();
            foreach (Type t in types)
            {
                if (!typeof(IStartup).IsAssignableFrom(t))
                {
                    continue;
                }

                var target = Activator.CreateInstance(t) as IStartup;

                result.Add(target);
            }

            return result;
        }
    }
}
