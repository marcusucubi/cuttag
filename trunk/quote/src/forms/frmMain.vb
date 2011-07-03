Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model
Imports System.ComponentModel

Public Class frmMain

    Private _Properties As frmComputationProperties
    Private _OtherProperties As frmOtherProperties
    Private _GageProperties As frmGageProperties

    Public Shared Property frmMain As frmMain

    Public Sub New()
        InitializeComponent()
        frmMain = Me
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

    Private Sub GagePropertiesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GagePropertiesToolStripMenuItem.Click
        ShowGageProperties()
    End Sub

    Private Sub CreateNewQuote()
        Dim ChildForm As New frmQuoteA
        ChildForm.MdiParent = Me
        ChildForm.Show(Me.DockPanel1)
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

    Private Sub ShowGageProperties()
        If (_GageProperties Is Nothing) Then
            _GageProperties = New frmGageProperties
            InitChild(_GageProperties)
        End If
        If (_GageProperties.IsHidden Or _GageProperties.IsDisposed) Then
            _GageProperties = New frmGageProperties
            InitChild(_GageProperties)
        End If
    End Sub

    Private Sub InitChild(ByVal frm As DockContent)
        frm.MdiParent = frmMain.frmMain
        frm.Show(frmMain.frmMain.DockPanel1)
        For Each w As DockWindow In DockPanel1.DockWindows
            If w.DockState = DockState.DockRight Then
                frm.DockHandler.DockTo(w.DockPanel, DockStyle.Fill)
                frm.Show(w.DockPanel, DockState.DockRight)
            End If
        Next

    End Sub

End Class

