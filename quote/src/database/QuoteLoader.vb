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
            q = New Model.Quote.Header(row.ID, row.CustomerName, _
                row.RequestForQuoteNumber, row.PartNumber)

            CommonLoader.LoadComponents(q)

            Dim o1 = LoadProperties(id, -1, q.ComputationProperties)
            q.SetComputationProperties(o1)
            Dim o2 = LoadProperties(id, -2, q.OtherProperties)
            q.SetOtherProperties(o2)

        End If

        Return q
    End Function

    Public Shared Function LoadProperties(ByVal id As Integer, _
                                     ByVal childId As Integer, _
                                     ByVal obj As Object) _
                                 As Object

        Dim adaptor As New QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter

        Dim loader As New PropertyLoader

        Dim table As _QuotePropertiesDataTable = _
                adaptor.GetDataByQuoteIDAndPropertyID(id, childId)

        For Each row As _QuotePropertiesRow In table.Rows
            If row("PropertyStringValue") IsNot DBNull.Value Then
                Dim node As New PropertyLoader.Node
                node.Name = row.PropertyName
                node.TypeName = "System.String"
                node.Value = row.PropertyStringValue
                node.Category = row.PropertyCatagory
                If Not loader.PropertyNames2.Contains(node.Name) Then
                    loader.PropertyNames.Add(node)
                    loader.PropertyNames2.Add(node.Name)
                End If
            ElseIf row("PropertyDecimalValue") IsNot DBNull.Value Then
                Dim node As New PropertyLoader.Node
                node.Name = row.PropertyName
                node.TypeName = "System.Decimal"
                node.Value = row.PropertyDecimalValue
                node.Category = row.PropertyCatagory
                If Not loader.PropertyNames2.Contains(node.Name) Then
                    loader.PropertyNames.Add(node)
                    loader.PropertyNames2.Add(node.Name)
                End If
            ElseIf row("PropertyIntegerValue") IsNot DBNull.Value Then
                Dim node As New PropertyLoader.Node
                node.Name = row.PropertyName
                node.TypeName = "System.Int32"
                node.Value = row.PropertyIntegerValue
                node.Category = row.PropertyCatagory
                If Not loader.PropertyNames2.Contains(node.Name) Then
                    loader.PropertyNames.Add(node)
                    loader.PropertyNames2.Add(node.Name)
                End If
            End If
        Next

        Dim o As New Object
        loader.BaseTypeName = obj.GetType.FullName  ' "DCS.Quote.Common.ComputationProperties"
        o = loader.Generate()
        Return o
    End Function

End Class
