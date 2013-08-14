namespace Model.IO
{
    using System;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Forms;
    
    using DB;
    
    using Model.Template;

    public static class TemplateSaver
    {
        public static int Save(Common.Header header)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

            // Ensure the properies are updated
            Host.App.MainForm.Focus();

            Model.Template.PrimaryProperties o = 
                header.PrimaryProperties as Model.Template.PrimaryProperties;

            int newId = 0;
            int id = o.CommonId;

            DB.QuoteDataBaseTableAdapters._QuoteTableAdapter adaptor = 
                new DB.QuoteDataBaseTableAdapters._QuoteTableAdapter();
            
            if (id > 0) 
            {
                adaptor.Connection.Open();
                o.CommonLastModified = System.DateTime.Now;
                
                adaptor.Update(
                    o.Customer.Name, 
                    o.RequestForQuoteNumber, 
                    o.PartNumber, 
                    false, 
                    null, 
                    o.CommonInitials, 
                    o.CommonCreatedDate, 
                    o.LastModified, 
                    o.Customer.Id, 
                    o.CommonId,
                    o.CommonId);
                
                adaptor.Connection.Close();
                newId = id;
            } 
            else 
            {
                adaptor.Connection.Open();
                QuoteTableProxy proxy = new QuoteTableProxy(adaptor);
                proxy.Transaction = adaptor.Connection.BeginTransaction();
                
                adaptor.Insert(
                    o.Customer.Name, 
                    o.RequestForQuoteNumber, 
                    o.PartNumber, 
                    false, 
                    0, 
                    o.CommonInitials, 
                    System.DateTime.Now, 
                    System.DateTime.Now, 
                    o.Customer.Id);
                
                SqlCommand cmd = new SqlCommand("SELECT @@IDENTITY", adaptor.Connection);
                cmd.Transaction = proxy.Transaction;
                newId = Convert.ToInt32(cmd.ExecuteScalar(), CultureInfo.CurrentCulture);
                proxy.Transaction.Commit();
                adaptor.Connection.Close();
                
                if (id == 0) 
                {
                    header.PrimaryProperties.SetId(newId);
                }
            }

            adaptor.Connection.Open();
            CommonSaver.DeleteProperties(newId);
            CommonSaver.SaveNoteProperties(newId, header.NoteProperties);
            CommonSaver.SaveOtherProperties(newId, header.OtherProperties, true);
            CommonSaver.SaveComputationProperties(newId, header.ComputationProperties, true);
            CommonSaver.DeleteComponents(newId);
            CommonSaver.SaveComponents(header, newId, true);
            adaptor.Connection.Close();

            header.ComputationProperties.IsDirty = false;
            header.OtherProperties.IsDirty = false;
            header.PrimaryProperties.IsDirty = false;
            header.IsDirty = false;

            System.Windows.Forms.Cursor.Current = Cursors.Default;

            return newId;
        }
    }
}
