namespace Model.IO
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;
    
    using DB;
    using DB.QuoteDataBaseTableAdapters;
    
    using Model.Quote;

    public class QuoteLoader
    {
        private ObjectGenerator propertyLoader = new ObjectGenerator();
        
        public static Model.Customer LookupCustomer(DB.QuoteDataBase._QuoteRow row)
        {
            string customer = null;
            int customerID = 0;

            if (row.IsCustomerIDNull()) 
            {
                if (!row.IsCustomerNameNull()) 
                {
                    Model.Customer temp = null;
                    temp = Model.IO.Misc.CustomerDB.GetByName(row.CustomerName);
                    if (temp != null) 
                    {
                        customerID = temp.Id;
                    }
                }
            } 
            else 
            {
                customerID = (int)row.CustomerID;
            }

            if (row.IsCustomerNameNull()) 
            {
                customer = string.Empty;
            } 
            else 
            {
                customer = row.CustomerName;
            }

            Model.Customer customerObj = new Model.Customer();
            customerObj.SetName(customer);
            customerObj.SetId(customerID);

            return customerObj;
        }

        public Header Load(int id)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

            DB.QuoteDataBaseTableAdapters._QuoteTableAdapter adaptor = 
                new DB.QuoteDataBaseTableAdapters._QuoteTableAdapter();
            DB.QuoteDataBase._QuoteDataTable table = new DB.QuoteDataBase._QuoteDataTable();
            Header q = new Header();

            Model.Customer customerObj = null;

            adaptor.FillByByQuoteID(table, (decimal)Convert.ToDouble(id));
            if (table.Rows.Count > 0) 
            {
                DB.QuoteDataBase._QuoteRow row = table.Rows[0] as DB.QuoteDataBase._QuoteRow;
                customerObj = LookupCustomer(row);

                string rfq = string.Empty;
                if (!row.IsRequestForQuoteNumberNull()) 
                {
                    rfq = row.RequestForQuoteNumber;
                }
                
                string part = string.Empty;
                if (!row.IsPartNumberNull()) 
                {
                    part = row.PartNumber;
                }
                
                int templateID = 0;
                if (!row.IsTemplateIDNull()) 
                {
                    templateID = row.TemplateID;
                }
                
                q = new Header(
                    (int)row.id,
                    rfq, 
                    part, 
                    row.Initials, 
                    row.CreatedDate, 
                    row.LastModifedDate, 
                    templateID);

                string initials = string.Empty;
                if (!row.IsInitialsNull()) 
                {
                    initials = row.Initials;
                }
                
                DateTime createdDate = default(DateTime);
                if (!row.IsCreatedDateNull()) 
                {
                    createdDate = row.CreatedDate;
                }
                
                DateTime lastModDate = default(DateTime);
                if (!row.IsLastModifedDateNull()) 
                {
                    lastModDate = row.LastModifedDate;
                }

                q.PrimaryProperties.CommonCustomer = customerObj;
                q.PrimaryProperties.CommonPartNumber = part;
                q.PrimaryProperties.CommonRequestForQuoteNumber = rfq;
                q.PrimaryProperties.CommonCreatedDate = createdDate;
                q.PrimaryProperties.CommonLastModified = lastModDate;
                q.PrimaryProperties.CommonInitials = initials;
                
                this.LoadComponents(q);

                Common.ComputationProperties o1 = this.LoadProperties(
                    id,
                    CommonSaver.ComputationPropertiesId, 
                    q.ComputationProperties) as Common.ComputationProperties;
                
                q.SetPublicComputationProperties(o1);
                
                Common.OtherProperties o2 = this.LoadProperties(
                    id, 
                    CommonSaver.OtherPropertiesId, 
                    q.OtherProperties) as Common.OtherProperties;
                q.SetPublicOtherProperties(o2);
                
                Common.NoteProperties o4 = this.LoadProperties(
                    id, 
                    CommonSaver.NotePropertiesId, 
                    q.NoteProperties) as Common.NoteProperties;
                q.SetPublicNoteProperties(o4);
            }

            System.Windows.Forms.Cursor.Current = Cursors.Default;

            return q;
        }

        public void LoadComponents(Model.Common.Header header)
        {
            _QuoteDetailTableAdapter adaptor = new _QuoteDetailTableAdapter();
            
            int id = header.PrimaryProperties.CommonId;
            DB.QuoteDataBase._QuoteDetailDataTable table = adaptor.GetDataByQuoteID(id);

            foreach (DB.QuoteDataBase._QuoteDetailRow row in table.Rows) 
            {
                QuoteLoaderTempObj temp = new QuoteLoaderTempObj();
                CommonLoader.LoadProperties(id, (int)row.id, temp);

                ProductBuildData data = new ProductBuildData();
                data.Code = row.ProductCode;
                data.Gage = temp.Gage;
                data.IsWire = row.IsWire;
                data.Vendor = string.Empty;
                data.Description = string.Empty;
                Product product = new Product(data);

                Model.Quote.Detail detail = header.NewDetail(product) as Model.Quote.Detail;
                
                detail.Qty = row.Qty;
                
                if (!row.IsSourceIDNull())
                {
                    detail.SourceId = row.SourceID;
                }
                
                detail.SequenceNumber = row.SequenceNumber;
                detail.UnitOfMeasure = row.UOM;

                Common.ISavableProperties o1 = null;
                if (detail.IsWire)
                {
                    o1 = this.LoadProperties(
                        id, 
                        (int)row.id,
                        detail.QuoteDetailProperties) 
                        as Common.ISavableProperties;
                }
                else
                {
                    o1 = this.LoadProperties(
                        id, 
                        (int)row.id,
                        detail.QuoteDetailProperties) 
                        as Common.ISavableProperties;
                }
                
                if (this.propertyLoader.NameList.Contains("MachineTime"))
                {
                    PropertyInfo prop = o1.GetType().GetProperty("MachineTime");
                    if (prop != null)
                    {
                        decimal value = (decimal)prop.GetValue(o1, new object[] { });
                        detail.MachineTime = value;
                    }
                }
                
                if (this.propertyLoader.NameList.Contains("UnitCost"))
                {
                    PropertyInfo prop = o1.GetType().GetProperty("UnitCost");
                    if (prop != null)
                    {
                        decimal value = (decimal)prop.GetValue(o1, new object[] { });
                        detail.UnitCost = value;
                    }
                }
                
                detail.SetProperties(o1);
            }
        }
        
        private object LoadProperties(
            int id, 
            int childId, 
            object obj,
            Type interfaceToImplement = null)
        {
            DB.QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter adaptor = 
                new DB.QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter();

            this.propertyLoader = new ObjectGenerator();
            
            if (interfaceToImplement != null)
            {
                this.propertyLoader.InterfaceToImplement = interfaceToImplement.FullName;
            }

            DB.QuoteDataBase._QuotePropertiesDataTable table = 
                adaptor.GetDataByQuoteIDAndPropertyID(id, childId);

            foreach (DB.QuoteDataBase._QuotePropertiesRow row in table.Rows) 
            {
                this.AddNode(row);
            }

            object o = new object();
            this.propertyLoader.BaseTypeName = obj.GetType().FullName;
            o = this.propertyLoader.Generate();
            
            return o;
        }

        private void AddNode(DB.QuoteDataBase._QuotePropertiesRow row)
        {
            ObjectGeneratorPropertyInfo node = new ObjectGeneratorPropertyInfo();
            node.Name = row.PropertyName;
            
            if (!row.IsPropertyStringValueNull()) 
            {
                node.TypeName = "System.String";
                node.Value = row.PropertyStringValue;
            } 
            else if (!row.IsPropertyDecimalValueNull()) 
            {
                node.TypeName = "System.Decimal";
                node.Value = row.PropertyDecimalValue;
            } 
            else if (!row.IsPropertyIntegerValueNull()) 
            {
                node.TypeName = "System.Int32";
                node.Value = row.PropertyIntegerValue;
            } 
            else if (!row.IsPropertyDateValueNull()) 
            {
                node.TypeName = "System.String";
                DateTime dt = row.PropertyDateValue;
                if (dt.Year > 1900) 
                {
                    node.Value = row.PropertyDateValue.ToShortDateString();
                } 
                else 
                {
                    node.Value = string.Empty;
                }
            }
            
            if (!row.IsPropertyCatagoryNull()) 
            {
                node.Category = row.PropertyCatagory;
            }
            
            if (!row.IsPropertyDescriptionNull()) 
            {
                node.Description = row.PropertyDescription;
            }
            
            this.propertyLoader.Add(node);
        }
    }
}