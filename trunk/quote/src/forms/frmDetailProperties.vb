Imports DCS.Quote.Model
Imports WeifenLuo.WinFormsUI.Docking

Public Class frmDetailProperties
    Inherits DockContent

    Private WithEvents _Properties As QuoteDetailProperties
    Private WithEvents _Active As ActiveQuoteDetail

    Private Sub _ActiveQuoteDetail_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _Active.PropertyChanged
        _Properties = ActiveQuoteDetail.ActiveQuoteDetail.QuoteDetail.QuoteDetailProperties
        UpdateProperties()
    End Sub

    Private Sub frmProperties_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me._Active = Nothing
        Me._Properties = Nothing
        Me.PropertyGrid1.SelectedObject = Nothing
    End Sub

    Private Sub _frmForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _Active = ActiveQuoteDetail.ActiveQuoteDetail
        '_Properties = ActiveQuoteDetail.ActiveQuoteDetail.QuoteDetail.QuoteDetailProperties
        UpdateProperties()
    End Sub

    Private Sub _ActiveDetail_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _Properties.PropertyChanged
        UpdateProperties()
    End Sub

    Private Sub UpdateProperties()
        If ActiveQuoteDetail.ActiveQuoteDetail.QuoteDetail IsNot Nothing Then
            Me.PropertyGrid1.SelectedObject = ActiveQuoteDetail.ActiveQuoteDetail.QuoteDetail.QuoteDetailProperties
        Else
            Me.PropertyGrid1.SelectedObject = Nothing
        End If
    End Sub

End Class