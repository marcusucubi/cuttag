Option Explicit On 
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data.SqlClient
Imports DCS.DCSShared
Imports System.IO
Public Class DCSShared
  Public Shared g_cn As SqlConnection
  Public Shared g_sPath As String
  Public Shared g_sLanguage As String
  Public Shared g_bEnglishColors As Boolean
  Public Shared g_sUnits As String
  Public Shared g_iOrganizationID As Integer
  Public Shared g_Reports As New DCSReports
  Public Shared g_sFullAccessUserGroup As String
  Public Shared g_bHasFullAccess As Boolean = False
  Public Shared g_bTagFormulasAtBottom As Boolean
	Public Shared g_bTagShowCoTerminationDiagram As Boolean
	Public Shared g_bTagShowCoTerminationFormula As Boolean
	Public Shared g_bTagShowCoConnFormula As Boolean
	Public Shared g_bTagUseOnlyWireNumber As Boolean
	Public Shared g_sDefaultCircuitNameSeparator As String
	Public Shared g_TagComponentPrefix As String
	Public Shared g_TagComponentSuffix As String
	Public Shared g_bSortSharedComponentsByCircuit As Boolean
  Public Shared g_sRoundLen As String
  Public Shared g_bFractionsAsDecimals As Boolean
  Public Shared g_bAllowMultipleTerminals As Boolean
  Public Shared g_iUpdateHistoryMax As Integer
  Public Shared g_bDefaultComponentName As Boolean
  Public Shared g_bAdjustCutLength As Boolean
  Public Shared g_bShowHints As Boolean
	Public Shared g_bHideInactiveParts As Boolean
	Public Shared g_bShowfrmAttributes As Boolean
  Public Shared g_bShowWireAttributes As Boolean
  Public Shared g_sSortWireAttributes As String
	'Public Shared g_bU
	Public Enum DCSCommandParameterType As Integer
		prmBoolean = 1
		prmGUID = 2
		prmDateTime = 3
		prmString = 4
		prmNText = 5
		prmInt = 6
		prmMoney = 7
		prmDouble = 8
	End Enum
  Public Enum hcObjectType As Integer
    Wire = 1
    Component = 2
    Operation = 3
    InstructionCode = 4
    Comment = 5
    CoTermination = 6
    CoConnector = 7
  End Enum
  Public Enum hcComponentType As Integer
    All = 0
    Terminals = 1
    Molds = 2
    Connectors = 3
    Other = 4
  End Enum
  Public Enum PartImportType As Integer
    Unknown = 0
    DXF = 1
    CSV = 2
  End Enum
  Public Enum hcFormulaCode As Integer
    SingleConductor = 1
    CableConductor = 2
    CableJacket = 3
  End Enum
  Public Shared g_bUseMaster As Boolean
  Public Enum RuleFlexibility As Integer
    Low = 1
    Medium = 2
    UnChangable = 3
  End Enum
	Public Enum RuleVerb As Integer	'Not Currently Used
		Equals = 1
		LessThanOrEqual = 2
		LessThan = 3
		Between = 4
		GreaterThan = 5
		GreaterThanOrEqual = 6
		IsMemberOf = 7
		Contains = 8
	End Enum
	Public Enum RuleType As Integer	'Make changes also in help files
		BatchDetermination = 1
		MachineLoading = 2
		MachinePieceRate = 3
		Standard = 11
		Component2Standard = 12
		Wire2Standard = 14
		RelatedAttribute = 21
	End Enum
  Public Enum RuleRelatedAttributAppliesTo As Integer
    CustomerTrait = 1
    NotCustomerTrait = 2
    SleeveSize = 3
    WireProperty = 4
  End Enum
  Public Enum RuleStandardAppliesTo As Integer
    SingleTermination = 1
    SharedTermination = 2
    SingleCount = 11
    SharedCount = 12
    PartNumberCount = 13
    CalculateSpliceSize = 21
    SpliceSizeRange = 22
    End Enum
	Public Shared Sub DCSSendKeys(ByVal keys As String)
		Try
			System.Windows.Forms.SendKeys.Send(keys)
		Catch ex As Exception
			Throw New Exception("Error with SendKeys. If using Vista try http://support.microsoft.com/kb/925168 - ", ex)
		End Try
	End Sub
	Public Shared Function PersistSetting(ByVal sSection As String, ByVal sKey As String, _
	 ByVal bSave As Boolean, ByVal oValueOrDefaultValue As Object) As Object
		'bSave: True=Write Setting to Registry False=Read Setting From Registry
		'Value: Value to Write for Save or Default value for read with with not key in registry
		Dim sValue As String
		If Not bSave Then		'Read from Registry
			sValue = GetSetting(Application.ProductName, sSection, sKey.ToString)
			If sValue = "" Then
				bSave = True
			Else
				Select Case oValueOrDefaultValue.GetType.ToString
					Case "System.Boolean"
						oValueOrDefaultValue = IIf(sValue = "Y", True, False)
					Case "System.String"
						oValueOrDefaultValue = sValue
					Case Else
						oValueOrDefaultValue = Val(sValue)
				End Select
			End If
		End If
		If bSave Then
			Select Case oValueOrDefaultValue.GetType.ToString
				Case "System.Boolean"
					sValue = IIf(oValueOrDefaultValue, "Y", "N")
				Case "System.String"
					sValue = oValueOrDefaultValue
				Case Else
					sValue = CStr(oValueOrDefaultValue)
			End Select
			SaveSetting(Application.ProductName, sSection, sKey.ToString, sValue)
		End If
		Return oValueOrDefaultValue
	End Function
