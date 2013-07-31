Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Data.OleDb
Imports System.Transactions

Imports Model.Common

Imports DB.QuoteDataBase
Imports DB.QuoteDataBaseTableAdapters

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

    Public Shared Sub LoadComponents(ByVal q As Model.Common.Header)

        Dim adaptor As New _QuoteDetailTableAdapter
        Dim partAdaptor As New WireComponentSourceTableAdapter
        Dim wireAdaptor As New WireSourceTableAdapter
        Dim gageAdaptor As New GageTableAdapter
        Dim id As Integer = q.PrimaryProperties.CommonId
        Dim table As _QuoteDetailDataTable = adaptor.GetDataByQuoteID(id)
        For Each row As _QuoteDetailRow In table.Rows

            Dim detail As Model.Common.Detail = Nothing

            Dim parts As WireComponentSourceDataTable
            parts = partAdaptor.GetDataByPartNumber(row.ProductCode)
            If (parts.Count > 0) Then
                Dim part As WireComponentSourceRow
                part = parts(0)
                Dim price As Decimal = 0
                If Not part.IsQuotePriceNull Then
                    price = part.QuotePrice
                End If
                
                Dim data As ProductBuildData = ProductDB.Load( _
                    part.PartNumber, price, _
                    0, False, Nothing, part, "", 0)
                Dim partObj As New Model.Product(Data)

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
                If Not wire.IsQuotePriceNull Then
                    price = wire.QuotePrice
                End If
                
                Dim data As ProductBuildData = ProductDB.Load( _
                    wire.PartNumber, price, _
                    gage, True, wire, Nothing, "", 0)
                Dim wireObj As New Model.Product(Data)

                detail = q.NewDetail(wireObj)
            End If

            If (detail IsNot Nothing) Then
                detail.Qty = row.Qty
                LoadProperties(id, row.id, detail.QuoteDetailProperties)
            End If
        Next
    End Sub

    Public Shared Sub LoadProperties(ByVal id As Integer, _
                                     ByVal childId As Integer, _
                                     ByVal obj As Object)

        Dim props As PropertyInfo() = obj.GetType.GetProperties
        Dim adaptor As New DB.QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter
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
                If Not row.IsPropertyStringValueNull Then
                    If p.PropertyType.Name = "Boolean" And p.CanWrite Then
                        If row.PropertyStringValue = "True" Then
                            p.SetValue(obj, True, Nothing)
                        Else
                            p.SetValue(obj, False, Nothing)
                        End If
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
                    If p.PropertyType.Name = "Decimal" And Not p.CanWrite Then
                        Dim m As MethodInfo = obj.GetType.GetMethod("Set" + p.Name)
                        If (m IsNot Nothing) Then
                            m.Invoke(obj, New Object() {row.PropertyDecimalValue})
                        End If
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
