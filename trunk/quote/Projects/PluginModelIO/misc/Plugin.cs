namespace Model.IO.Misc
{
    using System;

    public class PlugIn : Host.IInit
    {
        public void Init()
        {
            Model.IO.Misc.UnitOfMeasureDB.Initialize();
            Model.IO.Misc.CustomerDB.Initialize();
        }
    }
}
