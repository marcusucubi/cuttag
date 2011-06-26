Imports System.Windows.Forms
Imports DCS.Quote.Model


Public Class frmPartLookup

    Public Shared Property Product As New Product

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Dim data As DataRowView
        data = Me.lstParts.SelectedValue

        Dim number As String = data.Row("PartNumber")
        Dim cost As Decimal = data.Row("UnitCost")
        Dim type As ProductClass = ProductClass.WIRE

        Product = New Product(number, cost, type)

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmPartLookup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me._PartsTableAdapter.Fill(Me.CuttagSKEDataSet._Parts)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

End Class
