Imports DCS.Quote.Model
Imports WeifenLuo.WinFormsUI.Docking

Public Class frmDetailProperties
    Inherits DockContent

    Private WithEvents _ActiveQuoteDetail As ActiveQuoteDetail
    Private WithEvents _ActiveDetail As QuoteDetail

    Private Sub _ActiveQuoteDetail_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveQuoteDetail.PropertyChanged
        _ActiveQuoteDetail = ActiveQuoteDetail.ActiveQuoteDetail
        UpdateProperties()
    End Sub

    Private Sub frmProperties_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me._ActiveQuoteDetail = Nothing
        Me.PropertyGrid1.SelectedObject = Nothing
        Me._ActiveDetail = Nothing
    End Sub

    Private Sub _frmForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _ActiveQuoteDetail = ActiveQuoteDetail.ActiveQuoteDetail
        _ActiveDetail = ActiveQuoteDetail.ActiveQuoteDetail.QuoteDetail
        UpdateProperties()
    End Sub

    Private Sub _ActiveDetail_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveDetail.PropertyChanged
        UpdateProperties()
    End Sub

    Private Sub UpdateProperties()
        If ActiveQuoteDetail.ActiveQuoteDetail.QuoteDetail IsNot Nothing Then
            Me.PropertyGrid1.SelectedObject = ActiveQuoteDetail.ActiveQuoteDetail.QuoteDetail
        Else
            Me.PropertyGrid1.SelectedObject = Nothing
        End If
    End Sub

End Class