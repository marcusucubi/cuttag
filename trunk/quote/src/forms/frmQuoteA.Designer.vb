Imports DCS.Quote.Model

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmQuoteA
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent

    'Form overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.groupWires = New System.Windows.Forms.GroupBox()
        Me.lblTotalLength = New System.Windows.Forms.Label()
        Me.txtTotalLength = New System.Windows.Forms.TextBox()
        Me.HeaderSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.gridHeader = New System.Windows.Forms.DataGridView()
        Me.Fill = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CostDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gridDetail = New System.Windows.Forms.DataGridView()
        Me.DetailSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.panelButtons = New System.Windows.Forms.Panel()
        Me.btnAddWire = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fill3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProductClass = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProductCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UnitCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.groupWires.SuspendLayout()
        CType(Me.HeaderSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.gridHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DetailSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel2)
        Me.SplitContainer1.Size = New System.Drawing.Size(585, 327)
        Me.SplitContainer1.SplitterDistance = 115
        Me.SplitContainer1.TabIndex = 4
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.groupWires)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(585, 115)
        Me.Panel1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(218, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(190, 89)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Components"
        '
        'groupWires
        '
        Me.groupWires.Controls.Add(Me.lblTotalLength)
        Me.groupWires.Controls.Add(Me.txtTotalLength)
        Me.groupWires.Location = New System.Drawing.Point(12, 12)
        Me.groupWires.Name = "groupWires"
        Me.groupWires.Size = New System.Drawing.Size(200, 89)
        Me.groupWires.TabIndex = 0
        Me.groupWires.TabStop = False
        Me.groupWires.Text = "Wires"
        '
        'lblTotalLength
        '
        Me.lblTotalLength.AutoSize = True
        Me.lblTotalLength.Location = New System.Drawing.Point(8, 19)
        Me.lblTotalLength.Name = "lblTotalLength"
        Me.lblTotalLength.Size = New System.Drawing.Size(67, 13)
        Me.lblTotalLength.TabIndex = 1
        Me.lblTotalLength.Text = "Total Length"
        '
        'txtTotalLength
        '
        Me.txtTotalLength.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.HeaderSource, "TotalLength", True))
        Me.txtTotalLength.Location = New System.Drawing.Point(81, 16)
        Me.txtTotalLength.Name = "txtTotalLength"
        Me.txtTotalLength.ReadOnly = True
        Me.txtTotalLength.Size = New System.Drawing.Size(100, 20)
        Me.txtTotalLength.TabIndex = 0
        Me.txtTotalLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'HeaderSource
        '
        Me.HeaderSource.DataSource = GetType(DCS.Quote.Model.EditableQuoteHeader)
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.gridHeader)
        Me.Panel2.Controls.Add(Me.gridDetail)
        Me.Panel2.Controls.Add(Me.panelButtons)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(585, 208)
        Me.Panel2.TabIndex = 4
        '
        'gridHeader
        '
        Me.gridHeader.AllowUserToAddRows = False
        Me.gridHeader.AllowUserToDeleteRows = False
        Me.gridHeader.AutoGenerateColumns = False
        Me.gridHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridHeader.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Fill, Me.CostDataGridViewTextBoxColumn})
        Me.gridHeader.DataSource = Me.HeaderSource
        Me.gridHeader.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gridHeader.Location = New System.Drawing.Point(106, 156)
        Me.gridHeader.Name = "gridHeader"
        Me.gridHeader.ReadOnly = True
        Me.gridHeader.RowHeadersVisible = False
        Me.gridHeader.Size = New System.Drawing.Size(479, 52)
        Me.gridHeader.TabIndex = 6
        '
        'Fill
        '
        Me.Fill.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle7.Format = "C2"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.Fill.DefaultCellStyle = DataGridViewCellStyle7
        Me.Fill.HeaderText = ""
        Me.Fill.Name = "Fill"
        Me.Fill.ReadOnly = True
        '
        'CostDataGridViewTextBoxColumn
        '
        Me.CostDataGridViewTextBoxColumn.DataPropertyName = "Cost"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "C2"
        DataGridViewCellStyle8.NullValue = Nothing
        Me.CostDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle8
        Me.CostDataGridViewTextBoxColumn.HeaderText = "Total Cost"
        Me.CostDataGridViewTextBoxColumn.Name = "CostDataGridViewTextBoxColumn"
        Me.CostDataGridViewTextBoxColumn.ReadOnly = True
        '
        'gridDetail
        '
        Me.gridDetail.AllowUserToAddRows = False
        Me.gridDetail.AutoGenerateColumns = False
        Me.gridDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridDetail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Fill3, Me.ProductClass, Me.ProductCode, Me.UnitCost, Me.DataGridViewTextBoxColumn12, Me.TotalCost})
        Me.gridDetail.DataSource = Me.DetailSource
        Me.gridDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridDetail.Location = New System.Drawing.Point(106, 0)
        Me.gridDetail.Name = "gridDetail"
        Me.gridDetail.Size = New System.Drawing.Size(479, 208)
        Me.gridDetail.TabIndex = 5
        '
        'DetailSource
        '
        Me.DetailSource.DataSource = GetType(DCS.Quote.Model.EditableQuoteDetail)
        '
        'panelButtons
        '
        Me.panelButtons.Controls.Add(Me.btnAddWire)
        Me.panelButtons.Dock = System.Windows.Forms.DockStyle.Left
        Me.panelButtons.Location = New System.Drawing.Point(0, 0)
        Me.panelButtons.Name = "panelButtons"
        Me.panelButtons.Size = New System.Drawing.Size(106, 208)
        Me.panelButtons.TabIndex = 4
        '
        'btnAddWire
        '
        Me.btnAddWire.Location = New System.Drawing.Point(12, 12)
        Me.btnAddWire.Name = "btnAddWire"
        Me.btnAddWire.Size = New System.Drawing.Size(75, 23)
        Me.btnAddWire.TabIndex = 0
        Me.btnAddWire.Text = "Add Wire"
        Me.btnAddWire.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "ProductCode"
        Me.DataGridViewTextBoxColumn10.HeaderText = "ProductCode"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "ProductCode"
        Me.DataGridViewTextBoxColumn11.HeaderText = "ProductCode"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        '
        'Fill3
        '
        Me.Fill3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Fill3.HeaderText = ""
        Me.Fill3.Name = "Fill3"
        Me.Fill3.ReadOnly = True
        '
        'ProductClass
        '
        Me.ProductClass.DataPropertyName = "DisplayableProductClass"
        Me.ProductClass.HeaderText = "Type"
        Me.ProductClass.MinimumWidth = 50
        Me.ProductClass.Name = "ProductClass"
        Me.ProductClass.ReadOnly = True
        Me.ProductClass.Width = 50
        '
        'ProductCode
        '
        Me.ProductCode.DataPropertyName = "ProductCode"
        DataGridViewCellStyle9.Format = "C5"
        DataGridViewCellStyle9.NullValue = Nothing
        Me.ProductCode.DefaultCellStyle = DataGridViewCellStyle9
        Me.ProductCode.HeaderText = "ProductCode"
        Me.ProductCode.Name = "ProductCode"
        Me.ProductCode.ReadOnly = True
        '
        'UnitCost
        '
        Me.UnitCost.DataPropertyName = "UnitCost"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "C5"
        DataGridViewCellStyle10.NullValue = Nothing
        Me.UnitCost.DefaultCellStyle = DataGridViewCellStyle10
        Me.UnitCost.HeaderText = "Unit Cost"
        Me.UnitCost.Name = "UnitCost"
        Me.UnitCost.ReadOnly = True
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "Qty"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Format = "N0"
        DataGridViewCellStyle11.NullValue = Nothing
        Me.DataGridViewTextBoxColumn12.DefaultCellStyle = DataGridViewCellStyle11
        Me.DataGridViewTextBoxColumn12.HeaderText = "Qty"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        '
        'TotalCost
        '
        Me.TotalCost.DataPropertyName = "TotalCost"
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle12.Format = "C2"
        DataGridViewCellStyle12.NullValue = Nothing
        Me.TotalCost.DefaultCellStyle = DataGridViewCellStyle12
        Me.TotalCost.HeaderText = "Total Cost"
        Me.TotalCost.Name = "TotalCost"
        Me.TotalCost.ReadOnly = True
        '
        'frmQuoteA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(585, 327)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmQuoteA"
        Me.Text = "New Quote"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.groupWires.ResumeLayout(False)
        Me.groupWires.PerformLayout()
        CType(Me.HeaderSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.gridHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DetailSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents QtyDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QtyUnitDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TypeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TypeDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DetailSource As System.Windows.Forms.BindingSource
    Friend WithEvents HeaderSource As System.Windows.Forms.BindingSource
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents groupWires As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotalLength As System.Windows.Forms.Label
    Friend WithEvents txtTotalLength As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents gridHeader As System.Windows.Forms.DataGridView
    Friend WithEvents Fill As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CostDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents gridDetail As System.Windows.Forms.DataGridView
    Friend WithEvents panelButtons As System.Windows.Forms.Panel
    Friend WithEvents btnAddWire As System.Windows.Forms.Button
    Friend WithEvents Fill3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ProductClass As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ProductCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UnitCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TotalCost As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
