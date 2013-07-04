<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportPartsList
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
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.cboPartLookup = New System.Windows.Forms.ComboBox()
        Me.HQGetParts4LookupBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ImportDataSet = New PluginImport.ImportDataSet()
        Me.lblPartLookup = New System.Windows.Forms.Label()
        Me.HQ_GetParts4LookupTableAdapter = New PluginImport.ImportDataSetTableAdapters.HQ_GetParts4LookupTableAdapter()
        CType(Me.HQGetParts4LookupBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImportDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.OK_Button.Location = New System.Drawing.Point(361, 112)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 2
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(434, 112)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 3
        Me.Cancel_Button.Text = "Cancel"
        '
        'cboPartLookup
        '
        Me.cboPartLookup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPartLookup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPartLookup.DataSource = Me.HQGetParts4LookupBindingSource
        Me.cboPartLookup.DisplayMember = "Display"
        Me.cboPartLookup.FormattingEnabled = True
        Me.cboPartLookup.Location = New System.Drawing.Point(141, 21)
        Me.cboPartLookup.Name = "cboPartLookup"
        Me.cboPartLookup.Size = New System.Drawing.Size(361, 21)
        Me.cboPartLookup.TabIndex = 1
        Me.cboPartLookup.ValueMember = "PartID"
        '
        'HQGetParts4LookupBindingSource
        '
        Me.HQGetParts4LookupBindingSource.DataMember = "HQ_GetParts4Lookup"
        Me.HQGetParts4LookupBindingSource.DataSource = Me.ImportDataSet
        '
        'ImportDataSet
        '
        Me.ImportDataSet.DataSetName = "ImportDataSet"
        Me.ImportDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'lblPartLookup
        '
        Me.lblPartLookup.AutoSize = True
        Me.lblPartLookup.Location = New System.Drawing.Point(14, 24)
        Me.lblPartLookup.Name = "lblPartLookup"
        Me.lblPartLookup.Size = New System.Drawing.Size(121, 13)
        Me.lblPartLookup.TabIndex = 0
        Me.lblPartLookup.Text = "Part (Harness) to Import:"
        Me.lblPartLookup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'HQ_GetParts4LookupTableAdapter
        '
        Me.HQ_GetParts4LookupTableAdapter.ClearBeforeFill = True
        '
        'frmImportPartsList
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(524, 147)
        Me.Controls.Add(Me.lblPartLookup)
        Me.Controls.Add(Me.cboPartLookup)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.Cancel_Button)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmImportPartsList"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Import Parts List from Wire Harness Control"
        CType(Me.HQGetParts4LookupBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImportDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents cboPartLookup As System.Windows.Forms.ComboBox
    Friend WithEvents lblPartLookup As System.Windows.Forms.Label
    Friend WithEvents ImportDataSet As PluginImport.ImportDataSet
    Friend WithEvents HQGetParts4LookupBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents HQ_GetParts4LookupTableAdapter As PluginImport.ImportDataSetTableAdapters.HQ_GetParts4LookupTableAdapter
End Class
