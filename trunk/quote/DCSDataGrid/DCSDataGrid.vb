Option Explicit On 
Imports System.Reflection
Imports System.ComponentModel
Imports VB = Microsoft.VisualBasic
Imports System.Data
#Const DataBaseType = "SQL"
Public Class DataGrid
  Inherits System.Windows.Forms.DataGrid
  Private m_UnitOfMeasure As String
  Private m_bReloadNeeded As Boolean = False
  Public Event CalculateVirtualColumns(ByVal dr As DataRow, ByVal sColumn As String, ByRef Value As Object)
  Public Event RowDeleting(ByRef Cancel As Boolean, ByVal cm As CurrencyManager, ByVal alIndex As System.Collections.ArrayList)
  Public Event F2_Pressed(ByRef bHandled As Boolean)    '3/15/08
  Public Event UpArrowOnFirstRow(ByVal dg As DCS.DataGrid, ByRef bHandled As Boolean) '4/5/08
  Public Event DownArrowOnLastRow(ByVal dg As DCS.DataGrid, ByRef bHandled As Boolean) '4/5/08
  Public Event LeftArrowOnFirstRow(ByVal dg As DCS.DataGrid, ByRef bHandled As Boolean) '4/5/08
  Public Event RightArrowOnLastRow(ByVal dg As DCS.DataGrid, ByRef bHandled As Boolean) '4/5/08
  Public Event Ctrl_Pressed(ByRef bHandled As Boolean, ByVal kKeyPressed As Keys)    '5/1/08
#Region " Windows Form Designer generated code "

  Public Sub New()
    MyBase.New()

    'This call is required by the Windows Form Designer.
    InitializeComponent()

    'Add any initialization after the InitializeComponent() call

  End Sub

  'UserControl1 overrides dispose to clean up the component list.
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing Then
      If Not (components Is Nothing) Then
        components.Dispose()
      End If
    End If
    MyBase.Dispose(disposing)
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    components = New System.ComponentModel.Container
  End Sub

