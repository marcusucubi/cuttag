namespace Model.My.Resources
{
    using System;
    using System.Linq;

    [System.CodeDom.Compiler.GeneratedCodeAttribute(
        "System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), 
        System.Diagnostics.DebuggerNonUserCodeAttribute(), 
        System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal static class Resources
    {
        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager 
        {
            get 
            {
                if (object.ReferenceEquals(resourceMan, null)) 
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Model.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                
                return resourceMan;
            }
        }

        [System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture 
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }
    }
}
