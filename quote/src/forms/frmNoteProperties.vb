Imports WeifenLuo.WinFormsUI.Docking

Public Class frmNoteProperties
    Inherits DockContent

    Private WithEvents _ActiveQuote As ActiveHeader

    Private Sub _frmForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _ActiveQuote = ActiveHeader.ActiveHeader
        UpdateProperties()
    End Sub

    Private Sub UpdateProperties()
        If ActiveHeader.ActiveHeader.Header IsNot Nothing Then
            Dim notes As Model.BOM.NoteProperties
            notes = ActiveHeader.ActiveHeader.Header.NoteProperties
            Me.TextBox1.Text = notes.Note
        Else
            Me.TextBox1.Text = ""
        End If
    End Sub

    Private Sub _ActiveQuote_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveQuote.PropertyChanged
        _ActiveQuote = ActiveHeader.ActiveHeader
        UpdateProperties()
    End Sub

End Class