#End Region
  Public Property UnitOfMeasure() As String
    Get
      UnitOfMeasure = m_UnitOfMeasure
    End Get
    Set(ByVal Value As String)
      m_UnitOfMeasure = Value
    End Set
  End Property
  Public Property ReloadNeeded() As Boolean
    Get
      Return m_bReloadNeeded
    End Get
    Set(ByVal Value As Boolean)
      m_bReloadNeeded = Value
    End Set
  End Property
  Public ReadOnly Property CurrentRowVisible() As Boolean
    Get
      Dim retValue As Boolean = True
      Dim iCellCenter As Single = (GetCurrentCellBounds().Bottom + GetCurrentCellBounds().Top) / 2
      Dim iHalfFontHeight As Single = Font.Height / 2
      Dim iTextTopRow, iTextBottomRow As Integer
      iTextTopRow = HitTest(GetCurrentCellBounds().Y, iCellCenter - iHalfFontHeight).Row
      iTextBottomRow = HitTest(GetCurrentCellBounds().Y, iCellCenter + iHalfFontHeight).Row
      If iTextTopRow < 0 Or iTextBottomRow < 0 Then
        retValue = False
      End If
      Return retValue
    End Get
  End Property
  Public Function RaiseCalculateEvent(ByVal dr As DataRow, ByVal sColumn As String) As Object
		Dim Value As Object = Nothing
    Try
      RaiseEvent CalculateVirtualColumns(dr, sColumn, Value)
    Catch ex As Exception
      MessageBox.Show("Grid had a problem raising CalculateVirtualColumns event: " + ex.Message.ToString)
      Value = Nothing
    End Try
    Return Value
  End Function
  Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
    Debug.WriteLine("dgPrcsCmdKey: " + keyData.ToString + " CurrentCell: " + Me.CurrentCell.ToString)
    Dim cm As CurrencyManager = Me.BindingContext(Me.DataSource, Me.DataMember)
    If cm.Count > 0 Then
      Dim bHandled As Boolean
      Dim drv As DataRowView = cm.Current
      If (keyData And Keys.Modifiers) = Keys.Control Then
        If Not (keyData And Keys.KeyCode) = Keys.ControlKey Then
          RaiseEvent Ctrl_Pressed(bHandled, keyData And Keys.KeyCode)
          If bHandled Then
            Return True
          End If
        End If
      End If
      Select Case keyData
        Case Keys.Up
          If cm.Position = 0 Then RaiseEvent UpArrowOnFirstRow(Me, bHandled)
          If bHandled Then
            Return True
          End If
        Case Keys.Down
          If drv.IsNew Or (Me.ReadOnly And cm.Count - 1 = cm.Position) Then RaiseEvent DownArrowOnLastRow(Me, bHandled)
          If bHandled Then
            Return True
          End If
        Case Keys.Left
          If cm.Position = 0 And Me.CurrentCell.ColumnNumber = 0 Then RaiseEvent LeftArrowOnFirstRow(Me, bHandled)
          If bHandled Then
            Return True
          End If
        Case Keys.Right
          If (drv.IsNew Or (Me.ReadOnly And cm.Count - 1 = cm.Position)) And (Me.CurrentCell.ColumnNumber = Me.VisibleColumnCount - 1) Then RaiseEvent RightArrowOnLastRow(Me, bHandled)
          If bHandled Then
            Return True
          End If

        Case (Keys.Alt Or Keys.Down)
          If ForceSort() Then
            Return True
          End If
          If Not drv.IsNew Then
            Me.Expand(Me.CurrentCell.RowNumber)
                        SendKeys.Send("{End}{Tab}")
          End If
          Return True
        Case (Keys.Alt Or Keys.Up)
          If Not drv.IsNew Then
            Me.Collapse(Me.CurrentCell.RowNumber)
          End If
          Return True
        Case Keys.Tab
          If Me.CurrentCell.ColumnNumber = _
           Me.TableStyles(drv.Row.Table.TableName).GridColumnStyles.Count - 1 Then
            If ForceSort() Then
              Return True
            End If
          End If
        Case (Keys.Shift Or Keys.Tab)
          If Me.CurrentCell.ColumnNumber = 0 Then
            If ForceSort() Then
              Return True
            End If
          End If
        Case Keys.Delete
          If Me.ReadOnly Then Return True
          '''''''''''''
          Dim i As Integer
          Dim al As New System.Collections.ArrayList
          For i = 0 To cm.Count - 1
            If Me.IsSelected(i) Then
              al.Add(cm.List(i))
            End If
          Next
          Dim iCount As Integer = al.Count
          Dim s As String
          If iCount > 0 Then
            If iCount = 1 Then
              s = "this row"
            Else
              s = iCount.ToString + " rows"
            End If
            If MessageBox.Show("Delete " + s + "?", "Confirm Delete", MessageBoxButtons.YesNo) = DialogResult.No Then
              Return True
            Else
              Dim bCancel As Boolean = False
              RaiseEvent RowDeleting(bCancel, cm, al)
              If bCancel Then
                Return True
              End If
            End If
          End If
        Case Keys.F2     '3/15/08 F2 seem to be intercepted before grid textbox gets it
          RaiseEvent F2_Pressed(bHandled)
          If bHandled Then
            Return True
          End If
        Case Else
      End Select
    End If
    Return MyBase.ProcessCmdKey(msg, keyData)
  End Function
  'Public Overrides Function PreProcessMessage(ByRef msg As System.Windows.Forms.Message) As Boolean
  '	Dim keyCode As Keys = CType((msg.WParam.ToInt32 And Keys.KeyCode), Keys)
  '	Dim WM_KEYDOWN As Integer = &H100
  '	If msg.Msg = WM_KEYDOWN And keyCode = Keys.Delete Then
  '		If Me.ReadOnly Then Return True
  '		Dim i As Integer
  '		Dim cm As CurrencyManager = Me.BindingContext(Me.DataSource, Me.DataMember)
  '		Dim al As New System.Collections.ArrayList
  '		For i = 0 To cm.Count - 1
  '			If Me.IsSelected(i) Then
  '				al.Add(cm.List(i))
  '			End If
  '		Next
  '		Dim iCount As Integer = al.Count
  '		Dim s As String
  '		If iCount > 0 Then
  '			If iCount = 1 Then
  '				s = "this row"
  '			Else
  '				s = iCount.ToString + " rows"
  '			End If
  '			If MessageBox.Show("Delete " + s + "?", "Confirm Delete", MessageBoxButtons.YesNo) = DialogResult.No Then
  '				Return True
  '			Else
  '				Dim bCancel As Boolean = False
  '				RaiseEvent RowDeleting(bCancel, cm, al)
  '				If bCancel Then
  '					Return True
  '				End If
  '			End If
  '		End If
  '	End If
  '	Return MyBase.PreProcessMessage(msg)
  'End Function
	Public Sub DCSEndEdit()
		Dim cm As CurrencyManager = Me.BindingContext(Me.DataSource, Me.DataMember)
		Try
			'dddddd Caution: Me.CurrentCell.ColumnNumber may not be accurate
			Dim dgc As DataGridColumnStyle = _
			 Me.TableStyles(CType(cm.List, DataView).Table.TableName). _
			 GridColumnStyles(Me.CurrentCell.ColumnNumber)
			Me.EndEdit(dgc, Me.CurrentCell.RowNumber, False)
			cm.EndCurrentEdit()
		Catch ex As Exception
			MessageBox.Show("Datagrid had a problem ending the edit operation" _
			 + ControlChars.CrLf + ControlChars.CrLf + ex.Message)
		End Try
	End Sub
	Public Sub DCSUnselectAll()
		Dim i As Integer
		Dim cm As CurrencyManager = Me.BindingContext(Me.DataSource, Me.DataMember)
		For i = 0 To cm.Count - 1
			If Me.IsSelected(i) Then
				Me.UnSelect(i)
			End If
		Next
	End Sub
  Private Function ForceSort() As Boolean
    '*********************************************************************
    'Causes datagrid to sort rows before attempting to navigate to child 
    '  from newly created row. 
    '*********************************************************************
    Debug.WriteLine("ForceSort")
    Dim cm As CurrencyManager = Me.BindingContext(Me.DataSource, Me.DataMember)
    Dim iSavePosition As Integer = cm.Position
		'   Dim i As Integer
    Try
      DCSEndEdit()
      '*********************************************************************
      'Datagrid looses relation where navagating to child from newly created
      ' row. Position change seems to solve problem
      '*********************************************************************
      If cm.Position + 1 = CType(cm.List, DataView).Count Then
        Debug.WriteLine("in force sort , on last row")
        cm.Position -= 1
        cm.Position += 1
      End If
      If cm.Position <> iSavePosition Then
        Return True
      Else
        Return False
      End If
    Catch ex As Exception
      MessageBox.Show(ex.Message)
      Return True
    End Try
  End Function
  Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
    '**************************************************
    'created to avoid navigation to child of
    ' a row that is not currently selected
    '
    '**************************************************
    If Me.DataSource Is Nothing Then Exit Sub
    Dim hti As HitTestInfo = Me.HitTest(e.X, e.Y)
    Dim cm As CurrencyManager = Me.BindingContext(Me.DataSource, Me.DataMember)
    Dim bCancel As Boolean = False
    If hti.Type = Windows.Forms.DataGrid.HitTestType.RowHeader Then
      Debug.WriteLine("Hittest RowHeader")
      If ForceSort() Then
        bCancel = True
      ElseIf 1 Then
        Try
          If cm.Position <> hti.Row Then
            cm.Position = hti.Row
          End If
        Catch ex As Exception
          MessageBox.Show(ex.Message)
          bCancel = True
        End Try
      End If

      'Dim iSavePosition As Integer = cm.Position
      'Dim drv As DataRowView = cm.Current
      'If drv.IsEdit Then
      '    '     SendKeys.Send("{Down}")
      'End If
    End If
    'If hti.Type = Windows.Forms.DataGrid.HitTestType.Cell Or _
    '    hti.Type = Windows.Forms.DataGrid.HitTestType.RowHeader Then
    '    Try
    '        '               CType(cm.List, DataView).Sort = CType(cm.List, DataView).Sort
    '        If cm.Position <> hti.Row Then
    '            cm.Position = hti.Row
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        bCancel = True
    '    End Try
    '    'If Not cm.Current.isnew Then
    '    '    If h.Row <> cm.Position Then cm.Position = h.Row
    '    'End If
    '    'If h.Row <> cm.Position Then
    '    '    If Me.IsExpanded(h.Row) Then
    '    '        Me.Collapse(h.Row)
    '    '        cm.Position = h.Row
    '    '        Exit Sub
    '    '    End If
    '    'End If
    'End If
		If Not bCancel Then MyBase.OnMouseDown(e)
  End Sub
  Public Sub ScrollToRow(ByVal row As Integer)
    Me.GridVScrolled(Me, New ScrollEventArgs(ScrollEventType.LargeIncrement, row))
  End Sub
  'Private Sub DataGrid_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.CurrentCellChanged
  '    Dim cm As CurrencyManager = Me.BindingContext(Me.DataSource, Me.DataMember)
  '    If cm.Count > 0 Then
  '        Dim drv As DataRowView = cm.Current
  '        Dim dgc As DataGridColumnStyle = _
  '            Me.TableStyles(CType(cm.List, DataView).Table.TableName).GridColumnStyles( _
  '            Me.CurrentCell.ColumnNumber)
  '        Debug.WriteLine("DGCurCellChngd: " + Me.CurrentCell.ToString + "--IsEdit: " _
  '         + drv.IsEdit.ToString + " RowState" + drv.Row.RowState.ToString _
  '         + " Col: " + dgc.MappingName)
  '    End If
  'End Sub
