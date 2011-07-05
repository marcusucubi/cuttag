Imports System.Data.SqlClient
Imports DCS.Quote.Model
Imports DCS.Quote.QuoteDataBase
Imports System.Reflection
Imports System.Data.OleDb

Public Class QuoteLoader

    Public Sub Save(ByVal q As Model.QuoteHeader)

        ' Ensure the properies are updated
        frmMain.frmMain.Focus()

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        Dim o As PrimaryPropeties = q.PrimaryProperties
        If q.PrimaryProperties.QuoteNumnber > 0 Then
            adaptor.Update( _
                o.CustomerName, o.RequestForQuoteNumber, _
                o.PartNumber, o.QuoteNumnber)

        Else
            adaptor.Connection.Open()
            adaptor.Transaction = adaptor.Connection.BeginTransaction
            adaptor.Insert(o.CustomerName, o.PartNumber, o.RequestForQuoteNumber)
            Dim cmd As OleDbCommand = New OleDbCommand("SELECT @@IDENTITY", adaptor.Connection)
            cmd.Transaction = adaptor.Transaction
            Dim id As Integer = CInt(cmd.ExecuteScalar())
            adaptor.Transaction.Commit()
            adaptor.Connection.Close()
            o.SetID(id)
        End If

    End Sub

    Public Function Load(ByVal id As Long) As Model.QuoteHeader

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        Dim table As New QuoteDataBase._QuoteDataTable
        Dim q As New Model.QuoteHeader()

        adaptor.FillByQuoteID(table, id)
        If table.Rows.Count > 0 Then
            Dim row As QuoteDataBase._QuoteRow = table.Rows(0)
            q = New Model.QuoteHeader(row.ID)
            q.PrimaryProperties.CustomerName = row.CustomerName
            q.PrimaryProperties.PartNumber = row.PartNumber
            q.PrimaryProperties.RequestForQuoteNumber = row.RequestForQuoteNumber
        End If
        Return q
    End Function

    Private Function BuildInsertSql(ByVal obj As Object) As String
        Dim s As String = ""
        Dim props As PropertyInfo() = obj.GetType.GetProperties
        For Each p As PropertyInfo In props

        Next
        Return s
    End Function


End Class
