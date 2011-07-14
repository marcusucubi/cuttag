Imports System.Data.SqlClient
Imports DCS.Quote
Imports DCS.Quote.Model.Template
Imports System.Reflection
Imports System.Data.OleDb
Imports System.Transactions
Imports DCS.Quote.QuoteDataBaseTableAdapters
Imports DCS.Quote.QuoteDataBase
Imports DCS.Quote.Model

Public Class TemplateLoader

    Public Function Load(ByVal id As Long) As Header

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        Dim table As New QuoteDataBase._QuoteDataTable
        Dim q As New Header()

        adaptor.FillByQuoteID(table, id)
        If table.Rows.Count > 0 Then
            Dim row As QuoteDataBase._QuoteRow = table.Rows(0)
            q = New Header(row.ID)
            Dim customer As String = row.CustomerName
            Dim rfq As String = ""
            If Not row.IsRequestForQuoteNumberNull Then
                rfq = row.RequestForQuoteNumber
            End If
            Dim part As String = ""
            If Not row.IsPartNumberNull Then
                part = row.PartNumber
            End If
            q.PrimaryProperties.CommonCustomerName = customer
            q.PrimaryProperties.CommonPartNumber = part
            q.PrimaryProperties.CommonRequestForQuoteNumber = rfq

            CommonLoader.LoadComputationProperties(id, q.ComputationProperties)
            CommonLoader.LoadOtherProperties(id, q.OtherProperties)
            CommonLoader.LoadComponents(q)
        End If

        q.ComputationProperties.ClearDirty()
        q.OtherProperties.ClearDirty()
        q.PrimaryProperties.ClearDirty()
        q.ClearDirty()

        Return q
    End Function

End Class
