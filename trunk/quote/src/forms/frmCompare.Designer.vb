Imports WeifenLuo.WinFormsUI.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCompare
    Inherits DockContent

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
        Me.ListViewDestination = New System.Windows.Forms.ListView()
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'ListViewDestination
        '
        Me.ListViewDestination.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader8})
        Me.ListViewDestination.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewDestination.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewDestination.FullRowSelect = True
        Me.ListViewDestination.GridLines = True
        Me.ListViewDestination.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.ListViewDestination.Location = New System.Drawing.Point(0, 0)
        Me.ListViewDestination.MultiSelect = False
        Me.ListViewDestination.Name = "ListViewDestination"
        Me.ListViewDestination.OwnerDraw = True
        Me.ListViewDestination.Size = New System.Drawing.Size(672, 337)
        Me.ListViewDestination.TabIndex = 3
        Me.ListViewDestination.UseCompatibleStateImageBehavior = False
        Me.ListViewDestination.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = ""
        Me.ColumnHeader8.Width = 1600
        '
        'frmCompare
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(672, 337)
        Me.Controls.Add(Me.ListViewDestination)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmCompare"
        Me.Text = "frmCompare"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ListViewDestination As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader


End Class
