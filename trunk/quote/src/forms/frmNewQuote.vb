Imports System.Windows.Forms
Imports DCS.Quote.Model
Imports DCS.Quote.Model.Template

Public Class frmNewQuote

    Public Property Header As Header

    Private Sub frmNewQuote_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim q As Header = ActiveHeader.ActiveHeader.Header
        Me.txtTemplateNumber.Text = q.PrimaryProperties.CommonID
        Header = q
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
