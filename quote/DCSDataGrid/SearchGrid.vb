Imports System.ComponentModel
Imports VB = Microsoft.VisualBasic

'''''''''''''''''''''''''''''''Temp'''''''''''''''''''''
'Imports DCS
'Imports DCS.DCSShared
Imports DCS.SharedMethods
''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Class SearchGrid
	Inherits System.Windows.Forms.UserControl
	Private m_BoundColumnName As String
	Private m_DisplayColumnName As String
	Private m_ChildRelationName As String
	Private m_ChildLookupColumnName As String
	Private m_TextBoxSize As System.Drawing.Size
	Private m_DataSource As DataSet
	Private m_DataMember As String
	Private m_bIsModal As Boolean
	Private m_bUseLookup As Boolean = True
	Private m_bUseInputArray As Boolean = False
	Private m_bUseEmbeddedSearchGrid As Boolean = False
	Private m_arInputArray() As DCS.InputItem
	Private m_btxtLookup_TextChangedEnabled As Boolean
	Private m_bInitialEnter As Boolean
	Private m_bOK2UpdateLookupTextBox As Boolean
	Private m_oCurrentBoundValue As Object
	Private m_sGridFilter As String = ""
	Private m_sGridSort As String = ""
#Region " Windows Form Designer generated code "
	Public Sub New(Optional ByRef arInput() As DCS.InputItem = Nothing, Optional ByVal sBtnText As String = Nothing, _
		Optional ByVal sBtnToolTip As String = Nothing, Optional ByVal oPresetBoundValue As Object = Nothing)
		MyBase.New()
		'This call is required by the Windows Form Designer.

		If Not arInput Is Nothing Then
			m_arInputArray = arInput
			m_bUseInputArray = True
		End If

		If Not oPresetBoundValue Is Nothing Then	 'caller wants to GetCurrentBoundValue if not set by SearchGrid
			m_oCurrentBoundValue = oPresetBoundValue
		End If

		InitializeComponent()

		With btnRequestArrayUpdate
			If Not sBtnText Is Nothing Then
				.Visible = True
				.Text = sBtnText
				.Width = .Font.Size * Len(sBtnText)
				Dim tt As New ToolTip
				tt.SetToolTip(btnRequestArrayUpdate, sBtnToolTip)
			Else
				.Visible = False
			End If
		End With

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
	Friend WithEvents pnlBorder As System.Windows.Forms.Panel
	Friend WithEvents btnOK As System.Windows.Forms.Button
	Friend WithEvents btnCancel As System.Windows.Forms.Button
	Friend WithEvents pnlBottom As System.Windows.Forms.Panel
	Friend WithEvents pnlTop As System.Windows.Forms.Panel
	Public WithEvents dg As DCS.DataGrid
	Public WithEvents txtLookup As DCS.LookupTextBox
	Friend WithEvents pnlMiddle As System.Windows.Forms.Panel
	Friend WithEvents pnlArray As System.Windows.Forms.Panel
	Friend WithEvents btnRequestArrayUpdate As System.Windows.Forms.Button
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Me.txtLookup = New DCS.LookupTextBox
		Me.btnOK = New System.Windows.Forms.Button
		Me.pnlBorder = New System.Windows.Forms.Panel
		Me.pnlArray = New System.Windows.Forms.Panel
		Me.pnlMiddle = New System.Windows.Forms.Panel
		Me.dg = New DCS.DataGrid
		Me.pnlTop = New System.Windows.Forms.Panel
		Me.pnlBottom = New System.Windows.Forms.Panel
		Me.btnRequestArrayUpdate = New System.Windows.Forms.Button
		Me.btnCancel = New System.Windows.Forms.Button
		Me.pnlBorder.SuspendLayout()
		Me.pnlMiddle.SuspendLayout()
		CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnlTop.SuspendLayout()
		Me.pnlBottom.SuspendLayout()
		Me.SuspendLayout()
		'
		'txtLookup
		'
		Me.txtLookup.AutoSize = False
		Me.txtLookup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtLookup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtLookup.Location = New System.Drawing.Point(0, 0)
		Me.txtLookup.Name = "txtLookup"
		Me.txtLookup.Size = New System.Drawing.Size(100, 17)
		Me.txtLookup.TabIndex = 0
		Me.txtLookup.Text = ""
		'
		'btnOK
		'
		Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnOK.Location = New System.Drawing.Point(392, 4)
		Me.btnOK.Name = "btnOK"
		Me.btnOK.Size = New System.Drawing.Size(40, 23)
		Me.btnOK.TabIndex = 0
		Me.btnOK.Text = "&OK"
		'
		'pnlBorder
		'
		Me.pnlBorder.Controls.Add(Me.pnlArray)
		Me.pnlBorder.Controls.Add(Me.pnlMiddle)
		Me.pnlBorder.Controls.Add(Me.pnlTop)
		Me.pnlBorder.Controls.Add(Me.pnlBottom)
		Me.pnlBorder.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnlBorder.Location = New System.Drawing.Point(0, 0)
		Me.pnlBorder.Name = "pnlBorder"
		Me.pnlBorder.Size = New System.Drawing.Size(504, 200)
		Me.pnlBorder.TabIndex = 0
		'
		'pnlArray
		'
		Me.pnlArray.Dock = System.Windows.Forms.DockStyle.Top
		Me.pnlArray.Location = New System.Drawing.Point(0, 96)
		Me.pnlArray.Name = "pnlArray"
		Me.pnlArray.Size = New System.Drawing.Size(504, 40)
		Me.pnlArray.TabIndex = 0
		'
		'pnlMiddle
		'
		Me.pnlMiddle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.pnlMiddle.Controls.Add(Me.dg)
		Me.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Top
		Me.pnlMiddle.DockPadding.All = 1
		Me.pnlMiddle.Location = New System.Drawing.Point(0, 17)
		Me.pnlMiddle.Name = "pnlMiddle"
		Me.pnlMiddle.Size = New System.Drawing.Size(504, 79)
		Me.pnlMiddle.TabIndex = 1
		'
		'dg
		'
		Me.dg.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.dg.DataMember = ""
		Me.dg.Dock = System.Windows.Forms.DockStyle.Fill
		Me.dg.FlatMode = True
		Me.dg.HeaderFont = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.dg.HeaderForeColor = System.Drawing.SystemColors.ControlText
		Me.dg.Location = New System.Drawing.Point(1, 1)
		Me.dg.Name = "dg"
		Me.dg.ReloadNeeded = False
		Me.dg.Size = New System.Drawing.Size(500, 75)
		Me.dg.TabIndex = 0
		Me.dg.UnitOfMeasure = Nothing
		'
		'pnlTop
		'
		Me.pnlTop.BackColor = System.Drawing.SystemColors.ActiveCaption
		Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.pnlTop.Controls.Add(Me.txtLookup)
		Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
		Me.pnlTop.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.pnlTop.Location = New System.Drawing.Point(0, 0)
		Me.pnlTop.Name = "pnlTop"
		Me.pnlTop.Size = New System.Drawing.Size(504, 17)
		Me.pnlTop.TabIndex = 0
		'
		'pnlBottom
		'
		Me.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.pnlBottom.Controls.Add(Me.btnRequestArrayUpdate)
		Me.pnlBottom.Controls.Add(Me.btnCancel)
		Me.pnlBottom.Controls.Add(Me.btnOK)
		Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.pnlBottom.DockPadding.Bottom = 2
		Me.pnlBottom.Location = New System.Drawing.Point(0, 168)
		Me.pnlBottom.Name = "pnlBottom"
		Me.pnlBottom.Size = New System.Drawing.Size(504, 32)
		Me.pnlBottom.TabIndex = 2
		'
		'btnRequestArrayUpdate
		'
		Me.btnRequestArrayUpdate.Location = New System.Drawing.Point(8, 3)
		Me.btnRequestArrayUpdate.Name = "btnRequestArrayUpdate"
		Me.btnRequestArrayUpdate.Size = New System.Drawing.Size(136, 24)
		Me.btnRequestArrayUpdate.TabIndex = 2
		Me.btnRequestArrayUpdate.Text = "RequestArrayUpdate"
		'
		'btnCancel
		'
		Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnCancel.Location = New System.Drawing.Point(440, 4)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(56, 23)
		Me.btnCancel.TabIndex = 1
		Me.btnCancel.Text = "&Cancel"
		'
		'SearchGrid
		'
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.Controls.Add(Me.pnlBorder)
		Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Name = "SearchGrid"
		Me.Size = New System.Drawing.Size(504, 200)
		Me.pnlBorder.ResumeLayout(False)
		Me.pnlMiddle.ResumeLayout(False)
		CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnlTop.ResumeLayout(False)
		Me.pnlBottom.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
