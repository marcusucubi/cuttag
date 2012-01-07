<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainFormStatusStrip
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
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.sslblPartNumber = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sslblRFQ = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sslblQuoteDate = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.sslblPartNumber, Me.sslblRFQ, Me.sslblQuoteDate})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 54)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.ShowItemToolTips = True
        Me.StatusStrip1.Size = New System.Drawing.Size(366, 24)
        Me.StatusStrip1.TabIndex = 0
        '
        'sslblPartNumber
        '
        Me.sslblPartNumber.BorderSides = CType((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.sslblPartNumber.Name = "sslblPartNumber"
        Me.sslblPartNumber.Size = New System.Drawing.Size(40, 19)
        Me.sslblPartNumber.Text = "None"
        Me.sslblPartNumber.ToolTipText = "Active PartNumber"
        '
        'sslblRFQ
        '
        Me.sslblRFQ.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.sslblRFQ.Name = "sslblRFQ"
        Me.sslblRFQ.Size = New System.Drawing.Size(40, 19)
        Me.sslblRFQ.Text = "None"
        Me.sslblRFQ.ToolTipText = "Active RFC"
        '
        'sslblQuoteDate
        '
        Me.sslblQuoteDate.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.sslblQuoteDate.Name = "sslblQuoteDate"
        Me.sslblQuoteDate.Size = New System.Drawing.Size(40, 19)
        Me.sslblQuoteDate.Text = "None"
        Me.sslblQuoteDate.ToolTipText = "Active Quote Date"
        '
        'MainFormStatusStrip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.StatusStrip1)
        Me.Name = "MainFormStatusStrip"
        Me.Size = New System.Drawing.Size(366, 78)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents sslblPartNumber As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents sslblRFQ As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents sslblQuoteDate As System.Windows.Forms.ToolStripStatusLabel

End Class
