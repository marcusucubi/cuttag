Imports VB = Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data.Common
Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model

Public Class frmQuoteA
    Inherits DockContent

    Private m_QuoteHeader As New EditableQuoteHeader
    Private m_Detail As QuoteDetail

    Public Sub New()
        InitializeComponent()
        Me.HeaderSource.Add(m_QuoteHeader)
    End Sub

    Private Sub gridDetail_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs)
        m_QuoteHeader.Remove(m_Detail)
    End Sub

    Private Sub gridDetail_RowStateChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowStateChangedEventArgs)
        m_Detail = e.Row.DataBoundItem
    End Sub

    Private Sub btnAddWire_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddWire.Click

        Dim result As DialogResult = frmPartLookup.ShowDialog(Me)
        If result = DialogResult.OK Then
            Dim detail As EditableQuoteDetail
            detail = m_QuoteHeader.NewEditableQuoteDetail
            detail.Product = frmPartLookup.Product

            Me.DetailSource.Add(detail)
            Me.DetailSource.MoveNext()
        End If

    End Sub
End Class
