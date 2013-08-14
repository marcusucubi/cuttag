namespace Model.IO
{
    using System;
    using System.ComponentModel;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    
    using DB;
    using DB.QuoteDataBaseTableAdapters;
    
    using Model.Common;
    using Model.Template;

    public static class CommonSaver
    {
        public static readonly int ComputationPropertiesId = -1;
        public static readonly int OtherPropertiesId = -2;
        public static readonly int CustomPropertiesId = -3;
        public static readonly int NotePropertiesId = -4;
        
        public static void SaveNoteProperties(int id, SavableProperties obj)
        {
            SaveProperties(id, NotePropertiesId, obj, true);
        }

        public static void SaveCustomProperties(int id, SavableProperties obj)
        {
            SaveProperties(id, CustomPropertiesId, obj, true);
        }

        public static void SaveOtherProperties(int id, SavableProperties obj, bool saveAll)
        {
            SaveProperties(id, OtherPropertiesId, obj, saveAll);
        }

        public static void SaveComputationProperties(int id, SavableProperties obj, bool saveAll)
        {
            SaveProperties(id, ComputationPropertiesId, obj, saveAll);
        }

        public static void DeleteComponents(int id)
        {
            _QuoteDetailTableAdapter adaptor = new _QuoteDetailTableAdapter();
            DB.QuoteDataBase._QuoteDetailDataTable table = null;
            table = adaptor.GetDataByQuoteID(id);
            
            foreach (DB.QuoteDataBase._QuoteDetailRow row in table.Rows) 
            {
                adaptor.Delete(row.id);
            }
        }

        public static void DeleteProperties(int quoteId)
        {
            DB.QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter adaptor = 
                new DB.QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter();
            DB.QuoteDataBase._QuotePropertiesDataTable table = adaptor.GetDataByQuoteID(quoteId);
            
            foreach (DB.QuoteDataBase._QuotePropertiesRow row in table.Rows) 
            {
                adaptor.Delete(row.id);
            }
        }

        public static void SaveComponents(
            Model.Common.Header header, 
            int quoteId, 
            bool saveAll)
        {
            _QuoteDetailTableAdapter adaptor = new _QuoteDetailTableAdapter();
            
            foreach (Model.Common.Detail detail in header.Details) 
            {
                adaptor.Connection.Open();
                DB.DetailTableProxy proxy = new DB.DetailTableProxy(adaptor);
                proxy.Transaction = adaptor.Connection.BeginTransaction();
                
                adaptor.Insert(
                    quoteId, 
                    detail.SequenceNumber, 
                    detail.Qty, 
                    detail.Product.Code, 
                    detail.SourceId, 
                    detail.IsWire, 
                    detail.UnitOfMeasure);
                
                SqlCommand cmd = new SqlCommand("SELECT @@IDENTITY", adaptor.Connection);
                cmd.Transaction = proxy.Transaction;
                int id = Convert.ToInt32(cmd.ExecuteScalar(), CultureInfo.CurrentCulture);
                proxy.Transaction.Commit();
                adaptor.Connection.Close();

                SaveProperties(quoteId, id, detail.QuoteDetailProperties, saveAll);
                detail.IsDirty = false;
            }
        }
        
        private static void SaveProperties(
            int id, 
            int childId, 
            SavableProperties obj, 
            bool saveAll)
        {
            PropertyInfo[] props = obj.GetType().GetProperties();
            DB.QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter adaptor = 
                new DB.QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter();
            PropertyInfo nonDisplayable = null;
            
            IHasSubject hasSubject = obj as IHasSubject;
            foreach (PropertyInfo p in props) 
            {
                if (hasSubject != null)
                {
                    nonDisplayable = hasSubject.Subject.GetType().GetProperty(p.Name);
                }
                
                if (saveAll == false) 
                {
                    if (!p.CanWrite) 
                    {
                        continue;
                    }
                }

                string cat = "Misc";
                CategoryAttribute[] oa = 
                    p.GetCustomAttributes(typeof(CategoryAttribute), false) as CategoryAttribute[];
                if (oa.Length > 0) 
                {
                    cat = oa[0].Category;
                }

                string desc = string.Empty;
                DescriptionAttribute[] oa2 = 
                    p.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
                if (oa2.Length > 0) 
                {
                    desc = oa2[0].Description;
                }

                bool browsable = true;
                BrowsableAttribute[] oa3 = 
                    p.GetCustomAttributes(typeof(BrowsableAttribute), false) as BrowsableAttribute[];
                if (oa3.Length > 0) 
                {
                    browsable = oa3[0].Browsable;
                }
                
                if (!browsable) 
                {
                    continue;
                }

                string s = null;
                int i;
                decimal d;
                bool b;

                object o = null;
                if (nonDisplayable == null)
                {
                    o = p.GetValue(obj, null);
                } 
                else
                {
                    o = nonDisplayable.GetValue(hasSubject.Subject, null);
                }
                
                if (o is int) 
                {
                    i = Convert.ToInt32(o, CultureInfo.CurrentCulture);
                    adaptor.Insert(id, childId, p.Name, null, null, i, cat, desc, null);
                }
                
                if (o is string) 
                {
                    DB.QuoteDataBase._QuotePropertiesDataTable t = 
                        new DB.QuoteDataBase._QuotePropertiesDataTable();
                    var max = t.PropertyStringValueColumn.MaxLength;
                    s = Convert.ToString(o, CultureInfo.CurrentCulture);
                    
                    if (s.Length > max) 
                    {
                        s = s.Substring(0, max - 1);
                    }
                    
                    adaptor.Insert(id, childId, p.Name, s, null, null, cat, desc, null);
                }
                
                if (o is decimal) 
                {
                    d = Convert.ToDecimal(o, CultureInfo.CurrentCulture);
                    adaptor.Insert(id, childId, p.Name, null, d, null, cat, desc, null);
                }
                
                if (o is DateTime) 
                {
                    DateTime dt = Convert.ToDateTime(o, CultureInfo.CurrentCulture);
                    if (dt.Year > 1) 
                    {
                        adaptor.Insert(id, childId, p.Name, null, null, null, cat, desc, dt);
                    }
                }
                
                if (o is bool) 
                {
                    b = Convert.ToBoolean(o, CultureInfo.CurrentCulture);
                    if (b) 
                    {
                        adaptor.Insert(id, childId, p.Name, "True", null, null, cat, desc, null);
                    } 
                    else 
                    {
                        adaptor.Insert(id, childId, p.Name, "False", null, null, cat, desc, null);
                    }
                }
            }
        }
    }
}
