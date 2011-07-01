Imports DCS.Quote.devDataSet
Imports DCS.Quote.devDataSetTableAdapters

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
        Dim ds As devDataSet = New DCS.Quote.devDataSet()
        Dim table As _ShippingDataTable = New _ShippingDataTable()
        Dim adapter As _ShippingTableAdapter = New _ShippingTableAdapter()
        adapter.Fill(table)

        Dim a(table.Count) As String
        Dim i As Integer
        For Each s As devDataSet._ShippingRow In table.Rows
            a(i) = s.Index
            _Dictionary.Add(s.Index, s.Cost)
            i += 1
        Next
        Me.Descriptions = a
    End Sub

End Class
