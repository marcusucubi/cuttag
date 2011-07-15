Imports System.Windows.Forms

Public Class frmQuoteSearch

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmQuoteSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetupColoumns()
    End Sub

    Private Sub DataGridView1_CellMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        Me.Close()
    End Sub

    Private Sub btnPartNumber_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPartNumber.CheckedChanged

    End Sub

    Private Sub FillGrid()
        Dim s As String = ""
        If Me.btnPartNumber.Checked Then
            If Me.txtPartNumber.Text.Length > 0 Then
                s = "PartNumber like " & Me.txtPartNumber.Text & "%"
            End If
        End If
        If Me.btnRFQ.Checked Then
            If Me.txtRFQ.Text.Length > 0 Then
                s = "RequestForQuote like " & Me.txtPartNumber.Text & "%"
            End If
        End If
        If Me.btnID.Checked Then
            If Me.txtID.Text.Length > 0 Then
                s = "ID like " & Me.txtPartNumber.Text & "%"
            End If
        End If
        Me.QuoteBindingSource.Filter = s
        Me._QuoteTableAdapter.Fill(Me.QuoteDataBase._Quote)
        'Me.DataGridView1.DataSource = Me._QuoteTableAdapter
    End Sub

    Private Sub SetupColoumns()

        Dim IDColumn As New DataGridViewTextBoxColumn
        Dim CustomerNameColumn As New DataGridViewTextBoxColumn
        Dim RFQColumn As New DataGridViewTextBoxColumn
        Dim PartNumberColumn As New DataGridViewTextBoxColumn

        IDColumn.DataPropertyName = "ID"
        IDColumn.HeaderText = "ID"
        IDColumn.Name = "IDDataGridViewTextBoxColumn"
        IDColumn.ReadOnly = True

        CustomerNameColumn.DataPropertyName = "CustomerName"
        CustomerNameColumn.HeaderText = "CustomerName"
        CustomerNameColumn.Name = "CustomerNameDataGridViewTextBoxColumn"
        CustomerNameColumn.ReadOnly = True

        RFQColumn.DataPropertyName = "RequestForQuoteNumber"
        RFQColumn.HeaderText = "RequestForQuoteNumber"
        RFQColumn.Name = "RequestForQuoteNumberDataGridViewTextBoxColumn"
        RFQColumn.ReadOnly = True

        PartNumberColumn.DataPropertyName = "PartNumber"
        PartNumberColumn.HeaderText = "PartNumber"
        PartNumberColumn.Name = "PartNumberDataGridViewTextBoxColumn"
        PartNumberColumn.ReadOnly = True

        Me.DataGridView1.Columns.AddRange( _
            New System.Windows.Forms.DataGridViewColumn() { _
                IDColumn, _
                CustomerNameColumn, _
                RFQColumn, _
                PartNumberColumn
            })
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        FillGrid()
    End Sub

End Class
