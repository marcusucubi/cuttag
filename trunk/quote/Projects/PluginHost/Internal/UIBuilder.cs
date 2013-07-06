﻿using System;
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
            item.ToolStripItem = i1;

            IPluginMenuInit init = item.Action as IPluginMenuInit;
            if (init != null)
            {
                init.Init(i1);
            }

            if (item.Image != null)
            {
                i1.Image = item.Image;
                i1.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            }

            int index = FindIndex(item, items);

            if (item.MenuPosition == MenuPosition.Below)
            {
                if (index < items.Count)
                {
                    index++;
                }
            }

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
            PluginMenuItem menuItem,
            ToolStripItemCollection collection)
        {
            int result = 0;
            List<TopBottom> indexes = BuildMenuIndexArray(collection);

            foreach (ToolStripItem item in collection)
            {
                if (item.Name == menuItem.Anchor)
                {
                    result = collection.IndexOf(item);
                    break;
                }

                if (item.Tag == null)
                {
                    continue;
                }

                if (item.Tag.ToString() == menuItem.Anchor)
                {
                    result = collection.IndexOf(item);
                    break;
                }
            }

            return result;
        }

        private struct TopBottom
        {
            public int Top;
            public int Bottom;
        }

        static private List<TopBottom> BuildMenuIndexArray(
            ToolStripItemCollection collection)
        {
            List<TopBottom> result = new List<TopBottom>();

            TopBottom current = new TopBottom();

            for (int i = 0; i < collection.Count; i++)
            {
                ToolStripItem item = collection[i];
                current.Bottom = i;

                if (item is ToolStripSeparator)
                {
                    result.Add(current);
                    current = new TopBottom();
                    current.Top = i + 1;
                    current.Bottom = i + 2;
                }
            }

            result.Add(current);

            return result;
        }

    }
}
