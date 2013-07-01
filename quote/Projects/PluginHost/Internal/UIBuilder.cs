using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace PluginHost.Internal
{
    static class UIBuilder
    {
        static private void BuildUI(
            PluginProxy plugin,
            MenuStrip menu)
        {
            AssignMenuItems(plugin, menu);
        }

        static internal void BuildUI(
            PluginCollection collection,
            MenuStrip menu)
        {
            foreach (PluginProxy plugin in collection)
            {
                BuildUI(plugin, menu);
            }
        }

        static private void AssignMenuItems(
            PluginProxy plugin,
            MenuStrip menuStrip)
        {
            foreach (PluginMenuItem item in plugin.PluginMenuItems)
            {

                ToolStripItemCollection parent = menuStrip.Items;
                if (item.MenuName.Length > 0)
                {
                    string name = item.MenuName;
                    ToolStripMenuItem menuItem = FindParentMenu(menuStrip, name);

                    AddMenuItem(item, menuItem.DropDownItems);
                }
                else
                {
                    AddMenuItem(item, menuStrip.Items);
                }
            }

        }

        private static void AddMenuItem(
            PluginMenuItem item,
            ToolStripItemCollection items)
        {
            var i1 = new ToolStripMenuItem();
            i1.Text = item.Text;
            i1.Click += (sender, e) => { item.Action.Execute(); };

            int index = FindIndex(items);
            items.Insert(index, i1);
        }

        private static ToolStripMenuItem FindParentMenu(
            MenuStrip menuStrip, 
            string name)
        {
            ToolStripMenuItem menuItem = null;

            foreach (ToolStripItem tsi in menuStrip.Items)
            {
                if (tsi.Text.TrimStart('&').ToLower() == name.ToLower())
                {
                    ToolStripMenuItem temp = tsi as ToolStripMenuItem;
                    menuItem = temp;
                }
            }

            if (menuItem == null)
            {
                menuItem = new ToolStripMenuItem();
                menuItem.Text = name;
                menuStrip.Items.Add(menuItem);
            }

            return menuItem;
        }

        static private int FindIndex(
            ToolStripItemCollection collection)
        {
            int result = 0;

            for (int i = 0; i < collection.Count; i++)
            {
                ToolStripItem item = collection[i];

                if (item is ToolStripSeparator)
                {
                    result = i - 1;
                    break;
                }
            }

            return result;
        }

    }
}
