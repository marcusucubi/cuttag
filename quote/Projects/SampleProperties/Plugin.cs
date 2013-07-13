using System;
using Model;
using WeifenLuo.WinFormsUI.Docking;

namespace SampleProperties
{
    public class Plugin : PluginHost.IPluginInit
    {
        public void Init() 
        {
            Model.ModelEvents.TemplateViewed += ModelEvents_TemplateViewed;
        }

        public void ModelEvents_TemplateViewed(object souce, EventArgs args)
        {
            ViewController.Instance.ShowTree();
        }

    }
}
