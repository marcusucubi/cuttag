Imports System.Data.SqlClient
Imports DCS.Quote.Model
Imports DCS.Quote.QuoteDataBase

Public Class QuoteLoader

    Public Sub Save(ByVal q As Model.QuoteHeader)

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        Dim o As PrimaryPropeties = q.PrimaryProperties
        adaptor.Insert(o.CustomerName, o.PartNumber, o.QuoteNumnber)

    End Sub

    Public Function Load(ByVal id As Long) As Model.QuoteHeader

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        Dim table As New QuoteDataBase._QuoteDataTable

        adaptor.FillByQuoteID(table, id)
        Dim row As QuoteDataBase._QuoteRow = table.Rows(0)
        Dim q As New Model.QuoteHeader()

        q.PrimaryProperties.CustomerName = row.CustomerName
        q.PrimaryProperties.PartNumber = row.PartNumber
        q.PrimaryProperties.RequestForQuoteNumber = row.RequestForQuoteNumber

        Return q
    End Function

End Class
