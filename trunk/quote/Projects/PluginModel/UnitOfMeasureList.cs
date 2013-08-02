namespace Model
{
    using System;
    using System.Collections.ObjectModel;

    public static class UnitOfMeasureList
    {
        private static string[] arrayOfUnitOfMeasure;
        
        public static ReadOnlyCollection<string> Collection
        {
            get
            {
                System.Diagnostics.Debug.Assert(arrayOfUnitOfMeasure != null, "UnitOfMeasure not initialized");
                
                return new ReadOnlyCollection<string>(UnitOfMeasureList.arrayOfUnitOfMeasure);
            }
        }
        
        public static void Init(string[] list)
        {
            UnitOfMeasureList.arrayOfUnitOfMeasure = list;
        }
    }
}
