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
        'TODO: This line of code loads data into the 'QuoteDataBase._Quote' table. You can move, or remove it, as needed.
        Me._QuoteTableAdapter.Fill(Me.QuoteDataBase._Quote)
        'SetupColoumns()
    End Sub

    Private Sub DataGridView1_CellMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        FillGrid()
    End Sub

    Private Sub FillGrid()
        Dim s As String = ""
        If Me.btnPartNumber.Checked Then
            If Me.txtPartNumber.Text.Length > 0 Then
                s = "PartNumber like '" & Me.txtPartNumber.Text & "'"
                Me.QuoteBindingSource.Filter = s
                Me._QuoteTableAdapter.FillOrderByPartNumber(Me.QuoteDataBase._Quote, True)
            End If
        End If
        If Me.btnRFQ.Checked Then
            If Me.txtRFQ.Text.Length > 0 Then
                s = "RequestForQuote like '" & Me.txtRFQ.Text & "'"
                Me.QuoteBindingSource.Filter = s
                Me._QuoteTableAdapter.FillOrderByRFQ(Me.QuoteDataBase._Quote, True)
            End If
        End If
        If Me.btnID.Checked Then
            If Me.txtID.Text.Length > 0 Then
                s = "ID like '" & Me.txtID.Text & "'"
                Me.QuoteBindingSource.Filter = s
                Me._QuoteTableAdapter.Fill(Me.QuoteDataBase._Quote)
            End If
        End If
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
        CustomerNameColumn.HeaderText = "Customer Name"
        CustomerNameColumn.Name = "CustomerNameDataGridViewTextBoxColumn"
        CustomerNameColumn.ReadOnly = True

        RFQColumn.DataPropertyName = "RequestForQuoteNumber"
        RFQColumn.HeaderText = "RFQ"
        RFQColumn.Name = "RequestForQuoteNumberDataGridViewTextBoxColumn"
        RFQColumn.ReadOnly = True

        PartNumberColumn.DataPropertyName = "PartNumber"
        PartNumberColumn.HeaderText = "Part Number"
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

End Class
