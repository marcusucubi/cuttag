using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Xml;
using System.Drawing;

namespace PluginHost.Internal
{
    static class Loader
    {
        static internal PluginCollection Load()
        {
            PluginCollection result = new PluginCollection();

            List<string> paths = LoadPaths();

            foreach (string path in paths)
            {
                ProcessPlugin(result, path);
            }

            return result;
        }

        static private bool IsDebugging()
        {
            return Debugger.IsAttached;
        }

        static private List<string> LoadPaths()
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

        static private void ProcessPlugin(
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
                List<IPluginInit> inits = FindInits(a);
                PluginProxy plugin = new PluginProxy(items, inits, a);
                collection.Add(plugin);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Error loading " + fileName + "\r\n" + e.Message);
            }
        }

        static private List<PluginMenuItem> FindMenuItems(Assembly a)
        {
            List<PluginMenuItem> result = new List<PluginMenuItem>();

            Type[] types = a.GetTypes();
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

        static private void FindRegisteredClasses(Assembly a)
        {
            Type[] types = a.GetTypes();
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

        static private List<IPluginInit> FindInits(Assembly a)
        {
            List<IPluginInit> result = new List<IPluginInit>();

            Type[] types = a.GetTypes();
            foreach (Type t in types)
            {
                if (!typeof(IPluginInit).IsAssignableFrom(t))
                {
                    continue;
                }

                IPluginInit target =
                    Activator.CreateInstance(t) as IPluginInit;

                result.Add(target);
            }

            return result;
        }

        private static PluginMenuItem BuildMenuItem(Type t)
        {
            PluginMenuItemAttribute[] mias = (PluginMenuItemAttribute[])
                t.GetCustomAttributes(typeof(PluginMenuItemAttribute), false);
            if (mias.Length == 0)
            {
                return null;
            }

            PluginMenuItemAttribute mia = mias[0];
            IPluginMenuAction target =
                Activator.CreateInstance(t) as IPluginMenuAction;

            PluginMenuItem.BuildData data = new PluginMenuItem.BuildData();

            if (target is IPluginMenuAction)
            {
                data.Action = (IPluginMenuAction)target;
            }

            PluginMenuItemAttribute[] ppms = (PluginMenuItemAttribute[])
                t.GetCustomAttributes(typeof(PluginMenuItemAttribute), false);
            if (ppms.Length > 0)
            {
                PluginMenuItemAttribute ppm = ppms[0];
                data.MenuName = ppm.Parent;
                data.ShowInToolbar = ppm.ShowInToolbar;
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
