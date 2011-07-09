Imports System.Data.SqlClient
Imports DCS.Quote.Model
Imports DCS.Quote.QuoteDataBase
Imports System.Reflection
Imports System.Data.OleDb
Imports System.Transactions
Imports DCS.Quote.QuoteDataBaseTableAdapters

Public Class QuoteSaver

    Public Sub Save(ByVal q As Model.QuoteHeader)
        Save(q, True)
    End Sub

    Public Sub Save(ByVal q As Model.QuoteHeader, ByVal IsQuote As Boolean)

        ' Ensure the properies are updated
        frmMain.frmMain.Focus()

        Dim id As Integer

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        Dim o As PrimaryPropeties = q.PrimaryProperties
        If q.PrimaryProperties.QuoteNumnber > 0 Then
            adaptor.Update( _
                o.CustomerName, o.RequestForQuoteNumber, _
                o.PartNumber, o.QuoteNumnber, True)
            id = o.QuoteNumnber
        Else
            adaptor.Connection.Open()
            adaptor.Transaction = adaptor.Connection.BeginTransaction
            adaptor.Insert(o.CustomerName, o.PartNumber, o.RequestForQuoteNumber, IsQuote)
            Dim cmd As OleDbCommand = New OleDbCommand("SELECT @@IDENTITY", adaptor.Connection)
            cmd.Transaction = adaptor.Transaction
            id = CInt(cmd.ExecuteScalar())
            adaptor.Transaction.Commit()
            adaptor.Connection.Close()
            o.SetID(id)
        End If

        adaptor.Connection.Open()
        Me.DeleteProperties(id)
        Me.SaveProperties(id, 0, q.NonComputationProperties, Nothing)
        Me.SaveProperties(id, 0, q.ComputationProperties, Nothing)
        Me.DeleteComponents(id)
        Me.SaveComponents(q)
        adaptor.Connection.Close()

        My.Settings.LastTamplate1 = _
            ActiveTemplate.ActiveTemplate.QuoteHeader.PrimaryProperties.QuoteNumnber
    End Sub

    Private Sub SaveComponents(ByVal q As QuoteHeader)

        Dim adaptor As New _QuoteDetailTableAdapter
        Dim quoteId As Integer = q.PrimaryProperties.QuoteNumnber
        Dim table As _QuoteDetailDataTable = adaptor.GetDataByQuoteID(quoteId)
        For Each detail As QuoteDetail In q.QuoteDetails
            adaptor.Connection.Open()
            adaptor.Transaction = adaptor.Connection.BeginTransaction
            adaptor.Insert(q.PrimaryProperties.QuoteNumnber, detail.Qty, detail.ProductCode)
            Dim cmd As OleDbCommand = New OleDbCommand("SELECT @@IDENTITY", adaptor.Connection)
            cmd.Transaction = adaptor.Transaction
            Dim id As Integer = CInt(cmd.ExecuteScalar())
            adaptor.Transaction.Commit()
            adaptor.Connection.Close()

            Me.SaveProperties(quoteId, id, detail.QuoteDetailProperties, Nothing)
        Next

    End Sub

    Private Sub SaveProperties(ByVal id As Integer, _
                               ByVal childId As Integer, _
                               ByVal obj As Object, _
                               ByRef Transaction As OleDbTransaction)

        Dim props As PropertyInfo() = obj.GetType.GetProperties
        Dim adaptor As New QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter

        For Each p As PropertyInfo In props

            If Not p.CanWrite Then
                Continue For
            End If

            Dim s As String = Nothing
            Dim i As Integer = Nothing
            Dim d As Decimal = Nothing
            Dim o As Object = p.GetValue(obj, Nothing)

            If TypeOf o Is Integer Then
                i = CInt(o)
            End If
            If TypeOf o Is String Then
                s = CStr(o)
            End If
            If TypeOf o Is Decimal Then
                d = CDec(o)
            End If

            adaptor.Insert(id, childId, p.Name, s, d, i)
        Next
    End Sub

    Private Sub DeleteComponents(ByVal id As Integer)
        Dim adaptor As New _QuoteDetailTableAdapter
        adaptor.Delete(id)
    End Sub

    Private Sub DeleteProperties(ByVal QuoteID As Integer)

        Dim adaptor As New QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter
        Dim table As _QuotePropertiesDataTable = adaptor.GetDataByQuoteID(QuoteID)
        For Each row As _QuotePropertiesRow In table.Rows
            adaptor.Delete(row.ID)
        Next
    End Sub


End Class
