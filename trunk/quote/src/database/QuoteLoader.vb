﻿Imports System.Reflection
Imports DCS.Quote.Model
Imports DCS.Quote.Model.Quote
Imports DCS.Quote.QuoteDataBaseTableAdapters
Imports DCS.Quote.QuoteDataBase

Public Class QuoteLoader

	Dim _PropertyLoader As New ObjectGenerator

	Public Function Load(ByVal id As Long) As Model.Quote.Header

		frmMain.frmMain.UseWaitCursor = True
		My.Application.DoEvents()

		Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
		Dim table As New QuoteDataBase._QuoteDataTable
		Dim q As New Model.Quote.Header()

        Dim customerObj As BOM.Customer

        adaptor.FillByByQuoteID(table, CDbl(id))
		If table.Rows.Count > 0 Then
			Dim row As QuoteDataBase._QuoteRow = table.Rows(0)
            customerObj = Me.LookupCustomer(row)

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
            q = New Model.Quote.Header(row.id, customerObj.Name, rfq, part, templateID, _
              row.Initials, row.CreatedDate, row.LastModifedDate)
            'dd_Added 11/19/11
            Dim Initials As String = ""
            If Not row.IsInitialsNull Then
                Initials = row.Initials
            End If
            Dim createdDate As DateTime
            If Not row.IsCreatedDateNull Then
                createdDate = row.CreatedDate
            End If
            Dim lastModDate As DateTime
            If Not row.IsLastModifedDateNull Then
                lastModDate = row.LastModifedDate
            End If

            q.PrimaryProperties.CommonCustomer = customerObj
            q.PrimaryProperties.CommonPartNumber = part
            q.PrimaryProperties.CommonRequestForQuoteNumber = rfq
            q.PrimaryProperties.CommonCreatedDate = createdDate
            q.PrimaryProperties.CommonLastModified = lastModDate
            q.PrimaryProperties.CommonInitials = Initials
            'dd_Added End
            LoadComponents(q)

			Dim o1 = LoadProperties(id, _
					CommonSaver.COMPUTATION_PROPERTIES_ID, q.ComputationProperties)
			q.SetComputationProperties(o1)
			Dim o2 = LoadProperties(id, _
					CommonSaver.OTHER_PROPERTIES_ID, q.OtherProperties)
			q.SetOtherProperties(o2)
			Dim o3 = LoadProperties(id, _
					CommonSaver.CUSTOM_PROPERTIES_ID, q.CustomProperties)
			q.SetCustomProperties(o3)
            Dim o4 = LoadProperties(id, _
                CommonSaver.NOTE_PROPERTIES_ID, q.NoteProperties)
            q.SetNoteProperties(o4)

		End If

		frmMain.frmMain.UseWaitCursor = False

		Return q
	End Function

    Public Function LookupCustomer(row As QuoteDataBase._QuoteRow) As BOM.Customer

        Dim customer As String = row.CustomerName
        Dim customerID As Integer
        If row.IsCustomerIDNull Then
            If Not row.IsCustomerNameNull Then
                Dim temp As BOM.Customer
                temp = Model.BOM.Customer.GetByName(row.CustomerName)
                If (Not temp Is Nothing) Then
                    customerID = temp.ID
                End If
            End If
        Else
            customerID = row.CustomerID
        End If

        Dim customerObj As New Model.BOM.Customer
        customerObj.SetName(customer)
        customerObj.SetID(customerID)

        Return customerObj
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
		Dim partAdaptor As New WireComponentSourceTableAdapter
		Dim wireAdaptor As New WireSourceTableAdapter
		Dim gageAdaptor As New GageTableAdapter
		Dim id As Integer = q.PrimaryProperties.CommonID
		Dim table As _QuoteDetailDataTable = adaptor.GetDataByQuoteID(id)
		For Each row As _QuoteDetailRow In table.Rows

			Dim temp As New TempObj
			CommonLoader.LoadProperties(id, row.id, temp)

            Dim product As New Model.Product( _
              row.ProductCode, _
              temp.Gage, _
              0, _
              0, _
              row.IsWire, _
              "", _
              0,
              "", _
              0, _
              0)

			Dim detail As Detail = q.NewDetail(product)
            detail.Qty = row.Qty
            'detail.MachineTime = row.
			'dd_Added 10/3/11, 10/7/11
			With detail
				.IsWire = row.IsWire
				If Not row.IsSourceIDNull Then .SourceID = row.SourceID
                .SequenceNumber = row.SequenceNumber
                .UOM = row.UOM
			End With
			'dd_Added end

            Dim o1 As Object = LoadProperties(id, row.id, detail.QuoteDetailProperties)
            'dd_Added 11/28/11 - If conditions
            If Me._PropertyLoader.NameList.Contains("MachineTime") Then detail.MachineTime = o1.MachineTime
            If Me._PropertyLoader.NameList.Contains("UnitCost") Then detail.UnitCost = o1.UnitCost
            detail.SetProperties(o1)
        Next
	End Sub

	Public Class TempObj
		Public Property UnitOfMeasure As String
		Public Property Gage As String = ""
	End Class

End Class
