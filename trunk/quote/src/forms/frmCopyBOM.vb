Imports System.Windows.Forms

Public Class frmCopyBOM

    Public Property Initials As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Initials = Me.txtInitials.Text
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub txtInitials_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInitials.TextChanged
        If Me.txtInitials.Text.Length > 0 Then
            Me.OK_Button.Enabled = True
        Else
            Me.OK_Button.Enabled = False
        End If
    End Sub

End Class
