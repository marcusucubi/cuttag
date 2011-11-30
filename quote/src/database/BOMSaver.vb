Imports System.Data.SqlClient
Imports System.Transactions
Imports DCS.Quote.Model.BOM
Imports DCS.Quote.QuoteDataBaseTableAdapters

Public Class BOMSaver

    Public Function Save(ByVal q As Header) _
                        As Integer

        frmMain.frmMain.UseWaitCursor = True
        My.Application.DoEvents()

        ' Ensure the properies are updated
        frmMain.frmMain.Focus()

        Dim o As PrimaryPropeties = q.PrimaryProperties

        Dim newId As Integer
        Dim id As Integer = o.CommonID

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        If id > 0 Then
            adaptor.Connection.Open()
            o.CommonLastModified = Date.Now
            Dim test As Integer = adaptor.Update( _
                o.CustomerName, o.RequestForQuoteNumber, _
                o.PartNumber, False, Nothing, _
                o.CommonInitials, o.CommonCreatedDate, _
                o.LastModified, o.DueDate, o.QuoteDate, o.CommonID)
            adaptor.Connection.Close()
            newId = id
        Else
            adaptor.Connection.Open()
            adaptor.Transaction = adaptor.Connection.BeginTransaction
            adaptor.Insert(o.CustomerName, _
                o.RequestForQuoteNumber, o.PartNumber, False, 0, _
                o.CommonInitials, Date.Now, Date.Now, Date.Today, Date.Today)
            Dim cmd As SqlCommand = New SqlCommand( _
                "SELECT @@IDENTITY", adaptor.Connection)
            cmd.Transaction = adaptor.Transaction
            newId = CInt(cmd.ExecuteScalar())
            adaptor.Transaction.Commit()
            adaptor.Connection.Close()
            If id = 0 Then
                q.PrimaryProperties.SetID(newId)
            End If
        End If

        adaptor.Connection.Open()
        CommonSaver.DeleteProperties(newId)
        CommonSaver.SaveNoteProperties(newId, q.NoteProperties)
        CommonSaver.SaveOtherProperties(newId, q.OtherProperties, True)
        CommonSaver.SaveComputationProperties(newId, q.ComputationProperties, True)
        CommonSaver.DeleteComponents(newId)
        CommonSaver.SaveComponents(q, newId, True)
        adaptor.Connection.Close()

        q.PrimaryProperties.SendEvents()

        q.ComputationProperties.ClearDirty()
        q.OtherProperties.ClearDirty()
        q.PrimaryProperties.ClearDirty()
        q.ClearDirty()

        frmMain.frmMain.UseWaitCursor = False

        Return newId
    End Function

End Class
