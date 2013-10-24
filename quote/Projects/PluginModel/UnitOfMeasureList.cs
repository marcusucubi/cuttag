namespace Model
{
    using System;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Provides all of the unit of measure values
    /// </summary>
    public static class UnitOfMeasureList
    {
        /// <summary>
        /// An array of the unit of measure values.
        /// </summary>
        private static string[] arrayOfUnitOfMeasure;
        
        /// <summary>
        /// Gets a collection of unit of measure.
        /// </summary>
        public static ReadOnlyCollection<string> Collection
        {
            get
            {
                System.Diagnostics.Debug.Assert(arrayOfUnitOfMeasure != null, "UnitOfMeasure not initialized");
                
                return new ReadOnlyCollection<string>(UnitOfMeasureList.arrayOfUnitOfMeasure);
            }
        }
        
        /// <summary>
        /// Sets the values of unit of measures.
        /// </summary>
        /// <param name="list"></param>
        public static void Init(string[] list)
        {
            UnitOfMeasureList.arrayOfUnitOfMeasure = list;
        }
    }
}
