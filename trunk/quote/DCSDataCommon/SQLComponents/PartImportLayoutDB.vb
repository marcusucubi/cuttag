Imports System.Data
Imports DCS.DCSShared
Imports System.Data.SqlClient
Public Class PartImportLayoutDB
	Public Function GetLayout(ByVal ePartImportType As PartImportType) As DCS.dsPartImport
		Dim ds As New DCS.dsPartImport
		Dim bFail As Boolean = False
		Dim sErr As String = ""
		Dim sql As String = ""
		Dim iType As Integer = CType(ePartImportType, Integer)
		'June 2007
		Do		'single pass loop
			sql = "SELECT [LayoutID], [PartImportTypeID], [Description] FROM PartImportLayout" _
			 + " WHERE PartImportTypeID = " + iType.ToString
			If Not GetTable(ds.Layout, sql, sErr) Then Exit Do
			sql = "SELECT t.[TableID], t.[LayoutID], t.[TableName], t.[FixedName], t.[Identifier], " _
			+ "t.[ColumnHeadRelativeY], t.[TopRowBorderRelativeY], t.[TopRowBorderRelativeX], t.[RowHeight] FROM " _
			+ " PartImportLayout y INNER JOIN PartImportTableDef t ON y.LayoutID = t.LayoutID " _
			+ "WHERE t.IsActive = 1 AND y.PartImportTypeID = " + iType.ToString
			If Not GetTable(ds.TableDef, sql, sErr) Then Exit Do
			sql = "SELECT c.TableID, c.ColumnNumber, c.ColumnName, c.DBTableName, c.DBType, c.DBAttribute, c.WireEnd, " _
			+ "c.ColumnWidth, c.IsCentered, c.IsRequired, c.Verify FROM PartImportColumnDef c " _
			+ "INNER JOIN PartImportTableDef t ON t.TableID = c.TableID " _
			+ "INNER JOIN PartImportLayout y ON y.LayoutID = t.LayoutID " _
			+ "WHERE t.IsActive = 1 AND y.PartImportTypeID = " + iType.ToString
			If Not GetTable(ds.ColumnDef, sql, sErr) Then Exit Do
			sql = "SELECT a.TableID, a.AttributeNumber, a.AttributeName, a.CellBorderRelativeY, a.CellBorderRelativeX, " _
			+ "a.CellWidth, a.TextRelativeHandle, a.Applies2Instance, a.IsInstanceID FROM PartImportTableAttributes a " _
			+ "INNER JOIN PartImportTableDef t ON t.TableID = a.TableID " _
			 + "INNER JOIN PartImportLayout y ON y.LayoutID = t.LayoutID  " _
			+ "WHERE t.IsActive = 1 AND y.PartImportTypeID = " + iType.ToString
			If Not GetTable(ds.TableAttributes, sql, sErr) Then Exit Do
			sql = "SELECT a.GageInput, a.GageSubstitute FROM PartImportGageSubstitute a "
			If Not GetTable(ds.PartImportGageSubstitute, sql, sErr) Then Exit Do
			Exit Do
		Loop		'single pass loop
		'Try
		'	DCS.SQLSharedDB.FillDataTable(dsDxfLayout.DxfLayout, "SELECT * FROM DxfLayout")
		'Catch ex As Exception
		'	sErr = "Problem getting DxfLayout data." + ex.message
		'	bFail = True
		'End Try
		'Try
		'	DCS.SQLSharedDB.FillDataTable(dsDxfLayout.DxfTableDef, "SELECT * FROM DxfTableDef")
		'Catch ex As Exception
		'	sErr = "Problem getting DxfTableDef data.  " + ex.message
		'	bFail = True
		'End Try
		'Try
		'	DCS.SQLSharedDB.FillDataTable(dsDxfLayout.DxfColumnDef, "SELECT * FROM DxfColumnDef")
		'Catch ex As Exception
		'	sErr = "Problem getting DxfColumnDef data.  " + ex.message
		'	bFail = True
		'End Try
		'Try
		'	DCS.SQLSharedDB.FillDataTable(dsDxfLayout.DxfTableAttributes, "SELECT * FROM DxfTableAttributes")
		'Catch ex As Exception
		'	sErr = "Problem getting DxfTableAttributes data.  " + ex.message
		'	bFail = True
		'End Try
		'If bFail Then
		'	Throw New Exception(sErr)
		'End If
		If Len(sErr) > 0 Then
			Throw New Exception(sErr)
		End If
		Return ds
	End Function
	Private Function GetTable(ByVal tbl As System.Data.DataTable, ByVal sql As String, ByRef sErr As String) As Boolean
		Dim retValue As Boolean = True
		Try
			DCS.SQLSharedDB.FillDataTable(tbl, sql)
		Catch ex As Exception
			sErr = "Problem getting " + tbl.TableName + " data.  " + ex.Message
			retValue = False
		End Try
		Return retValue
	End Function
End Class
