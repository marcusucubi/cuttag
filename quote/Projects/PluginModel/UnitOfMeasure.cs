namespace Model
{
    using System;
    using System.Collections.ObjectModel;

    public static class UnitOfMeasure
    {
        private static string[] arrayOfUnitOfMeasure;
        
        public static ReadOnlyCollection<string> Collection
        {
            get
            {
                System.Diagnostics.Debug.Assert(arrayOfUnitOfMeasure != null, "UnitOfMeasure not initialized");
                
                return new ReadOnlyCollection<string>(UnitOfMeasure.arrayOfUnitOfMeasure);
            }
        }
        
        public static void Init(string[] list)
        {
            UnitOfMeasure.arrayOfUnitOfMeasure = list;
        }
    }
}
