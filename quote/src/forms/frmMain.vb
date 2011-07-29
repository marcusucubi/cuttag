Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model
Imports System.ComponentModel
Imports System.Collections.Specialized
Imports DCS.Quote.Model.Quote
Imports System.Reflection

Public Class frmMain

    Private _Properties As New frmComputationProperties
    Private _CustomProperties As New frmCustomProperties
    Private _OtherProperties As New frmOtherProperties
    Private _PrimaryProperties As New frmPrimaryProperties
    Private _DetailProperties As New frmDetailProperties
    Private WithEvents _ActiveHeader As ActiveHeader
    Private WithEvents _SaveableProperties As Common.SaveableProperties

    Public Shared Property frmMain As frmMain

    Public Sub New()
        InitializeComponent()
        frmMain = Me
        Me._ActiveHeader = ActiveHeader.ActiveHeader
    End Sub

    Private Sub _ActiveQuote_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveHeader.PropertyChanged
        EnableButtons()
        Me._SaveableProperties = ActiveHeader.ActiveHeader.Header
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        CreateNewTemplate()
    End Sub

    Private Sub menuNewQuote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuNewQuote.Click
        CreateNewTemplate()
    End Sub

    Private Sub NewQuoteMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewQuoteMenuItem.Click
        CreateNewQuote()
    End Sub

    Private Sub NewQuoteButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewQuoteButton.Click
        CreateNewQuote()
    End Sub

    Private Sub PropertiesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PropertiesToolStripMenuItem.Click
        ShowComputationProperties()
    End Sub

    Private Sub ComputationalPropertiesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComputationalPropertiesToolStripMenuItem.Click
        ShowOtherProperties()
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

    Private Sub LoadQuote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadQuoteItem1.Click
        LoadQuote()
    End Sub

    Private Sub LoadQuoteButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadQuoteButton.Click
        LoadQuote()
    End Sub

    Private Sub _SaveableProperties_SavableChange(ByVal subject As Common.SaveableProperties) Handles _SaveableProperties.SavableChange
        EnableButtons()
    End Sub

    Private Sub DetailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DetailToolStripMenuItem.Click
        ShowDetailProperties()
    End Sub

    Private Sub SearchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchToolStripMenuItem.Click
        frmQuoteSearch.ShowDialog(Me)
    End Sub

    Private Sub SearchTemplateMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchTemplateMenuItem1.Click
        frmTemplateSearch.ShowDialog(Me)
    End Sub

    Private Sub ToolSearchTemplates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolSearchTemplates.Click
        frmTemplateSearch.ShowDialog(Me)
    End Sub

    Private Sub ToolSearchQuotes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolSearchQuotes.Click
        frmQuoteSearch.ShowDialog(Me)
    End Sub

    Private Sub ExportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToolStripMenuItem.Click
        DoExport()
    End Sub

    Private Sub ExportButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportButton.Click
        DoExport()
    End Sub

    Private Sub DoExport()
        Dim frm As New frmExport
        frm.ShowDialog()
    End Sub

    Private Sub ToolStripTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripTemplate.Click
        Me.Cursor = Cursors.WaitCursor
        My.Application.DoEvents()
        If Me._ActiveHeader.Header.IsQuote Then
            Dim h As Model.Quote.PrimaryPropeties = Me._ActiveHeader.Header.PrimaryProperties
            LoadTemplate(h.TemplateNumber)
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Function CanCreateQuote() As Boolean
        Dim result As Boolean
        If Me._ActiveHeader.Header IsNot Nothing Then
            Dim id As Integer
            id = Me._ActiveHeader.Header.PrimaryProperties.CommonID
            Dim IsQuote As Boolean
            IsQuote = Me._ActiveHeader.Header.IsQuote
            If id > 0 And Not IsQuote Then
                result = True
            End If
        End If
        Return result
    End Function

    Private Sub CreateNewQuote()

        Dim frm As New frmNewQuote

        Dim result As DialogResult = frm.ShowDialog()
        If result = DialogResult.OK Then
            Dim saver As New QuoteSaver
            Dim id As Integer = saver.Save(frm.Header, frm.QuoteInfo, True)
            If (id > 0) Then
                LoadQuote(id)
            End If
        End If
    End Sub

    Private Sub CreateNewTemplate()

        Dim frm As New frmNewTemplate
        Dim result As DialogResult = frm.ShowDialog()
        If result = DialogResult.OK Then
            Dim ChildForm As New frmQuoteA(frm.Initials, frm.Initials)
            ChildForm.MdiParent = Me
            ChildForm.Show(Me.DockPanel1)
            DisplayViews()
        End If
    End Sub

    Private Sub DisplayViews()
        ShowDetailProperties()
        ShowPrimaryProperties()
        ShowOtherProperties()
        ShowCustomProperties()
        ShowComputationProperties()
    End Sub

    Private Sub EnableButtons()
        ToolStripTemplate.Enabled = False
        ExportToolStripMenuItem.Enabled = False
        ExportButton.Enabled = False
        If Me._ActiveHeader.Header Is Nothing Then
            SaveToolButton.Enabled = False
            SaveToolStripMenuItem.Enabled = False
        Else
            SaveToolStripMenuItem.Enabled = True
            If Me._ActiveHeader.Header.IsQuote Then
                SaveToolButton.Enabled = False
                SaveToolStripMenuItem.Enabled = False
                ToolStripTemplate.Enabled = True
                ExportToolStripMenuItem.Enabled = True
                ExportButton.Enabled = True
            Else
                If Me._ActiveHeader.Header.Dirty Then
                    SaveToolButton.Enabled = True
                    SaveToolStripMenuItem.Enabled = True
                Else
                    SaveToolButton.Enabled = False
                    SaveToolStripMenuItem.Enabled = False
                End If
            End If
        End If
        If CanCreateQuote() Then
            NewQuoteMenuItem.Enabled = True
            NewQuoteButton.Enabled = True
        Else
            NewQuoteMenuItem.Enabled = False
            NewQuoteButton.Enabled = False
        End If
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

    Private Sub ShowCustomProperties()
        If (_CustomProperties Is Nothing) Then
            _CustomProperties = New frmCustomProperties
            InitChild(_CustomProperties)
        End If
        If (_CustomProperties.IsHidden Or _CustomProperties.IsDisposed) Then
            _CustomProperties = New frmCustomProperties
            InitChild(_CustomProperties)
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
            DockPanel1.SuspendLayout(True)
            _DetailProperties.Show(DockPanel1, DockState.DockBottom)
            DockPanel1.ResumeLayout(True, True)
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
        If Not Me._ActiveHeader.Header.IsQuote Then
            Dim saver As New TemplateSaver
            saver.Save(Me._ActiveHeader.Header)
        End If
        EnableButtons()
    End Sub

    Private Sub LoadTemplate()
        Dim r As DialogResult = frmTemplateLookup.ShowDialog
        If r = DialogResult.OK Then
            LoadTemplate(frmTemplateLookup.QuoteID)
        End If
        EnableButtons()
    End Sub

    Public Sub LoadTemplate(ByVal id As Integer)

        If IsLoaded(id) Then
            Return
        End If

        Dim loader As New TemplateLoader
        Dim q As Common.Header

        q = loader.Load(id)
        If q.PrimaryProperties.CommonID = 0 Then
            MsgBox("Not Found")
        Else
            Dim ChildForm As New frmQuoteA(q)
            ChildForm.MdiParent = Me
            ChildForm.Show(Me.DockPanel1)
            Me.DisplayViews()
        End If
        EnableButtons()
    End Sub

    Private Sub LoadQuote()
        Dim r As DialogResult = frmQuoteLookup.ShowDialog
        If r = DialogResult.OK Then
            LoadQuote(frmQuoteLookup.QuoteID)
        End If
    End Sub

    Public Sub LoadQuote(ByVal id As Integer)

        If IsLoaded(id) Then
            Return
        End If

        Dim loader As New QuoteLoader
        Dim q As Header

        q = loader.Load(id)
        If q.PrimaryProperties.CommonID = 0 Then
            MsgBox("Not Found")
        Else
            Dim ChildForm As New frmQuoteA(q)
            ChildForm.MdiParent = Me
            ChildForm.Show(Me.DockPanel1)
            Me.DisplayViews()
        End If
        EnableButtons()
    End Sub

    Private Function IsLoaded(ByVal id As String) As Boolean
        Dim result As Boolean
        For Each w As frmQuoteA In Me.MdiChildren
            Dim test = w.QuoteHeader.PrimaryProperties.CommonID
            If id = test Then
                w.Activate()
                result = True
            End If
        Next
        Return result
    End Function

End Class

