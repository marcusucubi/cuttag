﻿Imports System.ComponentModel
Imports DCS.Quote.Model.Quote
Imports WeifenLuo.WinFormsUI.Docking

Public Class frmMain

    Private _Properties As New frmComputationProperties
    Private _CustomProperties As New frmCustomProperties
    Private _OtherProperties As New frmOtherProperties
    Private _PrimaryProperties As New frmPrimaryProperties
    Private _DetailProperties As New frmDetailProperties
    Private _NoteProperties As New frmNoteProperties
    Private _Output As New frmOutput
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
    Private Sub ViewToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ViewToolStripMenuItem.Click
        Me.ToggleDetailToolStripMenuItem.Visible = Not IsNothing(Me._ActiveHeader.Header)
    End Sub
    Private Sub ToggleDetailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToggleDetailToolStripMenuItem.Click
        If ToggleDetailToolStripMenuItem.Text = "Hide Detail" Then 'User wants to Hide
            ToggleDetailToolStripMenuItem.Text = "Show Detail"
            ToggleDetailToolStripMenuItem.ToolTipText = "Show read-only values"
            ActiveHeader.HideReadOnlyProperties = True
        Else ' 'User wants to Show read-only properties
            ToggleDetailToolStripMenuItem.Text = "Hide Detail"
            ToggleDetailToolStripMenuItem.ToolTipText = "Hide read-only values"
            ActiveHeader.HideReadOnlyProperties = False
        End If
        If Not IsNothing(_Properties) Then Me._Properties.PropertyGrid1.Refresh()
        If Not IsNothing(_PrimaryProperties) Then Me._PrimaryProperties.PropertyGrid1.Refresh()
        If Not IsNothing(_OtherProperties) Then Me._OtherProperties.PropertyGrid1.Refresh()
        If Not IsNothing(_DetailProperties) Then Me._DetailProperties.PropertyGrid1.Refresh()
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
        frmBOMSearch.ShowDialog(Me)
    End Sub
    Private Sub ToolSearchTemplates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolSearchTemplates.Click
        frmBOMSearch.ShowDialog(Me)
    End Sub
    Private Sub ToolSearchQuotes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolSearchQuotes.Click
        frmQuoteSearch.ShowDialog(Me)
    End Sub
    Private Sub NotesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NotesToolStripMenuItem.Click
        ShowNoteProperties()
    End Sub
    Private Sub CustomToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomToolStripMenuItem.Click
        ShowCustomProperties()
    End Sub
    Private Sub ExportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToolStripMenuItem.Click
        DoExport()
    End Sub
    Private Sub ExportButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportButton.Click
        DoExport()
    End Sub
    Private Sub BOMExportButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BOMExportButton.Click
        DoBOMExport()
    End Sub
    Private Sub OutputToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutputToolStripMenuItem.Click
        ShowOutput()
    End Sub
    Private Sub DoExport()
        Dim frm As New frmExport
        frm.ShowDialog()
    End Sub
    Private Sub DoBOMExport()
        Dim export As New ExportBOM
        export.Export(ActiveHeader.ActiveHeader.Header)
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
            Dim IsQuote As Boolean = Me._ActiveHeader.Header.IsQuote
            Dim IsDirty As Boolean = Me._ActiveHeader.Header.Dirty
            If id > 0 And Not IsQuote And Not IsDirty Then
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

        Dim frm As New frmNewBOM
        Dim result As DialogResult = frm.ShowDialog()
        If result = DialogResult.OK Then
            Dim ChildForm As New frmDocumentA(frm.Initials, frm.Initials)
            ChildForm.MdiParent = Me
            ChildForm.Show(Me.DockPanel1)
            DisplayViews()
            Me._PrimaryProperties.Show()

        End If
    End Sub
    Private Sub DisplayViews()
        ShowDetailProperties()
        ShowPrimaryProperties()
        ShowOtherProperties()
        ShowNoteProperties()
        '        ShowCustomProperties()
        ShowComputationProperties()
    End Sub
    Private Sub EnableButtons()
        ToolStripTemplate.Enabled = False
        ExportToolStripMenuItem.Enabled = False
        ExportButton.Enabled = False
        BOMExportButton.Enabled = False
        TextViewToolStripMenuItem1.Enabled = False
        TextViewToolStripMenuItem2.Enabled = False
        CompareWithToolStripMenuItem.Enabled = False
        CompareWithToolStripMenuItem1.Enabled = False
        If Me._ActiveHeader.Header Is Nothing Then
            SaveToolButton.Enabled = False
            SaveToolStripMenuItem.Enabled = False
        Else
            SaveToolStripMenuItem.Enabled = True
            If Me._ActiveHeader.Header.IsQuote Then
                TextViewToolStripMenuItem2.Enabled = True
                SaveToolButton.Enabled = False
                SaveToolStripMenuItem.Enabled = False
                ToolStripTemplate.Enabled = True
                ExportToolStripMenuItem.Enabled = True
                ExportButton.Enabled = True
                CompareWithToolStripMenuItem1.Enabled = True
            Else
                BOMExportButton.Enabled = True
                TextViewToolStripMenuItem1.Enabled = True
                CompareWithToolStripMenuItem.Enabled = True
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
    Private Sub ShowNoteProperties()
        If (_NoteProperties Is Nothing) Then
            _NoteProperties = New frmNoteProperties
            InitChild(_NoteProperties)
        End If
        If (_NoteProperties.IsHidden Or _NoteProperties.IsDisposed) Then
            _NoteProperties = New frmNoteProperties
            InitChild(_NoteProperties)
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
    Public Sub ShowOutput()
        If (_Output Is Nothing) Then
            _Output = New frmOutput
            DockPanel1.SuspendLayout(True)
            _Output.Show(DockPanel1, DockState.DockLeft)
            DockPanel1.ResumeLayout(True, True)
        End If
        If (_Output.IsHidden Or _Output.IsDisposed) Then
            _Output = New frmOutput
            InitChild(_Output, DockState.DockLeft)
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
            Dim saver As New BOMSaver
            saver.Save(Me._ActiveHeader.Header)
        End If
        EnableButtons()
    End Sub
    Private Sub LoadTemplate()
        Dim r As DialogResult = frmBOMLookup.ShowDialog
        If r = DialogResult.OK Then
            LoadTemplate(frmBOMLookup.QuoteID)
        End If
        EnableButtons()
    End Sub
    Public Sub LoadTemplate(ByVal id As Integer)

        If IsLoaded(id) Then
            Return
        End If

        Dim loader As New BOMLoader
        Dim q As Common.Header

        q = loader.Load(id)
        If q.PrimaryProperties.CommonID = 0 Then
            MsgBox("Not Found")
        Else
            Dim ChildForm As New frmDocumentA(q)
            ChildForm.MdiParent = Me
            ChildForm.Show(Me.DockPanel1)
            Me.DisplayViews()
        End If
        EnableButtons()
        Me._Properties.Show()
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
            Dim ChildForm As New frmDocumentA(q)
            ChildForm.MdiParent = Me
            ChildForm.Show(Me.DockPanel1)
            Me.DisplayViews()
        End If
        EnableButtons()
        Me._Properties.Show()
    End Sub
    Private Function IsLoaded(ByVal id As String) As Boolean
        Dim result As Boolean
        For Each child As DockContent In Me.MdiChildren

            If Not (TypeOf child Is frmDocumentA) Then
                Continue For
            End If

            Dim w As frmDocumentA
            w = child

            Dim test = w.QuoteHeader.PrimaryProperties.CommonID
            If id = test Then
                w.Activate()
                result = True
            End If
        Next
        Return result
    End Function
    Private Sub ImportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportToolStripMenuItem.Click

        Dim import As New QuoteImport
        import.DoImport()

    End Sub
    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
    Private Sub frmMain_MdiChildActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MdiChildActivate
        If IsNothing(Me._ActiveHeader.Header) OrElse Me._ActiveHeader.Header.IsQuote Then
            EditToolStripMenuItem.Visible = False
        Else
            EditToolStripMenuItem.Visible = True
        End If
    End Sub
    Private Sub AddItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddItemToolStripMenuItem.Click
        If Me.ActiveMdiChild.GetType.Name = "frmDocumentA" Then
            Dim frm As DCS.Quote.frmDocumentA = Me.ActiveMdiChild
            If Not Me._ActiveHeader.Header.IsQuote Then
                frm.AddItem()
            End If
        End If
    End Sub
    Private Sub DeleteItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteItemToolStripMenuItem.Click
        If Me.ActiveMdiChild.GetType.Name = "frmDocumentA" Then
            Dim frm As DCS.Quote.frmDocumentA = Me.ActiveMdiChild
            If Not Me._ActiveHeader.Header.IsQuote Then
                frm.DeleteItem()
            End If
        End If
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles OptionsToolStripMenuItem.Click
        frmOptions.ShowDialog()
    End Sub

    Private Sub TextViewToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles TextViewToolStripMenuItem1.Click
        Dim frmTextView As New frmTextView(Me._ActiveHeader.Header)
        frmTextView.MdiParent = Me
        frmTextView.Show(Me.DockPanel1)
    End Sub

    Private Sub TextViewToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs) Handles TextViewToolStripMenuItem2.Click
        Dim frmTextView As New frmTextView(Me._ActiveHeader.Header)
        frmTextView.MdiParent = Me
        frmTextView.Show(Me.DockPanel1)
    End Sub

    ''' <summary>
    ''' Used to display drop down menu for quote compare
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CompareMenuItem
        Inherits ToolStripMenuItem

        Private _Header As Common.Header

        Public Sub New(name As String, header As Common.Header)
            MyBase.New(name)
            _Header = header
        End Sub

        Public ReadOnly Property Header
            Get
                Return _Header
            End Get
        End Property

    End Class

    Private Sub CompareWithToolStripMenuItem_Drop(sender As System.Object, e As System.EventArgs) Handles CompareWithToolStripMenuItem.DropDownOpening

        Dim menu As ToolStripMenuItem = DirectCast(sender,  _
            ToolStripMenuItem)

        AddMenuItemsForOpenQuotes(menu)
    End Sub

    Private Sub CompareWithToolStripMenuItem1_Drop(sender As System.Object, e As System.EventArgs) Handles CompareWithToolStripMenuItem1.DropDownOpening

        Dim menu As ToolStripMenuItem = DirectCast(sender,  _
            ToolStripMenuItem)

        AddMenuItemsForOpenQuotes(menu)
    End Sub

    Private Sub AddMenuItemsForOpenQuotes(menu As ToolStripMenuItem)

        Dim submenu As ToolStripMenuItem = _
            DirectCast(menu.DropDownItems(0), ToolStripMenuItem)

        menu.DropDownItems.Clear()
        For Each d As DockContent In Me.DockPanel1.Documents

            If Not (TypeOf d Is frmDocumentA) Then
                Continue For
            End If

            Dim doc As frmDocumentA = d

            If _ActiveHeader.Header Is doc.QuoteHeader Then
                Continue For
            End If

            Dim name As String = doc.QuoteHeader.DisplayName

            Dim new_item As New CompareMenuItem(name, doc.QuoteHeader)
            menu.DropDownItems.Add(new_item)
            AddHandler new_item.Click, AddressOf CompareWithToolStripMenuItem_Click
        Next

    End Sub

    Private Sub CompareWithToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

        Dim menu As CompareMenuItem = DirectCast(sender, CompareMenuItem)

        Me.Cursor = Cursors.WaitCursor
        My.Application.DoEvents()

        Dim frmCompare As New frmCompare(Me._ActiveHeader.Header, menu.Header)
        frmCompare.MdiParent = Me
        frmCompare.Show(Me.DockPanel1)

        Me.Cursor = Cursors.Default
    End Sub

End Class