End Class
Public Class hcObjectTypeDataTable
  Inherits System.Data.DataTable
  Public Sub New(ByVal ar_hcObjectType() As hcObjectType)
    'Wire = 1
    'Component = 2
    'Operation = 3
    'InstructionCode = 4
    'Comment = 5
    'CoTermination = 6
    'CoConnector = 7
    MyBase.New()
    Me.Columns.Add("ID", System.Type.GetType("System.Int32"))
    Me.Columns.Add("Type", System.Type.GetType("System.String"))
    Dim hcOT As hcObjectType
    For Each hcOT In ar_hcObjectType
      Me.Rows.Add(New Object() {CType(hcOT, Integer), hcOT.ToString})
    Next
  End Sub
End Class
Public Class DCSPartNumberComparer
	Implements IComparer
	Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
		 Implements IComparer.Compare
		Return New CaseInsensitiveComparer().Compare(x.PartNumberFull, y.PartNumberFull)
	End Function

End Class

Public Class DCSPartNumber_ID
	Private m_sPartNumber As String
	Private m_sPartVersion As String
	Private m_sPartRevision As String
	Private m_sPartNumberAlt As String
	Private m_sPartNumberFull As String
	Private m_sPartNumberFullCompressed As String
	Private m_gPartID As Guid

	Public Sub New(ByVal sPartNumber As String, ByVal sPartVersion As String, _
	ByVal sPartRevision As String, ByVal oPartNumberAlt As Object, ByVal gPartID As Guid)
		m_sPartNumber = sPartNumber
		m_sPartVersion = sPartVersion
		m_sPartRevision = sPartRevision
		If IsDBNull(oPartNumberAlt) Then
			m_sPartNumberAlt = String.Empty
		Else
			m_sPartNumberAlt = oPartNumberAlt
		End If
		m_sPartNumberFull = m_sPartNumber.ToString + "/" + m_sPartVersion.ToString _
		+ "/" + m_sPartRevision.ToString + " (" + m_sPartNumberAlt.ToString + ")"
		m_sPartNumberFullCompressed = m_sPartNumber _
		 + IIf(m_sPartVersion = "0", "", "/" + m_sPartVersion) _
		 + IIf(m_sPartRevision = "0", "", "/" + m_sPartRevision) _
		 + IIf(m_sPartNumberAlt = "", "", "(" + m_sPartNumberAlt + ")")
		m_gPartID = gPartID
	End Sub	'New				 

	Public ReadOnly Property PartNumber() As String
		Get
			Return m_sPartNumber
		End Get
	End Property
	Public ReadOnly Property PartVersion() As String
		Get
			Return m_sPartVersion
		End Get
	End Property
	Public ReadOnly Property PartRevision() As String
		Get
			Return m_sPartRevision
		End Get
	End Property
	Public ReadOnly Property PartNumberAlt() As String
		Get
			Return m_sPartNumberAlt
		End Get
	End Property
	Public ReadOnly Property PartNumberFull() As String
		Get
			Return m_sPartNumberFull
		End Get
	End Property
	Public ReadOnly Property PartNumberFullCompressed() As String
		Get
			Return m_sPartNumberFullCompressed
		End Get
	End Property
	Public ReadOnly Property PartID() As Guid
		Get
			Return m_gPartID
		End Get
	End Property

	Public Overrides Function ToString() As String
		Return m_sPartNumberFull
	End Function 'ToString
