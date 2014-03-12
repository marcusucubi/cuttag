// <summary>
// Contains the PlugInLoader2 class.
// </summary>
// <copyright file="PlugInLoader2.cs" company="Davis Computer Services">
//  No copyright information.
// </copyright>
namespace Host.UI
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    
    using Host;
    
    /// <summary> 
    /// Responsible for loading plugins that are in plugins.xml.
    /// </summary> 
    public static class PlugInLoader2
    {
        /// <summary> 
        /// Loads all plugins.
        /// </summary> 
        /// <returns>A collection of plugins.</returns>
        public static ICollection<PlugInProxy2> Load()
        {
            var result = new List<PlugInProxy2>();
            ICollection<string> paths = PlugInLoader.LoadPaths();

            foreach (string path in paths)
            {
                PlugInProxyBuildData data = PlugInLoader.CreateBuildData(path);
                List<PlugInMenuItem> menus = FindMenuItems(data.Assembly);
            
                PlugInProxy2 proxy = new PlugInProxy2(data, menus);
                
                result.Add(proxy);
            }

            return result;
        }

        /// <summary> 
        /// Returns a list of menu item contained in the plugin.
        /// </summary> 
        /// <param name="assembly">The plugin's assembly.</param>
        /// <returns>A collection of menu items.</returns>
        private static List<PlugInMenuItem> FindMenuItems(Assembly assembly)
        {
            var result = new List<PlugInMenuItem>();

            Type[] types = assembly.GetTypes();
            foreach (Type t in types)
            {
                PlugInMenuItem item = BuildMenuItem(t);

                if (item != null)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        /// <summary> 
        /// Builds a PluginMenuItem for the input type.
        /// </summary> 
        /// <param name="menuItemType">The type of the menu item.</param>
        /// <returns>A plugin menu item.</returns>
        private static PlugInMenuItem BuildMenuItem(Type menuItemType)
        {
            var mias = (MenuItemAttribute[])
                menuItemType.GetCustomAttributes(typeof(MenuItemAttribute), false);
            if (mias.Length == 0)
            {
                return null;
            }

            MenuItemAttribute mia = mias[0];
            var target =
                Activator.CreateInstance(menuItemType) as IMenuAction;

            var data = new PlugInMenuItem.BuildData();

            if (target is IMenuAction)
            {
                data.Action = (IMenuAction)target;
            }

            var ppms = (MenuItemAttribute[])
                menuItemType.GetCustomAttributes(typeof(MenuItemAttribute), false);
            if (ppms.Length > 0)
            {
                var ppm = ppms[0];
                data.MenuName = ppm.Parent;
                data.ShowInToolBar = ppm.ShowInToolBar;
            }

            var hasIcon = target as IHasIcon;
            if (hasIcon != null)
            {
                data.Image = hasIcon.Image;
            }

            data.Text = mia.Text;

            return new PlugInMenuItem(data);
        }
    }
}
