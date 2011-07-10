Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model
Imports DCS.Quote.Model.Quote
Imports System.ComponentModel

Public Class frmComputationProperties
    Inherits DockContent

    Private WithEvents _ActiveQuote As ActiveTemplate
    Private WithEvents _QuoteProperties As ComputationProperties

    Private Sub _QuoteProperties_PropertyChanged(ByVal sender As Object, _
                                                 ByVal e As PropertyChangedEventArgs) _
                                             Handles _QuoteProperties.PropertyChanged
        Me.PropertyGrid1.Refresh()
    End Sub

    Private Sub frmProperties_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me._ActiveQuote = Nothing
        Me.PropertyGrid1.SelectedObject = Nothing
        _QuoteProperties = Nothing
    End Sub

    Private Sub _frmForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _ActiveQuote = ActiveTemplate.ActiveTemplate
        UpdateProperties()
    End Sub

    Private Sub UpdateProperties()
        If ActiveTemplate.ActiveTemplate.QuoteHeader IsNot Nothing Then
            Me.PropertyGrid1.SelectedObject = ActiveTemplate.ActiveTemplate.QuoteHeader.ComputationProperties
            _QuoteProperties = ActiveTemplate.ActiveTemplate.QuoteHeader.ComputationProperties
        Else
            Me.PropertyGrid1.SelectedObject = Nothing
            _QuoteProperties = Nothing
        End If
    End Sub

    Private Sub _ActiveQuote_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveQuote.PropertyChanged
        _ActiveQuote = ActiveTemplate.ActiveTemplate
        UpdateProperties()
    End Sub

End Class