End Class
Public Class DataGridLookupColumn
  Inherits System.Windows.Forms.DataGridTextBoxColumn
  '
  ' Use to display a column from a related table without defining a relation
  ' Datatable is the lookup table
	' ForeignKey is the foreign key column in the current table
	' LookupPrimaryKey = BoundColumnName (prikey of lookup table) = column returned to foreignkey column in current table
	'    in lookup table (e.g. WireComponentSourceID)
	' LookupColumn = lookup table column name of column to display in current table grid  (e.g. PartNumber)
	' The PrimaryKey of the lookup table must be a single column matching ForeignKey
	'  if LookupPrimaryKey missing in New
  ' New instance of SearchGrid instantiated in AllowSearch property
  Public WithEvents SearchGrid As DCS.SearchGrid
  Private m_DataTable As DCS.DCSDataTable
  Private m_ForeignKey As String 'current table col pointing to prikey of lookup table 
	Private m_LookupPrimaryKey As String 'lookup table primary key
	Private m_LookupColumn As String 'lookup table col to be displayed
  Private m_bAllowSearch As Boolean 'enables search grid to make this col editable
  Private m_DataSource As DataSet
  Private m_dg As DCS.DataGrid
  Private m_DataMember As String
  Private m_source As CurrencyManager
  Public Property AllowSearch(ByVal DataSource As DataSet, _
   ByVal Datamember As String, _
   Optional ByVal ChildRelation As String = Nothing, _
   Optional ByVal ChildLookupColumn As String = Nothing) As Boolean
    Get
      AllowSearch = m_bAllowSearch
    End Get
    Set(ByVal Value As Boolean)
      If Value Then
        MyBase.ReadOnly = False
        m_DataSource = DataSource
        m_DataMember = Datamember
        SearchGrid = New DCS.SearchGrid
        With SearchGrid
          .DataSource = DataSource
          .DataMember = Datamember
					.BoundColumnName = m_LookupPrimaryKey
          .DisplayColumnName = m_LookupColumn
          If (Not ChildRelation Is Nothing) And (Not ChildLookupColumn Is Nothing) Then
            .ChildRelationName = ChildRelation
            .ChildLookupColumnName = ChildLookupColumn
          End If
        End With
      End If
      m_bAllowSearch = Value
    End Set
  End Property
  Public Property IsDirtySearchLookupList() As Boolean
    Get
      IsDirtySearchLookupList = SearchGrid.IsDirtyLookupList
    End Get
    Set(ByVal Value As Boolean)
      SearchGrid.IsDirtyLookupList = Value
    End Set
  End Property
  '  <Description("DCS: Gets SearchGrid DataSource. Use AllowSearch property to set DataSource")> _
  Public ReadOnly Property SearchGridTableStyle() As DataGridTableStyle
    Get
      SearchGridTableStyle = SearchGrid.SearchGridTableStyle
    End Get
  End Property
  Public Sub SearchGridTableStylesAdd(ByVal ts As DataGridTableStyle)
    SearchGrid.SearchGridTableStylesAdd(ts)
  End Sub
  Public ReadOnly Property DataSource() As DataSet
    Get
      DataSource = m_DataSource
    End Get
  End Property
  Public ReadOnly Property DataMember() As String
    Get
      DataMember = m_DataMember
    End Get
  End Property
	Public Sub New(ByVal DataTable As DCS.DCSDataTable, ByVal ForeignKey As String, _
	 ByVal LookupColumn As String, Optional ByVal LookupPrimaryKey As String = "")
		m_DataTable = DataTable
		m_ForeignKey = ForeignKey
		m_LookupColumn = LookupColumn
		If LookupPrimaryKey = "" Then
			m_LookupPrimaryKey = ForeignKey
		Else
			m_LookupPrimaryKey = LookupPrimaryKey
		End If
		AddHandler Me.TextBox.GotFocus, AddressOf TextBox_GotFocus
		AddHandler Me.TextBox.LostFocus, AddressOf TextBox_LostFocus
		AddHandler Me.TextBox.KeyPress, AddressOf TextBoxKeyPress
		AddHandler Me.TextBox.MouseDown, AddressOf TextBoxMouseDown
	End Sub
  Protected Overrides Sub SetDataGridInColumn(ByVal value As System.Windows.Forms.DataGrid)
    '3/15/08
    m_dg = value
    MyBase.SetDataGridInColumn(value)
  End Sub
  Protected Overrides Function GetColumnValueAtRow(ByVal cm As CurrencyManager, ByVal RowNum As Integer) As Object
    '
    ' Get the current DataRow from the CurrencyManager.
    ' Use the GetParentRow and the DataRelation name to get the parent row.
    ' Return the field value from the parent row.
    '
    Try
      '            Return m_DataTable.Rows.Find(cm.Current.Datview.Rows(RowNum)("PartID"))("PartNumber")
      'Primary key of m_DataTable must be set to m_ForeignKey
      'Finds row with primarykey val=m_ForeignKey and returns value in m_LookupCol col

      '12/31/02 remmed all and added last line which was in previous version
      'If m_DataTable.Rows.Count < 1 Then
      '    MsgBox("Problem finding the data for the column " + Me.HeaderText _
      '     + ControlChars.CrLf + "The data files may be missing or damaged.")
      '    Return ""
      'End If
      'Dim dr As DataRow = m_DataTable.Rows.Find(CType(cm.List, DataView).Item(RowNum).Item(m_ForeignKey))
      'If dr Is Nothing Then
      '    If Not cm.Current.IsNew Then
      '        MsgBox("Problem finding the data for the column " + Me.HeaderText _
      '          + ControlChars.CrLf + "Data files may be missing or damaged.")
      '    End If
      '    Return ""
      'Else
      '    Return dr.Item(m_LookupColumn)
      'End If
      'end 12/31/02

      'ddddddddd  should check for value = nothing and return "" to avoid exection
      'next 6 line can be unremmed for testing 
      'Dim dv As DataView = CType(cm.List, DataView)
      'Dim drv As DataRowView = dv.Item(RowNum)
      'Dim value As Object = drv.Item(m_ForeignKey)
      'Dim dr As DataRow = m_DataTable.Rows.Find(value)
      'Dim v2 As Object = dr.Item(m_LookupColumn)
      'Return v2
      Return m_DataTable.Rows.Find(CType(cm.List, DataView).Item(RowNum).Item(m_ForeignKey)).Item(m_LookupColumn)

      '   Dim dr As DataRow = CType(cm.List, DataView).Item(RowNum).Row
      '  Dim drParent As DataRow = dr.GetParentRow(m_RelationName)
      ' Return drParent(m_ParentField).ToString()
    Catch ex As Exception
      'remmed 12/31/02       MsgBox("Get value at row error:  " + ex.Message)
      Return ""   ' handles NullReferenceException case when adding record
    End Try
  End Function
  Private Sub TextBox_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    AddHandler Me.m_dg.F2_Pressed, AddressOf m_dg_F2_Pressed '3/19/08
  End Sub
  Private Sub TextBox_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    RemoveHandler Me.m_dg.F2_Pressed, AddressOf m_dg_F2_Pressed '3/19/08
  End Sub
  Protected Overloads Overrides Sub Edit(ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, ByVal bounds As System.Drawing.Rectangle, ByVal [readOnly] As Boolean, ByVal instantText As String, ByVal cellIsVisible As Boolean)
    Try
      MyBase.Edit(source, rowNum, bounds, [readOnly], instantText, cellIsVisible)
      'this proc will happen each time column get focus
      '      AddHandler Me.m_dg.F2_Pressed, AddressOf m_dg_F2_Pressed
      Me.ReadOnly = [readOnly]
      m_source = source   'remember cm for update after searchgrid lookup
    Catch ex As Exception
      MsgBox("Lookup Column Edit Error" + ex.Message.ToString)
    End Try
  End Sub
  Private Sub m_dg_F2_Pressed(ByRef bHandled As Boolean)
    '3/15/08
    Debug.WriteLine("F2 in Lookup Column")
    GetUserInput()
    bHandled = True
  End Sub
  Private Sub TextBoxMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    GetUserInput()  'initiate searchgrid lookup
  End Sub
  Private Sub TextBoxKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    GetUserInput(e)  'initial searchgrid lookuup with user keypress key
    e.Handled = True
  End Sub
  Private Sub GetUserInput(Optional ByVal e As System.Windows.Forms.KeyPressEventArgs = Nothing)
    If m_bAllowSearch And Not Me.ReadOnly Then
      Dim oResult As Object = SearchGrid.ShowModal(Me.TextBox, e)
      If Not oResult Is Nothing Then
        'Me.TextBox.Text = oResult
        'Dim cm As CurrencyManager = Me.DataGridTableStyle.DataGrid. _
        '  BindingContext(Me.DataGridTableStyle.DataGrid.DataSource, _
        '  Me.DataGridTableStyle.DataGrid.DataMember)
        '        Me.SetColumnValueAtRow(m_source, m_rownum, SearchGrid.GetCurrentBoundValue())
        'If cm.Current.isnew Then
        Me.ColumnStartedEditing(Me.TextBox)    'tell grid to make pencil and init editing
				Try
					' m_ForeignKey=Column name in target grid's table 
					' SearchGrid.GetCurrentBoundValue() uses LookupPrimaryKey for value to return to m_ForeignKey 
					'     if LookupPrimaryKey missing in Me.New, defaults to m_ForeignKey
					m_source.Current(m_ForeignKey) = SearchGrid.GetCurrentBoundValue()
					' m_bIsEditing = True
					'Me.Commit(m_source, m_rownum)
					' m_bIsEditing = False
					' cm.EndCurrentEdit()
					Me.TextBox.Text = oResult
					' End If
					'  m_bIsEditing = True
				Catch ex As Exception
					MsgBox("Selection Rejected: Please try again.")
				End Try

      End If
    End If
  End Sub
  Protected Overrides Sub SetColumnValueAtRow(ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, ByVal value As Object)
    MyBase.SetColumnValueAtRow(source, rowNum, value)
  End Sub
  Protected Overrides Function Commit(ByVal dataSource As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer) As Boolean
    'dddd no need for this overided now that commit is handled above
    'udate 3/15/08 - need override to handle f2 event
    'If m_bIsEditing Then
    '    dataSource.Current(m_ForeignKey) = SearchGrid.GetCurrentBoundValue()
    '    m_bIsEditing = False
    '    Return True
    'Else
    '    Return MyBase.Commit(dataSource, rowNum) 'needed for cleanup
    'End If
    'Return True
    '    RemoveHandler Me.m_dg.F2_Pressed, AddressOf m_dg_F2_Pressed
    Return MyBase.Commit(dataSource, rowNum)  'needed for cleanup
  End Function
