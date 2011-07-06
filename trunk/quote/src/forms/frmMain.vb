﻿Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model
Imports System.ComponentModel
Imports System.Collections.Specialized

Public Class frmMain

    Private _Properties As New frmComputationProperties
    Private _OtherProperties As New frmOtherProperties
    Private _WeightProperties As New frmWeights
    Private _PrimaryProperties As New frmPrimaryProperties
    Private WithEvents _ActiveQuote As ActiveQuote
    Private WithEvents menuLastTemplate As New ToolStripMenuItem

    Public Shared Property frmMain As frmMain

    Public Sub New()
        InitializeComponent()
        frmMain = Me
        Me._ActiveQuote = ActiveQuote.ActiveQuote
    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UpdateLastFilesMenu()
    End Sub

    Protected Overrides Sub OnClosing(ByVal e As  _
        System.ComponentModel.CancelEventArgs)
    End Sub

    Private Sub _ActiveQuote_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveQuote.PropertyChanged
        If Me._ActiveQuote.QuoteHeader Is Nothing Then
            SaveToolStripMenuItem.Enabled = False
            SaveToolButton.Enabled = False
        Else
            SaveToolStripMenuItem.Enabled = True
            SaveToolButton.Enabled = True
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

    Private Sub CreateNewQuote()
        Dim ChildForm As New frmQuoteA
        ChildForm.MdiParent = Me
        ChildForm.Show(Me.DockPanel1)
        DisplayViews()
    End Sub

    Private Sub DisplayViews()
        ShowPrimaryProperties()
        ShowOtherProperties()
        ShowWeights()
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

    Private Sub InitChild(ByVal frm As DockContent)
        DockPanel1.SuspendLayout(True)
        frm.Show(DockPanel1, DockState.DockRight)
        DockPanel1.ResumeLayout(True, True)
    End Sub

    Private Sub SaveTemplate()
        Dim loader As New QuoteLoader
        loader.Save(Me._ActiveQuote.QuoteHeader)

        My.Settings.LastTamplate1 = Me._ActiveQuote.QuoteHeader.PrimaryProperties.QuoteNumnber
        UpdateLastFilesMenu()
    End Sub

    Private Sub LoadTemplate()
        Dim r As DialogResult = frmQuoteLookup.ShowDialog
        If r = DialogResult.OK Then
            LoadTemplate(frmQuoteLookup.QuoteID)
        End If
    End Sub

    Private Sub LoadTemplate(ByVal id As Integer)
        Dim loader As New QuoteLoader
        Dim q As Model.QuoteHeader

        q = loader.Load(frmQuoteLookup.QuoteID)
        Dim ChildForm As New frmQuoteA(q)
        ChildForm.MdiParent = Me
        ChildForm.Show(Me.DockPanel1)
        Me.DisplayViews()
    End Sub

    Public Sub UpdateLastFilesMenu()
        If My.Settings.LastTamplate1.Length > 0 Then
            Me.menuTemplate.DropDownItems.AddRange( _
                New ToolStripItem() {Me.menuLastTemplate})
            Me.menuLastTemplate.Name = "menuLastTemplate"
            Me.menuLastTemplate.Size = New System.Drawing.Size(152, 22)
            Me.menuLastTemplate.Text = "Load " + My.Settings.LastTamplate1
        End If
    End Sub

    Private Sub menuLastTemplate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuLastTemplate.Click
        LoadTemplate(My.Settings.LastTamplate1)
    End Sub

End Class

