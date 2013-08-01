namespace Model
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Linq;
    
    using Model.Template;

    public class CustomerConverter : ExpandableObjectConverter
    {
        private ArrayList values;
        
        public CustomerConverter()
        {
            DB.QuoteDataBaseTableAdapters.CustomerTableAdapter adaptor = new DB.QuoteDataBaseTableAdapters.CustomerTableAdapter();
            DB.QuoteDataBase.CustomerDataTable table = null;
            table = adaptor.GetData();
            this.values = new ArrayList();
            
            foreach (DB.QuoteDataBase.CustomerRow row in table.Rows) 
            {
                if (row.CustomerID > 0) 
                {
                    Customer c = new Customer();
                    c.SetId(row.CustomerID);
                    c.SetName(row.CustomerName.Trim());
                    this.values.Add(c);
                }
            }
        }

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
            StandardValuesCollection svc = new StandardValuesCollection(this.values);
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
