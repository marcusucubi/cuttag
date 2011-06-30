Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model
Imports System.ComponentModel

Public Class frmMain

    Private _Properties As frmProperties

    Public Shared Property frmMain As frmMain

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim ChildForm As New frmQuoteA
        ChildForm.MdiParent = Me
        ChildForm.Show(Me.DockPanel1)
        ShowProperties()
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        frmMain = Me
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        ShowProperties()
    End Sub

    Private Sub ShowProperties()
        If (_Properties Is Nothing) Then
            _Properties = New frmProperties
            InitChild(_Properties)
        End If
        If (_Properties.IsHidden Or _Properties.IsDisposed) Then
            _Properties = New frmProperties
            InitChild(_Properties)
        End If
    End Sub

    Private Sub InitChild(ByVal frm As DockContent)
        frm.MdiParent = frmMain.frmMain
        frm.Show(frmMain.frmMain.DockPanel1)
        frm.DockState = DockState.DockRight
    End Sub

End Class

