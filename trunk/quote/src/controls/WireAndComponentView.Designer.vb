<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WireAndComponentView
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.dgvQuoteDetail = New DCS.Quote.LookupDataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvQuoteDetail_SequenceNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvQuoteDetail_Lookup = New DCS.Quote.DataGridViewSearchColumn()
        Me.dgvQuoteDetail_Quantity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvQuoteDetail_UnitCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UnitOfMeasure = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvQuoteDetail_MachineTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvQuoteDetail_Type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvQuoteDetail_TotalCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvQuoteDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(545, 64)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        Me.ListView1.Visible = False
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Part Number"
        Me.ColumnHeader1.Width = 154
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Type"
        Me.ColumnHeader2.Width = 85
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Quantity"
        Me.ColumnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Unit Cost"
        Me.ColumnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Total Cost"
        Me.ColumnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'dgvQuoteDetail
        '
        Me.dgvQuoteDetail.AllowUserToOrderColumns = True
        Me.dgvQuoteDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvQuoteDetail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvQuoteDetail_SequenceNumber, Me.dgvQuoteDetail_Lookup, Me.dgvQuoteDetail_Quantity, Me.dgvQuoteDetail_UnitCost, Me.UnitOfMeasure, Me.dgvQuoteDetail_MachineTime, Me.dgvQuoteDetail_Type, Me.dgvQuoteDetail_TotalCost})
        Me.dgvQuoteDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvQuoteDetail.Location = New System.Drawing.Point(0, 64)
        Me.dgvQuoteDetail.Name = "dgvQuoteDetail"
        Me.dgvQuoteDetail.Size = New System.Drawing.Size(545, 117)
        Me.dgvQuoteDetail.TabIndex = 3
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Lookup"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "ProductCode"
        Me.DataGridViewTextBoxColumn2.HeaderText = "PartNumber"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn2.Width = 154
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "DisplayableProductClass"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Type"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 85
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Qty"
        Me.DataGridViewTextBoxColumn4.HeaderText = "Quanty"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 60
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "UnitCost"
        Me.DataGridViewTextBoxColumn5.HeaderText = "Unit Cost"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Width = 75
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "TotalCost"
        Me.DataGridViewTextBoxColumn6.HeaderText = "Total Cost"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 80
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "QuoteDetailProperties"
        Me.DataGridViewTextBoxColumn7.HeaderText = "Empty"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Visible = False
        '
        'dgvQuoteDetail_SequenceNumber
        '
        Me.dgvQuoteDetail_SequenceNumber.DataPropertyName = "SequenceNumber"
        Me.dgvQuoteDetail_SequenceNumber.HeaderText = "Seq#"
        Me.dgvQuoteDetail_SequenceNumber.Name = "dgvQuoteDetail_SequenceNumber"
        Me.dgvQuoteDetail_SequenceNumber.Width = 40
        '
        'dgvQuoteDetail_Lookup
        '
        Me.dgvQuoteDetail_Lookup.DataPropertyName = "ProductCode"
        Me.dgvQuoteDetail_Lookup.HeaderText = "PartNumber"
        Me.dgvQuoteDetail_Lookup.Name = "dgvQuoteDetail_Lookup"
        Me.dgvQuoteDetail_Lookup.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvQuoteDetail_Lookup.SearchGrid = Nothing
        Me.dgvQuoteDetail_Lookup.ToolTipText = "F2, Click or Type for lookup"
        '
        'dgvQuoteDetail_Quantity
        '
        Me.dgvQuoteDetail_Quantity.DataPropertyName = "Qty"
        Me.dgvQuoteDetail_Quantity.HeaderText = "Quantity"
        Me.dgvQuoteDetail_Quantity.Name = "dgvQuoteDetail_Quantity"
        Me.dgvQuoteDetail_Quantity.Width = 60
        '
        'dgvQuoteDetail_UnitCost
        '
        Me.dgvQuoteDetail_UnitCost.DataPropertyName = "UnitCost"
        Me.dgvQuoteDetail_UnitCost.HeaderText = "Unit Cost"
        Me.dgvQuoteDetail_UnitCost.Name = "dgvQuoteDetail_UnitCost"
        '
        'UnitOfMeasure
        '
        Me.UnitOfMeasure.DataPropertyName = "UOM"
        Me.UnitOfMeasure.HeaderText = "UOM"
        Me.UnitOfMeasure.Name = "UnitOfMeasure"
        Me.UnitOfMeasure.ReadOnly = True
        Me.UnitOfMeasure.Width = 60
        '
        'dgvQuoteDetail_MachineTime
        '
        Me.dgvQuoteDetail_MachineTime.DataPropertyName = "MachineTime"
        Me.dgvQuoteDetail_MachineTime.HeaderText = "Time"
        Me.dgvQuoteDetail_MachineTime.Name = "dgvQuoteDetail_MachineTime"
        Me.dgvQuoteDetail_MachineTime.Width = 50
        '
        'dgvQuoteDetail_Type
        '
        Me.dgvQuoteDetail_Type.DataPropertyName = "DisplayableProductClass"
        Me.dgvQuoteDetail_Type.HeaderText = "Type"
        Me.dgvQuoteDetail_Type.Name = "dgvQuoteDetail_Type"
        '
        'dgvQuoteDetail_TotalCost
        '
        Me.dgvQuoteDetail_TotalCost.DataPropertyName = "TotalCost"
        DataGridViewCellStyle1.Format = "N2"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.dgvQuoteDetail_TotalCost.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvQuoteDetail_TotalCost.HeaderText = "Total Cost"
        Me.dgvQuoteDetail_TotalCost.Name = "dgvQuoteDetail_TotalCost"
        '
        'WireAndComponentView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.dgvQuoteDetail)
        Me.Controls.Add(Me.ListView1)
        Me.Name = "WireAndComponentView"
        Me.Size = New System.Drawing.Size(545, 181)
        CType(Me.dgvQuoteDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
	Friend WithEvents ListView1 As System.Windows.Forms.ListView
	Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
	Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
	Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
	Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
	Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
	Friend WithEvents dgvQuoteDetail As DCS.Quote.LookupDataGridView
	Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvQuoteDetail_SequenceNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvQuoteDetail_Lookup As DCS.Quote.DataGridViewSearchColumn
    Friend WithEvents dgvQuoteDetail_Quantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvQuoteDetail_UnitCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UnitOfMeasure As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvQuoteDetail_MachineTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvQuoteDetail_Type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvQuoteDetail_TotalCost As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
