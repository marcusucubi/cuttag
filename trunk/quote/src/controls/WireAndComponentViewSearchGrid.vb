Imports DCS.SharedMethods
'Imports DCS.Quote.QuoteDataBase
'Imports DCS.Quote.QuoteDataBaseTableAdapters
Public Class WireAndComponentViewSearchGrid
	Inherits DCS.SearchGrid
	Public Sub New()
		MyBase.New()
		'		Dim ds As New QuoteDataBase
		'		Dim daS As New QuoteDataBaseTableAdapters.WireSourceTableAdapter
		'		Dim daK As New QuoteDataBaseTableAdapters.WireSourceKeyWordTableAdapter
		Dim sUserValue As String = ""
		With Me
			'			daS.Fill(ds.WireSource)
			'			daK.Fill(ds.WireSourceKeyWord)
			'			.DataSource = ds
			'.DataMember = "WireSource"
			.BoundColumnName = "SourceID"
			.DisplayColumnName = "PartNumber"
			'.ChildRelationName = "WireSourceKeyWords"
			'.ChildLookupColumnName = "KeyWord"
			.SearchGridTableStylesAdd(SetUpWireSourceSearchGrid())
		End With
	End Sub
	Private Function SetUpWireSourceSearchGridColumn(ByRef ds As QuoteDataBase) As DataGridTableStyle
		Dim retValue As DataGridTableStyle = Nothing
		Dim csl As DCS.DataGridLookupColumn
		Dim sts As DataGridTableStyle
		'  WireSourceID SearchGrid Setup
		csl = New DCS.DataGridLookupColumn(ds.Tables("ItemSourceLookupList"), _
	 "SourceID", "PartNumber")
		csl.AllowSearch(ds, "ItemSourceLookupList") = True
		'   set up search grid
		sts = SetUpWireSourceSearchGrid()
		csl.SearchGridTableStylesAdd(sts)

		'sts = New DataGridTableStyle
		'sts.MappingName = "WireSourceKeyWord"
		'DGAddColumn(sts, 120, "KeyWord")
		'csl.SearchGridTableStylesAdd(sts)
		'csl.Width = 60
		'csl.MappingName = "WireSourceID"
		'csl.HeaderText = "WirePN"

		retValue.GridColumnStyles.Add(csl)
		Return retValue
		'  end WireSourceID SearchGrid Setup
	End Function
	Private Function SetUpWireSourceSearchGrid() As DataGridTableStyle
		Dim retValue As DataGridTableStyle = New DataGridTableStyle
		retValue.MappingName = "ItemSourceLookupList"
		DGAddColumn(retValue, 62, "KeyWord")
		DGAddColumn(retValue, 62, "PartNumber", "Part#")
		DGAddColumn(retValue, 160, "Description")
		DGAddColumn(retValue, 35, "IsWire", "Wire?", "Bool")
		'	DGAddColumn(retValue, 45, "Gage", , "JoinTextBox", True)		'Virtual Column
		'	DGAddColumn(retValue, 48, "Color", , "JoinTextBox", True)		'Virtual Column
		'	DGAddColumn(retValue, 50, "WireType", "Type", "JoinTextBox", True)		'Virtual Column
		Return retValue
	End Function
End Class
