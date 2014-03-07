namespace Model
{
    using System;
    using System.Collections.ObjectModel;
    
    /// <summary>
    /// Contains a list of all customers.
    /// </summary>
    public static class CustomerList
    {
        /// <summary>
        /// Holds all of the customers.
        /// </summary>
        private static Customer[] arrayCustomers;
        
        /// <summary>
        /// Gets a collection of all customers.
        /// </summary>
        /// <value>A collection of customer objects.</value>
        public static ReadOnlyCollection<Customer> Collection
        {
            get
            {
                System.Diagnostics.Debug.Assert(arrayCustomers != null, "CustomerList not initialized");
                
                return new ReadOnlyCollection<Customer>(CustomerList.arrayCustomers);
            }
        }
        
        /// <summary>
        /// Sets the customers.
        /// </summary>
        /// <param name="list">An array of customer objects.</param>
        public static void Init(Customer[] list)
        {
            CustomerList.arrayCustomers = list;
        }
    }
}