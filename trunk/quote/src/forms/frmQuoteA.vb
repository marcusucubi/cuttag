Imports VB = Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data.Common
Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model
Imports System.ComponentModel

Public Class frmQuoteA
    Inherits DockContent

    Private m_QuoteHeader As New EditableQuoteHeader

    Public Sub New()
        InitializeComponent()
        Me.HeaderSource.Add(m_QuoteHeader)
        Me.gridDetail.DataSource = m_QuoteHeader.QuoteDetails
    End Sub

    Private Sub gridDetail_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles gridDetail.ColumnHeaderMouseClick
        Dim c As DataGridViewColumn = Me.gridDetail.Columns(e.ColumnIndex)
        Dim name As String = c.DataPropertyName
        Me.m_QuoteHeader.QuoteDetails.Sort = name
    End Sub

    Private Sub btnAddWire_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddComponent.Click

        Dim result As DialogResult = frmComponentLookup.ShowDialog(Me)

        If result = DialogResult.OK Then
            Dim detail As EditableQuoteDetail
            detail = m_QuoteHeader.NewQuoteDetail(frmComponentLookup.Product)

            Me.DetailSource.Add(detail)
            Me.DetailSource.MoveNext()
        End If
    End Sub

    Private Sub bntAddWire_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntAddWire.Click
        Dim result As DialogResult = frmWireLookup.ShowDialog(Me)
        If result = DialogResult.OK Then
            Dim detail As EditableQuoteDetail
            detail = m_QuoteHeader.NewQuoteDetail(frmWireLookup.Product)

            Me.DetailSource.Add(detail)
            Me.DetailSource.MoveNext()
        End If
    End Sub

End Class
