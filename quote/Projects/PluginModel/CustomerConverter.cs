namespace Model
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Linq;
    
    using Model.Template;

    public class CustomerConverter : ExpandableObjectConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            StandardValuesCollection svc = 
                new StandardValuesCollection(CustomerList.Collection);
            return svc;
        }

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

        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
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