#End Region
	Public Property BoundColumnName() As String
		Get
			BoundColumnName = m_BoundColumnName
		End Get
		Set(ByVal Value As String)
			m_BoundColumnName = Value
		End Set
	End Property
	Public Property ChildRelationName() As String
		Get
			ChildRelationName = m_ChildRelationName
		End Get
		Set(ByVal Value As String)
			m_ChildRelationName = Value
		End Set
	End Property
	Public Property TextBoxSize() As System.Drawing.Size
		Get
			Return m_TextBoxSize
		End Get
		Set(ByVal Value As System.Drawing.Size)
			m_TextBoxSize = Value
		End Set
	End Property
	Public Property ChildLookupColumnName() As String
		Get
			ChildLookupColumnName = m_ChildLookupColumnName
		End Get
		Set(ByVal Value As String)
			m_ChildLookupColumnName = Value
		End Set
	End Property
	Public ReadOnly Property SearchGridTableStyle() As DataGridTableStyle
		Get
			SearchGridTableStyle = dg.TableStyles(0)
		End Get
	End Property
	Public Property GridFilter() As String
		Get
			Return m_sGridFilter
		End Get
		Set(ByVal Value As String)
			m_sGridFilter = Value
		End Set
	End Property
	Public Property GridSort() As String
		Get
			Return m_sGridSort
		End Get
		Set(ByVal Value As String)
			m_sGridSort = Value
		End Set
	End Property

	Public Sub SearchGridTableStylesAdd(ByVal ts As DataGridTableStyle)
		dg.TableStyles.Add(ts)
	End Sub
	Public Property IsDirtyLookupList() As Boolean
		Get
			IsDirtyLookupList = txtLookup.IsDirty
		End Get
		Set(ByVal Value As Boolean)
			txtLookup.IsDirty = Value
		End Set
	End Property
	Public Property DisplayColumnName() As String
		Get
			DisplayColumnName = m_DisplayColumnName
		End Get
		Set(ByVal Value As String)
			m_DisplayColumnName = Value
		End Set
	End Property
	Public Property DataSource() As DataSet
		Get
			DataSource = m_DataSource
		End Get
		Set(ByVal Value As DataSet)
			m_DataSource = Value
		End Set
	End Property
	Public Property DataMember() As String
		Get
			DataMember = m_DataMember
		End Get
		Set(ByVal Value As String)
			m_DataMember = Value
		End Set
	End Property
	Public Property UseLookup() As Boolean
		Get
			Return m_bUseLookup
		End Get
		Set(ByVal Value As Boolean)
			m_bUseLookup = Value
		End Set
	End Property
	Private Sub SearchGrid_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If m_ChildRelationName Is Nothing Then
			dg.AllowNavigation = False
		Else
			dg.AllowNavigation = True
		End If
		dg.AllowSorting = False
		dg.ReadOnly = True
		dg.FlatMode = True
		dg.SetDataBinding(m_DataSource, m_DataMember)
		txtLookup.IsDirty = True
		m_bOK2UpdateLookupTextBox = True
	End Sub
	Private Property txtLookup_TextChangedEnabled() As Boolean
		Get
			Return m_btxtLookup_TextChangedEnabled
		End Get
		Set(ByVal Value As Boolean)
			m_btxtLookup_TextChangedEnabled = Value
			If Value Then
				AddHandler Me.txtLookup.TextChanged, AddressOf txtLookup_TextChanged
			Else
				RemoveHandler Me.txtLookup.TextChanged, AddressOf txtLookup_TextChanged
			End If
		End Set
	End Property
	Public Event RequestArrayUpdate(ByRef arInputArray() As DCS.InputItem)
	'Public Function GetCurrentInputArray() As DCS.InputItem
	'  Dim ii As DCS.InputItem
	'  For Each ii In m_arInputArray

	'  Next
	'End Function
	Public Function GetCurrentRow() As DataRow
		Dim retValue As DataRow = Nothing
		If m_bUseEmbeddedSearchGrid Then
			retValue = m_oCurrentBoundValue
		Else
			If m_bUseLookup Then
				If TypeOf (dg.BindingContext(m_DataSource, m_DataMember).Current) Is DataRowView Then
					retValue = CType(dg.BindingContext(m_DataSource, m_DataMember).Current, DataRowView).Row
				End If
			End If
		End If
		Return retValue
	End Function

	Public Function GetCurrentBoundValue() As Object
		Dim retValue As Object = Nothing
		If m_bUseEmbeddedSearchGrid Then
			retValue = m_oCurrentBoundValue
		Else
			If m_bUseLookup Then
				retValue = dg.BindingContext(m_DataSource, m_DataMember).Current(m_BoundColumnName)
			End If
		End If
		Return retValue
	End Function
	'ddddddddddd 7/24/04 changed .DataGridTextBox to TextBox
	Public Function ShowModal(ByVal ctrlParent As System.Windows.Forms.TextBox, _
				Optional ByVal e As System.Windows.Forms.KeyPressEventArgs = Nothing) As Object
		Dim retValue As Object = Nothing
		Do	 'One Pass Loop
			If m_bUseInputArray Then
				m_bUseLookup = False
			End If
			If m_bUseLookup And (IsNothing(m_DataSource) OrElse IsNothing(m_DataMember) OrElse m_DataSource.Tables(m_DataMember) Is Nothing OrElse m_DataSource.Tables(m_DataMember).Rows.Count() < 1) Then
				Dim sTableName As String = Nothing
				If IsNothing(m_DataSource) Then
					sTableName = " DataSource is Nothing."
				ElseIf IsNothing(m_DataMember) Then
					sTableName = "DataMember is Nothing."
				Else
					If Not IsNothing(m_DataMember) Then sTableName = " TableName" + m_DataMember.ToString
				End If
				MsgBox("No items exist.  The lookup will terminate. " + sTableName)
				Exit Do
			End If
			m_bIsModal = True
			Me.txtLookup.Text = ctrlParent.Text
			Me.txtLookup.Font = ctrlParent.Font
			'    m_TextBoxSize = m_TextBoxSize.Empty
			If Not m_TextBoxSize.IsEmpty Then		'Me.TextBoxSize was set
				Me.txtLookup.Size = m_TextBoxSize
				Me.pnlTop.Height = Me.txtLookup.Height
			End If
			Me.txtLookup.SelectionStart = ctrlParent.SelectionStart
			Me.txtLookup.SelectionLength = ctrlParent.SelectionLength
			'If m_bUseInputArray Then
			'  Dim b As New Button
			'  b.Location = New Point(0, 0)
			'  b.Text = "New Button"
			'  Me.pnlArray.Controls.Add(b)
			'End If

			Dim f As New Windows.Forms.Form
			ctrlParent.FindForm.AddOwnedForm(f)
			f.AcceptButton = Me.btnOK
			f.CancelButton = Me.btnCancel
			m_bInitialEnter = True
			If Not m_bUseLookup Then
				If m_bUseInputArray Then
					'pnlTop.Visible = False
					'pnlArray.Visible = True
					'pnlMiddle.Visible = False
					'pnlArray.Visible = True
					'Me.Height = pnlArray.Height + pnlBottom.Height
					pnlTop.Height += pnlTop.Height
					pnlTop.Visible = True
					pnlArray.Visible = False
					pnlMiddle.Dock = DockStyle.Fill
					pnlMiddle.Visible = False
					Me.Height -= pnlMiddle.Height
					Me.Width = SetUpInputArray()
				Else
					pnlTop.Visible = True
					pnlArray.Visible = False
					pnlMiddle.Dock = DockStyle.Fill
					pnlMiddle.Visible = False
					Me.Height -= pnlMiddle.Height
					Me.Width = IIf(txtLookup.Width < 3 * btnCancel.Width, 3 * btnCancel.Width, txtLookup.Width)
				End If
			Else
				pnlArray.Visible = False
				pnlMiddle.Visible = True
				Me.Width = GetGridSize()
				pnlMiddle.Dock = DockStyle.Fill
			End If
			f.Size = New Size(Me.Width, Me.Height)
			f.FormBorderStyle = FormBorderStyle.None
			f.Controls.Add(Me)
			Me.Show()
			Me.txtLookup.Left = 0
			f.StartPosition = FormStartPosition.Manual
			f.ShowInTaskbar = False
			Debug.WriteLine(" FT " + f.Top.ToString + " FH " + f.Height.ToString + " PT" + ctrlParent.FindForm.Top.ToString + " PH " + ctrlParent.FindForm.Height.ToString)
			f.Location = ctrlParent.PointToScreen(New Point(0, 0))
			If m_bUseInputArray Then
				f.Top -= ctrlParent.Height
				f.Top -= 3
			End If
			f.Top -= 2
			f.Left -= 2
			Debug.WriteLine(" FT " + f.Top.ToString + " FH " + f.Height.ToString + " PT" + ctrlParent.FindForm.Top.ToString + " PH " + ctrlParent.FindForm.Height.ToString)
			Dim iOverlapRight As Integer = (f.Left + f.Width) - (ctrlParent.FindForm.Left + ctrlParent.FindForm.Width)
			If iOverlapRight > 0 Then
				f.Left = f.Left - iOverlapRight
				txtLookup.Left = txtLookup.Left + iOverlapRight
			End If
			Dim iOverlapLeft As Integer = ctrlParent.FindForm.Left - f.Left
			If iOverlapLeft > 0 Then
				f.Left = f.Left + iOverlapLeft / 2
				txtLookup.Left = txtLookup.Left - iOverlapLeft / 2
			End If
			Debug.WriteLine(" FT " + f.Top.ToString + " FH " + f.Height.ToString + " PT" + ctrlParent.FindForm.Top.ToString + " PH " + ctrlParent.FindForm.Height.ToString)
			If f.Top + f.Height - (ctrlParent.FindForm.Top + ctrlParent.FindForm.Height) > 0 Then
				f.Top = f.Top - f.Height + txtLookup.Height
				Me.pnlBottom.Dock = DockStyle.Top
				Me.pnlTop.Dock = DockStyle.Bottom
			Else
				Me.pnlBottom.Dock = DockStyle.Bottom
				Me.pnlTop.Dock = DockStyle.Top
			End If
			Debug.WriteLine(" FT " + f.Top.ToString + " FH " + f.Height.ToString + " PT" + ctrlParent.FindForm.Top.ToString + " PH " + ctrlParent.FindForm.Height.ToString)
			'Note: vs2003 seems to have problem tab order for txtLookup
			'      Therefore tab set to 3 stop and 
			'      use SendKeys to set focus on txtLookup
			' Removed 2/18/17/08
			'    SendKeys.Send("{Tab}{Tab}")
			If Not e Is Nothing Then
				' Removed 2/18/17/08
				'      SendKeys.Send(e.KeyChar.ToString)
				' Added 2/18/17/08
				Me.txtLookup.Text = e.KeyChar.ToString
				Me.txtLookup.SelectionStart = 1
				'If Not m_bNoLookup Then
				'  ExecuteLookup()
				'End If
			End If

			'AddHandler Me.txtLookup.TextChanged, AddressOf txtLookup_TextChanged
			txtLookup_TextChangedEnabled = True
			f.ShowDialog()
			If f.DialogResult = DialogResult.OK Then
				If Len(Me.txtLookup.Text) > 0 Then
					retValue = Me.txtLookup.Text
				End If
			End If
			Exit Do
		Loop		'One Pass Loop
		Me.Hide()
		Return retValue
	End Function
	Private Sub SearchGrid_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Enter
		Debug.WriteLine("Entering SearchGrid")
		If m_bUseLookup And txtLookup.IsDirty Then
			RefreshtxtLookupList()
		End If
		'    txtLookup.Focus()
		ExecuteLookup()
		m_bInitialEnter = False
	End Sub
	Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
		Select Case keyData
			Case Keys.Enter
				btnOK.PerformClick()
				Return True
			Case Else
				Return MyBase.ProcessCmdKey(msg, keyData)
		End Select
	End Function
	Private Sub txtLookup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLookup.Click
		If m_bUseEmbeddedSearchGrid Then
			OpenEmbeddedSearchGrid()
		End If
	End Sub
	Private Sub txtLookup_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)	'Handler Manually Controlled
		If m_bUseEmbeddedSearchGrid Then
			OpenEmbeddedSearchGrid()
		End If
		If m_bUseLookup Then
			ExecuteLookup()
		End If
	End Sub
	Private Sub RefreshtxtLookupList()
		Dim cursorSave As Cursor = Me.Cursor
		Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
		Dim i As Integer
		Debug.WriteLine("Start refresh: " + Now.TimeOfDay.ToString)
		Dim dv As DataView = CType(dg.BindingContext(dg.DataSource, dg.DataMember), CurrencyManager).List
		Dim sql As String = ""
		If Len(m_sGridFilter) > 0 Then
			sql = m_sGridFilter + " AND "
		End If
		dv.RowFilter = sql + m_DisplayColumnName + " IS NOT NULL"
		sql = ""
		If Len(m_sGridSort) > 0 Then
			sql = m_sGridSort + ", "
		End If
		dv.Sort = sql + m_DisplayColumnName
		Dim iArSize As Integer = dv.Count() * 10	 'ddddddddddddddddddddd FIX THIS
		Dim arIndex(iArSize) As Integer
		Dim arLookup(iArSize) As String
		Array.Clear(arIndex, 0, arIndex.GetUpperBound(0))
		Array.Clear(arLookup, 0, arLookup.GetUpperBound(0))
		Debug.WriteLine("Start dv for next loop with dv var: " + Now.TimeOfDay.ToString)
		Dim c As Integer
		Dim sLookupMode As String
		If m_ChildRelationName Is Nothing Then
			sLookupMode = "SingleColumn"
		Else
			sLookupMode = "DoubleColumn"
		End If
		Select Case sLookupMode
			Case "SingleColumn"
				For i = 0 To dv.Count - 1
					arIndex(c) = i
					If Not IsDBNull(dv.Item(i)(m_DisplayColumnName)) Then
						arLookup(c) = dv.Item(i)(m_DisplayColumnName)
						c += 1
					End If
				Next i
			Case "DoubleColumn"
				Dim ar As DataRow()
				Dim dr As DataRow
				For i = 0 To dv.Count - 1
					arIndex(c) = i
					If Not IsDBNull(dv.Item(i)(m_DisplayColumnName)) Then
						arLookup(c) = dv.Item(i)(m_DisplayColumnName)
						c += 1
					End If
					ar = dv.Item(i).Row.GetChildRows(m_ChildRelationName)
					For Each dr In ar
						If Not IsDBNull(dr("KeyWord")) Then
							arIndex(c) = i
							arLookup(c) = dr("KeyWord")
							c += 1
						End If
					Next
					'******* This method is fast but requires more specific info
					'sql = "SourceTableID = '" + dv.Item(i)("WireSourceID").ToString + "'"
					'ar = dt.Select(sql)
					'For Each dr In ar
					'    If Not IsDBNull(dr("KeyWord")) Then
					'        arIndex(c) = i
					'        arLookup(c) = dr("KeyWord")
					'        c += 1
					'    End If
					'Next
					'********** This method is very slow *************
					'dvChild = dv.Item(i).CreateChildView(sChildRelation)
					'For Each drvChild In dvChild
					'    If Not IsDBNull(drvChild("KeyWord")) Then
					'        arIndex(c) = i
					'        arLookup(c) = drvChild("KeyWord")
					'        c += 1
					'    End If
					'Next
					'If Not IsDBNull(dv.Item(i)(m_DisplayColumnName)) Then
					'    arIndex(c) = i
					'    arLookup(c) = dv.Item(i)(m_DisplayColumnName)
					'    c += 1
					'End If
				Next i
		End Select
		c -= 1
		Debug.WriteLine("End dv for next loop with dv var: " + Now.TimeOfDay.ToString)
		ReDim Preserve arIndex(c), arLookup(c)
		Debug.WriteLine("End Redim: " + Now.TimeOfDay.ToString)
		Array.Sort(arLookup, arIndex)
		Debug.WriteLine("Sort Done: " + Now.TimeOfDay.ToString)
		With txtLookup()
			.arIndex = arIndex
			.arLookup = arLookup
			.Count = c
			.IsDirty = False
		End With
		Me.Cursor = cursorSave
		Debug.WriteLine("End refresh: " + Now.TimeOfDay.ToString)
	End Sub
	Public Sub UpdatetxtLookupText()
		Debug.WriteLine("UpdatetxtLookup. m_bOK2UpdateLookupTextBox = " + m_bOK2UpdateLookupTextBox.ToString)
		If m_bUseLookup And m_bOK2UpdateLookupTextBox Then
			txtLookup.Text = dg.BindingContext(m_DataSource, m_DataMember).Current(m_DisplayColumnName)
		End If
	End Sub
	Private Sub ExecuteLookup()
		Debug.WriteLine("ExecuteLookup")
		Dim btxtTextChangedEnabled_Save As Boolean = Me.txtLookup_TextChangedEnabled
		Me.txtLookup_TextChangedEnabled = False
		Do	 'Single Pass Loop
			If m_bUseLookup And (m_bInitialEnter Or Me.ActiveControl Is txtLookup()) Then
				m_bOK2UpdateLookupTextBox = False
				Dim i As Integer
				Dim s As String = txtLookup.Text
				If Len(s) > 0 Then
					If txtLookup.Count < 1 Then
						MessageBox.Show("The lookup list is empty.")
						Exit Do
					End If
					Dim cp As New System.Collections.CaseInsensitiveComparer
					For i = 0 To txtLookup.arLookup.GetUpperBound(0)
						If String.Compare(VB.Left(txtLookup.arLookup.GetValue(i), s.Length), s, True) = 0 Then
							Exit For
						End If
					Next
					If i > txtLookup.arLookup.GetUpperBound(0) Then
						Beep()
						DCSShared.DCSSendKeys("{BackSpace}")
						Exit Do
					Else
						dg.ScrollToRow(txtLookup.arIndex(i))
						dg.BindingContext(dg.DataSource, dg.DataMember).Position = txtLookup.arIndex(i)
					End If
				End If
				txtLookup.Focus()
				m_bOK2UpdateLookupTextBox = True
			End If
			Exit Do
		Loop	 'Single Pass Loop
		Me.txtLookup_TextChangedEnabled = btxtTextChangedEnabled_Save
	End Sub
	Private Sub dg_CalculateVirtualColumns(ByVal dr As System.Data.DataRow, ByVal sColumn As String, ByRef Value As Object) Handles dg.CalculateVirtualColumns
		Select Case dg.DataMember	 'Note: Case Sensitive
			Case "WireSource"
				Select Case sColumn
					Case "Gage", "Color", "WireType"
						Try
							Dim drSource As DataRow = m_DataSource.Tables("WireSource").Rows.Find(dr("WireSourceID"))
							If drSource Is Nothing Then
								MsgBox("Unable to find related data in WireSource.")
							Else
								If Not IsDBNull(drSource(sColumn)) Then
									Value = drSource(sColumn)
								End If
							End If
						Catch ex As Exception
							MsgBox("Problem calculating virtual columns for WireComponent. " + ex.Message)
						End Try
					Case Else
						Value = "Missing"
				End Select
			Case "WireComponentSource"
				Select Case sColumn
					Case "ComponentClass", "IsTerminal"
						If sColumn = "ComponentClass" Then sColumn = "Description"
						Try
							If Not IsDBNull(dr("WireComponentSourceID")) Then
								Dim drSource As DataRow = m_DataSource.Tables("WireComponentSource").Rows.Find(dr("WireComponentSourceID"))
								If drSource Is Nothing Then
									MsgBox("Unable to find related data in WireComponentSource.")
								Else
									Dim drClass As DataRow = m_DataSource.Tables("ComponentClass").Rows.Find(drSource("ClassID"))
									If drClass Is Nothing Then
										MsgBox("Unable to find related data in ComponentClass.")
									Else
										If Not IsDBNull(drClass(sColumn)) Then
											Value = drClass(sColumn)
										End If
									End If
								End If
							End If
						Catch ex As Exception
							MsgBox("Problem calculating virtual columns for WireComponent. " + ex.Message)
						End Try
					Case Else
						Value = "Missing"
				End Select
		End Select
	End Sub
	Private Sub dg_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dg.CurrentCellChanged
		UpdatetxtLookupText()
	End Sub
	Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
		dg.Collapse(-1)
		UpdatetxtLookupText()
		If m_bIsModal Then
			Me.ParentForm.DialogResult = DialogResult.OK
			Me.ParentForm.Close()
		Else
			Me.Hide()
		End If
	End Sub
	Private Sub OpenEmbeddedSearchGrid()
		Dim SearchGrid As DCS.SearchGrid
		'Dim gID As Guid
		Dim sts As DataGridTableStyle
		SearchGrid = New DCS.SearchGrid
		Dim oResult As Object
		'		Dim dr As DataRow
		With SearchGrid
			.TextBoxSize = Me.txtLookup.Size
			.DataSource = Me.DataSource
			.DataMember = "WireComponentSource"
			.BoundColumnName = "WireComponentSourceID"
			.DisplayColumnName = "PartNumber"
			.ChildRelationName = "WireComponentSourceKeyWords"
			.ChildLookupColumnName = "KeyWord"
			sts = New DataGridTableStyle
			sts.MappingName = "WireComponentSource"
			DGAddColumn(sts, 50, "PartNumber", "Part#")
			DGAddColumn(sts, 243, "Description")
			DGAddColumn(sts, 30, "IsTerminal", "Term", "JoinBool", True)		'Virtual Column
			DGAddColumn(sts, 24, "PositionCount", "Pos", , True)
			DGAddColumn(sts, 60, "ComponentClass", "Class", "JoinTextBox", True)		'Virtual Column
			.SearchGridTableStylesAdd(sts)
			sts = New DataGridTableStyle
			sts.MappingName = "WireComponentSourceKeyWord"
			DGAddColumn(sts, 120, "KeyWord")
			.SearchGridTableStylesAdd(sts)
		End With
		Me.pnlBottom.Visible = False
		oResult = SearchGrid.ShowModal(txtLookup)
		Me.pnlBottom.Visible = True
		If Not oResult Is Nothing Then
			txtLookup_TextChangedEnabled = False
			txtLookup.Text = oResult		'Update txtLookup without re-activating embedded searchgrid
			txtLookup_TextChangedEnabled = True
			Me.m_oCurrentBoundValue = SearchGrid.GetCurrentBoundValue
		End If
	End Sub
	Private Function SetUpInputArray() As Integer
		Dim retValue As Integer = 0
		Dim iMaxIndex As Integer = m_arInputArray.Length - 1
		Dim lbl(iMaxIndex) As Label
		Dim i As Integer
		Dim txt(iMaxIndex) As DCS.LookupTextBox
		'    m_bUseInputArray = True
		Dim iX As Integer = 0
		For i = 0 To iMaxIndex	 'Create input controls before InitailizeCompoent-seem to only work here 
			lbl(i) = New Windows.Forms.Label
			lbl(i).BackColor = Control.DefaultBackColor
			lbl(i).BorderStyle = BorderStyle.FixedSingle
			lbl(i).Location = New Point(iX, 0)
			lbl(i).Width = m_arInputArray(i).iWidth
			lbl(i).Text = m_arInputArray(i).sLabel
			If m_arInputArray(i).bIsAlignmentTextBox Then
				txt(i) = txtLookup()
			Else
				txt(i) = New DCS.LookupTextBox
				txt(i).AutoSize = False
			End If
			If Not m_TextBoxSize.IsEmpty Then
				lbl(i).Height = m_TextBoxSize.Height
				txt(i).Height = m_TextBoxSize.Height
			End If
			txt(i).Location = New Point(iX, lbl(0).Height)
			txt(i).Width = m_arInputArray(i).iWidth
			retValue += txt(i).Width
			txt(i).Text = m_arInputArray(i).sText
			txt(i).BorderStyle = BorderStyle.FixedSingle
			txt(i).ReadOnly = m_arInputArray(i).bReadOnly
			iX += m_arInputArray(i).iWidth
			m_arInputArray(i).TextControl = txt(i)
			Me.pnlTop.Controls.Add(lbl(i))
			Me.pnlTop.Controls.Add(txt(i))
			If m_arInputArray(i).bUsesSearchGrid Then
				m_bUseEmbeddedSearchGrid = True
			End If
		Next
		Return retValue
	End Function
	Private Function GetGridSize() As Integer
		Dim iBorderWidth As Integer = SystemInformation.BorderSize.Width
		Dim retValue As Integer = Me.dg.RowHeaderWidth + SystemInformation.VerticalScrollBarWidth
		retValue += pnlMiddle.DockPadding.Left + pnlMiddle.DockPadding.Left + 2 * iBorderWidth
		Dim cs As DataGridColumnStyle
		For Each cs In Me.dg.TableStyles(0).GridColumnStyles
			retValue += cs.Width
		Next
		Return retValue
	End Function
	Private Sub btnRequestArrayUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRequestArrayUpdate.Click
		RaiseEvent RequestArrayUpdate(m_arInputArray)
		Dim iIndex As Integer
		For iIndex = 0 To 3
			m_arInputArray(iIndex).TextControl.Text = m_arInputArray(iIndex).sText
		Next
	End Sub
End Class
Public Class InputItem
	Public sDataColumnName As String = ""
	Public sLabel As String = ""
	Public sText As String = ""
	Public bReadOnly As String = False
	Public iWidth As Integer = 100
	Public TextControl As TextBox
	Public bIsAlignmentTextBox As Boolean = False	'Textbox for this item should align with parent form textbox
	Public bUsesSearchGrid As Boolean = False	'This textbox launches a new searchgrid 
End Class
