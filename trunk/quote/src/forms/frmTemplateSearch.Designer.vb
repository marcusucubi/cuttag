<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTemplateSearch
    Inherits System.Windows.Forms.Form

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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtRFQ = New System.Windows.Forms.TextBox()
        Me.btnRFQ = New System.Windows.Forms.RadioButton()
        Me.btnPartNumber = New System.Windows.Forms.RadioButton()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtPartNumber = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.QuoteBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.QuoteDataBase = New DCS.Quote.QuoteDataBase()
        Me._QuoteTableAdapter = New DCS.Quote.QuoteDataBaseTableAdapters._QuoteTableAdapter()
        Me.IDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CustomerNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RequestForQuoteNumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PartNumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CreatedByDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IsQuoteDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.TemplateIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InitialsDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CreatedDateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LastModifedDateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.QuoteBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.QuoteDataBase, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRFQ)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnRFQ)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnPartNumber)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPartNumber)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridView1)
        Me.SplitContainer1.Size = New System.Drawing.Size(603, 262)
        Me.SplitContainer1.SplitterDistance = 67
        Me.SplitContainer1.TabIndex = 0
        '
        'txtRFQ
        '
        Me.txtRFQ.Location = New System.Drawing.Point(100, 37)
        Me.txtRFQ.Name = "txtRFQ"
        Me.txtRFQ.Size = New System.Drawing.Size(100, 20)
        Me.txtRFQ.TabIndex = 10
        '
        'btnRFQ
        '
        Me.btnRFQ.AutoSize = True
        Me.btnRFQ.Location = New System.Drawing.Point(10, 38)
        Me.btnRFQ.Name = "btnRFQ"
        Me.btnRFQ.Size = New System.Drawing.Size(47, 17)
        Me.btnRFQ.TabIndex = 9
        Me.btnRFQ.Text = "RFQ"
        Me.btnRFQ.UseVisualStyleBackColor = True
        '
        'btnPartNumber
        '
        Me.btnPartNumber.AutoSize = True
        Me.btnPartNumber.Checked = True
        Me.btnPartNumber.Location = New System.Drawing.Point(10, 12)
        Me.btnPartNumber.Name = "btnPartNumber"
        Me.btnPartNumber.Size = New System.Drawing.Size(84, 17)
        Me.btnPartNumber.TabIndex = 8
        Me.btnPartNumber.TabStop = True
        Me.btnPartNumber.Text = "Part Number"
        Me.btnPartNumber.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(228, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Search"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtPartNumber
        '
        Me.txtPartNumber.Location = New System.Drawing.Point(100, 11)
        Me.txtPartNumber.Name = "txtPartNumber"
        Me.txtPartNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtPartNumber.TabIndex = 6
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDDataGridViewTextBoxColumn, Me.CustomerNameDataGridViewTextBoxColumn, Me.RequestForQuoteNumberDataGridViewTextBoxColumn, Me.PartNumberDataGridViewTextBoxColumn, Me.CreatedByDataGridViewTextBoxColumn, Me.IsQuoteDataGridViewCheckBoxColumn, Me.TemplateIDDataGridViewTextBoxColumn, Me.InitialsDataGridViewTextBoxColumn, Me.CreatedDateDataGridViewTextBoxColumn, Me.LastModifedDateDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.QuoteBindingSource
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(603, 191)
        Me.DataGridView1.TabIndex = 1
        '
        'QuoteBindingSource
        '
        Me.QuoteBindingSource.DataMember = "_Quote"
        Me.QuoteBindingSource.DataSource = Me.QuoteDataBase
        '
        'QuoteDataBase
        '
        Me.QuoteDataBase.DataSetName = "QuoteDataBase"
        Me.QuoteDataBase.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        '_QuoteTableAdapter
        '
        Me._QuoteTableAdapter.ClearBeforeFill = True
        '
        'IDDataGridViewTextBoxColumn
        '
        Me.IDDataGridViewTextBoxColumn.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn.Name = "IDDataGridViewTextBoxColumn"
        Me.IDDataGridViewTextBoxColumn.ReadOnly = True
        '
        'CustomerNameDataGridViewTextBoxColumn
        '
        Me.CustomerNameDataGridViewTextBoxColumn.DataPropertyName = "CustomerName"
        Me.CustomerNameDataGridViewTextBoxColumn.HeaderText = "CustomerName"
        Me.CustomerNameDataGridViewTextBoxColumn.Name = "CustomerNameDataGridViewTextBoxColumn"
        Me.CustomerNameDataGridViewTextBoxColumn.ReadOnly = True
        '
        'RequestForQuoteNumberDataGridViewTextBoxColumn
        '
        Me.RequestForQuoteNumberDataGridViewTextBoxColumn.DataPropertyName = "RequestForQuoteNumber"
        Me.RequestForQuoteNumberDataGridViewTextBoxColumn.HeaderText = "RequestForQuoteNumber"
        Me.RequestForQuoteNumberDataGridViewTextBoxColumn.Name = "RequestForQuoteNumberDataGridViewTextBoxColumn"
        Me.RequestForQuoteNumberDataGridViewTextBoxColumn.ReadOnly = True
        '
        'PartNumberDataGridViewTextBoxColumn
        '
        Me.PartNumberDataGridViewTextBoxColumn.DataPropertyName = "PartNumber"
        Me.PartNumberDataGridViewTextBoxColumn.HeaderText = "PartNumber"
        Me.PartNumberDataGridViewTextBoxColumn.Name = "PartNumberDataGridViewTextBoxColumn"
        Me.PartNumberDataGridViewTextBoxColumn.ReadOnly = True
        '
        'CreatedByDataGridViewTextBoxColumn
        '
        Me.CreatedByDataGridViewTextBoxColumn.DataPropertyName = "CreatedBy"
        Me.CreatedByDataGridViewTextBoxColumn.HeaderText = "CreatedBy"
        Me.CreatedByDataGridViewTextBoxColumn.Name = "CreatedByDataGridViewTextBoxColumn"
        Me.CreatedByDataGridViewTextBoxColumn.ReadOnly = True
        '
        'IsQuoteDataGridViewCheckBoxColumn
        '
        Me.IsQuoteDataGridViewCheckBoxColumn.DataPropertyName = "IsQuote"
        Me.IsQuoteDataGridViewCheckBoxColumn.HeaderText = "IsQuote"
        Me.IsQuoteDataGridViewCheckBoxColumn.Name = "IsQuoteDataGridViewCheckBoxColumn"
        Me.IsQuoteDataGridViewCheckBoxColumn.ReadOnly = True
        '
        'TemplateIDDataGridViewTextBoxColumn
        '
        Me.TemplateIDDataGridViewTextBoxColumn.DataPropertyName = "TemplateID"
        Me.TemplateIDDataGridViewTextBoxColumn.HeaderText = "TemplateID"
        Me.TemplateIDDataGridViewTextBoxColumn.Name = "TemplateIDDataGridViewTextBoxColumn"
        Me.TemplateIDDataGridViewTextBoxColumn.ReadOnly = True
        '
        'InitialsDataGridViewTextBoxColumn
        '
        Me.InitialsDataGridViewTextBoxColumn.DataPropertyName = "Initials"
        Me.InitialsDataGridViewTextBoxColumn.HeaderText = "Initials"
        Me.InitialsDataGridViewTextBoxColumn.Name = "InitialsDataGridViewTextBoxColumn"
        Me.InitialsDataGridViewTextBoxColumn.ReadOnly = True
        '
        'CreatedDateDataGridViewTextBoxColumn
        '
        Me.CreatedDateDataGridViewTextBoxColumn.DataPropertyName = "CreatedDate"
        Me.CreatedDateDataGridViewTextBoxColumn.HeaderText = "CreatedDate"
        Me.CreatedDateDataGridViewTextBoxColumn.Name = "CreatedDateDataGridViewTextBoxColumn"
        Me.CreatedDateDataGridViewTextBoxColumn.ReadOnly = True
        '
        'LastModifedDateDataGridViewTextBoxColumn
        '
        Me.LastModifedDateDataGridViewTextBoxColumn.DataPropertyName = "LastModifedDate"
        Me.LastModifedDateDataGridViewTextBoxColumn.HeaderText = "LastModifedDate"
        Me.LastModifedDateDataGridViewTextBoxColumn.Name = "LastModifedDateDataGridViewTextBoxColumn"
        Me.LastModifedDateDataGridViewTextBoxColumn.ReadOnly = True
        '
        'frmTemplateSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(603, 262)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MinimizeBox = False
        Me.Name = "frmTemplateSearch"
        Me.ShowInTaskbar = False
        Me.Text = "Search Templates"
        Me.TopMost = True
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.QuoteBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.QuoteDataBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents QuoteDataBase As DCS.Quote.QuoteDataBase
    Friend WithEvents QuoteBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents _QuoteTableAdapter As DCS.Quote.QuoteDataBaseTableAdapters._QuoteTableAdapter
    Friend WithEvents txtRFQ As System.Windows.Forms.TextBox
    Friend WithEvents btnRFQ As System.Windows.Forms.RadioButton
    Friend WithEvents btnPartNumber As System.Windows.Forms.RadioButton
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtPartNumber As System.Windows.Forms.TextBox
    Friend WithEvents IDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CustomerNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RequestForQuoteNumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PartNumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CreatedByDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IsQuoteDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents TemplateIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InitialsDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CreatedDateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LastModifedDateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
