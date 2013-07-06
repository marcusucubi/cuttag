Imports System.Windows.Forms
Imports System.IO

Public Class frmExport

    Private _FilePath As String

    Private Sub frmExport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim path As String
        path = System.IO.Path.GetFullPath("Spreadsheets")

        Dim dirs() As String = Directory.GetFiles("Spreadsheets")
        For Each fullPath As String In dirs
            Dim lastPart As String = fullPath.Substring(fullPath.LastIndexOf("\") + 1)
            Me.ComboBox1.Items.Add(lastPart)
        Next
        Me.ComboBox1.SelectedIndex = 0

        Me._FilePath = path
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Export()
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub EnableButtons()
        Me.OK_Button.Enabled = True
    End Sub

    Private Sub Export()
        Dim export As New Export
        Dim file As String = _FilePath & "\" & Me.ComboBox1.Text
        export.Export(Model.ActiveHeader.ActiveHeader.Header, file)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim s As String = Directory.GetCurrentDirectory() + "\Spreadsheets"
        Process.Start("explorer.exe", s)
    End Sub

End Class
