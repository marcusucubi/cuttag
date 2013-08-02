namespace Model
{
    using System;
    using System.Collections.ObjectModel;
    
    using Model.Template;
    
    public static class CustomerList
    {
        private static Customer[] arrayCustomers;
        
        public static ReadOnlyCollection<Customer> Collection
        {
            get
            {
                System.Diagnostics.Debug.Assert(arrayCustomers != null, "CustomerList not initialized");
                
                return new ReadOnlyCollection<Customer>(CustomerList.arrayCustomers);
            }
        }
        
        public static void Init(Customer[] list)
        {
            CustomerList.arrayCustomers = list;
        }
    }
}