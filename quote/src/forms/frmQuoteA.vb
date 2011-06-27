Imports VB = Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data.Common
Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model

Public Class frmQuoteA
    Inherits DockContent

    Private m_QuoteHeader As New EditableQuoteHeader

    Public Sub New()
        InitializeComponent()
        Me.HeaderSource.Add(m_QuoteHeader)
    End Sub

    Private Sub gridDetail_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles gridDetail.UserDeletingRow
        Dim detail As QuoteDetail = e.Row.DataBoundItem
        Me.m_QuoteHeader.Remove(detail)
    End Sub

    Private Sub btnAddWire_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddComponent.Click
        Dim result As DialogResult = frmPartLookup.ShowDialog(Me)
        If result = DialogResult.OK Then
            Dim detail As EditableQuoteDetail
            detail = m_QuoteHeader.NewQuoteDetail
            detail.Product = frmPartLookup.Product

            Me.DetailSource.Add(detail)
            Me.DetailSource.MoveNext()
        End If
    End Sub

End Class
