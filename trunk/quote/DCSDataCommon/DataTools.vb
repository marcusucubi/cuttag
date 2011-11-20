Option Explicit On 
Imports VB = Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports DCS.DCSShared
Imports System.Windows.Forms
#Const DataBaseType = "SQL"
Public Class DCSDataSet
  Inherits System.Data.DataSet
  Private m_AutoUpdate As Boolean
  Private m_bInCopyMode As Boolean
  Private m_AllowDCSEvents As Boolean
  Public Property InCopyMode() As Boolean
    Get
      Return m_bInCopyMode
    End Get
    Set(ByVal Value As Boolean)
      m_bInCopyMode = Value
    End Set
  End Property
  Public Property AutoUpdate() As Boolean
    Get
      AutoUpdate = m_AutoUpdate
    End Get
    Set(ByVal Value As Boolean)
      m_AutoUpdate = Value
    End Set
  End Property
  Public Property AllowDCSEvents(Optional ByVal NewRow As Boolean = False, _
    Optional ByVal VirtualColumns As Boolean = False) As Boolean
    Get 'Added 3/15/08
      Return m_AllowDCSEvents
    End Get
    'Set AllowDCSEvents for each DCSDataTable in the dataset
    Set(ByVal Value As Boolean)
      Dim dt As DataTable
      For Each dt In Me.Tables
        If TypeOf (dt) Is DCS.DCSDataTable Then
          CType(dt, DCSDataTable).AllowDCSEvents(NewRow, VirtualColumns) = Value
        End If
      Next
      m_AllowDCSEvents = Value
    End Set
  End Property
  Public ReadOnly Property HasNullTable() As Boolean
    Get
      Dim retValue As Boolean = False
      Dim dt As DataTable
      For Each dt In Me.Tables
        If dt Is Nothing Then
          retValue = True
          Exit For
        End If
      Next
      Return retValue
    End Get
  End Property
  Public Function GetDCSTables() As ArrayList
    '       Dim ar(Me.Tables.Count) As DCS.DCSDataTable
    Dim arl As New ArrayList
    Dim dt As DataTable
    For Each dt In Me.Tables
      If TypeOf (dt) Is DCS.DCSDataTable Then
        arl.Add(dt)
      End If
    Next
    Return arl
  End Function
  Public Function DCSClone(ByVal cn As SqlConnection) As DCS.DCSDataSet
    Dim dsNew As DCS.DCSDataSet = Me.Clone
		dsNew.AutoUpdate = Me.AutoUpdate
    Dim dt, dtCopy As DCS.DCSDataTable
    '        Dim da As SqlDataAdapter
    Dim cb As SqlCommandBuilder
    Dim prmNew, prmOld As SqlParameter
    For Each dt In Me.GetDCSTables
      dtCopy = dsNew.Tables(dt.TableName)
      With dtCopy
        If Not dt.DCSDataAdapter Is Nothing Then
					.DCSDataAdapter = New SqlDataAdapter(dt.DCSDataAdapter.SelectCommand.CommandText, cn)
					.DCSDataAdapter.SelectCommand.CommandType = dt.DCSDataAdapter.SelectCommand.CommandType	'added 3/10/2011
          .DCSDataAdapter.SelectCommand.Connection = cn
					For Each prmOld In dt.DCSDataAdapter.SelectCommand.Parameters	'loop added 3/10/2011
						prmNew = New SqlParameter
						prmNew.ParameterName = prmOld.ParameterName
						prmNew.DbType = prmOld.DbType
						prmNew.Size = prmOld.Size
						prmNew.SourceColumn = prmOld.SourceColumn
						.DCSDataAdapter.SelectCommand.Parameters.Add(prmNew)
					Next
					.DCSDataAdapter.MissingSchemaAction = MissingSchemaAction.Error
					.DCSDataAdapter.MissingMappingAction = MissingMappingAction.Passthrough
					cb = New SqlCommandBuilder(.DCSDataAdapter)
				End If
				.DCSDataSet = dsNew
			End With
    Next
		Return dsNew
  End Function
End Class
Public Class DCSDataTable
  '----------------------------
  ' To get as dataview from an instance(i) of the this class
  '    use ctype(i.CurrencyManager.list,dataview)
  '    use ctype(i.CurrencyManger.list),dataview).sort = "..." to sort
  '----------------------------
  Inherits System.Data.DataTable
  Private m_DataSetDCS As DCS.DCSDataSet
  Private m_RowGuidCol As Int16 'col index of autoumber guid col
  Private m_CurrencyManager As System.Windows.Forms.CurrencyManager
  Private m_bAllowDCSEvents As Boolean
  Private m_bNewRowEvent As Boolean
  Private m_bRowTasksWaiting As Boolean
  'added to disable exception messages in OnRowChanged
  '  message are not propagated back to code
  Private m_bDisplayExceptions As Boolean
  Private m_bVirtualColumnsEvent As Boolean
  Private m_iUpdateHistoryMax As Integer
  Private m_cmParent As System.Windows.Forms.CurrencyManager
