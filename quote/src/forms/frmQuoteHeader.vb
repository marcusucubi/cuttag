﻿Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model

Public Class frmQuoteHeader
    Inherits DockContent

    Public Property frmQuoteA As frmQuoteA

    Private Sub frmQuoteHeader_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (frmQuoteA IsNot Nothing) Then
            Me.CtrWires31.QuoteHeader = frmQuoteA.QuoteHeader
            Me.CtrParts1.QuoteHeader = frmQuoteA.QuoteHeader
            Me.CtrPartsAndWires1.QuoteHeader = frmQuoteA.QuoteHeader
        End If
    End Sub

End Class