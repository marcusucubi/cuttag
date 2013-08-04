namespace Model.IO.Misc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using DB;

    public static class UnitOfMeasureDB
    {
        public static void Initialize()
        {
            List<string> l = new List<string>();

            DB.QuoteDataBase._UnitOfMeasureDataTable table = null;
            table = new DB.QuoteDataBaseTableAdapters._UnitOfMeasureTableAdapter().GetData();
            foreach (DB.QuoteDataBase._UnitOfMeasureRow row in table.Rows) 
            {
                l.Add(row.Name);
            }
            
            l.Sort();
            
            UnitOfMeasureList.Init(l.ToArray());
        }
    }
}
