namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Converts a unit of measure to a string.
    /// </summary>
    public class UnitOfMeasureConverter : StringConverter
    {
        /// <summary>
        /// Returns the standard values
        /// </summary>
        /// <param name="context">the context</param>
        /// <returns>The standard values</returns>
        public override TypeConverter.StandardValuesCollection 
            GetStandardValues(System.ComponentModel.ITypeDescriptorContext context)
        {
            List<string> l = UnitOfMeasureList.Collection.ToList();

            l.Sort();

            return new StandardValuesCollection(l);
        }

        /// <summary>
        /// Returns the stadard values
        /// </summary>
        /// <param name="context">the context</param>
        /// <returns>the standard values</returns>
        public override bool GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext context)
        {
            return false;
        }

        /// <summary>
        /// Returns true if supported
        /// </summary>
        /// <param name="context">the context</param>
        /// <returns>Returns true if supported</returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
