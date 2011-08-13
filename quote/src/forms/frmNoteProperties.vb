Imports WeifenLuo.WinFormsUI.Docking

Public Class frmNoteProperties
    Inherits DockContent

    Private WithEvents _ActiveQuote As ActiveHeader
    Private WithEvents _Notes As Common.NoteProperties

    Private Sub _frmForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _ActiveQuote = ActiveHeader.ActiveHeader
        If (_ActiveQuote.Header IsNot Nothing) Then
            _Notes = _ActiveQuote.Header.NoteProperties
        End If
        UpdateProperties()

        Dim t As New QuoteDataBase._QuotePropertiesDataTable
        Dim max = t.PropertyStringValueColumn.MaxLength
        Me.TextBox1.MaxLength = max
    End Sub

    Private Sub _ActiveQuote_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveQuote.PropertyChanged
        _ActiveQuote = ActiveHeader.ActiveHeader
        If (_ActiveQuote.Header IsNot Nothing) Then
            _Notes = _ActiveQuote.Header.NoteProperties
        End If
        UpdateProperties()
    End Sub

    Private Sub _Notes_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _Notes.PropertyChanged
        UpdateProperties()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If _Notes IsNot Nothing Then
            If _Notes.Note <> Me.TextBox1.Text Then
                _Notes.Note = Me.TextBox1.Text
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If _Notes IsNot Nothing Then
            If _Notes.Note <> Me.TextBox1.Text Then
                _Notes.Note = Me.TextBox1.Text
            End If
        End If
    End Sub

    Private Sub UpdateProperties()
        If _Notes IsNot Nothing Then
            Me.TextBox1.Text = _Notes.Note
        Else
            If (Me.TextBox1.Text.Length > 0) Then
                Me.TextBox1.Text = ""
            End If
        End If
    End Sub

End Class