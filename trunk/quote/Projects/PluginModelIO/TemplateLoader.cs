namespace Model.IO
{
    using System;
    using System.Linq;
    using System.Windows.Forms;
    
    using DB;
    using DB.QuoteDataBaseTableAdapters;
    
    using Model.Template;

    public static class TemplateLoader
    {
        public static Header Load(int id)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

            DB.QuoteDataBaseTableAdapters._QuoteTableAdapter adaptor = 
                new DB.QuoteDataBaseTableAdapters._QuoteTableAdapter();
            DB.QuoteDataBase._QuoteDataTable table = new DB.QuoteDataBase._QuoteDataTable();
            Header q = new Header();

            adaptor.FillByByQuoteID(table, id);
            if (table.Rows.Count > 0) 
            {
                DB.QuoteDataBase._QuoteRow row = table.Rows[0] as DB.QuoteDataBase._QuoteRow;
                q = new Header((int)row.id);

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

                Customer customerObj = LookupCustomer(row);

                q.PrimaryProperties.CommonCustomer = customerObj;
                q.PrimaryProperties.CommonPartNumber = part;
                q.PrimaryProperties.CommonRequestForQuoteNumber = rfq;
                q.PrimaryProperties.CommonCreatedDate = createdDate;
                q.PrimaryProperties.CommonLastModified = lastModDate;
                q.PrimaryProperties.CommonInitials = initials;

                CommonLoader.LoadComputationProperties(id, q.ComputationProperties);
                CommonLoader.LoadOtherProperties(id, q.OtherProperties);
                CommonLoader.LoadNoteProperties(id, q.NoteProperties);
                LoadComponents(q);
            }

            q.IsDirty = false;
            
            System.Windows.Forms.Cursor.Current = Cursors.Default;

            return q;
        }

        public static Customer LookupCustomer(DB.QuoteDataBase._QuoteRow row)
        {
            string customer = string.Empty;
            if (!row.IsCustomerNameNull()) 
            {
                customer = row.CustomerName;
            }
            
            int customerID = 0;

            if (row.IsCustomerIDNull()) 
            {
                if (!row.IsCustomerNameNull()) 
                {
                    Customer temp = null;
                    
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

            Customer customerObj = new Customer();
            customerObj.SetName(customer);
            customerObj.SetId(customerID);

            return customerObj;
        }

        public static void LoadComponents(Model.Common.Header header)
        {
            _QuoteDetailTableAdapter adaptor = new _QuoteDetailTableAdapter();
            
            int id = header.PrimaryProperties.CommonId;
            
            DB.QuoteDataBase._QuoteDetailDataTable table = adaptor.GetDataByQuoteID(id);

            foreach (DB.QuoteDataBase._QuoteDetailRow row in table.Rows) 
            {
                TemplateLoaderTempObj temp = new TemplateLoaderTempObj();
                CommonLoader.LoadProperties(id, (int)row.id, temp);

                ProductBuildData data = new ProductBuildData();
                data.Code = row.ProductCode;
                data.Gage = temp.Gage;
                data.IsWire = row.IsWire;
                data.Vendor = string.Empty;
                data.Description = string.Empty;
                Model.Product product = new Model.Product(data);

                Model.Common.Detail detail = header.NewDetail(product);
                detail.Qty = row.Qty;
                
                if (!row.IsSourceIDNull())
                {
                    detail.SourceId = row.SourceID;
                }
                
                detail.SequenceNumber = row.SequenceNumber;
                header.NextSequenceNumber = detail.SequenceNumber + 1;
                
                CommonLoader.LoadProperties(id, (int)row.id, detail.QuoteDetailProperties);
            }
        }
    }
}