#If DataBaseType = "SQL" Then
  Private m_DataAdapter As System.Data.SqlClient.SqlDataAdapter
#Else
      Private m_DataAdapter As System.Data.OleDb.OleDbDataAdapter
#End If
  Private m_bIsReadOnly As Boolean = False
  Public Event NewRowAdding(ByVal cm As CurrencyManager, ByVal cmParent As CurrencyManager)
  Public Event CurrentRowPositionChanged(ByVal cm As CurrencyManager, ByVal cmParent As CurrencyManager, ByVal IsNewRow As Boolean)
  Public Event CalculateVirtualColumns(ByVal cm As CurrencyManager, ByVal cmParent As CurrencyManager)
  Public Property cmParent() As System.Windows.Forms.CurrencyManager
    Get
      cmParent = m_cmParent
    End Get
    Set(ByVal Value As CurrencyManager)
      m_cmParent = Value
    End Set
  End Property
  Public Property DCSDataSet() As DCS.DCSDataSet
    Get
      Return m_DataSetDCS
    End Get
    Set(ByVal Value As DCS.DCSDataSet)
      m_DataSetDCS = Value
    End Set
  End Property
#If DataBaseType = "SQL" Then
  Public Property DCSDataAdapter() As System.Data.SqlClient.SqlDataAdapter
    Get
      Return m_DataAdapter
    End Get
    Set(ByVal Value As System.Data.SqlClient.SqlDataAdapter)
      m_DataAdapter = Value
    End Set
  End Property
#Else
#End If
  Public Property DisplayExceptions() As Boolean
    Get
      Return m_bDisplayExceptions
    End Get
    Set(ByVal Value As Boolean)
      m_bDisplayExceptions = Value
    End Set
  End Property
  Public Property RowTasksWaiting() As Boolean
    Get
      RowTasksWaiting = m_bRowTasksWaiting
    End Get
    Set(ByVal Value As Boolean)
      m_bRowTasksWaiting = Value
    End Set
  End Property
  Public Property CurrencyManager() As System.Windows.Forms.CurrencyManager
    Get
      CurrencyManager = m_CurrencyManager
    End Get
    Set(ByVal Value As System.Windows.Forms.CurrencyManager)
      m_CurrencyManager = Value
    End Set
  End Property
  Public Property AllowDCSEvents(Optional ByVal NewRow As Boolean = False, _
      Optional ByVal VirtualColumns As Boolean = False) As Boolean
    Get
      AllowDCSEvents = m_bAllowDCSEvents
    End Get
    Set(ByVal Value As Boolean)
      If Value Then
        If m_CurrencyManager Is Nothing Then
          MsgBox("The CurrencyManager property must be set before allowing DCS Events.")
        Else
          AddHandler m_CurrencyManager.PositionChanged, New System.EventHandler(AddressOf m_CurrencyManager_PositionChanged)
          m_bNewRowEvent = NewRow
          m_bVirtualColumnsEvent = VirtualColumns
          ' m_cmParent = cmParent
        End If
      Else
        If m_bAllowDCSEvents = True Then
          RemoveHandler m_CurrencyManager.PositionChanged, AddressOf m_CurrencyManager_PositionChanged
        End If
        m_bNewRowEvent = NewRow
        m_bVirtualColumnsEvent = VirtualColumns
        ' 2/4/03 made cmParent a property
        '  m_cmParent = Nothing
      End If
      'Added 2/4/08
      m_bAllowDCSEvents = Value
      'Remmed 1/27/09 - Prevents Setting to False
            '      m_bAllowDCSEvents = False 
    End Set
  End Property
  Public Property IsReadOnly() As Boolean '3/30/08
    Get
      Return m_bIsReadOnly
    End Get
    Set(ByVal Value As Boolean)
      m_bIsReadOnly = Value
    End Set
  End Property

#If DataBaseType = "SQL" Then
  Public Sub New(ByVal tableName As String, ByVal ds As DCS.DCSDataSet, _
        ByVal da As System.Data.SqlClient.SqlDataAdapter, _
        Optional ByVal rowguidcol As Int16 = -1, _
        Optional ByVal UpdateHistoryMax As Integer = 0)
#Else
      Public Sub New(ByVal tableName As String, ByVal ds As DCS.DCSDataSet, _
            ByVal da As System.Data.OleDb.OleDbDataAdapter, _
            Optional ByVal rowguidcol As Int16 = -1, _
            Optional ByVal UpdateHistoryMax as Integer = 0)
