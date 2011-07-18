Imports System.Data.SqlClient
Imports DCS.Quote.Model.Template
Imports DCS.Quote.Model
Imports DCS.Quote.QuoteDataBase
Imports System.Reflection
Imports System.Data.OleDb
Imports System.Transactions
Imports DCS.Quote.QuoteDataBaseTableAdapters

Public Class TemplateSaver

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
                o.LastModified, o.CommonID)
            adaptor.Connection.Close()
            newId = id
        Else
            adaptor.Connection.Open()
            adaptor.Transaction = adaptor.Connection.BeginTransaction
            adaptor.Insert(o.CustomerName, _
                o.PartNumber, o.RequestForQuoteNumber, False, Nothing, _
                o.CommonInitials, Date.Now, Date.Now)
            Dim cmd As OleDbCommand = New OleDbCommand( _
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
        CommonSaver.SaveOtherProperties(newId, q.OtherProperties, False)
        CommonSaver.SaveComputationProperties(newId, q.ComputationProperties, False)
        CommonSaver.DeleteComponents(newId)
        CommonSaver.SaveComponents(q, newId, False)
        adaptor.Connection.Close()

        q.ComputationProperties.ClearDirty()
        q.OtherProperties.ClearDirty()
        q.PrimaryProperties.ClearDirty()
        q.ClearDirty()

        frmMain.frmMain.UseWaitCursor = False

        Return newId
    End Function

End Class
