Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model

Public Class frmPrimaryProperties
    Inherits DockContent

    Private WithEvents _ActiveQuote As ActiveQuote
    Private WithEvents _Properties As PrimaryPropeties

    Private Sub _PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _Properties.PropertyChanged
        Me.PropertyGrid1.Refresh()
    End Sub

    Private Sub _Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me._ActiveQuote = Nothing
        Me.PropertyGrid1.SelectedObject = Nothing
        _Properties = Nothing
    End Sub

    Private Sub _Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _ActiveQuote = ActiveQuote.ActiveQuote
        UpdateProperties()
    End Sub

    Private Sub UpdateProperties()
        If ActiveQuote.ActiveQuote.QuoteHeader IsNot Nothing Then
            Me.PropertyGrid1.SelectedObject = ActiveQuote.ActiveQuote.QuoteHeader.PrimaryProperties
            _Properties = ActiveQuote.ActiveQuote.QuoteHeader.PrimaryProperties
        Else
            Me.PropertyGrid1.SelectedObject = Nothing
            _Properties = Nothing
        End If
    End Sub

    Private Sub _ActiveQuote_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveQuote.PropertyChanged
        _ActiveQuote = ActiveQuote.ActiveQuote
        UpdateProperties()
    End Sub

End Class