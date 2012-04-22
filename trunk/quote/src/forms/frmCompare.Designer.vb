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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCompare))
        Me.ListViewDestination = New System.Windows.Forms.ListView()
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.SameButton = New System.Windows.Forms.ToolStripButton()
        Me.NewLeftButton = New System.Windows.Forms.ToolStripButton()
        Me.NewRightButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ChangedButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStrip1.SuspendLayout()
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
        Me.ListViewDestination.Location = New System.Drawing.Point(0, 25)
        Me.ListViewDestination.MultiSelect = False
        Me.ListViewDestination.Name = "ListViewDestination"
        Me.ListViewDestination.OwnerDraw = True
        Me.ListViewDestination.Size = New System.Drawing.Size(672, 312)
        Me.ListViewDestination.TabIndex = 3
        Me.ListViewDestination.UseCompatibleStateImageBehavior = False
        Me.ListViewDestination.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = ""
        Me.ColumnHeader8.Width = 1600
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewLeftButton, Me.ToolStripSeparator2, Me.SameButton, Me.ToolStripSeparator1, Me.ChangedButton, Me.ToolStripSeparator3, Me.NewRightButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(672, 25)
        Me.ToolStrip1.TabIndex = 4
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'SameButton
        '
        Me.SameButton.CheckOnClick = True
        Me.SameButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.SameButton.Image = CType(resources.GetObject("SameButton.Image"), System.Drawing.Image)
        Me.SameButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SameButton.Name = "SameButton"
        Me.SameButton.Size = New System.Drawing.Size(40, 22)
        Me.SameButton.Text = "Same"
        '
        'NewLeftButton
        '
        Me.NewLeftButton.CheckOnClick = True
        Me.NewLeftButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.NewLeftButton.Image = CType(resources.GetObject("NewLeftButton.Image"), System.Drawing.Image)
        Me.NewLeftButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NewLeftButton.Name = "NewLeftButton"
        Me.NewLeftButton.Size = New System.Drawing.Size(69, 22)
        Me.NewLeftButton.Text = "Added Left"
        '
        'NewRightButton
        '
        Me.NewRightButton.CheckOnClick = True
        Me.NewRightButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.NewRightButton.Image = CType(resources.GetObject("NewRightButton.Image"), System.Drawing.Image)
        Me.NewRightButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NewRightButton.Name = "NewRightButton"
        Me.NewRightButton.Size = New System.Drawing.Size(77, 22)
        Me.NewRightButton.Text = "Added Right"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ChangedButton
        '
        Me.ChangedButton.CheckOnClick = True
        Me.ChangedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ChangedButton.Image = CType(resources.GetObject("ChangedButton.Image"), System.Drawing.Image)
        Me.ChangedButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ChangedButton.Name = "ChangedButton"
        Me.ChangedButton.Size = New System.Drawing.Size(59, 22)
        Me.ChangedButton.Text = "Changed"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'frmCompare
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(672, 337)
        Me.Controls.Add(Me.ListViewDestination)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmCompare"
        Me.Text = "frmCompare"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListViewDestination As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents SameButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents NewLeftButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents NewRightButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ChangedButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator


End Class
