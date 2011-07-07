Imports DCS.Quote.Model
Imports WeifenLuo.WinFormsUI.Docking

Public Class frmDetailProperties
    Inherits DockContent

    Private WithEvents _WireProperties As WireProperties
    Private WithEvents _ComponentProperties As ComponentProperties
    Private WithEvents _Active As ActiveQuoteDetail

    Private Sub _ActiveQuoteDetail_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _Active.PropertyChanged

        Dim detail As QuoteDetail = ActiveQuoteDetail.ActiveQuoteDetail.QuoteDetail
        If detail IsNot Nothing Then
            Dim o = detail.QuoteDetailProperties
            If TypeOf o Is WireProperties Then
                _WireProperties = detail.QuoteDetailProperties
                _ComponentProperties = Nothing
            End If
            If TypeOf o Is ComponentProperties Then
                _ComponentProperties = detail.QuoteDetailProperties
                _WireProperties = Nothing
            End If
        Else
            _ComponentProperties = Nothing
            _WireProperties = Nothing
        End If
        UpdateProperties()
    End Sub

    Private Sub frmProperties_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me._Active = Nothing
        Me._WireProperties = Nothing
        Me._ComponentProperties = Nothing
        Me.PropertyGrid1.SelectedObject = Nothing
    End Sub

    Private Sub _frmForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _Active = ActiveQuoteDetail.ActiveQuoteDetail
        UpdateProperties()
    End Sub

    Private Sub _ActiveDetail_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _WireProperties.PropertyChanged
        UpdateProperties()
    End Sub

    Private Sub UpdateProperties()
        If ActiveQuoteDetail.ActiveQuoteDetail.QuoteDetail IsNot Nothing Then

            Dim o = ActiveQuoteDetail.ActiveQuoteDetail.QuoteDetail.QuoteDetailProperties
            If TypeOf o Is WireProperties Then
                _WireProperties = o
                _ComponentProperties = Nothing
            End If
            If TypeOf o Is ComponentProperties Then
                _ComponentProperties = o
                _WireProperties = Nothing
            End If
            Me.PropertyGrid1.SelectedObject = o
        Else
            _ComponentProperties = Nothing
            _WireProperties = Nothing
            Me.PropertyGrid1.SelectedObject = Nothing
        End If
    End Sub

End Class