namespace Model
{
    using System;
    using System.Linq;

    public class PlugIn : Host.IInit
    {
        public void Init()
        {
            Model.ShippingDB.InitializeShipping();
        }
    }
}
