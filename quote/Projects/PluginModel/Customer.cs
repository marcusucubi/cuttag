namespace Model
{
    using System;
    using System.Linq;

    /// <summary>
    /// Represents the customer
    /// </summary>
    /// <remarks></remarks>
    public class Customer
    {
        /// <summary>
        /// The name.
        /// </summary>
        private string name;

        /// <summary>
        /// The ID.
        /// </summary>
        private int id;
        
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name 
        {
            get { return this.name; }
        }

        /// <summary>
        /// Gets the ID.
        /// </summary>
        public int Id 
        {
            get { return this.id; }
        }

        /// <summary>
        /// Creates a <c>customer</c> from the input string.
        /// </summary>
        /// <param name="value">A string that contains the id and name.</param>
        /// <returns>Returns a cusomter object.</returns>
        public static Customer CreateFromString(string value)
        {
            int index = 0;
            index = value.IndexOf(" ", StringComparison.CurrentCulture);

            if (index == -1) 
            {
                Customer c = new Customer();
                c.SetName(value);
                return c;
            }

            string left = value.Substring(0, index);
            string right = value.Substring(index);

            Customer customer = new Customer();
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
        /// <param name="name">The name.</param>
        public void SetName(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Sets the ID.
        /// </summary>
        /// <param name="id">The id.</param>
        public void SetId(int id)
        {
            this.id = id;
        }

        /// <summary>
        /// The hashtag of the ID.
        /// </summary>
        /// <returns>The hashtag of the ID.</returns>
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
            Customer other = obj as Customer;
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
