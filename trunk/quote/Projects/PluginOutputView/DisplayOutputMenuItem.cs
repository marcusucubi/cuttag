using System;
using System.Windows.Forms;

using PluginHost;
using System.Drawing;

namespace PluginOutputView
{
    [
    PluginMenuItem( 
        Text = "Output",  
        Parent = "View", 
        MenuAnchor = "ViewSep1", 
        MenuPosition=MenuPosition.Above
        )
    ]
    public class DisplayOutputMenuItem : IPluginMenuAction, HasIcon
    {
        public void Execute()
        {
            OutputPlugin.ShowOutputView();
        }

        public Image GetImage()
        {
            return GetImageByName("console");
        }

        public static Bitmap GetImageByName(string imageName)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string resourceName = asm.GetName().Name + ".Properties.Resources";
            var rm = new System.Resources.ResourceManager(resourceName, asm);
            return (Bitmap)rm.GetObject(imageName);

        }
    }
}
