Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model

Public Class frmProperties
    Inherits DockContent

    'Private WithEvents _frmMain As frmMain
    Private WithEvents _ActiveQuote As ActiveQuote
    Private WithEvents _QuoteProperties As QuoteProperties

    Private Sub _QuoteProperties_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _QuoteProperties.PropertyChanged
        Me.PropertyGrid1.Refresh()
    End Sub

    Private Sub _frmForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '_frmMain = frmMain.frmMain
        _ActiveQuote = ActiveQuote.ActiveQuote
        UpdateProperties()
    End Sub

    '    Private Sub _frmMain_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _frmMain.PropertyChanged
    '        UpdateProperties()
    '    End Sub

    Private Sub UpdateProperties()
        If ActiveQuote.ActiveQuote.QuoteHeader IsNot Nothing Then
            Me.PropertyGrid1.SelectedObject = _
                ActiveQuote.ActiveQuote.QuoteHeader.QuoteProperties
            _QuoteProperties = _
                ActiveQuote.ActiveQuote.QuoteHeader.QuoteProperties
        End If
    End Sub

End Class