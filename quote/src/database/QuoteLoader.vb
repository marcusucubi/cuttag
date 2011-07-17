﻿Imports System.Data.SqlClient
Imports DCS.Quote.Model
Imports DCS.Quote.QuoteDataBase
Imports System.Reflection
Imports System.Data.OleDb
Imports System.Transactions
Imports DCS.Quote.QuoteDataBaseTableAdapters
Imports DCS.Quote.Model.Quote

Public Class QuoteLoader

    Dim _PropertyLoader As New PropertyLoader

    Public Function Load(ByVal id As Long) As Model.Quote.Header

        frmMain.frmMain.UseWaitCursor = True
        My.Application.DoEvents()

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        Dim table As New QuoteDataBase._QuoteDataTable
        Dim q As New Model.Quote.Header()

        adaptor.FillByQuoteID(table, id)
        If table.Rows.Count > 0 Then
            Dim row As QuoteDataBase._QuoteRow = table.Rows(0)

            Dim customer As String = row.CustomerName
            Dim rfq As String = ""
            If Not row.IsRequestForQuoteNumberNull Then
                rfq = row.RequestForQuoteNumber
            End If
            Dim part As String = ""
            If Not row.IsPartNumberNull Then
                part = row.PartNumber
            End If
            Dim templateID As Long
            If Not row.IsTemplateIDNull Then
                templateID = row.TemplateID
            End If

            q = New Model.Quote.Header(row.ID, customer, rfq, part, templateID)

            LoadComponents(q)

            Dim o1 = LoadProperties(id, -1, q.ComputationProperties)
            q.SetComputationProperties(o1)
            Dim o2 = LoadProperties(id, -2, q.OtherProperties)
            q.SetOtherProperties(o2)

        End If

        frmMain.frmMain.UseWaitCursor = False

        Return q
    End Function

    Private Function LoadProperties(ByVal id As Integer, _
                                    ByVal childId As Integer, _
                                    ByVal obj As Object) _
                                    As Object

        Dim adaptor As New QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter

        _PropertyLoader = New PropertyLoader

        Dim table As _QuotePropertiesDataTable = _
                adaptor.GetDataByQuoteIDAndPropertyID(id, childId)

        For Each row As _QuotePropertiesRow In table.Rows
            AddNode(row)
        Next

        Dim o As New Object
        _PropertyLoader.BaseTypeName = obj.GetType.FullName
        o = _PropertyLoader.Generate()
        Return o
    End Function

    Private Sub AddNode(ByVal row As _QuotePropertiesRow)

        Dim node As New PropertyLoader.PropertyInfo
        node.Name = row.PropertyName
        If row("PropertyStringValue") IsNot DBNull.Value Then
            node.TypeName = "System.String"
            node.Value = row.PropertyStringValue
        ElseIf row("PropertyDecimalValue") IsNot DBNull.Value Then
            node.TypeName = "System.Decimal"
            node.Value = row.PropertyDecimalValue
        ElseIf row("PropertyIntegerValue") IsNot DBNull.Value Then
            node.TypeName = "System.Int32"
            node.Value = row.PropertyIntegerValue
        ElseIf Not row.IsPropertyDateValueNull Then
            node.TypeName = "System.String"
            Dim dt As DateTime = row.PropertyDateValue
            If dt.Year > 1900 Then
                node.Value = row.PropertyDateValue.ToShortDateString
            Else
                node.Value = ""
            End If
        End If
        node.Category = row.PropertyCatagory
        node.Description = row.PropertyDescription
        _PropertyLoader.Add(node)
    End Sub

    Public Sub LoadComponents(ByVal q As Header)

        Dim adaptor As New _QuoteDetailTableAdapter
        Dim partAdaptor As New _PartsTableAdapter
        Dim wireAdaptor As New _WiresTableAdapter
        Dim id As Integer = q.PrimaryProperties.CommonID
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

                detail = q.NewDetail(partObj)
            End If

            Dim wires As _WiresDataTable
            wires = wireAdaptor.GetDataByProductCode(row.ProductCode)
            If (wires.Count > 0) Then
                Dim wire As _WiresRow
                wire = wires(0)
                Dim wireObj As New Product( _
                    wire.PartNumber, wire.Price, _
                    wire.Gage, UnitOfMeasure.BY_LENGTH)

                detail = q.NewDetail(wireObj)
            End If

            If (detail IsNot Nothing) Then
                detail.Qty = row.Qty
                Dim o1 = LoadProperties(id, row.ID, detail.QuoteDetailProperties)
                detail.SetProperties(o1)
            End If
        Next
    End Sub

End Class