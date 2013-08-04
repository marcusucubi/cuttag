namespace Model.IO.Misc
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
        
        public static Customer GetById(int id)
        {
            Customer customer = new Customer();
            DB.QuoteDataBaseTableAdapters.CustomerTableAdapter adaptor = new DB.QuoteDataBaseTableAdapters.CustomerTableAdapter();
            DB.QuoteDataBase.CustomerDataTable table = null;
            table = adaptor.GetData();

            foreach (DB.QuoteDataBase.CustomerRow row in table.Rows) 
            {
                if (row.CustomerID == id) 
                {
                    customer.SetId(row.CustomerID);
                    customer.SetName(row.CustomerName.Trim());
                    return customer;
                }
            }

            return null;
        }

        public static Customer GetByName(string name)
        {
            Customer customer = new Customer();
            DB.QuoteDataBaseTableAdapters.CustomerTableAdapter adaptor = new DB.QuoteDataBaseTableAdapters.CustomerTableAdapter();
            DB.QuoteDataBase.CustomerDataTable table = null;
            table = adaptor.GetData();
            
            foreach (DB.QuoteDataBase.CustomerRow row in table.Rows) 
            {
                string cname = row.CustomerName.ToUpperInvariant().Trim();
                
                if (cname == name.ToUpperInvariant().Trim()) 
                {
                    customer.SetName(row.CustomerName.Trim());
                    customer.SetId(row.CustomerID);
                    return customer;
                }
            }

            return null;
        }
    }
}