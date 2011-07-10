Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model
Imports DCS.Quote.Model.Quote

Public Class frmPrimaryProperties
    Inherits DockContent

    Private WithEvents _ActiveQuote As ActiveTemplate
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
        _ActiveQuote = ActiveTemplate.ActiveTemplate
        UpdateProperties()
    End Sub

    Private Sub UpdateProperties()
        If ActiveTemplate.ActiveTemplate.QuoteHeader IsNot Nothing Then
            Me.PropertyGrid1.SelectedObject = ActiveTemplate.ActiveTemplate.QuoteHeader.PrimaryProperties
            _Properties = ActiveTemplate.ActiveTemplate.QuoteHeader.PrimaryProperties
        Else
            Me.PropertyGrid1.SelectedObject = Nothing
            _Properties = Nothing
        End If
    End Sub

    Private Sub _ActiveQuote_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveQuote.PropertyChanged
        _ActiveQuote = ActiveTemplate.ActiveTemplate
        UpdateProperties()
    End Sub

End Class