End Class
Public Class DataGridhcObjectTypeColumn
  Inherits System.Windows.Forms.DataGridTextBoxColumn
  Public Overrides Property [ReadOnly]() As Boolean
    Get
      Return MyBase.ReadOnly
    End Get
    Set(ByVal Value As Boolean)
      MyBase.ReadOnly = True
    End Set
  End Property
  Public Sub New()
    MyBase.new()
    Me.ReadOnly = True
  End Sub
  Protected Overrides Function GetColumnValueAtRow(ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer) As Object
    Dim oValue As Object = MyBase.GetColumnValueAtRow(source, rowNum)
    Dim retValue As String = ""
    Dim hc As DCSShared.hcObjectType
		If [Enum].IsDefined(hc.GetType, oValue) Then
			retValue = CType(oValue, DCSShared.hcObjectType).ToString
		End If
    Return retValue
  End Function
End Class
Public Class DataGridOneCharColumn
  Inherits System.Windows.Forms.DataGridTextBoxColumn
  Private m_ar() As Char
  Sub New()
    MyBase.New()
    'Set default characters allowed to L,R
    m_ar = New Char() {"L", "R"}
    AddHandler Me.TextBox.KeyPress, AddressOf TextBoxKeyPress
  End Sub
  Public Property CharactersAllowed() As Char()
    Get
      CharactersAllowed = m_ar
    End Get
    Set(ByVal Value As Char())
      m_ar = Value
    End Set
  End Property
  Private Sub TextBoxKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    If Not Me.ReadOnly Then
      Dim c As Char
      Dim bFound As Boolean = False
      For Each c In m_ar
        If UCase(e.KeyChar) = c Then
          Me.TextBox.Text = UCase(e.KeyChar)
          bFound = True
          Exit For
        End If
      Next
      If Not bFound Then
        Dim s As String = "Please use one of the following keys: "
        For Each c In m_ar
          s += c.ToString + ", "
        Next
        s = Mid(s, 1, Len(s) - 2)
        MessageBox.Show(s)
      End If
      e.Handled = True
    End If
  End Sub
  Protected Overloads Overrides Sub Edit(ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, ByVal bounds As System.Drawing.Rectangle, ByVal [readOnly] As Boolean, ByVal instantText As String, ByVal cellIsVisible As Boolean)
    MyBase.Edit(source, rowNum, bounds, [readOnly], instantText, cellIsVisible)
    Me.ReadOnly = [readOnly]
    'dddd Maybe add code here to update me.textbox to avoid required 2nd key stroke
  End Sub
End Class
Public Class DataGridUMColumn
  Inherits System.Windows.Forms.DataGridTextBoxColumn
  Protected Overrides Function GetColumnValueAtRow(ByVal cm As CurrencyManager, ByVal RowNum As Integer) As Object
    '
    ' Get data from the underlying record and format for display.
    '
		Dim RetValue As Object = Nothing
		Dim oVal As Object = MyBase.GetColumnValueAtRow(cm, RowNum)
    If oVal.GetType Is GetType(DBNull) Then
			RetValue = ""		 ' String to display for DBNull.
    Else
      ' CDec on next statement will throw an exception if this
      ' column style is bound to a column containing non-numeric data.
      Select Case CType(Me.DataGridTableStyle.DataGrid, DCS.DataGrid).UnitOfMeasure
        Case "MM"
					RetValue = oVal
        Case "IN"
					RetValue = (CDec(oVal) / 25.4)
      End Select
		End If
		Return RetValue
  End Function
  Protected Overrides Function Commit(ByVal cm As CurrencyManager, ByVal RowNum As Integer) As Boolean
    '
    ' Parse the data and write to underlying record.
    '
    Dim box As System.Windows.Forms.DataGridTextBox = CType(Me.TextBox, _
     System.Windows.Forms.DataGridTextBox), Value As Decimal
    ' Do not write data if not editing.
    If box.IsInEditOrNavigateMode Then
      'dddddd this was "return true" but left screen residue
      Return MyBase.Commit(cm, RowNum)
    End If
    If TextBox.Text = "" Then   ' in this example, "" maps to DBNull
      SetColumnValueAtRow(cm, RowNum, DBNull.Value)
    Else
      ' Parse the data.
      Value = Val(TextBox.Text)
      Try
        Select Case CType(Me.DataGridTableStyle.DataGrid, DCS.DataGrid).UnitOfMeasure
          Case "MM"
          Case "IN"
            Value = Value * 25.4
        End Select
      Catch
        Return False    ' Exit on error and display old "good" value.
      End Try
      SetColumnValueAtRow(cm, RowNum, Value)   ' Write new value.
    End If
    Me.EndEdit()   ' Let the DataGrid know that processing is completed.
    'dddddd this was "return true" but left screen residue on new row
    Return MyBase.Commit(cm, RowNum)
  End Function
End Class
Public Class DataGridJoinTextBoxColumn
  Inherits System.Windows.Forms.DataGridTextBoxColumn
  Private m_RelationName As String
  Private m_ParentField As DataColumn
  Private m_bUseCalculateEvent As Boolean
  Public Sub New(ByVal UseCalculateEvent As Boolean, _
   Optional ByVal RelationName As String = "", _
   Optional ByVal ParentField As DataColumn = Nothing)
    m_RelationName = RelationName
    m_ParentField = ParentField
    m_bUseCalculateEvent = UseCalculateEvent
    MyBase.ReadOnly = True   ' this column's base style is read only
  End Sub
  Protected Overrides Function GetColumnValueAtRow(ByVal cm As CurrencyManager, ByVal RowNum As Integer) As Object
    Try
      If m_bUseCalculateEvent Then
        Dim dr As DataRow = CType(cm.List, DataView).Item(RowNum).Row
        Return CType(Me.DataGridTableStyle.DataGrid, DCS.DataGrid). _
         RaiseCalculateEvent( _
         CType(cm.List, DataView).Item(RowNum).Row, _
         Me.MappingName)
      Else
        Dim dr As DataRow = CType(cm.List, DataView).Item(RowNum).Row
        Dim drParent As DataRow = dr.GetParentRow(m_RelationName)
        Return drParent(m_ParentField).ToString()
      End If

    Catch ex As Exception
      Return ""   ' handles NullReferenceException case when adding record
    End Try
  End Function
  Protected Overrides Function Commit(ByVal cm As CurrencyManager, ByVal RowNum As Integer) As Boolean
    '
    ' Dummy implementation because it is read-only.
    '
    Return False
  End Function
  Public Shadows ReadOnly Property [ReadOnly]() As Boolean
    '
    ' Shadow the base property so it cannot be set.
    ' Return TRUE so the DataGrid cannot allow edits.
    '
    Get
      Return True
    End Get
  End Property
