Imports System.Data.SqlClient
Imports DCS.Quote.Model
Imports DCS.Quote.QuoteDataBase
Imports System.Reflection
Imports System.Data.OleDb
Imports System.Transactions
Imports DCS.Quote.QuoteDataBaseTableAdapters

Public Class QuoteSaver

    Public Function Save(ByVal q As Model.QuoteHeader) As Integer
        Return Save(q, False)
    End Function

    Public Function Save(ByVal q As Model.QuoteHeader, _
                         ByVal IsQuote As Boolean) _
                        As Integer

        ' Ensure the properies are updated
        frmMain.frmMain.Focus()

        Dim o As PrimaryPropeties = q.PrimaryProperties

        Dim newId As Integer
        Dim id As Integer = o.QuoteNumnber
        If IsQuote Then
            id = 0
        End If

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        If id > 0 Then
            adaptor.Update( _
                o.CustomerName, o.RequestForQuoteNumber, _
                o.PartNumber, o.QuoteNumnber, False)
            newId = id
        Else
            adaptor.Connection.Open()
            adaptor.Transaction = adaptor.Connection.BeginTransaction
            adaptor.Insert(o.CustomerName, _
                o.PartNumber, o.RequestForQuoteNumber, IsQuote)
            Dim cmd As OleDbCommand = New OleDbCommand( _
                "SELECT @@IDENTITY", adaptor.Connection)
            cmd.Transaction = adaptor.Transaction
            newId = CInt(cmd.ExecuteScalar())
            adaptor.Transaction.Commit()
            adaptor.Connection.Close()
        End If

        adaptor.Connection.Open()
        Me.DeleteProperties(newId)
        Dim SaveAll As Boolean = IsQuote
        Me.SaveProperties(newId, 0, q.NonComputationProperties, SaveAll)
        Me.SaveProperties(newId, 0, q.ComputationProperties, SaveAll)
        Me.DeleteComponents(newId)
        Me.SaveComponents(q, newId)
        adaptor.Connection.Close()

        My.Settings.LastTamplate1 = _
            ActiveTemplate.ActiveTemplate.QuoteHeader.PrimaryProperties.QuoteNumnber
        Return newId
    End Function

    Private Sub SaveComponents(ByVal q As QuoteHeader, ByVal quoteId As Integer)

        Dim adaptor As New _QuoteDetailTableAdapter
        Dim oldId As Integer = q.PrimaryProperties.QuoteNumnber
        Dim table As _QuoteDetailDataTable = adaptor.GetDataByQuoteID(oldId)
        For Each detail As QuoteDetail In q.QuoteDetails
            adaptor.Connection.Open()
            adaptor.Transaction = adaptor.Connection.BeginTransaction
            adaptor.Insert(quoteId, detail.Qty, detail.ProductCode)
            Dim cmd As OleDbCommand = New OleDbCommand( _
                "SELECT @@IDENTITY", adaptor.Connection)
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
                               ByVal SaveAll As Boolean)

        Dim props As PropertyInfo() = obj.GetType.GetProperties
        Dim adaptor As New QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter

        For Each p As PropertyInfo In props

            If SaveAll = False Then
                If Not p.CanWrite Then
                    Continue For
                End If
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
