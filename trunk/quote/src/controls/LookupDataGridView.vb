Imports DCS.DataGrid
Imports DCS.SharedMethods
Public Class LookupDataGridView
    Inherits System.Windows.Forms.DataGridView
    Private c_bIsNewRow As Boolean = False
    Public Event NewRowEnteredByUser(ByVal iRowIndex As Integer)
    'Protected Overrides Sub OnUserDeletingRow(ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs)
    '    MyBase.OnUserDeletingRow(e)
    'End Sub
    Protected Overrides Sub OnRowEnter(ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        If e.RowIndex = Me.NewRowIndex Then
            c_bIsNewRow = True
        Else
            c_bIsNewRow = False
        End If
        MyBase.OnRowEnter(e)
    End Sub
    Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        MyBase.OnKeyDown(e)
         If Me.c_bIsNewRow Then
            RaiseEvent NewRowEnteredByUser(Me.CurrentRow.Index)
            Me.c_bIsNewRow = False
        End If
    End Sub
    Protected Overrides Sub OnMouseClick(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseClick(e)
        If e.Button = MouseButtons.Left And Me.c_bIsNewRow Then
            RaiseEvent NewRowEnteredByUser(Me.CurrentRow.Index)
            Me.c_bIsNewRow = False
        End If
    End Sub
    '   Protected Overrides Sub OnNewRowNeeded(ByVal e As System.Windows.Forms.DataGridViewRowEventArgs)
    '       MyBase.OnNewRowNeeded(e)
    '   End Sub
    'Protected Overrides Sub OnUserAddedRow(ByVal e As System.Windows.Forms.DataGridViewRowEventArgs)
    '	MyBase.OnUserAddedRow(e)
    'End Sub
    'Protected Overrides Sub OnCellClick(ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
    '	MyBase.OnCellClick(e)
    'End Sub
    '   Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
    '       '		Dim c As DataGridViewCell = Nothing
    '       'Dim hit As DataGridView.HitTestInfo = Nothing
    '       'If e.Button = MouseButtons.Left Then
    '       'hit = Me.HitTest(e.X, e.Y)
    '       'If hit.Type = DataGridViewHitTestType.ColumnHeader Then MsgBox("hit header")
    '       '	'			If hit.Type = DataGridViewHitTestType.Cell Then
    '       '	'	c = Me.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
    '       '	'	Debug.WriteLine("MouseDown on (" + c.RowIndex.ToString + "," + c.ColumnIndex.ToString + ")")
    '       '	'	End If
    '       '	Debug.WriteLine("hit row: " + hit.RowIndex.ToString)
    '       'End If

    '       ''		If IsNothing(hit.RowIndex) OrElse Not Me.RowCount - 1 = hit.RowIndex Then
    '       ''	MyBase.OnMouseDown(e)
    '       ''	End If
    '       MyBase.OnMouseDown(e)
    '   End Sub
    'Public Overrides Function PreProcessMessage(ByRef msg As System.Windows.Forms.Message) As Boolean
    '	Return MyBase.PreProcessMessage(msg)
    'End Function
    'Public Overrides Function BeginEdit(ByVal selectAll As Boolean) As Boolean
    '	'only mouse click for some reason
    '	'Debug.WriteLine("lookup dgv begin edit")
    '	Return MyBase.BeginEdit(selectAll)
    'End Function
    'Protected Overrides Sub OnCellBeginEdit(ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs)
    '	'Debug.WriteLine("lookup dgv begin edit")
    '	MyBase.OnCellBeginEdit(e)
    'End Sub
    'Protected Overrides Sub OnEditingControlShowing(ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs)
    '	'Debug.WriteLine("lookup dgv EditingControlShowing")
    '	MyBase.OnEditingControlShowing(e)

    'End Sub
    Public Sub New()
        ' This call is required by the designer.
		InitializeComponent()
		' Add any initialization after the InitializeComponent() call.
    End Sub
End Class

<DataGridViewColumnDesignTimeVisible(True)>
Public Class DataGridViewSearchColumn
	Inherits DataGridViewTextBoxColumn
	Protected c_SearchGrid As DCS.SearchGrid
	Public Property SearchGrid As DCS.SearchGrid
		Get
			Return c_SearchGrid
		End Get
		Set(ByVal value As DCS.SearchGrid)
			c_SearchGrid = value
		End Set
	End Property

	Public Sub New()
		Me.CellTemplate = New DataGridSearchCell()
		'	SetUpSearchGrid()

	End Sub
	Private Function SetUpWireSourceSearchGridColumn(ByRef ds As QuoteDataBase) As DataGridTableStyle
		Dim retValue As DataGridTableStyle = Nothing
		Dim csl As DCS.DataGridLookupColumn
		Dim sts As DataGridTableStyle
		'  WireSourceID SearchGrid Setup
		csl = New DCS.DataGridLookupColumn(ds.Tables("WireSource"), _
		"WireSourceID", "PartNumber")
		csl.AllowSearch(ds, "WireSource", "WireSourceKeyWords", "KeyWord") = True
		'   set up search grid
		sts = SetUpWireSourceSearchGrid()
		csl.SearchGridTableStylesAdd(sts)
		sts = New DataGridTableStyle
		sts.MappingName = "WireSourceKeyWord"
		DGAddColumn(sts, 120, "KeyWord")
		csl.SearchGridTableStylesAdd(sts)
		csl.Width = 60
		csl.MappingName = "WireSourceID"
		csl.HeaderText = "WirePN"
		retValue.GridColumnStyles.Add(csl)
		Return retValue
		'  end WireSourceID SearchGrid Setup
	End Function
	Private Function SetUpWireSourceSearchGrid() As DataGridTableStyle
		Dim retValue As DataGridTableStyle = New DataGridTableStyle
		retValue.MappingName = "WireSource"
		DGAddColumn(retValue, 62, "PartNumber")
		DGAddColumn(retValue, 160, "Description")
		DGAddColumn(retValue, 40, "ConductorCount", "#Cond")
		DGAddColumn(retValue, 35, "IsTwisted", "Twst", "Bool")
		DGAddColumn(retValue, 45, "Gage", , "JoinTextBox", True)		'Virtual Column
		DGAddColumn(retValue, 48, "Color", , "JoinTextBox", True)		'Virtual Column
		DGAddColumn(retValue, 50, "WireType", "Type", "JoinTextBox", True)		'Virtual Column
		Return retValue
	End Function
	Private Sub SetUpSearchGrid()
		Dim ds As New QuoteDataBase
		'		Dim daS As New QuoteDataBaseTableAdapters.WireSourceTableAdapter
		'		Dim daK As New QuoteDataBaseTableAdapters.WireSourceKeyWordTableAdapter
		Dim sUserValue As String = ""
		c_SearchGrid = New DCS.SearchGrid
		With c_SearchGrid
			'		daS.Fill(ds.WireSource)
			'		daK.Fill(ds.WireSourceKeyWord)
			.DataSource = ds
			.DataMember = "WireSource"
			.BoundColumnName = "WireSourceID"
			.DisplayColumnName = "PartNumber"
			.ChildRelationName = "WireSourceKeyWords"
			.ChildLookupColumnName = "KeyWord"
		End With
		c_SearchGrid.SearchGridTableStylesAdd(SetUpWireSourceSearchGrid())

	End Sub

End Class

Public Class DataGridSearchCell
	Inherits DataGridViewTextBoxCell
	Public Overrides Sub InitializeEditingControl(ByVal rowIndex As Integer, ByVal initialFormattedValue As Object, ByVal dataGridViewCellStyle As System.Windows.Forms.DataGridViewCellStyle)
		Debug.WriteLine("InitializeEditingControl")
		MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle)

	End Sub

	Protected Overrides Sub OnEnter(ByVal rowIndex As Integer, ByVal throughMouseClick As Boolean)
		MyBase.OnEnter(rowIndex, throughMouseClick)
		Debug.WriteLine("DataGridSearchCell OnEnter Override")

	End Sub

	Public Sub New()
		MyBase.New()

	End Sub
	Public Overrides ReadOnly Property EditType() As Type
		Get
			' Return the type of the editing contol that MaskedEditEditingControl uses.
			Return GetType(DataGridSearchTextBox)
		End Get
	End Property


End Class
Public Class DataGridSearchTextBox
	Inherits TextBox
	Implements IDataGridViewEditingControl
	Private c_dataGridViewControl As DataGridView
	Private c_valueIsChanged As Boolean = False
	Private c_rowIndexNum As Integer
	Private c_SaveValue As String
	Private c_SearchGrid As DCS.SearchGrid

	Public Sub New()
		MyBase.New()
		Debug.WriteLine("SeachText New")
	End Sub
	Protected Overrides Sub OnEnter(ByVal e As System.EventArgs)

		Me.c_SaveValue = Me.Text
		Dim sUserValue As String = ""
		Debug.WriteLine("SeachText On Enter")

		If Not IsNothing(c_SearchGrid) Then
			sUserValue = c_SearchGrid.ShowModal(Me)
			If Len(sUserValue) > 0 AndAlso Not Me.Text = sUserValue Then
				Me.Text = sUserValue
				c_valueIsChanged = True
				Me.EditingControlDataGridView.NotifyCurrentCellDirty(True)
			Else
				Me.Text = c_SaveValue
			End If
		End If
		Me.c_dataGridViewControl.EndEdit()
		'MyBase.OnEnter(e)
	End Sub

	Private Function SetUpWireSourceSearchGridColumn(ByRef ds As QuoteDataBase) As DataGridTableStyle
		Dim retValue As DataGridTableStyle = Nothing
		Dim csl As DCS.DataGridLookupColumn
		Dim sts As DataGridTableStyle
		'  WireSourceID SearchGrid Setup
		csl = New DCS.DataGridLookupColumn(ds.Tables("WireSource"), _
		"WireSourceID", "PartNumber")
		csl.AllowSearch(ds, "WireSource", "WireSourceKeyWords", "KeyWord") = True
		'   set up search grid
		sts = SetUpWireSourceSearchGrid()
		csl.SearchGridTableStylesAdd(sts)
		sts = New DataGridTableStyle
		sts.MappingName = "WireSourceKeyWord"
		DGAddColumn(sts, 120, "KeyWord")
		csl.SearchGridTableStylesAdd(sts)
		csl.Width = 60
		csl.MappingName = "WireSourceID"
		csl.HeaderText = "WirePN"
		retValue.GridColumnStyles.Add(csl)
		Return retValue
		'  end WireSourceID SearchGrid Setup
	End Function
	Private Function SetUpWireSourceSearchGrid() As DataGridTableStyle
		Dim retValue As DataGridTableStyle = New DataGridTableStyle
		retValue.MappingName = "WireSource"
		DGAddColumn(retValue, 62, "PartNumber")
		DGAddColumn(retValue, 160, "Description")
		DGAddColumn(retValue, 40, "ConductorCount", "#Cond")
		DGAddColumn(retValue, 35, "IsTwisted", "Twst", "Bool")
		DGAddColumn(retValue, 45, "Gage", , "JoinTextBox", True)		'Virtual Column
		DGAddColumn(retValue, 48, "Color", , "JoinTextBox", True)		'Virtual Column
		DGAddColumn(retValue, 50, "WireType", "Type", "JoinTextBox", True)		'Virtual Column
		Return retValue
	End Function

	Public Property EditingControlFormattedValue() As Object Implements _
 IDataGridViewEditingControl.EditingControlFormattedValue
		Get
			Return Me.c_valueIsChanged.ToString
		End Get
		Set(ByVal value As Object)
			If TypeOf value Is [String] Then
				Me.Text = value.ToString
			End If
		End Set
	End Property
	Public Function GetEditingControlFormattedValue(ByVal context As  _
	DataGridViewDataErrorContexts) As Object Implements _
	IDataGridViewEditingControl.GetEditingControlFormattedValue
		Return Me.Text
	End Function
	Public Sub ApplyCellStyleToEditingControl(ByVal dataGridViewCellStyle As  _
 DataGridViewCellStyle) Implements _
 IDataGridViewEditingControl.ApplyCellStyleToEditingControl
		Me.Font = dataGridViewCellStyle.Font
		Me.ForeColor = dataGridViewCellStyle.ForeColor
		Me.BackColor = dataGridViewCellStyle.BackColor
	End Sub
	Public Property EditingControlRowIndex() As Integer Implements _
	IDataGridViewEditingControl.EditingControlRowIndex
		Get
			Return c_rowIndexNum
		End Get
		Set(ByVal value As Integer)
			c_rowIndexNum = value
		End Set
	End Property
	Public Function EditingControlWantsInputKey(ByVal key As Keys, ByVal _
 dataGridViewWantsInputKey As Boolean) As Boolean Implements _
 IDataGridViewEditingControl.EditingControlWantsInputKey
		Return True
	End Function
	Public Sub PrepareEditingControlForEdit(ByVal selectAll As Boolean) Implements _
 IDataGridViewEditingControl.PrepareEditingControlForEdit
	End Sub
	Public ReadOnly Property RepositionEditingControlOnValueChange() As Boolean _
	Implements IDataGridViewEditingControl.RepositionEditingControlOnValueChange
		Get
			Return False
		End Get
	End Property
	Public Property EditingControlDataGridView() As DataGridView Implements _
 IDataGridViewEditingControl.EditingControlDataGridView
		Get
			Return c_dataGridViewControl
		End Get
		Set(ByVal value As DataGridView)
			c_dataGridViewControl = value
			c_SearchGrid = CType(c_dataGridViewControl.Columns(c_dataGridViewControl.CurrentCell.ColumnIndex), DataGridViewSearchColumn).SearchGrid
		End Set
	End Property
	Public Property EditingControlValueChanged() As Boolean Implements _
 IDataGridViewEditingControl.EditingControlValueChanged
		Get
			Return c_valueIsChanged
		End Get
		Set(ByVal value As Boolean)
			c_valueIsChanged = value
		End Set
	End Property
	Public ReadOnly Property EditingPanelCursor() As Cursor Implements _
 IDataGridViewEditingControl.EditingPanelCursor
		Get
			Return MyBase.Cursor
		End Get
	End Property
	'Protected Overrides Sub OnTextChanged(ByVal eventargs As EventArgs)
	'	' Notify the DataGridView that the contents of the cell have changed.
	'	valueIsChanged = True
	'	Me.EditingControlDataGridView.NotifyCurrentCellDirty(True)
	'	MyBase.OnTextChanged(eventargs)
	'End Sub
End Class
