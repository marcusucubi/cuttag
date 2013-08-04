namespace Model.IO
{
    using System;
    using System.Linq;
    using System.Reflection;
    
    using DB;
    using DB.QuoteDataBaseTableAdapters;

    public static class CommonLoader
    {
        public static void LoadNoteProperties(int id, object target)
        {
            LoadProperties(id, CommonSaver.NotePropertiesId, target);
        }

        public static void LoadOtherProperties(int id, object target)
        {
            LoadProperties(id, CommonSaver.OtherPropertiesId, target);
        }

        public static void LoadComputationProperties(int id, object target)
        {
            LoadProperties(id, CommonSaver.ComputationPropertiesId, target);
        }

        public static void LoadComponents(Model.Common.Header header)
        {
            _QuoteDetailTableAdapter adaptor = new _QuoteDetailTableAdapter();
            WireComponentSourceTableAdapter partAdaptor = new WireComponentSourceTableAdapter();
            WireSourceTableAdapter wireAdaptor = new WireSourceTableAdapter();
            GageTableAdapter gageAdaptor = new GageTableAdapter();
            
            int id = header.PrimaryProperties.CommonId;
            
            DB.QuoteDataBase._QuoteDetailDataTable table = adaptor.GetDataByQuoteID(id);

            foreach (DB.QuoteDataBase._QuoteDetailRow row in table.Rows) 
            {
                Model.Common.Detail detail = null;

                DB.QuoteDataBase.WireComponentSourceDataTable parts = null;
                parts = partAdaptor.GetDataByPartNumber(row.ProductCode);
                
                if (parts.Count > 0)
                {
                    DB.QuoteDataBase.WireComponentSourceRow part = null;
                    part = parts[0];
                    decimal price = 0;
                    
                    if (!part.IsQuotePriceNull()) 
                    {
                        price = part.QuotePrice;
                    }

                    Model.Product partObj = ProductDB.Load(
                        part.PartNumber, 
                        price, 
                        string.Empty, 
                        false, 
                        null, 
                        part, 
                        string.Empty, 
                        0);

                    detail = header.NewDetail(partObj);
                }

                DB.QuoteDataBase.WireSourceDataTable wires = null;
                wires = wireAdaptor.GetDataByPartNumber(row.ProductCode);
                
                if (wires.Count > 0)
                {
                    DB.QuoteDataBase.WireSourceRow wire = null;
                    wire = wires[0];
                    
                    string gage = string.Empty;
                    DB.QuoteDataBase.GageDataTable gageTable = null;
                    
                    gageTable = gageAdaptor.GetDataByGageID(wire.GageID);
                    if (gageTable != null) 
                    {
                        DB.QuoteDataBase.GageRow gageRow = gageTable.Rows[0] as DB.QuoteDataBase.GageRow;
                        gage = gageRow.Gage;
                    }

                    decimal price = 0;
                    if (!wire.IsQuotePriceNull()) 
                    {
                        price = wire.QuotePrice;
                    }

                    Model.Product wireObj = ProductDB.Load(
                        wire.PartNumber, 
                        price, 
                        gage, 
                        true, 
                        wire, 
                        null, 
                        string.Empty, 
                        0);

                    detail = header.NewDetail(wireObj);
                }

                if (detail != null)
                {
                    detail.Qty = row.Qty;
                    LoadProperties(id, (int)row.id, detail.QuoteDetailProperties);
                }
            }
        }

        public static void LoadProperties(
            int id, 
            int childId, 
            object target)
        {
            PropertyInfo[] props = target.GetType().GetProperties();
            
            DB.QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter adaptor = 
                new DB.QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter();

            foreach (PropertyInfo p in props) 
            {
                DB.QuoteDataBase._QuotePropertiesDataTable table = 
                    adaptor.GetDataByQuoteAndName(id, childId, p.Name);

                if (table.Rows.Count > 0) 
                {
                    DB.QuoteDataBase._QuotePropertiesRow row = 
                        table.Rows[0] as DB.QuoteDataBase._QuotePropertiesRow;
                    
                    if (!row.IsPropertyStringValueNull()) 
                    {
                        if (p.PropertyType.Name == "String" & p.CanWrite) 
                        {
                            p.SetValue(target, row.PropertyStringValue, null);
                        }
                    }
                    
                    if (!row.IsPropertyStringValueNull()) 
                    {
                        if (p.PropertyType.Name == "Boolean" & p.CanWrite) 
                        {
                            if (row.PropertyStringValue == "True") 
                            {
                                p.SetValue(target, true, null);
                            } 
                            else 
                            {
                                p.SetValue(target, false, null);
                            }
                        }
                    }
                    
                    if (!row.IsPropertyIntegerValueNull()) 
                    {
                        if (p.PropertyType.Name == "Int32" & p.CanWrite) 
                        {
                            p.SetValue(target, row.PropertyIntegerValue, null);
                        }
                    }
                    
                    if (!row.IsPropertyDecimalValueNull()) 
                    {
                        if (p.PropertyType.Name == "Decimal" & p.CanWrite) 
                        {
                            p.SetValue(target, row.PropertyDecimalValue, null);
                        }
                        
                        if (p.PropertyType.Name == "Decimal" & !p.CanWrite) 
                        {
                            MethodInfo m = target.GetType().GetMethod("Set" + p.Name);
                            if (m != null)
                            {
                                m.Invoke(target, new object[] { row.PropertyDecimalValue });
                            }
                        }
                    }
                    
                    if (!row.IsPropertyDateValueNull()) 
                    {
                        if (p.PropertyType.Name == "DateTime" & p.CanWrite) 
                        {
                            DateTime dt = row.PropertyDateValue;
                            if (dt.Year > 1900) 
                            {
                                p.SetValue(target, row.PropertyDateValue, null);
                            }
                        }
                    }
                }
            }
        }
    }
}
