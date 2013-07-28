using System;
using System.Windows.Forms;
using System.Drawing;

using Host;

namespace PluginOutputView
{
    [
    MenuItem( 
        Text = "Output",  
        Parent = "View"
        )
    ]
    public class DisplayOutputMenuItem : IMenuAction, IHasIcon
    {
        public void Execute()
        {
            OutputPlugin.ShowOutputView();
        }

        public Image Image
        {
            get
            {
                return GetImageByName("console");
            }
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