End Class
Public Class DataGridJoinBoolColumn
  Inherits System.Windows.Forms.DataGridBoolColumn
  Private m_RelationName As String
  Private m_ParentField As DataColumn
  Private m_bUseCalculateEvent As Boolean
  Public Sub New(ByVal UseCalculateEvent As Boolean, _
   Optional ByVal RelationName As String = "", _
   Optional ByVal ParentField As DataColumn = Nothing)
    m_RelationName = RelationName
    m_ParentField = ParentField
    m_bUseCalculateEvent = UseCalculateEvent
    MyBase.ReadOnly = True   ' this column's base style is read only
  End Sub

  Protected Overrides Function GetColumnValueAtRow(ByVal cm As CurrencyManager, ByVal RowNum As Integer) As Object
    Try
      If m_bUseCalculateEvent Then
        Dim dr As DataRow = CType(cm.List, DataView).Item(RowNum).Row
        If CType(Me.DataGridTableStyle.DataGrid, DCS.DataGrid). _
         RaiseCalculateEvent( _
         CType(cm.List, DataView).Item(RowNum).Row, _
         Me.MappingName) Then
          Return True
        Else
          Return False
        End If
      Else
        Dim dr As DataRow = CType(cm.List, DataView).Item(RowNum).Row
        Dim drParent As DataRow = dr.GetParentRow(m_RelationName)
        Return drParent(m_ParentField).ToString()
      End If

    Catch ex As Exception
      Return False   ' handles NullReferenceException case when adding record
    End Try
  End Function
  Protected Overrides Function Commit(ByVal cm As CurrencyManager, ByVal RowNum As Integer) As Boolean
    '
    ' Dummy implementation because it is read-only.
    '
    Return False
  End Function
  Public Shadows ReadOnly Property [ReadOnly]() As Boolean
    '
    ' Shadow the base property so it cannot be set.
    ' Return TRUE so the DataGrid cannot allow edits.
    '
    Get
      Return True
    End Get
  End Property
End Class
Public Class DataGridTextBoxColumn
  ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
  ' As of 7/2/08 - only used in ctlAttributes Grids description column
  ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
  Inherits System.Windows.Forms.DataGridTextBoxColumn
  Private m_dg As DCS.DataGrid
  Public Event TextBox_ClickOrF2(ByVal dg As Windows.Forms.DataGrid)
  Sub New()
    MyBase.New()
    AddHandler Me.TextBox.GotFocus, AddressOf TextBox_GotFocus
    AddHandler Me.TextBox.LostFocus, AddressOf TextBox_LostFocus
    AddHandler Me.TextBox.Leave, AddressOf TextBox_Leave
  End Sub

  Protected Overrides Sub SetDataGridInColumn(ByVal value As System.Windows.Forms.DataGrid)
    m_dg = value
    MyBase.SetDataGridInColumn(value)
  End Sub
  Private Sub TextBox_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    Debug.WriteLine("TextBox_GotFocus in DCS.DataGridTextBoxColumn")
    AddHandler Me.m_dg.F2_Pressed, AddressOf m_dg_F2_Pressed
    AddHandler Me.TextBox.Click, AddressOf TextBox_Click
  End Sub
  Private Sub TextBox_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    Debug.WriteLine("TextBox_LostFocus in DCS.DataGridTextBoxColumn")
    RemoveHandler Me.m_dg.F2_Pressed, AddressOf m_dg_F2_Pressed
    RemoveHandler Me.TextBox.Click, AddressOf TextBox_Click
  End Sub
  Private Sub TextBox_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
    Debug.WriteLine("TextBox_Leave in DCS.DataGridTextBoxColumn")
    Me.TextBox.Hide()
  End Sub

  'Protected Overloads Overrides Sub Edit(ByVal source As System.Windows.Forms.CurrencyManager, _
  '  ByVal rowNum As Integer, ByVal bounds As System.Drawing.Rectangle, _
  '  ByVal [readOnly] As Boolean, ByVal instantText As String, _
  '  ByVal cellIsVisible As Boolean)
  '  MyBase.Edit([source], rowNum, bounds, [readOnly], instantText, cellIsVisible)
  'End Sub
  Private Sub TextBox_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    Debug.WriteLine("Click in DCS.DataGridTextBoxColumn")
    RaiseEvent TextBox_ClickOrF2(Me.m_dg)
  End Sub
  Private Sub m_dg_F2_Pressed(ByRef bHandled As Boolean)
    Debug.WriteLine("F2 in DCS.DataGridTextBoxColumn")
    RaiseEvent TextBox_ClickOrF2(Me.m_dg)
    bHandled = True
  End Sub
  'Protected Overrides Function Commit(ByVal dataSource As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer) As Boolean
  '  Return MyBase.Commit(dataSource, rowNum)
  'End Function