End Class	'DCSPartNumber_ID
Public Class DCSReportInfo
	Private m_Name As String
	Private m_Path As String
	Public Property Name() As String
		Get
			Return m_Name
		End Get
		Set(ByVal Value As String)
			m_Name = Value
		End Set
	End Property
	Public Property Path() As String
		Get
			Return m_Path
		End Get
		Set(ByVal Value As String)
			m_Path = Value
		End Set
	End Property
	Public Sub New(ByVal Name As String)
		m_Name = Name
	End Sub
End Class
Public Class DCSReports
  Inherits System.Collections.CollectionBase
  Default Public ReadOnly Property Item(ByVal ReportName As String) As DCSReportInfo
    Get
      Dim i As DCSReportInfo
      For Each i In list
        If i.Name = ReportName Then
          Return list(list.IndexOf(i))
        End If
      Next
      Return Nothing
    End Get
  End Property
  Default Public ReadOnly Property Item(ByVal Index As Integer) As DCSReportInfo
    Get
      Return list(Index)
    End Get
  End Property
  Public Sub New()
    list.Add(New DCSReportInfo("Tags"))
    list.Add(New DCSReportInfo("PartLabels"))
    list.Add(New DCSReportInfo("WhereUsed"))
		List.Add(New DCSReportInfo("PartDetail"))
		List.Add(New DCSReportInfo("BOM"))
		List.Add(New DCSReportInfo("PartSummary"))
    list.Add(New DCSReportInfo("BatchDetail"))
    list.Add(New DCSReportInfo("CoTermination"))
    list.Add(New DCSReportInfo("CoConnector"))
    list.Add(New DCSReportInfo("CircuitCheckOff"))
    list.Add(New DCSReportInfo("MachineLoad"))
		List.Add(New DCSReportInfo("MachineOrphans"))
		List.Add(New DCSReportInfo("QuoteBOMList"))
	End Sub
End Class
Public Class DCSReportsTextBoxArray
  Inherits System.Collections.CollectionBase
  Private ReadOnly HostForm As System.Windows.Forms.Form
  Default Public ReadOnly Property Item(ByVal Index As Integer) As _
      System.Windows.Forms.TextBox
    Get
      Return CType(Me.List.Item(Index), System.Windows.Forms.TextBox)
    End Get
  End Property

  Public Sub ClickHandler(ByVal sender As Object, ByVal e As _
      System.EventArgs)
    Dim sIndex As String = CType(sender, System.Windows.Forms.Button).Tag
    Dim ctl As Windows.Forms.Control
		Dim tb As Windows.Forms.TextBox = Nothing
    For Each ctl In HostForm.Controls
      If ctl.Name = "tb" + sIndex Then
        tb = CType(ctl, Windows.Forms.TextBox)
        Exit For
      End If
    Next
    If tb Is Nothing Then
      MessageBox.Show("Internal problem with Report Location form. Please contact DCS")
      GoTo ExitSub
    End If
    Dim dlgOpen As New OpenFileDialog
    With dlgOpen
      .CheckPathExists = True
      .CheckFileExists = True
      .RestoreDirectory = False
      .Title = "Select Report"
      .Filter = "Reports (*.rpt)|*.rpt|All Files(*.*)|*.*"
      .FilterIndex = 1
      If Not tb.Text = "" AndAlso Directory.Exists(Directory.GetParent(tb.Text).ToString) Then
        .InitialDirectory = Directory.GetParent(tb.Text).ToString
      Else
        .InitialDirectory = g_sPath + "\Reports"
      End If
      If .ShowDialog() = DialogResult.OK Then
        tb.Text = dlgOpen.FileName
      End If
    End With
