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

            Dim detail As Common.Detail = Nothing

            Dim parts As _PartsDataTable
            parts = partAdaptor.GetDataByProductCode(row.ProductCode)
            If (parts.Count > 0) Then
                Dim part As _PartsRow
                part = parts(0)
                Dim partObj As New Product( _
                    part.PartNumber, part.UnitCost, _
                    0, UnitOfMeasure.BY_EACH)

                detail = q.NewDetail(partObj)
            End If

            Dim wires As _WiresDataTable
            wires = wireAdaptor.GetDataByProductCode(row.ProductCode)
            If (wires.Count > 0) Then
                Dim wire As _WiresRow
                wire = wires(0)
                Dim gage As String = ""
                If Not wire.IsGageNull Then
                    gage = wire.Gage
                End If
                Dim wireObj As New Product( _
                    wire.PartNumber, wire.Price, _
                    gage, UnitOfMeasure.BY_LENGTH)

                detail = q.NewDetail(wireObj)
            End If

            If (detail IsNot Nothing) Then
                detail.Qty = row.Qty
                LoadProperties(id, row.ID, detail.QuoteDetailProperties)
            End If
        Next
    End Sub

    Public Shared Sub LoadOtherProperties(ByVal id As Integer, _
                                          ByVal obj As Object)
        LoadProperties(id, CommonSaver.OTHER_PROPERTIES_ID, obj)
    End Sub

    Public Shared Sub LoadComputationProperties(ByVal id As Integer, _
                                                ByVal obj As Object)
        LoadProperties(id, CommonSaver.COMPUTATION_PROPERTIES_ID, obj)
    End Sub

    Private Shared Sub LoadProperties(ByVal id As Integer, _
                                     ByVal childId As Integer, _
                                     ByVal obj As Object)

        Dim props As PropertyInfo() = obj.GetType.GetProperties
        Dim adaptor As New QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter

        For Each p As PropertyInfo In props

            Dim table As _QuotePropertiesDataTable = _
                adaptor.GetDataByQuoteAndName(id, childId, p.Name)

            If table.Rows.Count > 0 Then
                Dim row As _QuotePropertiesRow = table.Rows(0)
                If Not row.IsPropertyStringValueNull Then
                    If p.PropertyType.Name = "String" And p.CanWrite Then
                        p.SetValue(obj, row.PropertyStringValue, Nothing)
                    End If
                End If
                If Not row.IsPropertyIntegerValueNull Then
                    If p.PropertyType.Name = "Int32" And p.CanWrite Then
                        p.SetValue(obj, row.PropertyIntegerValue, Nothing)
                    End If
                End If
                If Not row.IsPropertyDecimalValueNull Then
                    If p.PropertyType.Name = "Decimal" And p.CanWrite Then
                        p.SetValue(obj, row.PropertyDecimalValue, Nothing)
                    End If
                End If
                If Not row.IsPropertyDateValueNull Then
                    If p.PropertyType.Name = "DateTime" And p.CanWrite Then
                        Dim dt As DateTime = row.PropertyDateValue
                        If dt.Year > 1900 Then
                            p.SetValue(obj, row.PropertyDateValue, Nothing)
                        End If
                    End If
                End If
            End If
        Next
    End Sub

End Class
