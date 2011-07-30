Imports System.Windows.Forms

Public Class frmBOMLookup

    Public Shared Property QuoteID As Long

    Private Sub frmQuoteOpen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me._QuoteTableAdapter.Fill(Me.QuoteDataBase._Quote)
        GetQuoteID()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        GetQuoteID()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmTemplateLookup_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        GetQuoteID()
    End Sub

    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.TextChanged
        GetQuoteID()
    End Sub

    Private Sub GetQuoteID()
        Try
            QuoteID = CLng(Me.ComboBox1.Text)
        Catch ex As Exception
            QuoteID = 0
        End Try
        If QuoteID = 0 Then
            Me.OK_Button.Enabled = False
        Else
            Me.OK_Button.Enabled = True
        End If
    End Sub

End Class
