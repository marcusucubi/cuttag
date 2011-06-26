'Imports System.Data.OleDb
'Imports System.Runtime.InteropServices
'Imports DCS.DCSShared
'Imports XL = Microsoft.Office.Interop.Excel
'Imports System.Threading
'Public Class LoadXL
'	Private WithEvents oXL As XL.Application
'	Public Function Connect2XL() As Guid
'		Dim retValue As Guid = Guid.Empty
'		oXL = New XL.Application
'		Dim oWB As XL.Workbook
'		Dim oSheet, oSheet2 As XL.Worksheet
'		Dim oQuoteDB As New DCS.QuoteDB
'		Dim dsQ As New DCS.dsQuote
'		Dim drH As DCS.dsQuote.QuoteHeaderRow

'		'		drH = oQuoteDB.GetHeaderRow(New Guid("55C29BC3-4B82-42FC-800B-41468AB1113C"))
'		'		Dim t As dsQuote.QuoteDetailDataTable = oQuoteDB.GetDetailRows(New Guid("55C29BC3-4B82-42FC-800B-41468AB1113C"))

'		Dim sCustomer As String
'		drH = dsQ.QuoteHeader.NewQuoteHeaderRow
'		oXL.Visible = False
'		Dim dlg As New DCS.DCSOpenFileDialog("WHQ", "XLImportFile", "")
'		Dim sFN As String = dlg.ShowDialog("Select Quote Import File", "MS Excel Workbook(*.xls)|*.xls|All Files(*.*)|*.*")
'		If sFN = "" Then
'			MsgBox("Excel quote file was not loaded.")
'		Else
'			Try
'				oWB = oXL.Workbooks.Open(sFN)
'				oSheet = oWB.Worksheets(1)
'				oSheet2 = oWB.Worksheets(2)
'				Dim gNewQuoteID As Guid = Guid.NewGuid
'				Dim sPartNumber As String
'				With drH
'					.QuoteID = gNewQuoteID
'					.QuoteNumber = oSheet.Range("SKE_QuoteNumber").Value
'					sCustomer = oSheet.Range("Customer").Value
'					If sCustomer.Contains("Caterpillar") Then
'						.CustomerID = 1000
'					Else
'						.CustomerID = 0
'					End If
'					.ContactName = oSheet.Range("ContactName").Value
'					.RFQ = oSheet.Range("RFQ").Value
'					.QuoteDate = System.DateTime.Today
'					.DueDate = CType(oSheet.Range("DueDate").Value, Date)
'					sPartNumber = oSheet.Range("PartNumber").Value
'					.PartNumber = sPartNumber.Substring(0, sPartNumber.LastIndexOf("-"))
'					.PartVersion = "0"
'					.PartRevision = sPartNumber.Substring(sPartNumber.LastIndexOf("-") + 1)
'					.PartID = Nothing
'					.ShippedBefore = IIf(oSheet.Range("ShippedBefore").Value = "Y", True, False)
'					.NewRevision = IIf(oSheet.Range("NewRevision").Value = "Y", True, False)
'					.Comment = oSheet.Range("Comment").Value
'					.CreatedBy = oSheet.Range("CreatedBy").Value
'					.StartDate = CType(oSheet.Range("StartDate").Value, Date)
'					.CompletedDate = CType(oSheet.Range("CompletedDate").Value, Date)
'					.VerifiedBy = oSheet.Range("VerifiedBy").Value
'					.VerifiedDate = CType(oSheet.Range("VerifiedDate").Value, Date)
'					.IsToolingPORecd = IIf(oSheet.Range("IsToolingPORecd").Value = 2, False, True)
'					.EAU = CType(oSheet.Range("EAU").Value, Integer)
'					.Minimum = CType(oSheet.Range("Minimum").Value, Integer)
'					.LeadTimeInitial = CType(oSheet.Range("LeadTimeInitial").Value, Integer)
'					.LeadTimeStandard = CType(oSheet.Range("LeadTimeStandard").Value, Integer)
'					.QuoteTypeID = CType(oSheet.Range("QuoteType").Value, Integer)
'					.UnitPrice = CType(oSheet.Range("UnitPrice").Value, Decimal)
'					.MinimumOrder = 35.0
'					.LaborMinutes = CType(oSheet.Range("LaborMinutes").Value, Decimal)
'					.LaborRate = CType(oSheet.Range("LaborRate").Value, Decimal)
'					.Tooling = CType(oSheet.Range("Tooling").Value, Decimal)
'					.Shipping = CType(oSheet.Range("Shipping").Value, Decimal)
'					.FormBoardRequired = IIf(oSheet.Range("FormBoardRequired").Value = 2, False, True)
'					.FormBoardCost = CType(oSheet.Range("FormBoardCost").Value, Decimal)
'					.SingleDefQty = CType(oSheet.Range("SingleDefQty").Value, Integer)
'					.OrderQty = CType(oSheet.Range("OrderQty").Value, Integer)
'					.WireTime = CType(oSheet.Range("WireTime").Value, Decimal)
'					.CutTime = CType(oSheet.Range("CutTime").Value, Decimal)
'					.Cuts = CType(oSheet.Range("Cuts").Value, Integer)
'					.BoxSize = oSheet.Range("BoxSize").Value
'					.BoxPrice = CType(oSheet.Range("BoxPrice").Value, Decimal)
'					.CuPrice = CType(oSheet2.Range("CuPrice").Value, Decimal)
'					.CuWeight = (CType(oSheet.Range("CuWeight").Value, Decimal))
'					.CuWeightMultiplier = 1.03
'					.TimeMultiplier = CType(oSheet.Range("TimeMultiplier").Value, Decimal)
'				End With
'				dsQ.QuoteHeader.Rows.Add(drH)
'				If Not oQuoteDB.UpdateHeaderRow(drH) Then Throw New Exception("Quote save failed. Please report problem.")
'				Dim iRow, iCol, iLineNumber As Integer
'				iRow = oSheet.Range("ComponentNonWire").Row	'27
'				iCol = oSheet.Range("ComponentNonWire").Column	'1
'				iLineNumber = 1
'				Dim drD As DCS.dsQuote.QuoteDetailRow
'				Do While Not oSheet.Cells(iRow, iCol + 4).Formula.ToString.Contains("=SUM(")
'					If Val(oSheet.Cells(iRow, iCol).Value) > 0 Then
'						drD = dsQ.QuoteDetail.NewQuoteDetailRow
'						With drD
'							.QuoteID = gNewQuoteID
'							.LineNumber = iLineNumber
'							.Qty = CType(oSheet.Cells(iRow, iCol).Value, Decimal)
'							.PartNumber = oSheet.Cells(iRow, iCol + 2).Value
'							.UnitCost = CType(Val(oSheet.Cells(iRow, iCol + 3).Value), Decimal)
'							.IsWire = False
'							.Time = CType(oSheet.Cells(iRow, iCol + 5).Value, Decimal)
'						End With
'						dsQ.QuoteDetail.Rows.Add(drD)
'						If Not oQuoteDB.UpdateDetailRow(drD) Then Throw New Exception("Quote save failed. Please report problem.")
'					End If
'					iRow += 1
'					iLineNumber += 1
'				Loop
'				iRow = oSheet.Range("ComponentWire").Row	'28
'				iCol = oSheet.Range("ComponentWire").Column	'9
'				Do While Not oSheet.Cells(iRow, iCol + 4).Formula.ToString.Contains("=SUM(")
'					drD = dsQ.QuoteDetail.NewQuoteDetailRow
'					With drD
'						.QuoteID = gNewQuoteID
'						.LineNumber = iLineNumber
'						.Qty = CType(oSheet.Cells(iRow, iCol).Value, Decimal)
'						.PartNumber = oSheet.Cells(iRow, iCol + 2).Value
'						.UnitCost = CType(oSheet.Cells(iRow, iCol + 3).Value, Decimal)
'						.IsWire = True
'					End With
'					dsQ.QuoteDetail.Rows.Add(drD)
'					If Not oQuoteDB.UpdateDetailRow(drD) Then Throw New Exception("Quote save failed. Please report problem.")
'					iRow += 1
'					iLineNumber += 1
'				Loop
'				MsgBox("Quote data saved")
'				Dim pointer As New IntPtr(oXL.Hwnd)
'				oXL.Visible = True 'must be temp visible to kill process
'				Dim proc As System.Diagnostics.Process
'				For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
'					If proc.MainWindowHandle = pointer Then
'						proc.Kill()
'					End If
'				Next
'				retValue = gNewQuoteID
'			Catch ex As Exception
'				MsgBox("Error extracting and saving the quote. Please report this error: " + ex.ToString)
'			End Try
'			oSheet = Nothing
'			oSheet2 = Nothing
'			oWB = Nothing
'			oWB = Nothing
'			oXL = Nothing
'		End If
'		Return retValue
'	End Function
'End Class
