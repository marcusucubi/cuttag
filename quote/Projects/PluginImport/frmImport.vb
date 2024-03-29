﻿Imports System.Windows.Forms

Public Class frmImport

    Public Property QuoteNumber As Integer
    Public Property ImportAll As Boolean
    Public Property ImportTest As Boolean

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Dim table As ImportDataSet.QuoteHeaderDataTable
        table = New ImportDataSetTableAdapters.QuoteHeaderTableAdapter().GetData()
        Me.ComboBox1.DataSource = table
        Me.ComboBox1.DisplayMember = "QuoteNumber"
        SelectProduct()
        EnableButtons()

        System.Windows.Forms.Cursor.Current = Cursors.Default

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        SelectProduct()
        EnableButtons()
    End Sub

    Private Sub SelectProduct()

        Dim View As System.Data.DataRowView = Me.ComboBox1.SelectedItem
        Dim row As ImportDataSet.QuoteHeaderRow = View.Row

        Me.QuoteNumber = row.QuoteNumber

    End Sub

    Private Sub ComboBox1_TextUpdate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.TextUpdate
        EnableButtons()
    End Sub

    Private Sub EnableButtons()

        Dim num As String = Me.ComboBox1.Text
        If num = "all" Then
            Me.OK_Button.Enabled = True
            ImportAll = True
            Return
        End If
        If num = "test" Then
            Me.OK_Button.Enabled = True
            ImportTest = True
            Return
        End If

        Dim table As ImportDataSet.QuoteHeaderDataTable
        table = New ImportDataSetTableAdapters.QuoteHeaderTableAdapter().GetDataByQuoteNumber(num)
        If table.Count > 0 Then
            Me.OK_Button.Enabled = True
        Else
            Me.OK_Button.Enabled = False
        End If

    End Sub

End Class
