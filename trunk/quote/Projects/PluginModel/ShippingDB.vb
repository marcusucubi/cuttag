Imports System.Collections.ObjectModel
Imports DB

Public NotInheritable Class ShippingDB
    
    Private Sub New()
        
    End Sub
    
    Public Shared Sub InitializeShipping()
        
        Dim ds As QuoteDataBase = New QuoteDataBase()
        Dim table As QuoteDataBase._ShippingDataTable = New QuoteDataBase._ShippingDataTable()
        Dim adapter As QuoteDataBaseTableAdapters._ShippingTableAdapter = New QuoteDataBaseTableAdapters._ShippingTableAdapter()
        adapter.Fill(table)

        Dim a(table.Count) As String
        Dim i As Integer
        For Each s As QuoteDataBase._ShippingRow In table.Rows
            a(i) = s.Index
            Shipping.Dictionary.Add(s.Index, s.Cost)
            i += 1
        Next
        
        Shipping.Descriptions = New ReadOnlyCollection(Of String)(a)
    End Sub
    
End Class
