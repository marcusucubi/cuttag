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
                List<IStartup2> startup2s = FindStartup2s(data.Assembly);
            
                var proxy = new PlugInProxy2(data, menus, startup2s);
                
                result.Add(proxy);
            }
            
            InitializePlugIns(result);

            return result;
        }

        /// <summary>
        /// Calls the initialize method on each plug in.
        /// </summary>
        /// <param name="plugins">The collection of plug ins.</param>
        public static void InitializePlugIns(ICollection<PlugInProxy2> plugins)
        {
            List<PlugInProxy> list = new List<PlugInProxy>();
            
            foreach (PlugInProxy2 proxy in plugins)
            {
                list.Add(proxy);
            }
            
            PlugInLoader.InitializePlugIns(list);
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
        /// Find the classes that implement <see cref="IStartup2" />.
        /// </summary>
        /// <param name="assembly">The assembly to use.</param>
        /// <returns>A collection of <see cref="IStartup2" /></returns>
        private static List<IStartup2> FindStartup2s(Assembly assembly)
        {
            var result = new List<IStartup2>();

            Type[] types = assembly.GetTypes();
            foreach (Type t in types)
            {
                if (!typeof(IStartup2).IsAssignableFrom(t))
                {
                    continue;
                }

                var target = Activator.CreateInstance(t) as IStartup2;

                result.Add(target);
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
