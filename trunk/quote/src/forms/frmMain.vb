Imports WeifenLuo.WinFormsUI.Docking

Imports Model
Imports Model.Quote
Imports Model.IO
Imports Doc
Imports Host

Public Class frmMain

    Private WithEvents _ActiveHeader As ActiveHeader
    Private WithEvents _SaveableProperties As Model.Common.SavableProperties
    Public Shared Property frmMain As frmMain

    Public Sub New()
        InitializeComponent()

        frmMain = Me
        Me._ActiveHeader = ActiveHeader.Instance
    End Sub

    Private Sub frmMain_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Host.App.Init( _
            Me, DockPanel1, Me.MenuStrip1, _
            Me.ToolStrip1, Me.MainFormStatusStrip1.FlowLayoutPanel1, _
            Me.MainFormStatusStrip1.ToolTip1)

        AddHandler Model.ModelEvents.TemplateCreated, AddressOf OnNewQuote

    End Sub

    Private Sub OnNewQuote(source As Object, args As ModelEventArgs)
        Me.LoadTemplate(args.Id)
    End Sub

    Private Sub _ActiveQuote_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveHeader.PropertyChanged
        EnableButtons()
        Me._SaveableProperties = ActiveHeader.Instance.Header
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
    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        SaveTemplate()
    End Sub
    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolButton.Click
        SaveTemplate()
    End Sub
    Private Sub CopyToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        CopyTemplate()
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
    Private Sub _SaveableProperties_SavableChange(ByVal subject As Model.Common.SavableProperties, ByVal e As System.EventArgs) Handles _SaveableProperties.SavableChange
        EnableButtons()
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
    Private Sub ToolStripTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripTemplate.Click
        Me.Cursor = Cursors.WaitCursor
        My.Application.DoEvents()
        If Me._ActiveHeader.Header.IsQuote Then
            Dim h As Model.Quote.PrimaryProperties = Me._ActiveHeader.Header.PrimaryProperties
            LoadTemplate(h.TemplateNumber)
        End If
        Me.Cursor = Cursors.Default
    End Sub
    Private Function CanCreateQuote() As Boolean
        Dim result As Boolean
        If Me._ActiveHeader.Header IsNot Nothing Then
            Dim id As Integer
            id = Me._ActiveHeader.Header.PrimaryProperties.CommonId
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
            Dim saver As New Model.IO.QuoteSaver
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
            Dim ChildForm As New frmDocumentA(frm.Initials, frm.Initials)
            ChildForm.MdiParent = Me
            ChildForm.Show(Me.DockPanel1)
        End If

        Model.ModelEvents.NotifyTemplateViewed()
    End Sub

    Private Sub EnableButtons()
        ToolStripTemplate.Enabled = False
        If Me._ActiveHeader.Header Is Nothing Then
            SaveToolButton.Enabled = False
            CopyToolStripMenuItem.Enabled = False
            SaveToolStripMenuItem.Enabled = False
        Else
            SaveToolStripMenuItem.Enabled = True
            CopyToolStripMenuItem.Enabled = False
            If Me._ActiveHeader.Header.IsQuote Then
                SaveToolButton.Enabled = False
                SaveToolStripMenuItem.Enabled = False
                ToolStripTemplate.Enabled = True
            Else
                If Me._ActiveHeader.Header.Dirty Then
                    SaveToolButton.Enabled = True
                    SaveToolStripMenuItem.Enabled = True
                    CopyToolStripMenuItem.Enabled = False
                Else
                    SaveToolButton.Enabled = False
                    SaveToolStripMenuItem.Enabled = False
                    CopyToolStripMenuItem.Enabled = True
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

    Private Sub SaveTemplate()
        If Not Me._ActiveHeader.Header.IsQuote Then
            Dim saver As New TemplateSaver
            saver.Save(Me._ActiveHeader.Header)
        End If
        EnableButtons()
    End Sub

    Private Sub CopyTemplate()
        If Not Me._ActiveHeader.Header.IsQuote Then
            Dim frm As New frmCopyTemplate
            Dim result As DialogResult = frm.ShowDialog()
            If result.HasFlag(DialogResult.OK) Then
                SaveTemplate()
                Dim copy As New TemplateCopier
                Dim id = copy.Copy(Me._ActiveHeader.Header)
                LoadTemplate(id)
            End If
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
        Dim q As Model.Common.Header

        q = loader.Load(id)
        If q.PrimaryProperties.CommonId = 0 Then
            MsgBox("Not Found")
        Else
            Dim ChildForm As New frmDocumentA(q)
            ChildForm.MdiParent = Me
            ChildForm.Show(Me.DockPanel1)
        End If
        Model.ModelEvents.NotifyTemplateViewed()
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
        If q.PrimaryProperties.CommonId = 0 Then
            MsgBox("Not Found")
        Else
            Dim ChildForm As New frmDocumentA(q)
            ChildForm.MdiParent = Me
            ChildForm.Show(Me.DockPanel1)
        End If
        Model.ModelEvents.NotifyQuoteViewed()
        EnableButtons()
    End Sub

    Private Function IsLoaded(ByVal id As String) As Boolean
        Dim result As Boolean
        For Each child As DockContent In Me.MdiChildren

            If Not (TypeOf child Is frmDocumentA) Then
                Continue For
            End If

            Dim w As frmDocumentA
            w = child

            Dim test = w.QuoteHeader.PrimaryProperties.CommonId
            If id = test Then
                w.Activate()
                result = True
            End If
        Next
        Return result
    End Function

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
            Dim frm As frmDocumentA = Me.ActiveMdiChild
            If Not Me._ActiveHeader.Header.IsQuote Then
                frm.AddItem()
            End If
        End If
    End Sub

    Private Sub DeleteItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteItemToolStripMenuItem.Click
        If Me.ActiveMdiChild.GetType.Name = "frmDocumentA" Then
            Dim frm As frmDocumentA = Me.ActiveMdiChild
            If Not Me._ActiveHeader.Header.IsQuote Then
                frm.DeleteItem()
            End If
        End If
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles OptionsToolStripMenuItem.Click
        frmOptions.ShowDialog()
    End Sub

End Class

