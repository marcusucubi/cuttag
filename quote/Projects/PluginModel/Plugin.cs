namespace Model
{
    using System;
    using System.Linq;

    public class PlugIn : Host.IInit
    {
        public void Init()
        {
            Model.ShippingDB.Initialize();
            Model.UnitOfMeasureDB.Initialize();
            Model.CustomerDB.Initialize();
        }
    }
}
