namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public class QuoteTypeList : System.ComponentModel.StringConverter
    {
        public const string Production = "Production";
        
        public const string Pilot = "Pilot";
        
        public const string Prove = "Prove";

        public const string SingleDefinite = "Single Definate";
        
        public override TypeConverter.StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context)
        {
            List<string> l = new List<string>();
            l.AddRange(new string[] 
            {
                Production,
                Pilot,
                Prove,
                SingleDefinite
            });
            
            return new StandardValuesCollection(l);
        }

        public override bool GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
