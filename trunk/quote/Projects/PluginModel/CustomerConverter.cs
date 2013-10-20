namespace Model
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Linq;
    
    using Model.Template;

    /// <summary>
    /// Provides UI support to display the customers in a dropdown list.
    /// </summary>
    public class CustomerConverter : ExpandableObjectConverter
    {
        /// <summary>
        /// Always returns true.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Returns true.</returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Always returns true.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Returns true.</returns>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Returns the customers in the CustomerList.
        /// </summary>
        /// <param name="context">The Context.</param>
        /// <returns>The customers in the CustomerList.</returns>
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            StandardValuesCollection svc = 
                new StandardValuesCollection(CustomerList.Collection);
            return svc;
        }

        /// <summary>
        /// Returns true for strings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sourceType">The source type.</param>
        /// <returns>Returns true for strings.</returns>
        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType)
        {
            if (object.ReferenceEquals(sourceType, typeof(string))) 
            {
                return true;
            } 
            else 
            {
                return base.CanConvertFrom(context, sourceType);
            }
        }

        /// <summary>
        /// Returns a customer object.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The string.</param>
        /// <returns>Returns a customer object.</returns>
        public override object ConvertFrom(
            System.ComponentModel.ITypeDescriptorContext context, 
            System.Globalization.CultureInfo culture, 
            object value)
        {
            if (object.ReferenceEquals(value.GetType(), typeof(string))) 
            {
                return Customer.CreateFromString(value.ToString());
            } 
            else 
            {
                return base.ConvertFrom(context, culture, value);
            }
        }
    }
}
