Imports Model
Imports Model.Common

Imports WeifenLuo.WinFormsUI.Docking

Public Class frmDetailProperties
    Inherits DockContent

    Private WithEvents _WireProperties As Template.DisplayableWireProperties
    Private WithEvents _ComponentProperties As Template.ComponentProperties
    Private WithEvents _Active As ActiveDetail

    Private Sub _ActiveQuoteDetail_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _Active.PropertyChanged

        Dim detail As Detail
        detail = ActiveDetail.ActiveDetail.Detail
        If detail IsNot Nothing Then
            Dim o = detail.QuoteDetailProperties
            If TypeOf o Is Template.WireProperties Then
                _WireProperties = detail.QuoteDetailProperties
                _ComponentProperties = Nothing
            End If
            If TypeOf o Is Template.DisplayableComponentProperties Then
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
    End Sub

    Private Sub _frmForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _Active = ActiveDetail.ActiveDetail
        UpdateProperties()
    End Sub

    Private Sub _ActiveDetail_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _WireProperties.PropertyChanged
        UpdateProperties()
    End Sub

    Private Sub _ComponentProperties_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles _ComponentProperties.PropertyChanged
        UpdateProperties()
    End Sub

    Private Sub UpdateProperties()
        If ActiveDetail.ActiveDetail.Detail IsNot Nothing Then

            Dim o = ActiveDetail.ActiveDetail.Detail.QuoteDetailProperties
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