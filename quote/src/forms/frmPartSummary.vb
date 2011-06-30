Imports WeifenLuo.WinFormsUI.Docking

Public Class frmPartSummary
    Inherits DockContent

    Public Property frmQuoteA As frmQuoteA

    Private Sub frmQuoteHeader_Load(ByVal sender As Object, _
                                    ByVal e As System.EventArgs) _
                                Handles Me.Load
        If (frmQuoteA IsNot Nothing) Then
            Me.CtrParts1.QuoteHeader = frmQuoteA.QuoteHeader
        End If
    End Sub

End Class