ExitSub:
  End Sub
  Public Sub LoadTextBoxes()
    '
    'Creates a label,textbox,button for each member of g_Reports 
    '  Control unique identifier= tb.name, btn.tag
    '
    Dim i As DCSReportInfo
    Dim tb As System.Windows.Forms.TextBox
    Dim lbl As System.Windows.Forms.Label
    Dim btn As System.Windows.Forms.Button
    For Each i In g_Reports
      tb = New System.Windows.Forms.TextBox
      lbl = New System.Windows.Forms.Label
      btn = New System.Windows.Forms.Button
      AddHandler btn.Click, AddressOf ClickHandler
      Me.List.Add(tb)
      HostForm.Controls.Add(tb)
      HostForm.Controls.Add(lbl)
      HostForm.Controls.Add(btn)
      With tb
        .Top = Count * 25
        .Left = 135
        .Name = "tb" + Me.Count.ToString
        .Text = i.Path
        .TabStop = False
        .Width = 350
        .ReadOnly = True
      End With
      With lbl
        .Top = Count * 25
        .Left = 25
        .TextAlign = ContentAlignment.MiddleRight
        .Text = i.Name + ":"
      End With
      With btn
        .Top = Count * 25
        .Tag = Count
        .Left = 490
        .Text = "Modify..."
      End With
    Next

  End Sub
  Public Sub UnloadTextBoxes()
    Dim i As DCSReportInfo
    Dim iIndex As Integer = 0
    For Each i In g_Reports

      i.Path = CType(Me.List(iIndex), Windows.Forms.TextBox).Text
      iIndex += 1

    Next

  End Sub

  Public Sub New(ByVal host As System.Windows.Forms.Form)
    HostForm = host
    LoadTextBoxes()
  End Sub
End Class
Public Class DCSMath
  Private Shared m_sFormat As String
  Private Shared m_dVal As Double
  Public Shared Function DCSRound(ByVal DecimalValue As Double, ByVal FractionFormat As String, _
          Optional ByVal FractionsAsDecimals As Boolean = False) As String
    m_sFormat = FractionFormat
    m_dVal = DecimalValue
    Select Case m_sFormat
      Case "0 Decimals"
        m_dVal = Math.Round(m_dVal, 0)
      Case "1 Decimal"
        m_dVal = Math.Round(m_dVal, 1)
      Case "2 Decimals"
        m_dVal = Math.Round(m_dVal, 2)
      Case "3 Decimals"
        m_dVal = Math.Round(m_dVal, 3)
      Case "4 Decimals"
        m_dVal = Math.Round(m_dVal, 4)
      Case Else
        Return MakeFraction(FractionsAsDecimals)
    End Select
    Return m_dVal.ToString

  End Function
  Private Shared Function MakeFraction(ByVal FractionsAsDecimals As Boolean) As String
    Dim D As Integer
    Select Case m_sFormat
      Case "Nearest 1/2"
        D = 2
      Case "Nearest 1/4"
        D = 4
      Case "Nearest 1/8"
        D = 8
      Case "Nearest 1/16"
        D = 16
      Case "Nearest 1/32"
        D = 32
      Case Else
        Return (m_dVal.ToString)
    End Select
    Dim sSign As String = IIf(m_dVal < 0, "-", "")
    Dim W As Integer = Math.Floor(Math.Abs(m_dVal))
    Dim F As Double = Math.Abs(m_dVal) - W
    Dim N As Integer = Math.Round(F / (1 / D), 0)
    Reduce(N, D)
    If N = D Then Return sSign + (W + 1).ToString
    If N = 0 Then Return sSign + W.ToString
    If FractionsAsDecimals Then
      Return sSign + (W + (N / D)).ToString
    Else
      Return sSign + IIf(W = 0, "", W.ToString) + " " + N.ToString + "/" + D.ToString
    End If

  End Function
  Private Shared Sub Reduce(ByRef N As Integer, ByRef D As Integer)
    Dim B As Integer = 32
    Do
      If Math.IEEERemainder(N, B) = 0 And Math.IEEERemainder(D, B) = 0 Then
        N = N / B
        D = D / B
      End If
      B /= 2
    Loop Until B = 1
  End Sub
End Class