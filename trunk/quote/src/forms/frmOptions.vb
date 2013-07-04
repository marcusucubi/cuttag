Imports System.Windows.Forms

Imports Model.Common

Public Class frmOptions

    Private Sub frmOptions_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        _DecimalPointsComboBox.Text = GlobalOptions.DecimalPointsToDisplay
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK

        GlobalOptions.DecimalPointsToDisplay = _DecimalPointsComboBox.Text

        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
