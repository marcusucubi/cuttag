<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomProperties
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
        Me.ToolStripContainer1 = New System.Windows.Forms.ToolStripContainer()
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid()
        Me.PropertyGrid2 = New System.Windows.Forms.PropertyGrid()
        Me.ToolStripContainer1.ContentPanel.SuspendLayout()
        Me.ToolStripContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStripContainer1
        '
        '
        'ToolStripContainer1.ContentPanel
        '
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.PropertyGrid2)
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.PropertyGrid1)
        Me.ToolStripContainer1.ContentPanel.Size = New System.Drawing.Size(186, 216)
        Me.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer1.Name = "ToolStripContainer1"
        Me.ToolStripContainer1.Size = New System.Drawing.Size(186, 216)
        Me.ToolStripContainer1.TabIndex = 2
        Me.ToolStripContainer1.Text = "ToolStripContainer1"
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PropertyGrid1.HelpVisible = False
        Me.PropertyGrid1.Location = New System.Drawing.Point(0, 169)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.PropertySort = System.Windows.Forms.PropertySort.NoSort
        Me.PropertyGrid1.Size = New System.Drawing.Size(186, 47)
        Me.PropertyGrid1.TabIndex = 0
        Me.PropertyGrid1.ToolbarVisible = False
        '
        'PropertyGrid2
        '
        Me.PropertyGrid2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PropertyGrid2.Location = New System.Drawing.Point(0, 0)
        Me.PropertyGrid2.Name = "PropertyGrid2"
        Me.PropertyGrid2.Size = New System.Drawing.Size(186, 169)
        Me.PropertyGrid2.TabIndex = 1
        '
        'frmCustom
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(186, 216)
        Me.Controls.Add(Me.ToolStripContainer1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmCustom"
        Me.Text = "Custom"
        Me.ToolStripContainer1.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer1.ResumeLayout(False)
        Me.ToolStripContainer1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStripContainer1 As System.Windows.Forms.ToolStripContainer
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
    Friend WithEvents PropertyGrid2 As System.Windows.Forms.PropertyGrid
End Class
