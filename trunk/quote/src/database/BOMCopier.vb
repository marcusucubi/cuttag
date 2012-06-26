Imports System.Data.SqlClient
Imports System.Transactions
Imports DCS.Quote.Model.BOM
Imports DCS.Quote.QuoteDataBaseTableAdapters

Public Class BOMCopier

    Public Function Copy(ByVal q As Header) _
                        As Integer

        frmMain.frmMain.UseWaitCursor = True
        My.Application.DoEvents()

        ' Ensure the properies are updated
        frmMain.frmMain.Focus()

        Dim o As PrimaryPropeties = q.PrimaryProperties

        Dim newId As Integer

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        adaptor.Connection.Open()
        adaptor.Transaction = adaptor.Connection.BeginTransaction
        adaptor.Insert(o.Customer.Name, _
            o.RequestForQuoteNumber, o.PartNumber, False, 0, _
            o.CommonInitials, Date.Now, Date.Now, o.Customer.ID)
        Dim cmd As SqlCommand = New SqlCommand( _
            "SELECT @@IDENTITY", adaptor.Connection)
        cmd.Transaction = adaptor.Transaction
        newId = CInt(cmd.ExecuteScalar())
        adaptor.Transaction.Commit()
        adaptor.Connection.Close()

        adaptor.Connection.Open()
        CommonSaver.DeleteProperties(newId)
        CommonSaver.SaveNoteProperties(newId, q.NoteProperties)
        CommonSaver.SaveOtherProperties(newId, q.OtherProperties, True)
        CommonSaver.SaveComputationProperties(newId, q.ComputationProperties, True)
        CommonSaver.DeleteComponents(newId)
        CommonSaver.SaveComponents(q, newId, True)
        adaptor.Connection.Close()

        frmMain.frmMain.UseWaitCursor = False

        Return newId
    End Function

End Class
