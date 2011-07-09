Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model
Imports System.ComponentModel
Imports System.Collections.Specialized

Public Class frmMain

    Private _Properties As New frmComputationProperties
    Private _OtherProperties As New frmOtherProperties
    Private _WeightProperties As New frmWeights
    Private _PrimaryProperties As New frmPrimaryProperties
    Private _DetailProperties As New frmDetailProperties
    Private WithEvents _ActiveQuote As ActiveTemplate

    Public Shared Property frmMain As frmMain

    Public Sub New()
        InitializeComponent()
        frmMain = Me
        Me._ActiveQuote = ActiveTemplate.ActiveTemplate
    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UpdateLastFilesMenu()
    End Sub

    Private Sub _ActiveQuote_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveQuote.PropertyChanged
        If Me._ActiveQuote.QuoteHeader Is Nothing Then
            SaveToolStripMenuItem.Enabled = False
            SaveToolButton.Enabled = False
            NewQuoteMenuItem.Enabled = False
        Else
            SaveToolStripMenuItem.Enabled = True
            SaveToolButton.Enabled = True
            NewQuoteMenuItem.Enabled = True
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        CreateNewQuote()
    End Sub

    Private Sub menuNewQuote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuNewQuote.Click
        CreateNewQuote()
    End Sub

    Private Sub PropertiesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PropertiesToolStripMenuItem.Click
        ShowComputationProperties()
    End Sub

    Private Sub ComputationalPropertiesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComputationalPropertiesToolStripMenuItem.Click
        ShowOtherProperties()
    End Sub

    Private Sub WeightToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GagePropertiesToolStripMenuItem.Click
        ShowWeights()
    End Sub

    Private Sub PrimaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrimaryToolStripMenuItem.Click
        ShowPrimaryProperties()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        SaveTemplate()
    End Sub

    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolButton.Click
        SaveTemplate()
    End Sub

    Private Sub LoadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadToolStripMenuItem.Click
        LoadTemplate()
    End Sub

    Private Sub LoadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadButton.Click
        LoadTemplate()
    End Sub

    Private Sub LoadQuoe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadQuoteItem1.Click
        LoadQuote()
    End Sub

    Private Sub LoadLastToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadLastToolStripMenuItem.Click
        LoadTemplate(My.Settings.LastTamplate1)
    End Sub

    Private Sub CreateNewQuote()
        Dim ChildForm As New frmQuoteA
        ChildForm.MdiParent = Me
        ChildForm.Show(Me.DockPanel1)
        DisplayViews()
    End Sub

    Private Sub DisplayViews()
        ShowDetailProperties()
        ShowPrimaryProperties()
        ShowOtherProperties()
        ShowComputationProperties()
    End Sub

    Private Sub ShowComputationProperties()
        If (_Properties Is Nothing) Then
            _Properties = New frmComputationProperties
            InitChild(_Properties)
        End If
        If (_Properties.IsHidden Or _Properties.IsDisposed) Then
            _Properties = New frmComputationProperties
            InitChild(_Properties)
        End If
    End Sub

    Private Sub ShowOtherProperties()
        If (_OtherProperties Is Nothing) Then
            _OtherProperties = New frmOtherProperties
            InitChild(_OtherProperties)
        End If
        If (_OtherProperties.IsHidden Or _OtherProperties.IsDisposed) Then
            _OtherProperties = New frmOtherProperties
            InitChild(_OtherProperties)
        End If
    End Sub

    Private Sub ShowWeights()
        If (_WeightProperties Is Nothing) Then
            _WeightProperties = New frmWeights
            InitChild(_WeightProperties)
        End If
        If (_WeightProperties.IsHidden Or _WeightProperties.IsDisposed) Then
            _WeightProperties = New frmWeights
            InitChild(_WeightProperties)
        End If
    End Sub

    Private Sub ShowPrimaryProperties()
        If (_PrimaryProperties Is Nothing) Then
            _PrimaryProperties = New frmPrimaryProperties
            InitChild(_PrimaryProperties)
        End If
        If (_PrimaryProperties.IsHidden Or _PrimaryProperties.IsDisposed) Then
            _PrimaryProperties = New frmPrimaryProperties
            InitChild(_PrimaryProperties)
        End If
    End Sub

    Private Sub ShowDetailProperties()
        If (_DetailProperties Is Nothing) Then
            _DetailProperties = New frmDetailProperties
            InitChild(_DetailProperties, DockState.DockBottom)
        End If
        If (_DetailProperties.IsHidden Or _DetailProperties.IsDisposed) Then
            _DetailProperties = New frmDetailProperties
            InitChild(_DetailProperties, DockState.DockBottom)
        End If
    End Sub

    Private Sub InitChild(ByVal frm As DockContent)
        DockPanel1.SuspendLayout(True)
        frm.Show(DockPanel1, DockState.DockRight)
        DockPanel1.ResumeLayout(True, True)
    End Sub

    Private Sub InitChild(ByVal frm As DockContent, ByVal state As DockState)
        DockPanel1.SuspendLayout(True)
        frm.Show(DockPanel1, state)
        DockPanel1.ResumeLayout(True, True)
    End Sub

    Private Sub SaveTemplate()
        Dim saver As New QuoteSaver
        saver.Save(Me._ActiveQuote.QuoteHeader)
        UpdateLastFilesMenu()
    End Sub

    Private Sub LoadTemplate()
        Dim r As DialogResult = frmTemplateLookup.ShowDialog
        If r = DialogResult.OK Then
            LoadTemplate(frmTemplateLookup.QuoteID)
        End If
    End Sub

    Private Sub LoadTemplate(ByVal id As Integer)

        If IsLoaded(id) Then
            Return
        End If

        Dim loader As New QuoteLoader
        Dim q As Model.QuoteHeader

        q = loader.Load(id)
        Dim ChildForm As New frmQuoteA(q)
        ChildForm.MdiParent = Me
        ChildForm.Show(Me.DockPanel1)
        Me.DisplayViews()
    End Sub

    Private Sub LoadQuote()
        Dim r As DialogResult = frmQuoteLookup.ShowDialog
        If r = DialogResult.OK Then
            LoadQuote(frmQuoteLookup.QuoteID)
        End If
    End Sub

    Private Sub LoadQuote(ByVal id As Integer)

        If IsLoaded(id) Then
            Return
        End If

        Dim loader As New QuoteLoader
        Dim q As Model.QuoteHeader

        q = loader.Load(id)
        Dim ChildForm As New frmQuoteA(q)
        ChildForm.MdiParent = Me
        ChildForm.Show(Me.DockPanel1)
        Me.DisplayViews()
    End Sub

    Private Sub UpdateLastFilesMenu()
        Dim id As String = My.Settings.LastTamplate1
        If id.Length > 0 Then
            Me.LoadLastToolStripMenuItem.Enabled = True
            Me.LoadLastToolStripMenuItem.Text = "Load Last (" + id + ")"
        Else
            Me.LoadLastToolStripMenuItem.Enabled = False
            Me.LoadLastToolStripMenuItem.Text = "Load Last"
        End If
    End Sub

    Private Function IsLoaded(ByVal id As String) As Boolean
        Dim result As Boolean
        For Each w As frmQuoteA In Me.MdiChildren
            Dim test = w.QuoteHeader.PrimaryProperties.QuoteNumnber
            If id = test Then
                w.Activate()
                result = True
            End If
        Next
        Return result
    End Function

End Class