#End If
    MyBase.New(tableName)
    m_DataSetDCS = ds
    m_iUpdateHistoryMax = UpdateHistoryMax
    m_DataAdapter = da
    m_RowGuidCol = rowguidcol
    m_bDisplayExceptions = True
    If Not m_DataAdapter Is Nothing Then
      m_DataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey
    End If
  End Sub
  'Public Sub New(ByVal tableName As String)
  '    MyBase.New(tableName)
  'End Sub
  Private Sub New()
    MyBase.New()
    'Parameterless version needed for copy and merge
  End Sub
  Public Sub AddColumns(ByVal cols() As System.Data.DataColumn)
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Creates columns in Me-DCSDataTable from a column array 
    ' used primarily by Me.AddColumns(DataTable)
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Dim col As System.Data.DataColumn
    Try
      For Each col In cols
        Me.Columns.Add(col.ColumnName, col.DataType)
        If Not IsDBNull(col.DefaultValue) Then
          Me.Columns(col.ColumnName).DefaultValue = col.DefaultValue
        End If
        Me.Columns(col.ColumnName).AllowDBNull = col.AllowDBNull
      Next
    Catch ex As Exception
      Throw New Exception("Problem with DCSDataTable.AddColumns(ColumnArray): " + ex.Message)
    End Try
  End Sub
  Public Sub AddColumns(ByVal dt As System.Data.DataTable)
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Creates columns in Me-DCSDataTable from an exiting table 
    '  Used primarly to copy columns from a stronly-type dataset
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Dim cols(dt.Columns.Count - 1) As System.Data.DataColumn
    dt.Columns.CopyTo(cols, 0)
    Try
      AddColumns(cols)
    Catch ex As Exception
      Throw New Exception("Problem with DCSDataTable.AddColumns(DataTable): " + ex.Message)
    End Try
  End Sub
  Public Sub RefreshVirtualColumns(Optional ByVal CurrentRowOnly As Boolean = False)
        ' disabled, use DataGridJoinColumn
		'    GoTo Skip
		'    Try
		'      If Not m_CurrencyManager Is Nothing AndAlso m_CurrencyManager.Count > 0 Then
		'        If CurrentRowOnly Then
		'          RaiseEvent CalculateVirtualColumns(m_CurrencyManager, m_cmParent)
		'        Else
		'					Dim iSavePosition As Integer = m_CurrencyManager.Position
		'          m_CurrencyManager.Position = 0
		'          Do
		'            m_CurrencyManager.Position += 1
		'          Loop Until m_CurrencyManager.Position = m_CurrencyManager.Count - 1
		'          m_CurrencyManager.Position = iSavePosition
		'        End If
		'      End If
		'    Catch ex As Exception
		'      MsgBox(ex.Message)
		'    End Try
		'Skip:
  End Sub
  Public Function CopyRow(ByVal SourceRow As DataRow, _
    ByVal ColumnNames2Change() As String, ByVal NewColumnValues As Object) As DataRow
    Dim drItemarray(SourceRow.ItemArray.GetUpperBound(0)) As Object
    drItemarray = SourceRow.ItemArray
    Dim i As Integer
    For i = 0 To ColumnNames2Change.GetUpperBound(0)
      drItemarray(Me.Columns.IndexOf(ColumnNames2Change(i))) = NewColumnValues(i)
    Next
    Dim drNew As DataRow = Me.NewRow
    drNew.ItemArray = drItemarray
    'TargetTable, TargetDataAdapter
    Try
			Me.Rows.Add(drNew)
			'			Me.Rows.Add(New Object() {Guid.NewGuid(), "testdd004", "0", "0", "004" _
			'			, 0, 0, 0, 0, 0, 0, 0, 0, "Test", Now(), 10})
		Catch ex As Exception
			Throw ex
    End Try
    ' dd eliminated next line 10/3/03 - handled in OnRowChanged
    '   m_DataAdapter.Update(New DataRow() {drNew})
    Return drNew
  End Function
	Public Function Fill(ByVal ParameterValue As Object, Optional ByVal ParameterValue2 As Object = Nothing) As Integer
		m_DataAdapter.SelectCommand.Parameters(0).Value = ParameterValue
		If Not ParameterValue2 Is Nothing Then
			m_DataAdapter.SelectCommand.Parameters(1).Value = ParameterValue2
		End If
		Return (Me.Fill())
	End Function
  Public Function Fill() As Integer
    Dim iRows As Integer
    Dim bSaveAutoUpdateStatus As Boolean
    bSaveAutoUpdateStatus = m_DataSetDCS.AutoUpdate
		m_DataSetDCS.AutoUpdate = False
		iRows = m_DataAdapter.Fill(Me)
    m_DataSetDCS.AutoUpdate = bSaveAutoUpdateStatus
    Return (iRows)
  End Function
  'Protected Overrides Sub OnColumnChanged(ByVal e As System.Data.DataColumnChangeEventArgs)
  '    Static i As Integer
  '    i += 1
  '    Debug.WriteLine(Me.TableName.ToString _
  '      + " Col: " + e.Column.ColumnName.ToString + " ProVal: " + e.ProposedValue.ToString _
  '      + " : " + i.ToString)
  '    MyBase.OnColumnChanged(e)
  'End Sub
  Private Sub UpdateHistory(ByVal row As DataRow)
    If m_DataSetDCS.InCopyMode Then GoTo ExitSub
    Try
      If m_RowGuidCol > -1 Then
        Dim dtUH As DCSDataTable = CType(Me.DataSet.Tables("UpdateHistory"), DCSDataTable)
        dtUH.Fill(row(m_RowGuidCol))
        Dim iRows As Integer = dtUH.Rows.Count
        If iRows >= g_iUpdateHistoryMax Then
          Dim iIndex As Integer
          For iIndex = 0 To iRows - g_iUpdateHistoryMax
            dtUH.Rows(0).Delete()
          Next
        End If
                dtUH.Rows.Add(New Object() { _
              row(m_RowGuidCol), Environment.UserName, Now()})
        dtUH.Clear()
      End If
    Catch ex As Exception
      MessageBox.Show("Problem updating the History file." + ControlChars.CrLf _
        + ex.Message.ToString)
    End Try
