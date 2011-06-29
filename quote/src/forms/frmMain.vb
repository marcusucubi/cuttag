Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class frmMain

    Public Shared Property frmMain As frmMain

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim ChildForm As New frmQuoteA
        ChildForm.MdiParent = Me
        ChildForm.Show(Me.DockPanel1)
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        frmMain = Me
    End Sub
End Class