End Class
Public Class DataGridComboBoxColumn
  Inherits System.Windows.Forms.DataGridTextBoxColumn
  Private _DataGridComboBox As DataGridColumnComboBox
  Private _source As System.Windows.Forms.CurrencyManager
  Private _rowNum As Integer
  Private _isEditing As Boolean
  Private _savedValue As String
  Public Event TextChangedByUser(ByVal sPreviousText As String, ByVal sNewText As String, ByRef bCancel As Boolean)
  Public Sub New()
    _source = Nothing
    _isEditing = False
    _DataGridComboBox = New DataGridColumnComboBox
    _DataGridComboBox.DropDownStyle = ComboBoxStyle.DropDownList
    Dim tooltip1 As New ToolTip
    '  tooltip1.SetToolTip(Me.ComboBox, "Hi There")
    AddHandler _DataGridComboBox.Leave, AddressOf ComboBoxLeave
    AddHandler _DataGridComboBox.Enter, AddressOf ComboBoxEnter
  End Sub 'New
  Public Property ComboBox() As DataGridColumnComboBox
    Get
      Return _DataGridComboBox
    End Get
    Set(ByVal Value As DataGridColumnComboBox)
      _DataGridComboBox = Value
    End Set
  End Property
  Private Sub ComboBoxEnter(ByVal sender As Object, ByVal e As EventArgs)
    _isEditing = True
    _savedValue = _DataGridComboBox.Text
    Static cbc As Integer = 0
    cbc += 1
    Debug.WriteLine("cbc - combobox handler enter" + cbc.ToString)

  End Sub 'ComboMadeCurrent
  Private Sub ComboBoxLeave(ByVal sender As Object, ByVal e As EventArgs)
    If _isEditing Then
      If _savedValue <> _DataGridComboBox.Text Then
        SetColumnValueAtRow(_source, _rowNum, _DataGridComboBox.Text)
      End If
      _isEditing = False
      Invalidate()
    End If
    _savedValue = Nothing   'dddddddd
    Debug.WriteLine("ComboBoxLeave")  '''''''''''''''ddddddddddddddddddddd
    _DataGridComboBox.Hide()
  End Sub 'LeaveComboBox
  Protected Overloads Overrides Sub Edit(ByVal [source] As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, ByVal bounds As System.Drawing.Rectangle, ByVal [readOnly] As Boolean, ByVal instantText As String, ByVal cellIsVisible As Boolean)
    MyBase.Edit([source], rowNum, bounds, [readOnly], instantText, cellIsVisible)
    If Not [readOnly] Then
      _rowNum = rowNum
      _source = [source]
      _DataGridComboBox.Parent = Me.TextBox.Parent
      _DataGridComboBox.Location = Me.TextBox.Location
      _DataGridComboBox.Size = New Size(Me.TextBox.Size.Width, _DataGridComboBox.Size.Height)
      _DataGridComboBox.SelectedIndex = _DataGridComboBox.FindStringExact(Me.TextBox.Text)
      _DataGridComboBox.Text = Me.TextBox.Text
      '2/25/06 Reverse next two line to avoid losing grid focus
      _DataGridComboBox.Visible = True
      Me.TextBox.Visible = False
      _DataGridComboBox.BringToFront()
      _DataGridComboBox.Focus()
    End If
  End Sub 'Edit
  Protected Overrides Function Commit(ByVal dataSource As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer) As Boolean
    MyBase.Commit(dataSource, rowNum)
    If _isEditing Then
      _isEditing = False
      If _savedValue <> _DataGridComboBox.Text Then
        SetColumnValueAtRow(dataSource, rowNum, _DataGridComboBox.Text)
      End If
    End If
    Me.TextBox.Visible = True
    Me.TextBox.BringToFront()
    Me.TextBox.Focus()
    _DataGridComboBox.Visible = False
    Debug.WriteLine("ComboBoxCommit")  '''''''''''''''ddddddddddddddddddddd
    Return True
  End Function 'Commit
  Protected Overrides Function GetColumnValueAtRow(ByVal [source] As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer) As Object
    Dim s As Object = MyBase.GetColumnValueAtRow([source], rowNum)
    Dim dv As DataView = CType(Me._DataGridComboBox.DataSource, DataView)
    Dim rowCount As Integer = dv.Count
    Dim i As Integer = 0
    Dim s1 As Object
    'if things are slow, you could order your dataview
    '& use binary search instead of this linear one
    While i < rowCount
      s1 = dv(i)(Me._DataGridComboBox.ValueMember)
      If (Not s1 Is DBNull.Value) AndAlso _
       (Not s Is DBNull.Value) AndAlso _
       s = s1 Then
        Exit While
      End If
      i = i + 1
    End While
    If i < rowCount Then
      Return dv(i)(Me._DataGridComboBox.DisplayMember)
    End If
    Return DBNull.Value
  End Function 'GetColumnValueAtRow
  Protected Overrides Sub SetColumnValueAtRow(ByVal [source] As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, ByVal value As Object)
    Dim s As Object = value
    Dim dv As DataView = CType(Me._DataGridComboBox.DataSource, DataView)
    Dim rowCount As Integer = dv.Count
    Dim i As Integer = 0
    Dim s1 As Object
    'if things are slow, you could order your dataview
    '& use binary search instead of this linear one
    While i < rowCount
      s1 = dv(i)(Me._DataGridComboBox.DisplayMember)
      If (Not s1 Is DBNull.Value) AndAlso _
       s = s1 Then
        Exit While
      End If
      i = i + 1
    End While
    If i < rowCount Then
      s = dv(i)(Me._DataGridComboBox.ValueMember)
    Else
      s = DBNull.Value
    End If
    MyBase.SetColumnValueAtRow([source], rowNum, s)
  End Sub 'SetColumnValueAtRow 
