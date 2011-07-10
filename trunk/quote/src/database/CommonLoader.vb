Imports System.Data.SqlClient
Imports DCS.Quote.Model
Imports DCS.Quote.QuoteDataBase
Imports System.Reflection
Imports System.Data.OleDb
Imports System.Transactions
Imports DCS.Quote.QuoteDataBaseTableAdapters

Public Class CommonLoader

    Public Shared Sub LoadComponents(ByVal q As Common.Header)

        Dim adaptor As New _QuoteDetailTableAdapter
        Dim partAdaptor As New _PartsTableAdapter
        Dim wireAdaptor As New _WiresTableAdapter
        Dim id As Integer = q.PrimaryProperties.CommonID
        Dim table As _QuoteDetailDataTable = adaptor.GetDataByQuoteID(id)
        For Each row As _QuoteDetailRow In table.Rows

            Dim detail As Model.Quote.Detail = Nothing

            Dim parts As _PartsDataTable
            parts = partAdaptor.GetDataByProductCode(row.ProductCode)
            If (parts.Count > 0) Then
                Dim part As _PartsRow
                part = parts(0)
                Dim partObj As New Product( _
                    part.PartNumber, part.UnitCost, _
                    0, UnitOfMeasure.BY_EACH)

                detail = New Model.Quote.Detail(q, partObj)
            End If

            Dim wires As _WiresDataTable
            wires = wireAdaptor.GetDataByProductCode(row.ProductCode)
            If (wires.Count > 0) Then
                Dim wire As _WiresRow
                wire = wires(0)
                Dim wireObj As New Product( _
                    wire.PartNumber, wire.Price, _
                    wire.Gage, UnitOfMeasure.BY_LENGTH)

                detail = New Model.Quote.Detail(q, wireObj)
            End If

            If (detail IsNot Nothing) Then
                detail.Qty = row.Qty
                LoadProperties(id, row.ID, detail.QuoteDetailProperties)
                q.Details.Add(detail)
            End If
        Next
    End Sub

    Public Shared Sub LoadProperties(ByVal id As Integer, _
                               ByVal childId As Integer, _
                               ByVal obj As Object)

        Dim props As PropertyInfo() = obj.GetType.GetProperties
        Dim adaptor As New QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter

        For Each p As PropertyInfo In props

            Dim table As _QuotePropertiesDataTable = _
                adaptor.GetDataByQuoteAndName(id, childId, p.Name)

            If table.Rows.Count > 0 Then
                Dim row As _QuotePropertiesRow = table.Rows(0)
                If row("PropertyStringValue") IsNot DBNull.Value Then
                    If p.PropertyType.Name = "String" Then
                        p.SetValue(obj, row.PropertyStringValue, Nothing)
                    End If
                End If
                If row("PropertyDecimalValue") IsNot DBNull.Value Then
                    If p.PropertyType.Name = "Decimal" Then
                        p.SetValue(obj, row.PropertyDecimalValue, Nothing)
                    End If
                End If
                If row("PropertyIntegerValue") IsNot DBNull.Value Then
                    If p.PropertyType.Name = "Int32" Then
                        p.SetValue(obj, row.PropertyIntegerValue, Nothing)
                    End If
                End If
            End If
        Next
    End Sub

End Class
