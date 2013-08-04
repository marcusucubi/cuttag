namespace Model.Template
{
    using System;
    using System.Linq;

    /// <summary>
    /// Represents the customer
    /// </summary>
    /// <remarks></remarks>
    public class Customer
    {
        private string name;

        private int id;
        
        public string Name 
        {
            get { return this.name; }
        }

        public int Id 
        {
            get { return this.id; }
        }

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

            Template.Customer customer = new Template.Customer();
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
        
        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetId(int id)
        {
            this.id = id;
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Customer other = obj as Customer;
            return this.id == other.id;
        }

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
