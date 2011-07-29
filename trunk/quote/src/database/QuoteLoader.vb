Imports System.Data.SqlClient
Imports DCS.Quote.Model
Imports DCS.Quote.QuoteDataBase
Imports System.Reflection
Imports System.Data.OleDb
Imports System.Transactions
Imports DCS.Quote.QuoteDataBaseTableAdapters
Imports DCS.Quote.Model.Quote

Public Class QuoteLoader

    Dim _PropertyLoader As New ObjectGenerator

    Public Function Load(ByVal id As Long) As Model.Quote.Header

        frmMain.frmMain.UseWaitCursor = True
        My.Application.DoEvents()

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        Dim table As New QuoteDataBase._QuoteDataTable
        Dim q As New Model.Quote.Header()

        adaptor.FillByByQuoteID(table, CDbl(id))
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

            q = New Model.Quote.Header(row.ID, customer, rfq, part, templateID, _
                row.Initials, row.CreatedDate, row.LastModifedDate)

            LoadComponents(q)

            Dim o1 = LoadProperties(id, _
                CommonSaver.COMPUTATION_PROPERTIES_ID, q.ComputationProperties)
            q.SetComputationProperties(o1)
            Dim o2 = LoadProperties(id, _
                CommonSaver.OTHER_PROPERTIES_ID, q.OtherProperties)
            q.SetOtherProperties(o2)
            Dim o3 = LoadProperties(id, _
                CommonSaver.CUSTOM_PROPERTIES_ID, q.CustomProperties)
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

        _PropertyLoader = New ObjectGenerator

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

        Dim node As New ObjectGenerator.PropertyInfo
        node.Name = row.PropertyName
        If Not row.IsPropertyStringValueNull Then
            node.TypeName = "System.String"
            node.Value = row.PropertyStringValue
        ElseIf Not row.IsPropertyDecimalValueNull Then
            node.TypeName = "System.Decimal"
            node.Value = row.PropertyDecimalValue
        ElseIf Not row.IsPropertyIntegerValueNull Then
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
        If Not row.IsPropertyCatagoryNull Then
            node.Category = row.PropertyCatagory
        End If
        If Not row.IsPropertyDescriptionNull Then
            node.Description = row.PropertyDescription
        End If
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
            parts = partAdaptor.GetDataByPartNumber(row.ProductCode)
            If (parts.Count > 0) Then
                Dim part As _PartsRow
                part = parts(0)
                Dim partObj As New Product( _
                    part.PartNumber, part.UnitCost, _
                    0, UnitOfMeasure.BY_EACH, Nothing, part)

                detail = q.NewDetail(partObj)
            End If

            Dim wires As _WiresDataTable
            wires = wireAdaptor.GetDataByPartNumber(row.ProductCode)
            If (wires.Count > 0) Then
                Dim wire As _WiresRow
                wire = wires(0)
                Dim gage As String = ""
                If Not wire.IsGageNull Then
                    gage = wire.Gage
                End If
                Dim wireObj As New Product( _
                    wire.PartNumber, wire.Price, _
                    gage, UnitOfMeasure.BY_LENGTH, wire, Nothing)

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
