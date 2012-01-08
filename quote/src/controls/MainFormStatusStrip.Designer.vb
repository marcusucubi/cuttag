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
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me._PartNumber = New System.Windows.Forms.Label()
        Me._RFQ = New System.Windows.Forms.Label()
        Me._QuoteDate = New System.Windows.Forms.Label()
        Me._IsNew = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me._PartNumber)
        Me.FlowLayoutPanel1.Controls.Add(Me._RFQ)
        Me.FlowLayoutPanel1.Controls.Add(Me._QuoteDate)
        Me.FlowLayoutPanel1.Controls.Add(Me._IsNew)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Padding = New System.Windows.Forms.Padding(0, 5, 0, 5)
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(366, 25)
        Me.FlowLayoutPanel1.TabIndex = 5
        '
        '_PartNumber
        '
        Me._PartNumber.AutoSize = True
        Me._PartNumber.BackColor = System.Drawing.SystemColors.Info
        Me._PartNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._PartNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._PartNumber.Location = New System.Drawing.Point(3, 5)
        Me._PartNumber.MinimumSize = New System.Drawing.Size(0, 15)
        Me._PartNumber.Name = "_PartNumber"
        Me._PartNumber.Size = New System.Drawing.Size(39, 15)
        Me._PartNumber.TabIndex = 0
        Me._PartNumber.Text = "None"
        Me._PartNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me._PartNumber, "Active Part Number")
        '
        '_RFQ
        '
        Me._RFQ.AutoSize = True
        Me._RFQ.BackColor = System.Drawing.SystemColors.Info
        Me._RFQ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._RFQ.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._RFQ.Location = New System.Drawing.Point(48, 5)
        Me._RFQ.MinimumSize = New System.Drawing.Size(0, 15)
        Me._RFQ.Name = "_RFQ"
        Me._RFQ.Size = New System.Drawing.Size(39, 15)
        Me._RFQ.TabIndex = 1
        Me._RFQ.Text = "None"
        Me._RFQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me._RFQ, "Active RFC")
        '
        '_QuoteDate
        '
        Me._QuoteDate.AutoSize = True
        Me._QuoteDate.BackColor = System.Drawing.SystemColors.Info
        Me._QuoteDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._QuoteDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._QuoteDate.Location = New System.Drawing.Point(93, 5)
        Me._QuoteDate.MinimumSize = New System.Drawing.Size(0, 15)
        Me._QuoteDate.Name = "_QuoteDate"
        Me._QuoteDate.Size = New System.Drawing.Size(39, 15)
        Me._QuoteDate.TabIndex = 2
        Me._QuoteDate.Text = "None"
        Me._QuoteDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me._QuoteDate, "Active Quote Date")
        '
        '_IsNew
        '
        Me._IsNew.AutoSize = True
        Me._IsNew.BackColor = System.Drawing.SystemColors.Info
        Me._IsNew.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._IsNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._IsNew.Location = New System.Drawing.Point(138, 5)
        Me._IsNew.MinimumSize = New System.Drawing.Size(0, 15)
        Me._IsNew.Name = "_IsNew"
        Me._IsNew.Size = New System.Drawing.Size(39, 15)
        Me._IsNew.TabIndex = 3
        Me._IsNew.Text = "None"
        Me._IsNew.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me._IsNew, "Is Quote New")
        '
        'MainFormStatusStrip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Name = "MainFormStatusStrip"
        Me.Size = New System.Drawing.Size(366, 24)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents _PartNumber As System.Windows.Forms.Label
    Friend WithEvents _RFQ As System.Windows.Forms.Label
    Friend WithEvents _QuoteDate As System.Windows.Forms.Label
    Friend WithEvents _IsNew As System.Windows.Forms.Label

End Class
