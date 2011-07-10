Imports DCS.Quote.Model
Imports DCS.Quote.Model.Quote

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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.panelButtons = New System.Windows.Forms.Panel()
        Me.bntAddWire = New System.Windows.Forms.Button()
        Me.btnAddComponent = New System.Windows.Forms.Button()
        Me.gridDetail = New System.Windows.Forms.DataGridView()
        Me.Fill3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DisplayableProductClass = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProductCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UnitCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DetailSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.HeaderSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.panelButtons.SuspendLayout()
        CType(Me.gridDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DetailSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HeaderSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.SystemColors.Control
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel2)
        Me.SplitContainer1.Size = New System.Drawing.Size(362, 277)
        Me.SplitContainer1.SplitterDistance = 139
        Me.SplitContainer1.TabIndex = 4
        '
        'Panel2
        '
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(358, 130)
        Me.Panel2.TabIndex = 4
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
        'panelButtons
        '
        Me.panelButtons.Controls.Add(Me.bntAddWire)
        Me.panelButtons.Controls.Add(Me.btnAddComponent)
        Me.panelButtons.Dock = System.Windows.Forms.DockStyle.Left
        Me.panelButtons.Location = New System.Drawing.Point(0, 0)
        Me.panelButtons.Name = "panelButtons"
        Me.panelButtons.Size = New System.Drawing.Size(106, 218)
        Me.panelButtons.TabIndex = 5
        '
        'bntAddWire
        '
        Me.bntAddWire.Location = New System.Drawing.Point(12, 12)
        Me.bntAddWire.Name = "bntAddWire"
        Me.bntAddWire.Size = New System.Drawing.Size(75, 23)
        Me.bntAddWire.TabIndex = 1
        Me.bntAddWire.Text = "Add Wire"
        Me.bntAddWire.UseVisualStyleBackColor = True
        '
        'btnAddComponent
        '
        Me.btnAddComponent.Location = New System.Drawing.Point(12, 41)
        Me.btnAddComponent.Name = "btnAddComponent"
        Me.btnAddComponent.Size = New System.Drawing.Size(75, 38)
        Me.btnAddComponent.TabIndex = 0
        Me.btnAddComponent.Text = "Add Component"
        Me.btnAddComponent.UseVisualStyleBackColor = True
        '
        'gridDetail
        '
        Me.gridDetail.AllowUserToAddRows = False
        Me.gridDetail.AllowUserToOrderColumns = True
        Me.gridDetail.AutoGenerateColumns = False
        Me.gridDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridDetail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Fill3, Me.DisplayableProductClass, Me.ProductCode, Me.UnitCost, Me.DataGridViewTextBoxColumn12, Me.TotalCost})
        Me.gridDetail.DataSource = Me.DetailSource
        Me.gridDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridDetail.Location = New System.Drawing.Point(106, 0)
        Me.gridDetail.MultiSelect = False
        Me.gridDetail.Name = "gridDetail"
        Me.gridDetail.Size = New System.Drawing.Size(535, 218)
        Me.gridDetail.TabIndex = 8
        '
        'Fill3
        '
        Me.Fill3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Fill3.HeaderText = ""
        Me.Fill3.Name = "Fill3"
        Me.Fill3.ReadOnly = True
        '
        'DisplayableProductClass
        '
        Me.DisplayableProductClass.DataPropertyName = "DisplayableProductClass"
        Me.DisplayableProductClass.HeaderText = "Type"
        Me.DisplayableProductClass.Name = "DisplayableProductClass"
        Me.DisplayableProductClass.ReadOnly = True
        '
        'ProductCode
        '
        Me.ProductCode.DataPropertyName = "ProductCode"
        DataGridViewCellStyle1.Format = "C5"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.ProductCode.DefaultCellStyle = DataGridViewCellStyle1
        Me.ProductCode.HeaderText = "Product Code"
        Me.ProductCode.Name = "ProductCode"
        Me.ProductCode.ReadOnly = True
        '
        'UnitCost
        '
        Me.UnitCost.DataPropertyName = "UnitCost"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "C2"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.UnitCost.DefaultCellStyle = DataGridViewCellStyle2
        Me.UnitCost.HeaderText = "Unit Cost"
        Me.UnitCost.Name = "UnitCost"
        Me.UnitCost.ReadOnly = True
        Me.UnitCost.Width = 70
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "Qty"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.DataGridViewTextBoxColumn12.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn12.HeaderText = "Qty"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.Width = 60
        '
        'TotalCost
        '
        Me.TotalCost.DataPropertyName = "TotalCost"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "C2"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.TotalCost.DefaultCellStyle = DataGridViewCellStyle4
        Me.TotalCost.HeaderText = "Total Cost"
        Me.TotalCost.Name = "TotalCost"
        Me.TotalCost.ReadOnly = True
        '
        'DetailSource
        '
        Me.DetailSource.DataSource = GetType(Detail)
        '
        'HeaderSource
        '
        Me.HeaderSource.DataSource = GetType(Model.Quote.Header)
        '
        'frmQuoteA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(641, 218)
        Me.Controls.Add(Me.gridDetail)
        Me.Controls.Add(Me.panelButtons)
        Me.Controls.Add(Me.SplitContainer1)
        Me.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmQuoteA"
        Me.Text = "New Template"
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.panelButtons.ResumeLayout(False)
        CType(Me.gridDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DetailSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HeaderSource, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents panelButtons As System.Windows.Forms.Panel
    Friend WithEvents bntAddWire As System.Windows.Forms.Button
    Friend WithEvents btnAddComponent As System.Windows.Forms.Button
    Friend WithEvents CostDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents gridDetail As System.Windows.Forms.DataGridView
    Friend WithEvents Fill3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DisplayableProductClass As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ProductCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UnitCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TotalCost As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
