Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model

Public Class frmSummary
    Inherits DockContent

    Private _QuoteHeader As QuoteHeader
    Public Property frmQuoteA As frmQuoteA

    Private Sub frmQuoteHeader_Load(ByVal sender As Object, _
                                    ByVal e As System.EventArgs) _
                                Handles Me.Load
        If (frmQuoteA IsNot Nothing) Then
            Me._QuoteHeader = frmQuoteA.QuoteHeader
            Me.PropertyGrid1.SelectedObject = Me._QuoteHeader.QuoteProperties
        End If
    End Sub

End Class