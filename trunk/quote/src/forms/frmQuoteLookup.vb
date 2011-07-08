Imports System.Windows.Forms

Public Class frmQuoteLookup

    Public Shared Property QuoteID As Long

    Private Sub frmQuoteOpen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me._QuoteTableAdapter.Fill(Me.DevDataSet1._Quote)
        Dim view As DataRowView = Me.ListBox1.SelectedItem
        If view IsNot Nothing Then
            QuoteID = view.Row(0)
        End If
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ListBox1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.DoubleClick
        Dim view As DataRowView = Me.ListBox1.SelectedItem
        If view IsNot Nothing Then
            QuoteID = view.Row(0)
        End If
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub ListBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.Click
        Dim view As DataRowView = Me.ListBox1.SelectedItem
        If view IsNot Nothing Then
            QuoteID = view.Row(0)
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim view As DataRowView = Me.ListBox1.SelectedItem
        If view IsNot Nothing Then
            QuoteID = view.Row(0)
        End If
    End Sub

End Class