End Class 'DataGridComboBoxColumn
#Region " ComboBox Column "
Public Class DataGridComboBoxStyle
  Inherits System.Windows.Forms.DataGridColumnStyle
  Private m_cbo As DataGridColumnComboBox
  Private m_isEditing As Boolean
  Private m_bWordWrap As Boolean = False
  Private m_value As String
  Public ReadOnly Property ColumnComboBox() As DataGridColumnComboBox 'aaa
    Get
      Return m_cbo
    End Get
    'Set(ByVal Value As DataGridColumnComboBox)
    '  m_cbo = Value
    'End Set
  End Property
  Public Property WordWrap() As Boolean
    Get
      Return m_bWordWrap
    End Get
    Set(ByVal Value As Boolean)
      m_bWordWrap = Value
    End Set
  End Property
  ' Public Event SelectionChangedByUser(ByVal sPreviousText As String, ByVal sNewText As String, ByRef bCancel As Boolean)
  Public Event TextChangedByUser(ByVal dg As Windows.Forms.DataGrid, ByVal sPreviousText As String, ByVal sNewText As String, ByRef bCancelChange As Boolean)
  Public Sub New()
    MyBase.New()
    m_cbo = New DataGridColumnComboBox(False, Me)
    Me.m_cbo.Visible = False
    Me.Alignment = HorizontalAlignment.Left
    Me.NullText = String.Empty
    AddHandler Me.m_cbo.Leave, AddressOf HideControl
  End Sub
  Private Sub New(ByVal MappingName As String)
    Me.MappingName = MappingName
  End Sub
  Public Sub New(ByVal MappingName As String, _
                 ByVal Width As Integer, _
                 ByVal Alignment As HorizontalAlignment, _
                 ByVal HeaderText As String, _
                 ByVal NullText As String, _
                 ByVal ListDrop As ComboBoxStyle, _
                 ByVal WordWrap As Boolean)
    Me.New(MappingName)
    Me.Width = Width
    Me.Alignment = Alignment
    Me.HeaderText = HeaderText
    Me.NullText = NullText
    Me.WordWrap = WordWrap
  End Sub
  Friend Sub EnableTextChangedEvent(ByVal bEnable As Boolean)
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    If bEnable Then
      AddHandler Me.m_cbo.SelectedIndexChanged, AddressOf ComboBoxTextChanged
    Else
      RemoveHandler Me.m_cbo.SelectedIndexChanged, AddressOf ComboBoxTextChanged
    End If
  End Sub
  Private Sub ComboBoxTextChanged(ByVal sender As Object, _
                                           ByVal e As EventArgs)
    If Not Me.m_isEditing Then
      Me.m_isEditing = True
      EnableTextChangedEvent(False)
      MyBase.ColumnStartedEditing(Me.m_cbo)
      EnableTextChangedEvent(True)
    End If
    Dim bCancel As Boolean
    If Not m_value = m_cbo.Text Then
      RaiseEvent TextChangedByUser(Me.DataGridTableStyle.DataGrid, m_value, m_cbo.Text, bCancel)
    Else
      bCancel = True
    End If
    If bCancel Then Me.Abort(0)
  End Sub
  Private Sub HideControl(ByVal sender As Object, _
                         ByVal e As EventArgs)
    'Handles ComboBox.leave
    Me.m_cbo.Bounds = Rectangle.Empty
    Me.m_cbo.Hide()
    Debug.WriteLine("HideControl - m_Value/m_cbo.Text: " + m_value + "/" + Me.m_cbo.Text)
  End Sub
  Protected Overrides Sub EnterNullValue()
    Me.m_cbo.Text = Me.NullText
  End Sub
  Protected Overrides Sub SetDataGridInColumn(ByVal value As System.Windows.Forms.DataGrid)
    MyBase.SetDataGridInColumn(value)
    If Not (Me.m_cbo.Parent Is Nothing) Then
      Me.m_cbo.Parent.Controls.Remove(Me.m_cbo)
    End If
    If Not (value Is Nothing) Then
      value.Controls.Add(Me.m_cbo)
    End If
    Debug.WriteLine("SetDataGridInColumn - m_Value/m_cbo.Text: " + m_value + "/" + Me.m_cbo.Text)
  End Sub
  Protected Overloads Overrides Sub Edit(ByVal [source] As CurrencyManager, _
                                         ByVal rowNum As Integer, _
                                         ByVal bounds As Rectangle, _
                                         ByVal [readOnly] As Boolean, _
                                         ByVal instantText As String, _
                                         ByVal cellIsVisible As Boolean)
    EnableTextChangedEvent(False)
    If IsDBNull(GetColumnValueAtRow([source], rowNum)) Then
      m_value = Me.NullText
    Else
      m_value = CStr(GetColumnValueAtRow([source], rowNum))
    End If
    If cellIsVisible Then
      Me.m_cbo.Bounds = New Rectangle _
      (bounds.X + 2, bounds.Y + 2, bounds.Width - 4, _
      bounds.Height - 4)
      '    If m_value = Me.NullText Then
      '        Me.m_cbo.SelectedIndex = -1
      ' End If
      Me.m_cbo.Text = m_value
      Me.m_cbo.Visible = True
      EnableTextChangedEvent(True)
    Else
      Me.m_cbo.Text = m_value
      Me.m_cbo.Visible = False
    End If
    If Me.m_cbo.Visible Then
      DataGridTableStyle.DataGrid.Invalidate(bounds)
    End If
    'Focus the ComboBox so that user can scroll values
    Me.m_cbo.Focus()
    Debug.WriteLine("Edit - m_Value/m_cbo.Text: " + m_value + "/" + Me.m_cbo.Text)
  End Sub
  Protected Overrides Sub Abort(ByVal rowNum As Integer)
    Debug.WriteLine("Abort-cbo.Text/m_value: " + m_cbo.Text + "/" + m_value.ToString)
    m_isEditing = False
    EnableTextChangedEvent(False)
    Me.m_cbo.Bounds = Rectangle.Empty
    Me.m_cbo.SelectedIndex = -1 '3/30/08
    CType(Me.m_cbo.Parent, DCS.DataGrid).DCSEndEdit()
    Invalidate()
    'Me.cbo.Bounds = Rectangle.Empty
    'Me.cbo.Hide()
    ' Me.EndUpdate()
  End Sub
  Protected Overrides Function Commit(ByVal dataSource As CurrencyManager, _
                                      ByVal rowNum As Integer) As Boolean
    Do 'Single Pass Loop
      Me.m_cbo.Bounds = Rectangle.Empty
      EnableTextChangedEvent(False)
      If Not m_isEditing Then
        Exit Do
      End If
      m_isEditing = False
      If Me.m_cbo.Text.CompareTo(NullText) = 0 Then
        SetColumnValueAtRow(dataSource, rowNum, System.DBNull.Value)
      Else
        SetColumnValueAtRow(dataSource, rowNum, Me.m_cbo.Text)
      End If
      m_value = Me.m_cbo.Text
      Invalidate()
    Loop 'Single Pass Loop
    Me.m_cbo.SelectedIndex = -1 '3/30/08
    Debug.WriteLine("Commit(m_isEditing=" + m_isEditing.ToString + ") - m_cbo.Text: " + Me.m_cbo.Text)
    Return True
  End Function
  Protected Overrides Function GetPreferredSize(ByVal g As Graphics, _
                                                ByVal value As Object) As Size
    Return New Size(100, Me.m_cbo.PreferredHeight + 4)
  End Function
  Protected Overrides Function GetMinimumHeight() As Integer
    Return Me.m_cbo.PreferredHeight + 4
  End Function
  Protected Overrides Function GetPreferredHeight(ByVal g As Graphics, _
                                                  ByVal value As Object) As Integer
    Return Me.m_cbo.PreferredHeight + 4
  End Function
  Protected Overloads Overrides Sub Paint(ByVal g As Graphics, _
                                          ByVal bounds As Rectangle, _
                                          ByVal [source] As CurrencyManager, _
                                          ByVal rowNum As Integer)
    Paint(g, bounds, [source], rowNum, False)
  End Sub
  Protected Overloads Overrides Sub Paint(ByVal g As Graphics, _
                                          ByVal bounds As Rectangle, _
                                          ByVal [source] As CurrencyManager, _
                                          ByVal rowNum As Integer, _
                                          ByVal alignToRight As Boolean)
    Paint(g, bounds, [source], rowNum, Brushes.Red, Brushes.Blue, alignToRight)
  End Sub
  Protected Overloads Overrides Sub Paint(ByVal g As Graphics, _
                                          ByVal bounds As Rectangle, _
                                          ByVal [source] As CurrencyManager, _
                                          ByVal rowNum As Integer, _
                                          ByVal backBrush As Brush, _
                                          ByVal foreBrush As Brush, _
                                          ByVal alignToRight As Boolean)

    Dim o As Object = Me.GetColumnValueAtRow([source], rowNum)
    Dim value As String
    If IsDBNull(o) Then
      value = Me.NullText
    Else
      value = CStr(o)
    End If
    Dim rect As Rectangle = bounds
    g.FillRectangle(backBrush, rect)
    rect.Offset(0, 2)
    rect.Height -= 2
    Dim sf As New StringFormat
    If alignToRight Then
      sf.FormatFlags = sf.FormatFlags Or StringFormatFlags.DirectionRightToLeft
    End If
    If Me.Alignment = HorizontalAlignment.Left Then
      sf.Alignment = StringAlignment.Near
    ElseIf Me.Alignment = HorizontalAlignment.Center Then
      sf.Alignment = StringAlignment.Center
    Else
      sf.Alignment = StringAlignment.Far
    End If
    If Not Me.WordWrap Then
      sf.FormatFlags = sf.FormatFlags Or StringFormatFlags.NoWrap
    End If
    g.DrawString(value, Me.DataGridTableStyle.DataGrid.Font, foreBrush, RectangleF.FromLTRB(rect.X, rect.Y, rect.Right, rect.Bottom), sf)
  End Sub
End Class
#End Region
'Public Class DataGridTextBox
'  Inherits System.Windows.Forms.DataGridTextBox
'  'Private Sub DataGridTextBox_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Enter
'  '  MsgBox("entered dcs.datagridtextbox")
'  'End Sub
'End Class
Public Class ComboShortCutKey
  Public enKey As Keys
  Public iIndex As Integer
  Public Sub New(ByVal enKey As Keys, ByVal iIndex As Integer)
    Me.enKey = enKey
    Me.iIndex = iIndex
  End Sub
