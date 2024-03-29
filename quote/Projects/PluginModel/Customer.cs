namespace Model
{
    using System;
    using System.Linq;

    /// <summary>
    /// Represents the customer.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// The name of the customer.
        /// </summary>
        private string name;

        /// <summary>
        /// The ID of the customer.
        /// </summary>
        private int id;
        
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name of the customer.</value>
        public string Name 
        {
            get { return this.name; }
        }

        /// <summary>
        /// Gets the ID.
        /// </summary>
        /// <value>The id of the customer.</value>
        public int Id 
        {
            get { return this.id; }
        }

        /// <summary>
        /// Creates a <c>customer</c> from the input string.
        /// </summary>
        /// <param name="value">A string that contains the id and name.</param>
        /// <returns>Returns a customer object.</returns>
        public static Customer CreateFromString(string value)
        {
            int index = 0;
            index = value.IndexOf(" ", StringComparison.CurrentCulture);

            if (index == -1) 
            {
                var c = new Customer();
                c.SetName(value);
                return c;
            }

            string left = value.Substring(0, index);
            string right = value.Substring(index);

            var customer = new Customer();
            if (left.Length > 0) 
            {
                int id = 0;

                var parseOk = int.TryParse(left, out id);
                if (!parseOk) 
                {
                    id = 0;
                }

                string name = right.Trim();

                customer.SetId(id);
                customer.SetName(name);
            } 
            else 
            {
                customer.SetName(value.ToString());
            }

            return customer;
        }
        
        /// <summary>
        /// Sets the name.
        /// </summary>
        /// <param name="name">The name of the customer.</param>
        public void SetName(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Sets the ID.
        /// </summary>
        /// <param name="id">The id of the customer.</param>
        public void SetId(int id)
        {
            this.id = id;
        }

        /// <summary>
        /// The hash of the ID.
        /// </summary>
        /// <returns>Returns the hash of the ID.</returns>
        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        /// <summary>
        /// True if the IDs are the same.
        /// </summary>
        /// <param name="obj">The other customer.</param>
        /// <returns>Returns true if the IDs are the same.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as Customer;
            return this.id == other.id;
        }

        /// <summary>
        /// For debugging.
        /// </summary>
        /// <returns>A description of the customer.</returns>
        public override string ToString()
        {
            if (this.id == 0) 
            {
                return this.name;
            }
            
            return this.id + " " + this.name;
        }
    }
}
