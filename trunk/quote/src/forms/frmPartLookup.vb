Imports System.Windows.Forms

Public Class frmPartLookup

    Public Shared Property Product As New Product

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Dim data As DataRowView
        data = Me.lstParts.SelectedValue

        Dim code As Product = _
            New Product(data.Row("PartNumber"), data.Row("UnitCost"))
        Product = code

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmPartLookup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me._PartsTableAdapter.Fill(Me.CuttagSKEDataSet._Parts)
    End Sub

End Class
