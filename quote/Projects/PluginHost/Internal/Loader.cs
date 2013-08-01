// <summary>
// Contains the Loader class.
// </summary>
// <copyright file="Loader.cs" company="Davis Computer Services">
//  No copyright information.
// </copyright>
namespace Host.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Xml;
    
    /// <summary> 
    /// Responsible for loading plugins that are in plugins.xml.
    /// </summary> 
    internal static class Loader
    {
        /// <summary> 
        /// Loads all plugins.
        /// </summary> 
        /// <returns>A collection of plugins.</returns>
        internal static PluginCollection Load()
        {
            PluginCollection result = new PluginCollection();

            List<string> paths = LoadPaths();

            foreach (string path in paths)
            {
                ProcessPlugin(result, path);
            }

            return result;
        }

        /// <summary> 
        /// Returns the file names contained in plugins.xml.
        /// </summary> 
        /// <returns>The file names contained in plugins.xml.</returns>
        private static List<string> LoadPaths()
        {
            List<string> paths = new List<string>();

            XmlDocument doc = new XmlDocument();

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
        /// <param name="collection" >A collection of plugins.</param>
        /// <param name="path" >The path of the assembly file.</param>
        private static void ProcessPlugin(
            PluginCollection collection,
            string path)
        {
            string fullPath = Path.GetFullPath(path);
            string fileName = Path.GetFileName(path);
            string localFullName = Path.GetFullPath(fileName);

            Console.WriteLine("Loading plugin " + fullPath);

            try
            {
                Assembly a = Assembly.LoadFile(localFullName);

                FindRegisteredClasses(a);
                List<PluginMenuItem> items = FindMenuItems(a);
                List<IInit> inits = FindInits(a);
                PluginProxy plugin = new PluginProxy(items, inits, a);
                collection.Add(plugin);
            }
            catch (System.IO.IOException e)
            {
                string text = "Error loading " + fileName + "\r\n" + e.Message;
                System.Windows.Forms.MessageBox.Show(
                    text, 
                    string.Empty, 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.None, 
                    MessageBoxDefaultButton.Button1, 
                    (MessageBoxOptions)0, 
                    false);
            }
        }

        /// <summary> 
        /// Returns a list of menu item contained in the plugin.
        /// </summary> 
        /// <param name="assembly">The plugin's assembly.</param>
        /// <returns>A collection of menu items.</returns>
        private static List<PluginMenuItem> FindMenuItems(Assembly assembly)
        {
            List<PluginMenuItem> result = new List<PluginMenuItem>();

            Type[] types = assembly.GetTypes();
            foreach (Type t in types)
            {
                PluginMenuItem item = BuildMenuItem(t);

                if (item != null)
                {
                    result.Add(item);
                }
            }

            return result;
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
                RegisterAttribute[] mias = (RegisterAttribute[])
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
        private static List<IInit> FindInits(Assembly assembly)
        {
            List<IInit> result = new List<IInit>();

            Type[] types = assembly.GetTypes();
            foreach (Type t in types)
            {
                if (!typeof(IInit).IsAssignableFrom(t))
                {
                    continue;
                }

                IInit target = Activator.CreateInstance(t) as IInit;

                result.Add(target);
            }

            return result;
        }

        /// <summary> 
        /// Builds a PluginMenuItem for the input type.
        /// </summary> 
        /// <param name="menuItemType">The type of the menu item.</param>
        /// <returns>A plugin menu item.</returns>
        private static PluginMenuItem BuildMenuItem(Type menuItemType)
        {
            MenuItemAttribute[] mias = (MenuItemAttribute[])
                menuItemType.GetCustomAttributes(typeof(MenuItemAttribute), false);
            if (mias.Length == 0)
            {
                return null;
            }

            MenuItemAttribute mia = mias[0];
            IMenuAction target =
                Activator.CreateInstance(menuItemType) as IMenuAction;

            PluginMenuItem.BuildData data = new PluginMenuItem.BuildData();

            if (target is IMenuAction)
            {
                data.Action = (IMenuAction)target;
            }

            MenuItemAttribute[] ppms = (MenuItemAttribute[])
                menuItemType.GetCustomAttributes(typeof(MenuItemAttribute), false);
            if (ppms.Length > 0)
            {
                MenuItemAttribute ppm = ppms[0];
                data.MenuName = ppm.Parent;
                data.ShowInToolbar = ppm.ShowInToolBar;
            }

            IHasIcon hasIcon = target as IHasIcon;
            if (hasIcon != null)
            {
                data.Image = hasIcon.Image;
            }

            data.Text = mia.Text;

            return new PluginMenuItem(data);
        }
    }
}
