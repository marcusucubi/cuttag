﻿using System;
using System.Windows.Forms;

using PluginHost;

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
    public class DisplayOutputMenuItem : IPluginMenuAction
    {
        public void Execute()
        {
            OutputPlugin.ShowOutputView();
        }
    }
}
