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
        Me._QuoteTableAdapter.FillWithQuotes(Me.QuoteDataBase._Quote)
        SetupColoumns()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        FillGrid()
    End Sub

    Private Sub txtPartNumber_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPartNumber.GotFocus
        Me.btnPartNumber.Checked = True
    End Sub

    Private Sub txtRFQ_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRFQ.GotFocus
        Me.btnRFQ.Checked = True
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        If e.RowIndex < 0 OrElse Not e.ColumnIndex = _
                    DataGridView1.Columns("OpenColumn").Index Then
            Return
        End If

        Dim id As Int32 = CInt(DataGridView1(1, e.RowIndex).Value)

        Me.Cursor = Cursors.WaitCursor
        My.Application.DoEvents()
        frmMain.frmMain.LoadQuote(id)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub FillGrid()
        Dim s As String = ""
        Me.QuoteBindingSource.Filter = ""
        If Me.btnPartNumber.Checked Then
            If Me.txtPartNumber.Text.Length > 0 Then
                s = "PartNumber like '" & Me.txtPartNumber.Text & "%'"
                Me.QuoteBindingSource.Filter = s
                Me.QuoteBindingSource.Sort = "PartNumber"
            End If
        End If
        If Me.btnRFQ.Checked Then
            If Me.txtRFQ.Text.Length > 0 Then
                s = "RequestForQuoteNumber like '" & Me.txtRFQ.Text & "%'"
                Me.QuoteBindingSource.Filter = s
                Me.QuoteBindingSource.Sort = "RequestForQuoteNumber"
            End If
        End If
    End Sub

    Private Sub SetupColoumns()

        Dim IDColumn As New DataGridViewTextBoxColumn
        Dim CustomerNameColumn As New DataGridViewTextBoxColumn
        Dim RFQColumn As New DataGridViewTextBoxColumn
        Dim PartNumberColumn As New DataGridViewTextBoxColumn
        Dim InitialsColumn As New DataGridViewTextBoxColumn
        Dim CreatedDateColumn As New DataGridViewTextBoxColumn
        Dim OpenColumn As New DataGridViewButtonColumn()

        IDColumn.DataPropertyName = "ID"
        IDColumn.HeaderText = "Quote Number"
        IDColumn.Name = "IDDataGridViewTextBoxColumn"
        IDColumn.ReadOnly = True
        IDColumn.Width = 60

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

        OpenColumn.HeaderText = "Open"
        OpenColumn.Name = "OpenColumn"
        OpenColumn.UseColumnTextForButtonValue = True
        OpenColumn.Width = 40

        InitialsColumn.DataPropertyName = "Initials"
        InitialsColumn.HeaderText = "Initials"
        InitialsColumn.Name = "InitialsDataGridViewTextBoxColumn"
        InitialsColumn.ReadOnly = True

        CreatedDateColumn.DataPropertyName = "CreatedDate"
        CreatedDateColumn.HeaderText = "CreatedDate"
        CreatedDateColumn.Name = "CreatedDateDataGridViewTextBoxColumn"
        CreatedDateColumn.ReadOnly = True

        Me.DataGridView1.Columns.Clear()
        Me.DataGridView1.Columns.AddRange( _
            New System.Windows.Forms.DataGridViewColumn() { _
                OpenColumn, IDColumn, _
                RFQColumn, _
                PartNumberColumn, _
                CreatedDateColumn, _
                InitialsColumn, _
                CustomerNameColumn
            })
    End Sub

End Class
