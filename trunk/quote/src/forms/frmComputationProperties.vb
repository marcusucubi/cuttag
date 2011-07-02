Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model

Public Class frmComputationProperties
    Inherits DockContent

    Private WithEvents _ActiveQuote As ActiveQuote
    Private WithEvents _QuoteProperties As ComputationProperties

    Private Sub _QuoteProperties_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _QuoteProperties.PropertyChanged
        Me.PropertyGrid1.Refresh()
    End Sub

    Private Sub frmProperties_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me._ActiveQuote = Nothing
        Me.PropertyGrid1.SelectedObject = Nothing
        _QuoteProperties = Nothing
    End Sub

    Private Sub _frmForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _ActiveQuote = ActiveQuote.ActiveQuote
        UpdateProperties()
    End Sub

    Private Sub UpdateProperties()
        If ActiveQuote.ActiveQuote.QuoteHeader IsNot Nothing Then
            Me.PropertyGrid1.SelectedObject = ActiveQuote.ActiveQuote.QuoteHeader.ComputationProperties
            _QuoteProperties = ActiveQuote.ActiveQuote.QuoteHeader.ComputationProperties
        Else
            Me.PropertyGrid1.SelectedObject = Nothing
            _QuoteProperties = Nothing
        End If
    End Sub

    Private Sub _ActiveQuote_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveQuote.PropertyChanged
        _ActiveQuote = ActiveQuote.ActiveQuote
        UpdateProperties()
    End Sub

End Class