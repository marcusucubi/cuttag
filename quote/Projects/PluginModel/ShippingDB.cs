namespace Model
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    public sealed class ShippingDB
    {
        private ShippingDB()
        {
        }

        public static void InitializeShipping()
        {
            DB.QuoteDataBase._ShippingDataTable table = new DB.QuoteDataBase._ShippingDataTable();
            DB.QuoteDataBaseTableAdapters._ShippingTableAdapter adapter = new DB.QuoteDataBaseTableAdapters._ShippingTableAdapter();
            adapter.Fill(table);

            string[] a = new string[table.Count + 1];
            int i = 0;
            foreach (DB.QuoteDataBase._ShippingRow s in table.Rows) 
            {
                a[i] = s.Index;
                Shipping.Dictionary.Add(s.Index, s.Cost);
                i += 1;
            }

            Shipping.SetupDescriptions(new ReadOnlyCollection<string>(a));
        }
    }
}
