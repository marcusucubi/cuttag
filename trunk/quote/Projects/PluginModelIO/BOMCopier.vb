Imports System.Data.SqlClient
Imports System.Transactions

Imports Model.Template
Imports DB.QuoteDataBaseTableAdapters
Imports System.Windows.Forms

Public Class BOMCopier

    Public Function Copy(ByVal q As Header) _
                        As Integer

        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        ' Ensure the properies are updated
        PluginHost.App.MainForm.Focus()

        Dim o As PrimaryPropeties = q.PrimaryProperties

        Dim newId As Integer

        Dim adaptor As New DB.QuoteDataBaseTableAdapters._QuoteTableAdapter
        adaptor.Connection.Open()
        Dim helper As New DB.QuoteTableProxy(adaptor)
        helper.Transaction = adaptor.Connection.BeginTransaction
        adaptor.Insert(o.Customer.Name, _
            o.RequestForQuoteNumber, o.PartNumber, False, 0, _
            o.CommonInitials, Date.Now, Date.Now, o.Customer.ID)
        Dim cmd As SqlCommand = New SqlCommand( _
            "SELECT @@IDENTITY", adaptor.Connection)
        cmd.Transaction = helper.Transaction
        newId = CInt(cmd.ExecuteScalar())
        helper.Transaction.Commit()
        adaptor.Connection.Close()

        adaptor.Connection.Open()
        CommonSaver.DeleteProperties(newId)
        CommonSaver.SaveNoteProperties(newId, q.NoteProperties)
        CommonSaver.SaveOtherProperties(newId, q.OtherProperties, True)
        CommonSaver.SaveComputationProperties(newId, q.ComputationProperties, True)
        CommonSaver.DeleteComponents(newId)
        CommonSaver.SaveComponents(q, newId, True)
        adaptor.Connection.Close()

        System.Windows.Forms.Cursor.Current = Cursors.Default

        Return newId
    End Function

End Class
