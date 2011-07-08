
Public Class Shipping

    Private _Dictionary As New SortedDictionary(Of String, Decimal)

    Public Shared Property Shipping As New Shipping
    Public Property Descriptions As String() = New String() {""}

    Public Function Lookup(ByVal Description As String) As Decimal
        If (_Dictionary.ContainsKey(Description)) Then
            Return _Dictionary(Description)
        End If
        Return "Not Found"
    End Function

    Private Sub New()
        FetchShippingRows()
    End Sub

    Private Sub FetchShippingRows()
        Dim ds As QuoteDataBase = New DCS.Quote.QuoteDataBase()
        Dim table As QuoteDataBase._ShippingDataTable = New QuoteDataBase._ShippingDataTable()
        Dim adapter As QuoteDataBaseTableAdapters._ShippingTableAdapter = New QuoteDataBaseTableAdapters._ShippingTableAdapter()
        adapter.Fill(table)

        Dim a(table.Count) As String
        Dim i As Integer
        For Each s As QuoteDataBase._ShippingRow In table.Rows
            a(i) = s.Index
            _Dictionary.Add(s.Index, s.Cost)
            i += 1
        Next
        Me.Descriptions = a
    End Sub

End Class
