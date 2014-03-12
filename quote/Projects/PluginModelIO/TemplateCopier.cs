namespace Model.IO
{
    using System;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Forms;
    
    using Model.Template;

    public static class TemplateCopier
    {
        public static int Copy(Common.Header header)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

            // Ensure the properies are updated
            Host.UI.UIApp.MainForm.Focus();

            PrimaryProperties o = header.PrimaryProperties as Model.Template.PrimaryProperties;

            int newId = 0;

            var adaptor = new DB.QuoteDataBaseTableAdapters._QuoteTableAdapter();
            adaptor.Connection.Open();
            var helper = new DB.QuoteTableProxy(adaptor);
            helper.Transaction = adaptor.Connection.BeginTransaction();
            
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
            
            var cmd = new SqlCommand("SELECT @@IDENTITY", adaptor.Connection);
            cmd.Transaction = helper.Transaction;
            
            newId = Convert.ToInt32(cmd.ExecuteScalar(), CultureInfo.CurrentCulture);
            helper.Transaction.Commit();
            adaptor.Connection.Close();

            adaptor.Connection.Open();
            CommonSaver.DeleteProperties(newId);
            CommonSaver.SaveNoteProperties(newId, header.NoteProperties);
            CommonSaver.SaveOtherProperties(newId, header.OtherProperties, true);
            CommonSaver.SaveComputationProperties(newId, header.ComputationProperties, true);
            CommonSaver.DeleteComponents(newId);
            CommonSaver.SaveComponents(header, newId, true);
            adaptor.Connection.Close();

            Cursor.Current = Cursors.Default;

            return newId;
        }
    }
}
