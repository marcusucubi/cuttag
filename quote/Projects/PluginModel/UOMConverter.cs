namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public class UnitOfMeasureConverter : StringConverter
    {
        public override TypeConverter.StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context)
        {
            List<string> l = new List<string>();

            DB.QuoteDataBase._UnitOfMeasureDataTable table = null;
            table = new DB.QuoteDataBaseTableAdapters._UnitOfMeasureTableAdapter().GetData();
            foreach (DB.QuoteDataBase._UnitOfMeasureRow row in table.Rows) 
            {
                l.AddRange(new string[] { row.Name });
            }
            
            l.Sort();

            return new StandardValuesCollection(l);
        }

        public override bool GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext context)
        {
            return false;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
