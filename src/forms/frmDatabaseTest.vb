Imports System.Data.SqlClient

Public Class frmDatabaseTest

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Try
            Dim Connection As New SqlConnection(Me.TextBox1.Text)
            Connection.Open()
            Connection.Close()
            MsgBox("Success")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

End Class