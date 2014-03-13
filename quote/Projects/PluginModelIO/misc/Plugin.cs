namespace Model.IO.Misc
{
    using System;

    public class PlugIn : Host.IStartup
    {
        public void Initialize()
        {
            UnitOfMeasureDB.Initialize();
            CustomerDB.Initialize();
        }
    }
}
