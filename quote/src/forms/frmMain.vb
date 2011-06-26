Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class frmMain

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim ChildForm As New frmQuoteA
        ChildForm.MdiParent = Me
        ChildForm.Show(Me.DockPanel1)
    End Sub

End Class

