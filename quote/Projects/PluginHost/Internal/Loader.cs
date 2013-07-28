﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;

namespace Host.Internal
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

        static private List<IInit> FindInits(Assembly a)
        {
            List<IInit> result = new List<IInit>();

            Type[] types = a.GetTypes();
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

        private static PluginMenuItem BuildMenuItem(Type t)
        {
            MenuItemAttribute[] mias = (MenuItemAttribute[])
                t.GetCustomAttributes(typeof(MenuItemAttribute), false);
            if (mias.Length == 0)
            {
                return null;
            }

            MenuItemAttribute mia = mias[0];
            IMenuAction target =
                Activator.CreateInstance(t) as IMenuAction;

            PluginMenuItem.BuildData data = new PluginMenuItem.BuildData();

            if (target is IMenuAction)
            {
                data.Action = (IMenuAction)target;
            }

            MenuItemAttribute[] ppms = (MenuItemAttribute[])
                t.GetCustomAttributes(typeof(MenuItemAttribute), false);
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
