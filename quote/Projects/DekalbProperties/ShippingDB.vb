Imports System.Collections.ObjectModel
Imports System.Linq

Public NotInheritable Class ShippingDB

    Private Sub New()
    End Sub

    Public Shared Sub Initialize()
        Dim table As New DB.QuoteDataBase._ShippingDataTable()
        Dim adapter As New DB.QuoteDataBaseTableAdapters._ShippingTableAdapter()
        adapter.Fill(table)

        Dim a As String() = New String(table.Count) {}
        Dim i As Integer = 0
        For Each s As DB.QuoteDataBase._ShippingRow In table.Rows
            a(i) = s.Index
            Shipping.Dictionary.Add(s.Index, s.Cost)
            i += 1
        Next

        Shipping.SetupDescriptions(New ReadOnlyCollection(Of String)(a))
    End Sub
End Class
