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
        /// Returns the standard values.
        /// </summary>
        /// <param name="context">The context object.</param>
        /// <returns>The standard values.</returns>
        public override TypeConverter.StandardValuesCollection 
            GetStandardValues(System.ComponentModel.ITypeDescriptorContext context)
        {
            List<string> l = UnitOfMeasureList.Collection.ToList();

            l.Sort();

            return new StandardValuesCollection(l);
        }

        /// <summary>
        /// Returns the standard values.
        /// </summary>
        /// <param name="context">The context object.</param>
        /// <returns>The standard values.</returns>
        public override bool GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext context)
        {
            return false;
        }

        /// <summary>
        /// Always returns true.
        /// </summary>
        /// <param name="context">The context object.</param>
        /// <returns>Returns true if supported.</returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
