Imports System.Data.SqlClient
Imports DCS.Quote.Model.Quote
Imports DCS.Quote.Model
Imports DCS.Quote.QuoteDataBase
Imports System.Reflection
Imports System.Data.OleDb
Imports System.Transactions
Imports DCS.Quote.QuoteDataBaseTableAdapters

Public Class QuoteSaver

    Public Class QuoteInfoClass
        Public Property PartNumber As String
        Public Property RFQ As String
    End Class

    Public Function Save(ByVal q As Model.Template.Header, _
                         ByVal info As QuoteInfoClass) As Integer
        Return Save(q, info, False)
    End Function

    Public Function Save(ByVal q As Model.Template.Header, _
                         ByVal info As QuoteInfoClass, _
                         ByVal IsQuote As Boolean) _
                        As Integer

        frmMain.frmMain.UseWaitCursor = True
        My.Application.DoEvents()

        ' Ensure the properies are updated
        frmMain.frmMain.Focus()

        Dim o As Model.Template.PrimaryPropeties = q.PrimaryProperties

        Dim newId As Integer
        Dim id As Integer = o.CommonID
        If IsQuote Then
            id = 0
        End If

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        If id > 0 Then
            adaptor.Update( _
                o.CustomerName, info.RFQ, info.PartNumber, o.CommonID, _
                False, q.ID)
            newId = id
        Else
            adaptor.Connection.Open()
            adaptor.Transaction = adaptor.Connection.BeginTransaction
            adaptor.Insert(o.CustomerName, _
                info.PartNumber, info.RFQ, IsQuote, q.ID)
            Dim cmd As OleDbCommand = New OleDbCommand( _
                "SELECT @@IDENTITY", adaptor.Connection)
            cmd.Transaction = adaptor.Transaction
            newId = CInt(cmd.ExecuteScalar())
            adaptor.Transaction.Commit()
            adaptor.Connection.Close()
            If id = 0 And Not IsQuote Then
                q.PrimaryProperties.SetID(newId)
            End If
        End If

        adaptor.Connection.Open()
        CommonSaver.DeleteProperties(newId)
        CommonSaver.SaveOtherProperties(newId, q.OtherProperties, True)
        CommonSaver.SaveComputationProperties(newId, _
            q.ComputationProperties, True)
        CommonSaver.DeleteComponents(newId)
        CommonSaver.SaveComponents(q, newId, True)
        adaptor.Connection.Close()

        frmMain.frmMain.UseWaitCursor = False

        Return newId
    End Function

End Class
