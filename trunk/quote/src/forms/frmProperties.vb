Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model

Public Class frmProperties
    Inherits DockContent

    Private _QuoteHeader As QuoteHeader
    Private WithEvents _QuoteProperties As QuoteProperties

    Public Property frmQuoteA As frmQuoteA

    Private Sub frmQuoteHeader_Load(ByVal sender As Object, _
                                    ByVal e As System.EventArgs) _
                                Handles Me.Load
        If (frmQuoteA IsNot Nothing) Then
            Me._QuoteHeader = frmQuoteA.QuoteHeader
            Me._QuoteProperties = Me._QuoteHeader.QuoteProperties
            Me.PropertyGrid1.SelectedObject = _QuoteProperties
        End If
    End Sub

    Private Sub _QuoteProperties_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _QuoteProperties.PropertyChanged
        Me.PropertyGrid1.Refresh()
    End Sub

End Class