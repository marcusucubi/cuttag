<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrWires3
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtWireCount = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTotalLengthFeet = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTotalLengthDm = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtWireTime = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtWireTime)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtWireCount)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtTotalLengthFeet)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtTotalLengthDm)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(221, 228)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Wires"
        '
        'txtWireCount
        '
        Me.txtWireCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWireCount.Location = New System.Drawing.Point(131, 16)
        Me.txtWireCount.Name = "txtWireCount"
        Me.txtWireCount.Size = New System.Drawing.Size(78, 20)
        Me.txtWireCount.TabIndex = 5
        Me.txtWireCount.Text = "0"
        Me.txtWireCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Count:"
        '
        'txtTotalLengthFeet
        '
        Me.txtTotalLengthFeet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalLengthFeet.Location = New System.Drawing.Point(131, 36)
        Me.txtTotalLengthFeet.Name = "txtTotalLengthFeet"
        Me.txtTotalLengthFeet.Size = New System.Drawing.Size(78, 20)
        Me.txtTotalLengthFeet.TabIndex = 3
        Me.txtTotalLengthFeet.Text = "0"
        Me.txtTotalLengthFeet.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Total Length: (feet)"
        '
        'txtTotalLengthDm
        '
        Me.txtTotalLengthDm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalLengthDm.Location = New System.Drawing.Point(131, 56)
        Me.txtTotalLengthDm.Name = "txtTotalLengthDm"
        Me.txtTotalLengthDm.Size = New System.Drawing.Size(78, 20)
        Me.txtTotalLengthDm.TabIndex = 1
        Me.txtTotalLengthDm.Text = "0"
        Me.txtTotalLengthDm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Total Length: (dm)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 80)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Wire Time:"
        '
        'txtWireTime
        '
        Me.txtWireTime.Location = New System.Drawing.Point(132, 77)
        Me.txtWireTime.Name = "txtWireTime"
        Me.txtWireTime.Size = New System.Drawing.Size(77, 20)
        Me.txtWireTime.TabIndex = 7
        Me.txtWireTime.Text = "25"
        Me.txtWireTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ctrWires3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ctrWires3"
        Me.Size = New System.Drawing.Size(221, 228)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTotalLengthDm As System.Windows.Forms.Label
    Friend WithEvents txtTotalLengthFeet As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtWireCount As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtWireTime As System.Windows.Forms.TextBox

End Class
