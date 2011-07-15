<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmQuoteSearch
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
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.btnID = New System.Windows.Forms.RadioButton()
        Me.txtRFQ = New System.Windows.Forms.TextBox()
        Me.btnRFQ = New System.Windows.Forms.RadioButton()
        Me.btnPartNumber = New System.Windows.Forms.RadioButton()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtPartNumber = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.QuoteBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.QuoteDataBase = New DCS.Quote.QuoteDataBase()
        Me._QuoteTableAdapter = New DCS.Quote.QuoteDataBaseTableAdapters._QuoteTableAdapter()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtID)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnID)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRFQ)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnRFQ)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnPartNumber)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPartNumber)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridView1)
        Me.SplitContainer1.Size = New System.Drawing.Size(435, 315)
        Me.SplitContainer1.SplitterDistance = 103
        Me.SplitContainer1.TabIndex = 1
        '
        'txtID
        '
        Me.txtID.Location = New System.Drawing.Point(102, 67)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(100, 20)
        Me.txtID.TabIndex = 7
        '
        'btnID
        '
        Me.btnID.AutoSize = True
        Me.btnID.Location = New System.Drawing.Point(12, 68)
        Me.btnID.Name = "btnID"
        Me.btnID.Size = New System.Drawing.Size(36, 17)
        Me.btnID.TabIndex = 6
        Me.btnID.TabStop = True
        Me.btnID.Text = "ID"
        Me.btnID.UseVisualStyleBackColor = True
        '
        'txtRFQ
        '
        Me.txtRFQ.Location = New System.Drawing.Point(102, 41)
        Me.txtRFQ.Name = "txtRFQ"
        Me.txtRFQ.Size = New System.Drawing.Size(100, 20)
        Me.txtRFQ.TabIndex = 5
        '
        'btnRFQ
        '
        Me.btnRFQ.AutoSize = True
        Me.btnRFQ.Location = New System.Drawing.Point(12, 42)
        Me.btnRFQ.Name = "btnRFQ"
        Me.btnRFQ.Size = New System.Drawing.Size(47, 17)
        Me.btnRFQ.TabIndex = 4
        Me.btnRFQ.Text = "RFQ"
        Me.btnRFQ.UseVisualStyleBackColor = True
        '
        'btnPartNumber
        '
        Me.btnPartNumber.AutoSize = True
        Me.btnPartNumber.Checked = True
        Me.btnPartNumber.Location = New System.Drawing.Point(12, 16)
        Me.btnPartNumber.Name = "btnPartNumber"
        Me.btnPartNumber.Size = New System.Drawing.Size(84, 17)
        Me.btnPartNumber.TabIndex = 3
        Me.btnPartNumber.TabStop = True
        Me.btnPartNumber.Text = "Part Number"
        Me.btnPartNumber.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(230, 16)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Search"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtPartNumber
        '
        Me.txtPartNumber.Location = New System.Drawing.Point(102, 15)
        Me.txtPartNumber.Name = "txtPartNumber"
        Me.txtPartNumber.Size = New System.Drawing.Size(100, 20)
        Me.txtPartNumber.TabIndex = 1
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.ShowRowErrors = False
        Me.DataGridView1.Size = New System.Drawing.Size(435, 208)
        Me.DataGridView1.TabIndex = 0
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
        'frmQuoteSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 315)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmQuoteSearch"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Quote Search"
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
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtPartNumber As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents QuoteDataBase As DCS.Quote.QuoteDataBase
    Friend WithEvents QuoteBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents _QuoteTableAdapter As DCS.Quote.QuoteDataBaseTableAdapters._QuoteTableAdapter
    Friend WithEvents txtRFQ As System.Windows.Forms.TextBox
    Friend WithEvents btnRFQ As System.Windows.Forms.RadioButton
    Friend WithEvents btnPartNumber As System.Windows.Forms.RadioButton
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents btnID As System.Windows.Forms.RadioButton

End Class
