Imports DCS.Quote.Model
Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model.Quote

Public Class frmOtherProperties
    Inherits DockContent

    Private WithEvents _ActiveQuote As ActiveTemplate
    Private WithEvents _NonQuoteProperties As OtherProperties

    Private Sub _QuoteProperties_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _NonQuoteProperties.PropertyChanged
        Me.PropertyGrid1.Refresh()
    End Sub

    Private Sub frmProperties_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me._ActiveQuote = Nothing
        Me.PropertyGrid1.SelectedObject = Nothing
        _NonQuoteProperties = Nothing
    End Sub

    Private Sub _frmForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _ActiveQuote = ActiveTemplate.ActiveTemplate
        UpdateProperties()
    End Sub

    Private Sub UpdateProperties()
        If ActiveTemplate.ActiveTemplate.QuoteHeader IsNot Nothing Then
            Me.PropertyGrid1.SelectedObject = ActiveTemplate.ActiveTemplate.QuoteHeader.NonComputationProperties
            _NonQuoteProperties = ActiveTemplate.ActiveTemplate.QuoteHeader.NonComputationProperties
        Else
            Me.PropertyGrid1.SelectedObject = Nothing
            _NonQuoteProperties = Nothing
        End If
    End Sub

    Private Sub _ActiveQuote_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveQuote.PropertyChanged
        _ActiveQuote = ActiveTemplate.ActiveTemplate
        UpdateProperties()
    End Sub

End Class