Imports System.Windows.Forms

Public Class frmImport

    Public Property QuoteNumber As Integer

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim table As ImportDataSet.QuoteHeaderDataTable
        table = New ImportDataSetTableAdapters.QuoteHeaderTableAdapter().GetData()
        Me.ComboBox1.DataSource = table
        Me.ComboBox1.DisplayMember = "QuoteNumber"
        SelectProduct()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        SelectProduct()
    End Sub

    Private Sub SelectProduct()

        Dim View As System.Data.DataRowView = Me.ComboBox1.SelectedItem
        Dim row As ImportDataSet.QuoteHeaderRow = View.Row

        Me.QuoteNumber = row.QuoteNumber

    End Sub

End Class
