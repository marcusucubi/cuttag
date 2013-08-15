namespace Model.IO
{
    using System;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Forms;

    public static class QuoteSaver
    {
        public static int Save(
            Model.Template.Header header, 
            QuoteSaverQuoteInfoClass info)
        {
            return Save(header, info, false);
        }

        public static int Save(
            Model.Template.Header header, 
            QuoteSaverQuoteInfoClass info, 
            bool headerIsAQuote)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

            // Ensure the properies are updated
            Host.App.MainForm.Focus();

            Model.Template.PrimaryProperties o = 
                header.PrimaryProperties as Model.Template.PrimaryProperties;

            int newId = 0;
            int id = o.CommonId;
            
            if (headerIsAQuote) 
            {
                id = 0;
            }

            DB.QuoteDataBaseTableAdapters._QuoteTableAdapter adaptor = 
                new DB.QuoteDataBaseTableAdapters._QuoteTableAdapter();
            
            if (id > 0) 
            {
                adaptor.Update(
                    o.Customer.Name, 
                    info.RequestForQuote, 
                    info.PartNumber, 
                    false,
                    header.Id, 
                    o.Initials, 
                    header.PrimaryProperties.CommonCreatedDate, 
                    System.DateTime.Now, 
                    o.Customer.Id, 
                    o.CommonId,
                    o.CommonId);
                
                newId = id;
            } 
            else 
            {
                adaptor.Connection.Open();
                DB.QuoteTableProxy proxy = new DB.QuoteTableProxy(adaptor);
                proxy.Transaction = adaptor.Connection.BeginTransaction();
                
                adaptor.Insert(
                    o.Customer.Name, 
                    info.RequestForQuote, 
                    info.PartNumber, 
                    headerIsAQuote, 
                    header.Id, 
                    info.Initials, 
                    System.DateTime.Now, 
                    System.DateTime.Now, 
                    o.Customer.Id);
                
                SqlCommand cmd = new SqlCommand("SELECT @@IDENTITY", adaptor.Connection);
                cmd.Transaction = proxy.Transaction;
                newId = Convert.ToInt32(cmd.ExecuteScalar(), CultureInfo.CurrentCulture);
                
                proxy.Transaction.Commit();
                adaptor.Connection.Close();
                
                if (id == 0 & !headerIsAQuote) 
                {
                    header.PrimaryProperties.SetId(newId);
                }
            }

            adaptor.Connection.Open();
            CommonSaver.DeleteProperties(newId);
            CommonSaver.SaveOtherProperties(newId, header.OtherProperties, true);
            CommonSaver.SaveComputationProperties(newId, header.ComputationProperties, true);
            CommonSaver.SaveNoteProperties(newId, header.NoteProperties);
            CommonSaver.DeleteComponents(newId);
            CommonSaver.SaveComponents(header, newId, true);
            adaptor.Connection.Close();

            System.Windows.Forms.Cursor.Current = Cursors.Default;

            return newId;
        }
    }
}
