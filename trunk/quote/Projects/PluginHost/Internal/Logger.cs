using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace PluginHost.Internal
{
    static class Logger
    {
        static internal void Log(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
