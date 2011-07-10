Imports System.Data.SqlClient
Imports DCS.Quote.Model
Imports DCS.Quote.QuoteDataBase
Imports System.Reflection
Imports System.Data.OleDb
Imports System.Transactions
Imports DCS.Quote.QuoteDataBaseTableAdapters

Public Class QuoteLoader

    Public Function Load(ByVal id As Long) As Model.Quote.Header

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        Dim table As New QuoteDataBase._QuoteDataTable
        Dim q As New Model.Quote.Header()

        adaptor.FillByQuoteID(table, id)
        If table.Rows.Count > 0 Then
            Dim row As QuoteDataBase._QuoteRow = table.Rows(0)
            q = New Model.Quote.Header(row.ID)
            q.PrimaryProperties.CommonCustomerName = row.CustomerName
            q.PrimaryProperties.CommonPartNumber = row.PartNumber
            q.PrimaryProperties.CommonRequestForQuoteNumber = row.RequestForQuoteNumber

            CommonLoader.LoadProperties(id, -1, q.ComputationProperties)
            CommonLoader.LoadProperties(id, -1, q.OtherProperties)
            CommonLoader.LoadComponents(q)
        End If

        Return q
    End Function

End Class
