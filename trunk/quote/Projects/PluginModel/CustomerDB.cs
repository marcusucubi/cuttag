namespace Model
{
    using System;
    using System.Collections.Generic;

    using Model.Template;
    
    public static class CustomerDB
    {
        public static void Initialize()
        {
            DB.QuoteDataBaseTableAdapters.CustomerTableAdapter adaptor = 
                new DB.QuoteDataBaseTableAdapters.CustomerTableAdapter();
            DB.QuoteDataBase.CustomerDataTable table = null;
            
            table = adaptor.GetData();
            
            List<Customer> list = new List<Customer>();
            
            foreach (DB.QuoteDataBase.CustomerRow row in table.Rows) 
            {
                if (row.CustomerID > 0) 
                {
                    Customer c = new Customer();
                    c.SetId(row.CustomerID);
                    c.SetName(row.CustomerName.Trim());
                    list.Add(c);
                }
            }
            
            CustomerList.Init(list.ToArray());
        }
    }
}
