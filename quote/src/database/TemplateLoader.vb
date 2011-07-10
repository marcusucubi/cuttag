Imports System.Data.SqlClient
Imports DCS.Quote
Imports DCS.Quote.Model.Template
Imports System.Reflection
Imports System.Data.OleDb
Imports System.Transactions
Imports DCS.Quote.QuoteDataBaseTableAdapters
Imports DCS.Quote.QuoteDataBase
Imports DCS.Quote.Model

Public Class TemplateLoader

    Public Function Load(ByVal id As Long) As Header

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        Dim table As New QuoteDataBase._QuoteDataTable
        Dim q As New Header()

        adaptor.FillByQuoteID(table, id)
        If table.Rows.Count > 0 Then
            Dim row As QuoteDataBase._QuoteRow = table.Rows(0)
            q = New Header(row.ID)
            q.PrimaryProperties.CommonCustomerName = row.CustomerName
            q.PrimaryProperties.CommonPartNumber = row.PartNumber
            q.PrimaryProperties.CommonRequestForQuoteNumber = row.RequestForQuoteNumber

            LoadProperties(id, -1, q.ComputationProperties)
            LoadProperties(id, -1, q.OtherProperties)
            Me.LoadComponents(q)
        End If

        q.ComputationProperties.ClearDirty()
        q.OtherProperties.ClearDirty()
        q.PrimaryProperties.ClearDirty()
        q.ClearDirty()


        Return q
    End Function

    Private Sub LoadComponents(ByVal q As Header)

        Dim adaptor As New _QuoteDetailTableAdapter
        Dim partAdaptor As New _PartsTableAdapter
        Dim wireAdaptor As New _WiresTableAdapter
        Dim id As Integer = q.PrimaryProperties.CommonQuoteNumber
        Dim table As _QuoteDetailDataTable = adaptor.GetDataByQuoteID(id)
        For Each row As _QuoteDetailRow In table.Rows

            Dim detail As Detail = Nothing

            Dim parts As _PartsDataTable
            parts = partAdaptor.GetDataByProductCode(row.ProductCode)
            If (parts.Count > 0) Then
                Dim part As _PartsRow
                part = parts(0)
                Dim partObj As New Product( _
                    part.PartNumber, part.UnitCost, _
                    0, UnitOfMeasure.BY_EACH)

                detail = New Detail(q, partObj)
            End If

            Dim wires As _WiresDataTable
            wires = wireAdaptor.GetDataByProductCode(row.ProductCode)
            If (wires.Count > 0) Then
                Dim wire As _WiresRow
                wire = wires(0)
                Dim wireObj As New Product( _
                    wire.PartNumber, wire.Price, _
                    wire.Gage, UnitOfMeasure.BY_LENGTH)

                detail = New Detail(q, wireObj)
            End If

            If (detail IsNot Nothing) Then
                detail.Qty = row.Qty
                Me.LoadProperties(id, row.ID, detail.QuoteDetailProperties)
                q.Details.Add(detail)
            End If
        Next
    End Sub

    Private Sub LoadProperties(ByVal id As Integer, _
                               ByVal childId As Integer, _
                               ByVal obj As Object)

        Dim props As PropertyInfo() = obj.GetType.GetProperties
        Dim adaptor As New QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter

        For Each p As PropertyInfo In props

            If Not p.CanWrite Then
                Continue For
            End If

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
