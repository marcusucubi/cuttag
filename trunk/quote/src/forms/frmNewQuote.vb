Imports System.Windows.Forms
Imports DCS.Quote.Model
Imports DCS.Quote.Model.Template

Public Class frmNewQuote

    Public Property Header As Header
    Public Property QuoteInfo As New QuoteSaver.QuoteInfoClass

    Private Sub frmNewQuote_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim q As Header = ActiveHeader.ActiveHeader.Header
        Me.txtTemplateNumber.Text = q.PrimaryProperties.CommonID
        Header = q
        Me.txtPartNumber.Text = q.PrimaryProperties.CommonPartNumber
        Me.txtRFQ.Text = q.PrimaryProperties.CommonRequestForQuoteNumber
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        QuoteInfo.PartNumber = Me.txtPartNumber.Text
        QuoteInfo.RFQ = Me.txtRFQ.Text
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
