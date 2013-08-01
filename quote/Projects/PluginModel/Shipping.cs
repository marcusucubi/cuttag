namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public sealed class Shipping
    {
        private static SortedDictionary<string, decimal> dictionary = new SortedDictionary<string, decimal>();

        private static ReadOnlyCollection<string> descriptions = new ReadOnlyCollection<string>(new string[] { string.Empty });

        private Shipping()
        {
        }

        public static SortedDictionary<string, decimal> Dictionary 
        {
            get { return dictionary; }
        }

        public static ReadOnlyCollection<string> Descriptions 
        {
            get { return descriptions; }
        }

        public static void SetupDescriptions(ReadOnlyCollection<string> descriptions)
        {
            Shipping.descriptions = descriptions;
        }

        public static decimal Lookup(string description)
        {
            if (dictionary.ContainsKey(description))
            {
                return dictionary[description];
            }

            return 0;
        }
    }
}
