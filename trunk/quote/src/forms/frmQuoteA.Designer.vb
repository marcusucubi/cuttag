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
        Me.panelButtons = New System.Windows.Forms.Panel()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.bntAddWire = New System.Windows.Forms.Button()
        Me.btnAddComponent = New System.Windows.Forms.Button()
        Me.WireAndComponentView1 = New DCS.Quote.WireAndComponentView()
        Me.panelButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelButtons
        '
        Me.panelButtons.Controls.Add(Me.btnDelete)
        Me.panelButtons.Controls.Add(Me.bntAddWire)
        Me.panelButtons.Controls.Add(Me.btnAddComponent)
        Me.panelButtons.Dock = System.Windows.Forms.DockStyle.Left
        Me.panelButtons.Location = New System.Drawing.Point(0, 0)
        Me.panelButtons.Name = "panelButtons"
        Me.panelButtons.Size = New System.Drawing.Size(106, 218)
        Me.panelButtons.TabIndex = 5
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(12, 85)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
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
        'WireAndComponentView1
        '
        Me.WireAndComponentView1.DetailCollection = Nothing
        Me.WireAndComponentView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WireAndComponentView1.Location = New System.Drawing.Point(106, 0)
        Me.WireAndComponentView1.Name = "WireAndComponentView1"
        Me.WireAndComponentView1.Size = New System.Drawing.Size(475, 218)
        Me.WireAndComponentView1.TabIndex = 6
        '
        'frmQuoteA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(581, 218)
        Me.Controls.Add(Me.WireAndComponentView1)
        Me.Controls.Add(Me.panelButtons)
        Me.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmQuoteA"
        Me.Text = "New Template"
        Me.panelButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panelButtons As System.Windows.Forms.Panel
    Friend WithEvents bntAddWire As System.Windows.Forms.Button
    Friend WithEvents btnAddComponent As System.Windows.Forms.Button
    Friend WithEvents WireAndComponentView1 As DCS.Quote.WireAndComponentView
    Friend WithEvents btnDelete As System.Windows.Forms.Button
End Class
