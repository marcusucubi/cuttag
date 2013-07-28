namespace Host
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Resources;
    using System.Threading;

    public static class ResourceHelper
    {
        public static T GetResource<T>(
            Assembly assembly,
            string name)
        {
            object obj = GetResource(assembly, name);

            if (obj == null)
            {
                return default(T);
            }

            return (T)obj;
        }

        public static object GetResource(
            Assembly assembly, 
            string name)
        {
            var rm = new ResourceManager(
                "PluginExport.Properties.Resources", assembly);

            return rm.GetObject(name);
        }
    }
}
