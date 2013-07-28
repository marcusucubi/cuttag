namespace Host.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;
    using System.Windows.Forms;

    static class UIBuilder
    {
        internal static void BuildUI(
            PluginCollection collection,
            MenuStrip menu,
            ToolStrip toolStrip)
        {
            foreach (PluginProxy plugin in collection)
            {
                BuildUI(plugin, menu, toolStrip);
            }
        }

        private static void BuildUI(
            PluginProxy plugin,
            MenuStrip menu,
            ToolStrip toolStrip)
        {
            DoInit(plugin);
            AssignMenuItems(plugin, menu);
            AssignButtonItems(plugin, toolStrip);
        }

        private static void DoInit(PluginProxy plugin)
        {
            foreach (IInit init in plugin.Plugins)
            {
                init.Init();
            }
        }

        private static void AssignMenuItems(
            PluginProxy plugin,
            MenuStrip menuStrip)
        {
            List<PluginMenuItem> sortedItems = new List<PluginMenuItem>();
            sortedItems.AddRange(plugin.PluginMenuItems);
            sortedItems.Sort();

            foreach (PluginMenuItem item in sortedItems)
            {

                if (item.MenuName.Length > 0)
                {
                    string name = item.MenuName;
                    ToolStripMenuItem menuItem = FindParentMenu(menuStrip, name);

                    AddMenuItem(plugin, item, menuItem.DropDownItems);
                }
                else
                {
                    AddMenuItem(plugin, item, menuStrip.Items);
                }
            }

        }

        private static void AssignButtonItems(
            PluginProxy plugin,
            ToolStrip toolStrip)
        {
            foreach (PluginMenuItem item in plugin.PluginMenuItems)
            {
                if (item.ShowInToolbar)
                {
                    AddToolItem(item, toolStrip, plugin);
                }
            }
        }

        private static void AddToolItem(
            PluginMenuItem item,
            ToolStrip toolStrip,
            PluginProxy plugin)
        {
            var i1 = new ToolStripButton();
            i1.Text = item.Text;
            i1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            i1.Click += (sender, e) => { item.Action.Execute(); };

            IMenuInit init = item.Action as IMenuInit;
            if (init != null)
            {
                init.InitButton(i1);
            }

            if (item.Image != null)
            {
                i1.Image = item.Image;
                i1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            }

            int index = FindIndex(toolStrip.Items, plugin);

            if (index < toolStrip.Items.Count)
            {
                index++;
            }

            toolStrip.Items.Insert(index, i1);
        }

        private static void AddMenuItem(
            PluginProxy plugin,
            PluginMenuItem item,
            ToolStripItemCollection items)
        {

            var i1 = new ToolStripMenuItem();
            i1.Text = item.Text;
            i1.Click += (sender, e) => { item.Action.Execute(); };

            IMenuInit init = item.Action as IMenuInit;
            if (init != null)
            {
                init.InitMenu(i1);
            }

            if (item.Image != null)
            {
                i1.Image = item.Image;
                i1.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            }

            int index = FindIndex(items, plugin);

            if (index < items.Count)
            {
                index++;
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
                if (tsi.Text.TrimStart('&').ToLower(CultureInfo.CurrentCulture) == 
                    name.ToLower(CultureInfo.CurrentCulture))
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

        private static int FindIndex(
            ToolStripItemCollection collection,
            PluginProxy plugin)
        {
            int result = -1;

            foreach (ToolStripItem item in collection)
            {
                if (item.Name == plugin.Assembly.GetName().Name)
                {
                    result = collection.IndexOf(item);
                    break;
                }
            }

            if (result == -1)
            {
                var sep = new ToolStripSeparator();
                sep.Name = plugin.Assembly.GetName().Name;
                collection.Add(sep);
                result = collection.IndexOf(sep);
            }

            return result;
        }

    }
}
