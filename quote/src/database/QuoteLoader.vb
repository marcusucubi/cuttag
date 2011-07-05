Imports System.Data.SqlClient
Imports DCS.Quote.Model
Imports DCS.Quote.QuoteDataBase

Public Class QuoteLoader

    Public Sub Save(ByVal q As Model.QuoteHeader)

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        Dim o As PrimaryPropeties = q.PrimaryProperties
        If q.PrimaryProperties.QuoteNumnber > 0 Then
            Dim table As New QuoteDataBase._QuoteDataTable
            Dim row As QuoteDataBase._QuoteRow = table.New_QuoteRow()
            row.CustomerName = o.CustomerName
            row.RequestForQuoteNumber = o.RequestForQuoteNumber
            row.PartNumber = o.PartNumber
            adaptor.Update(row)
        Else
            adaptor.Insert(o.CustomerName, o.PartNumber, o.RequestForQuoteNumber)
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

End Class
