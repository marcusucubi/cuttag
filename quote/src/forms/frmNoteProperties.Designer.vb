<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNoteProperties
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.lblNoteInternal = New System.Windows.Forms.Label()
        Me.sptNote = New System.Windows.Forms.Splitter()
        Me.pnlNote2Cust = New System.Windows.Forms.Panel()
        Me.txtNote2Customer = New System.Windows.Forms.TextBox()
        Me.lblNote2Customer = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.pnlNote2Cust.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.lblNoteInternal)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 118)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(284, 144)
        Me.Panel1.TabIndex = 2
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Font = New System.Drawing.Font("Lucida Console", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(0, 16)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(284, 128)
        Me.TextBox1.TabIndex = 2
        '
        'lblNoteInternal
        '
        Me.lblNoteInternal.BackColor = System.Drawing.SystemColors.MenuBar
        Me.lblNoteInternal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNoteInternal.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblNoteInternal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoteInternal.Location = New System.Drawing.Point(0, 0)
        Me.lblNoteInternal.Name = "lblNoteInternal"
        Me.lblNoteInternal.Size = New System.Drawing.Size(284, 16)
        Me.lblNoteInternal.TabIndex = 0
        Me.lblNoteInternal.Text = "Internal Note"
        Me.lblNoteInternal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'sptNote
        '
        Me.sptNote.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.sptNote.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.sptNote.Location = New System.Drawing.Point(0, 114)
        Me.sptNote.Name = "sptNote"
        Me.sptNote.Size = New System.Drawing.Size(284, 4)
        Me.sptNote.TabIndex = 1
        Me.sptNote.TabStop = False
        '
        'pnlNote2Cust
        '
        Me.pnlNote2Cust.Controls.Add(Me.txtNote2Customer)
        Me.pnlNote2Cust.Controls.Add(Me.lblNote2Customer)
        Me.pnlNote2Cust.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlNote2Cust.Location = New System.Drawing.Point(0, 0)
        Me.pnlNote2Cust.Name = "pnlNote2Cust"
        Me.pnlNote2Cust.Size = New System.Drawing.Size(284, 114)
        Me.pnlNote2Cust.TabIndex = 0
        '
        'txtNote2Customer
        '
        Me.txtNote2Customer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNote2Customer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtNote2Customer.Font = New System.Drawing.Font("Lucida Console", 10.0!)
        Me.txtNote2Customer.Location = New System.Drawing.Point(0, 16)
        Me.txtNote2Customer.Multiline = True
        Me.txtNote2Customer.Name = "txtNote2Customer"
        Me.txtNote2Customer.Size = New System.Drawing.Size(284, 98)
        Me.txtNote2Customer.TabIndex = 1
        '
        'lblNote2Customer
        '
        Me.lblNote2Customer.BackColor = System.Drawing.SystemColors.MenuBar
        Me.lblNote2Customer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNote2Customer.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblNote2Customer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNote2Customer.Location = New System.Drawing.Point(0, 0)
        Me.lblNote2Customer.Name = "lblNote2Customer"
        Me.lblNote2Customer.Size = New System.Drawing.Size(284, 16)
        Me.lblNote2Customer.TabIndex = 0
        Me.lblNote2Customer.Text = "Note to Customer"
        Me.lblNote2Customer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmNoteProperties
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.pnlNote2Cust)
        Me.Controls.Add(Me.sptNote)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmNoteProperties"
        Me.Text = "Note"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlNote2Cust.ResumeLayout(False)
        Me.pnlNote2Cust.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents sptNote As System.Windows.Forms.Splitter
    Friend WithEvents pnlNote2Cust As System.Windows.Forms.Panel
    Friend WithEvents txtNote2Customer As System.Windows.Forms.TextBox
    Friend WithEvents lblNote2Customer As System.Windows.Forms.Label
    Friend WithEvents lblNoteInternal As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
End Class
