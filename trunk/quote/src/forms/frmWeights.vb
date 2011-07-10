Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model
Imports DCS.Quote.Model.Quote

Public Class frmWeights
    Inherits DockContent

    Private WithEvents _ActiveQuote As ActiveHeader
    Private WithEvents _GageProperties As Common.Weights

    Private Sub _QuoteProperties_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _GageProperties.PropertyChanged
        Me.PropertyGrid1.Refresh()
    End Sub

    Private Sub frmProperties_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me._ActiveQuote = Nothing
        Me.PropertyGrid1.SelectedObject = Nothing
        _GageProperties = Nothing
    End Sub

    Private Sub _frmForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _ActiveQuote = ActiveHeader.ActiveHeader
        UpdateProperties()
    End Sub

    Private Sub UpdateProperties()
        If ActiveHeader.ActiveHeader.Header IsNot Nothing Then
            Me.PropertyGrid1.SelectedObject = ActiveHeader.ActiveHeader.Header.WeightProperties
            _GageProperties = ActiveHeader.ActiveHeader.Header.WeightProperties
        Else
            Me.PropertyGrid1.SelectedObject = Nothing
            _GageProperties = Nothing
        End If
    End Sub

    Private Sub _ActiveQuote_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveQuote.PropertyChanged
        _ActiveQuote = ActiveHeader.ActiveHeader
        UpdateProperties()
    End Sub

End Class