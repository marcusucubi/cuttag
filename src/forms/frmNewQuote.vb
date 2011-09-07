Imports System.Windows.Forms
Imports DCS.Quote.Model
Imports DCS.Quote.Model.BOM

Public Class frmNewQuote

    Public Property Header As Header
    Public Property QuoteInfo As New QuoteSaver.QuoteInfoClass

    Private Sub frmNewQuote_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim q As Header = ActiveHeader.ActiveHeader.Header
        Me.txtTemplateNumber.Text = q.PrimaryProperties.CommonID
        Header = q
        Me.txtPartNumber.Text = q.PrimaryProperties.CommonPartNumber
        Me.txtRFQ.Text = q.PrimaryProperties.CommonRequestForQuoteNumber
        EnableButtons()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        QuoteInfo.PartNumber = Me.txtPartNumber.Text
        QuoteInfo.RFQ = Me.txtRFQ.Text
        QuoteInfo.Initials = Me.txtInitials.Text
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub txtInitials_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInitials.TextChanged
        EnableButtons()
    End Sub

    Private Sub EnableButtons()
        If Me.txtInitials.Text.Length > 0 Then
            Me.OK_Button.Enabled = True
        Else
            Me.OK_Button.Enabled = False
        End If
    End Sub

End Class
