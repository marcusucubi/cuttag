﻿Imports System.Data.SqlClient
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
            q.PrimaryProperties.CommonCustomerName = row.CustomerName
            q.PrimaryProperties.CommonPartNumber = row.PartNumber
            q.PrimaryProperties.CommonRequestForQuoteNumber = row.RequestForQuoteNumber

            CommonLoader.LoadProperties(id, -1, q.ComputationProperties)
            CommonLoader.LoadProperties(id, -1, q.OtherProperties)
            CommonLoader.LoadComponents(q)
        End If

        q.ComputationProperties.ClearDirty()
        q.OtherProperties.ClearDirty()
        q.PrimaryProperties.ClearDirty()
        q.ClearDirty()

        Return q
    End Function

End Class
