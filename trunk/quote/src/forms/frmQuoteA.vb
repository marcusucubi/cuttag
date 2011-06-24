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

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Dim result As DialogResult = frmPartLookup.ShowDialog(Me)
        If result = DialogResult.OK Then
            Dim detail As EditableQuoteDetail
            detail = m_QuoteHeader.NewEditableQuoteDetail
            detail.Product = frmPartLookup.Product

            Me.DetailSource.Add(detail)
            Me.DetailSource.MoveNext()
        End If

    End Sub

    Private Sub gridDetail_UserDeletedRow(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) _
        Handles gridDetail.UserDeletedRow
        Me.m_QuoteHeader.Remove(e.Row.DataBoundItem)
    End Sub

    Private Sub frmQuoteA_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Console.WriteLine("frmQuoteA_FormClosing")
    End Sub

End Class