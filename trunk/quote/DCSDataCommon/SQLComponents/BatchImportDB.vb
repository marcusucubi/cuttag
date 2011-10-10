Imports DCS.DCSShared
Public Class BatchImportDB
	Public Function GetBatchImportTypeRow() As dsBatchImport.BatchImportTypeRow
		Dim retValue As dsBatchImport.BatchImportTypeRow = Nothing
		Try
			Dim daType As New dsBatchImportTableAdapters.BatchImportTypeTableAdapter
			daType.Connection = g_cn
			Dim dt As dsBatchImport.BatchImportTypeDataTable = daType.GetData(True)
			Select Case dt.Rows.Count
				Case 0
					MsgBox("No BatchImportType default row found in database. Please notify database administrator.  Import Failed")
				Case 1
					retValue = dt.Rows(0)
				Case Else
					MsgBox("Multiple default BatchImportType rows found in database. Please notify database administrator. Import will proceed with first default row.")
					retValue = dt.Rows(0)
			End Select
		Catch ex As Exception
			MsgBox("Error loading BatchImportType default row. Error = " + ex.ToString)
		End Try
		Return retValue
	End Function
	Public Function GetBatchImportDetail(ByVal iBatchImportTypeID As Integer) As dsBatchImport.BatchImportDetailDataTable
		Dim retValue As dsBatchImport.BatchImportDetailDataTable = Nothing
		Try
			Dim daDetail As New dsBatchImportTableAdapters.BatchImportDetailTableAdapter
			daDetail.Connection = g_cn
			retValue = daDetail.GetData(iBatchImportTypeID)
			If retValue.Rows.Count = 0 Then
				MsgBox("No BatchImportDetails rows found in database. Please notify database administrator.  Import Failed")
				retValue = Nothing
			End If
		Catch ex As Exception
			MsgBox("Error loading BatchImportDetail table. Error = " + ex.ToString)
		End Try
		Return retValue
	End Function
End Class