ExitSub:
  End Sub
  Protected Overrides Sub OnRowChanged(ByVal e As System.Data.DataRowChangeEventArgs)
    If m_DataSetDCS.AutoUpdate Then
      'Debug.WriteLine("Row Changed: " + Me.TableName.ToString + "--" + _
      '  Me.CurrencyManager.Current.row.rowstate.ToString + "--" + _
      '  Me.CurrencyManager.Current(4).ToString + "--" + Now().TimeOfDay.ToString)
      Select Case e.Action
        Case DataRowAction.Add, DataRowAction.Change
          If Me.IsReadOnly Then Exit Select '3/15/08
          Try
            m_DataAdapter.Update(New DataRow() {e.Row()})
            If m_iUpdateHistoryMax > 0 Then
                            UpdateHistory(e.Row)
            End If
          Catch ex As Exception
            If m_bDisplayExceptions Then
              MessageBox.Show(ex.ToString())
            End If
            Return
          End Try
        Case DataRowAction.Delete, DataRowAction.Rollback
          MsgBox("Problem with Row States in Updating Database")
        Case DataRowAction.Commit, DataRowAction.Nothing
      End Select
      '      Debug.WriteLine(Now().TimeOfDay)
    End If
    MyBase.OnRowChanged(e)
  End Sub
  Protected Overrides Sub OnRowDeleted(ByVal e As System.Data.DataRowChangeEventArgs)
    If m_DataSetDCS.AutoUpdate Then
      Select Case e.Action
        Case DataRowAction.Delete
          Try
            m_DataAdapter.Update(New DataRow() {e.Row()})
          Catch ex As Exception
            MessageBox.Show(ex.ToString())
            Return
          End Try
        Case DataRowAction.Add, DataRowAction.Commit, DataRowAction.Change, DataRowAction.Rollback
          MsgBox("Problem with Row States in Updating Database")
        Case DataRowAction.Nothing
      End Select
    End If
    MyBase.OnRowDeleted(e)
  End Sub
  Private Sub m_CurrencyManager_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'This handler is added/removed by AllowDCSEvents property
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Debug.WriteLine("CurrencyManager_PositionChanged for " + Me.TableName)
    Try
      If sender.count > Me.Rows.Count Then 'this is a new row
        If m_RowGuidCol > -1 Then
          sender.current(m_RowGuidCol) = Guid.NewGuid
        End If
        If m_bNewRowEvent And sender.current.row.rowstate = DataRowState.Detached Then
          Debug.WriteLine("CurrencyManager_PositionChanged-NewRow Adding for " + Me.TableName)
          RaiseEvent NewRowAdding(sender, m_cmParent)
        End If
        RaiseEvent CurrentRowPositionChanged(sender, m_cmParent, True)
      Else
        RaiseEvent CurrentRowPositionChanged(sender, m_cmParent, False)
        If m_bVirtualColumnsEvent Then
          'Disabled CalculateVirtualColumns, switced to DataGridJoinColumn
          '      RaiseEvent CalculateVirtualColumns(sender, m_cmParent)
        End If
      End If
    Catch ex As Exception
      MessageBox.Show("Problem with DCSTable.m_CurrencyManager_PositionChanged :" + ex.ToString())
    End Try
  End Sub
  'Protected Overrides Sub OnRowChanging(ByVal e As System.Data.DataRowChangeEventArgs)
  '  MyBase.OnRowChanging(e)
  'End Sub
End Class
