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

            Logger.Log("Opening " + fullPath);

            FileStream stream = File.OpenRead(fullPath);
            doc.Load(stream);

            string select;
            if (IsDebugging())
            {
                select = "/plugins/debug/*";
            }
            else
            {
                select = "/plugins/release/*";
            }

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

            Logger.Log("Loading plugin " + fullPath);

            Assembly a = Assembly.LoadFile(localFullName);

            List<PluginMenuItem> items = FindMenuItems(a);
            PluginProxy plugin = new PluginProxy(items);
            collection.Add(plugin);
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
                data.MenuAnchor = ppm.MenuAnchor;
                data.ButtonAnchor = ppm.ButtonAnchor;
                data.MenuPosition = ppm.MenuPosition;
            }

            HasIcon hasIcon = target as HasIcon;
            if (hasIcon != null)
            {
                data.Image = hasIcon.GetImage();
            }

            data.Text = mia.Text;

            return new PluginMenuItem(data);
        }
    }
}