End Class
Public Class DataGridColumnComboBox
  Inherits System.Windows.Forms.ComboBox
  Private m_bDataBoundList As Boolean = True
  Private m_bKeyUpOrDown As Boolean = False
  Private WM_KEYUP As Integer = &H101
  Private m_ShortCutKeys() As ComboShortCutKey
  Private m_DataGridColumnStyle As DataGridColumnStyle

  '  Public Event TextChangedByUser(ByVal sPreviousText As String, ByVal sNewText As String, ByRef bCancel As Boolean)
  Public Sub New(Optional ByVal bDataBoundList As Boolean = True, _
    Optional ByRef cs As DataGridColumnStyle = Nothing)
    MyBase.New()
    Me.m_DataGridColumnStyle = cs
    Me.DropDownStyle = ComboBoxStyle.DropDownList
    m_bDataBoundList = bDataBoundList
  End Sub
  'Friend Function RaiseTextChangedByUserEvent(ByVal sOldValue As String) As Boolean
  '  Dim bCancel As Boolean
  '  If Not Me.m_bKeyUpOrDown Then
  '    RaiseEvent TextChangedByUser(sOldValue, Me.Text, bCancel)
  '  End If
  '  Return bCancel
  'End Function
  Protected Overrides Function ProcessKeyMessage(ByRef m As System.Windows.Forms.Message) As Boolean
    'when column is entered by tab, the combo seems to send keyup(tab) back to system
    '   causing focus to move to next column
    Dim keyCode As Keys = CType(CInt(m.WParam.ToInt32), Keys)
    If m.Msg = WM_KEYUP And keyCode = Keys.Tab Then
      Return True
    End If
    Return MyBase.ProcessKeyMessage(m)
  End Function
  Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
    Debug.WriteLine("DataGridColumnComboBox_ProcessCmdKey: " + keyData.ToString)
    Dim retValue As Boolean = False
    Select Case keyData
      Case (Keys.Alt Or Keys.Down)
        Me.DroppedDown = True
        retValue = True
      Case (Keys.Alt Or Keys.Up)
        Me.DroppedDown = False
        retValue = True
      Case Keys.Down
        If m_bDataBoundList Then
          Me.DataManager.Position += 1
          retValue = True
        End If
      Case Keys.Up
        If m_bDataBoundList Then
          Me.DataManager.Position -= 1
          retValue = True
        End If
    End Select
    'Handle following key actions for style type DataGridColumnStyle
    If Me.DroppedDown And Not m_DataGridColumnStyle Is Nothing AndAlso TypeOf (m_DataGridColumnStyle) Is DataGridComboBoxStyle Then
      Dim iDirection As Integer = 0
      Select Case keyData
        Case Keys.Up
          If Me.SelectedIndex > 0 Then
            iDirection = -1
          Else
            retValue = True
          End If
        Case Keys.Down
          If Me.SelectedIndex < Me.Items.Count - 1 Then
            iDirection = 1
          Else
            retValue = True
          End If
      End Select
      If Not iDirection = 0 Then
        With CType(m_DataGridColumnStyle, DCS.DataGridComboBoxStyle)
          .EnableTextChangedEvent(False)
          Me.SelectedIndex += iDirection
          .EnableTextChangedEvent(True)
          retValue = True
        End With
      End If
    End If
    If retValue Then
      Return True
    Else
      Return MyBase.ProcessCmdKey(msg, keyData)
    End If
  End Function
  Public Function AddShortCutKey(ByVal scKey As ComboShortCutKey) As Boolean
    Return Me.AddShortCutKey(scKey.enKey, scKey.iIndex)
  End Function
  Public Function AddShortCutKey(ByVal enKey As Keys, ByVal iIndex As Integer) As Boolean
    Dim retValue As Boolean = False
    If iIndex > -1 And iIndex < Me.Items.Count Then
      retValue = True
      Dim sc As New ComboShortCutKey(enKey, iIndex)
      If m_ShortCutKeys Is Nothing Then
        m_ShortCutKeys = New ComboShortCutKey() {New ComboShortCutKey(enKey, iIndex)}
      Else
        ReDim Preserve m_ShortCutKeys(m_ShortCutKeys.Length)
        m_ShortCutKeys(m_ShortCutKeys.Length - 1) = New ComboShortCutKey(enKey, iIndex)
      End If
    End If
    Return False
  End Function
  Private Sub DataGridColumnComboBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Debug.WriteLine("DataGridColumnComboBox_KeyDown: " + e.KeyCode.ToString)
    If Not Me.m_ShortCutKeys Is Nothing Then
      Dim scKey As ComboShortCutKey
      For Each scKey In Me.m_ShortCutKeys
        If e.KeyCode = scKey.enKey Then
          Me.SelectedIndex = scKey.iIndex
        End If
      Next
    End If
  End Sub
End Class 'DataGridColumnComboBox
Public Class DCSComboBox
  Inherits System.Windows.Forms.ComboBox
  '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
  'Currently used for cboPartLookup
  '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
  Event KeyPreviewDCS(ByRef Handled As Boolean, ByVal keyData As System.Windows.Forms.Keys)
  Private m_TextSaved As String
  Property TextSaved() As String
    Get
      TextSaved = m_TextSaved
    End Get
    Set(ByVal Value As String)
      m_TextSaved = Value
    End Set
  End Property

  Public Sub NewList(ByVal value() As Object)
    '  Me.Items.AddRange(New Object() {"1", "2", "3"})
		Me.Items.AddRange(value)
		'Me.AddItemsCore(value) 'Method Deprecated
  End Sub
  Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
    Dim bHandled As Boolean = False
    RaiseEvent KeyPreviewDCS(bHandled, keyData)
    If bHandled Then Return True
    Return MyBase.ProcessCmdKey(msg, keyData)
  End Function
End Class 'DCSComboBox
Public Class SharedMethods
  Public Shared Function MakeNewGuid() As Guid
    Return Guid.NewGuid
  End Function
  Public Shared Sub DataGridConvertColumn(ByRef dgTableStyle As DataGridTableStyle, _
   ByVal columnMappingName As String, ByRef NewCol As Object)
    Dim tsTemp As New DataGridTableStyle
    ' Dim newCol As New DataGridComboBoxColumn()
    tsTemp.GridColumnStyles.Clear()
    Dim swap As String = dgTableStyle.MappingName
    dgTableStyle.MappingName = "None"
    tsTemp.MappingName = swap
    Dim cs As DataGridColumnStyle
    For Each cs In dgTableStyle.GridColumnStyles
      If cs.MappingName = columnMappingName Then
        NewCol.MappingName = cs.MappingName
        NewCol.HeaderText = cs.HeaderText
        NewCol.Width = cs.Width
        tsTemp.GridColumnStyles.Add(NewCol)
        If NewCol.GetType.ToString = "DCS.DataGridComboBoxColumn" Then
          tsTemp.PreferredRowHeight = NewCol.ComboBox.Height + 3
        End If
      Else
        tsTemp.GridColumnStyles.Add(cs)
      End If
    Next
    dgTableStyle = tsTemp
  End Sub
  Public Shared Function DGAddColumn(ByVal ts As DataGridTableStyle, ByVal iWidth As Integer, _
   ByVal sMappingName As String, Optional ByVal sHeaderText As String = Nothing, _
   Optional ByVal sColumnType As String = "TextBox", _
   Optional ByVal bReadOnly As Boolean = False, _
   Optional ByVal sToolTip As String = "") As DataGridColumnStyle
    Dim cs As DataGridColumnStyle
		Dim tt As ToolTip = Nothing
    If Len(sToolTip) > 0 Then tt = New ToolTip
    Select Case sColumnType
      Case "Bool"
        cs = New DataGridBoolColumn
        CType(cs, DataGridBoolColumn).AllowNull = False
      Case "OneChar"
        cs = New DCS.DataGridOneCharColumn
      Case "UM"
        cs = New DCS.DataGridUMColumn
      Case "JoinTextBox"
        cs = New DCS.DataGridJoinTextBoxColumn(True)
      Case "JoinBool"
        cs = New DCS.DataGridJoinBoolColumn(True)
        CType(cs, DCS.DataGridJoinBoolColumn).AllowNull = False
      Case "ComboBox"
        cs = New DataGridComboBoxStyle
        If Not tt Is Nothing Then
          tt.SetToolTip(CType(cs, DCS.DataGridComboBoxStyle).ColumnComboBox, sToolTip)
        End If
      Case "DCS.TextBox"
        cs = New DCS.DataGridTextBoxColumn
        If Not tt Is Nothing Then
          tt.SetToolTip(CType(cs, DCS.DataGridTextBoxColumn).TextBox, sToolTip)
        End If
      Case Else
        cs = New Windows.Forms.DataGridTextBoxColumn
        If Not tt Is Nothing Then
          tt.SetToolTip(CType(cs, Windows.Forms.DataGridTextBoxColumn).TextBox, sToolTip)
        End If
    End Select
    cs.Width = iWidth
    cs.MappingName = sMappingName
    If sHeaderText Is Nothing Then
      cs.HeaderText = sMappingName
    Else
      cs.HeaderText = sHeaderText
    End If
    If bReadOnly Then
      cs.ReadOnly = True
    End If
    ts.GridColumnStyles.Add(cs)
    Return cs
  End Function
End Class 'SharedMethods
