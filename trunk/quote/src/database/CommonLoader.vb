Imports System.Data.SqlClient
Imports DCS.Quote.Model
Imports DCS.Quote.QuoteDataBase
Imports System.Reflection
Imports System.Data.OleDb
Imports System.Transactions
Imports DCS.Quote.QuoteDataBaseTableAdapters
Imports DCS.Quote.Common
Imports DCS.Quote.Common.CustomPropertiesGenerator

Public Class CommonLoader

    Public Shared Sub LoadNoteProperties(ByVal id As Integer, _
                                         ByVal obj As Object)
        LoadProperties(id, CommonSaver.NOTE_PROPERTIES_ID, obj)
    End Sub

    Public Shared Sub LoadOtherProperties(ByVal id As Integer, _
                                          ByVal obj As Object)
        LoadProperties(id, CommonSaver.OTHER_PROPERTIES_ID, obj)
    End Sub

    Public Shared Sub LoadComputationProperties(ByVal id As Integer, _
                                                ByVal obj As Object)
        LoadProperties(id, CommonSaver.COMPUTATION_PROPERTIES_ID, obj)
    End Sub

    Public Shared Sub LoadComponents(ByVal q As Common.Header)

        Dim adaptor As New _QuoteDetailTableAdapter
        Dim partAdaptor As New WireComponentSourceTableAdapter
        Dim wireAdaptor As New WireSourceTableAdapter
        Dim gageAdaptor As New GageTableAdapter
        Dim id As Integer = q.PrimaryProperties.CommonID
        Dim table As _QuoteDetailDataTable = adaptor.GetDataByQuoteID(id)
        For Each row As _QuoteDetailRow In table.Rows

            Dim detail As Common.Detail = Nothing

            Dim parts As WireComponentSourceDataTable
            parts = partAdaptor.GetDataByPartNumber(row.ProductCode)
            If (parts.Count > 0) Then
                Dim part As WireComponentSourceRow
                part = parts(0)
                Dim price As Decimal = 0
                If Not part.IsQuotePriceNull Then
                    price = part.QuotePrice
                End If
                Dim partObj As New Product( _
                    part.PartNumber, price, _
                    0, UnitOfMeasure.BY_EACH, Nothing, part)

                detail = q.NewDetail(partObj)
            End If

            Dim wires As WireSourceDataTable
            wires = wireAdaptor.GetDataByPartNumber(row.ProductCode)
            If (wires.Count > 0) Then
                Dim wire As WireSourceRow
                wire = wires(0)
                Dim gage As String = ""
                Dim gageTable As GageDataTable
                gageTable = gageAdaptor.GetDataByGageID(wire.GageID)
                If gageTable IsNot Nothing Then
                    Dim gageRow As GageRow = gageTable.Rows(0)
                    gage = gageRow.Gage
                End If

                Dim price As Decimal = 0
                If Not wire.IsWireTypeIDNull Then
                    price = wire.QuotePrice
                End If
                Dim wireObj As Product
                wireObj = New Product( _
                    wire.PartNumber, price, _
                    gage, UnitOfMeasure.BY_LENGTH, wire, Nothing)

                detail = q.NewDetail(wireObj)
            End If

            If (detail IsNot Nothing) Then
                detail.Qty = row.Qty
                LoadProperties(id, row.id, detail.QuoteDetailProperties)
            End If
        Next
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

    Public Shared Sub LoadCustomPropertiesGenerator(ByVal gen As CustomPropertiesGenerator)

        Dim adaptor As New QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter

        Dim table As QuoteDataBase._QuotePropertiesDataTable
        table = adaptor.GetDataByQuoteID(CommonSaver.CUSTOM_PROPERTIES_ID)
        gen.Properties.Clear()

        For Each row As QuoteDataBase._QuotePropertiesRow In table.Rows
            Dim prop As New PropInfo
            If Not row.IsPropertyStringValueNull Then
                prop.Expression = row.PropertyStringValue
            End If
            If Not row.IsPropertyNameNull Then
                prop.Name = row.PropertyName
            End If
            gen.Properties.Add(prop)
        Next
    End Sub

End Class
