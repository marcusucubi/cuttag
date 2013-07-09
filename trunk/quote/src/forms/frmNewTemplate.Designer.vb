<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewTemplate
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtInitials = New System.Windows.Forms.TextBox()
        Me.pnlImportSource = New System.Windows.Forms.Panel()
        Me.rbImported = New System.Windows.Forms.RadioButton()
        Me.rbComputed = New System.Windows.Forms.RadioButton()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.pnlImportSource.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(123, 67)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 0
        Me.Cancel_Button.Text = "Cancel"
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Enabled = False
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 1
        Me.OK_Button.Text = "OK"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Initials:"
        '
        'txtInitials
        '
        Me.txtInitials.Location = New System.Drawing.Point(57, 18)
        Me.txtInitials.Name = "txtInitials"
        Me.txtInitials.Size = New System.Drawing.Size(209, 20)
        Me.txtInitials.TabIndex = 1
        '
        'pnlImportSource
        '
        Me.pnlImportSource.Controls.Add(Me.rbImported)
        Me.pnlImportSource.Controls.Add(Me.rbComputed)
        Me.pnlImportSource.Location = New System.Drawing.Point(10, 35)
        Me.pnlImportSource.Name = "pnlImportSource"
        Me.pnlImportSource.Size = New System.Drawing.Size(256, 32)
        Me.pnlImportSource.TabIndex = 2
        Me.pnlImportSource.Visible = False
        '
        'rbImported
        '
        Me.rbImported.AutoSize = True
        Me.rbImported.Location = New System.Drawing.Point(152, 8)
        Me.rbImported.Name = "rbImported"
        Me.rbImported.Size = New System.Drawing.Size(93, 17)
        Me.rbImported.TabIndex = 1
        Me.rbImported.Text = "Imported Parts"
        Me.rbImported.UseVisualStyleBackColor = True
        '
        'rbComputed
        '
        Me.rbComputed.AutoSize = True
        Me.rbComputed.Checked = True
        Me.rbComputed.Location = New System.Drawing.Point(37, 6)
        Me.rbComputed.Name = "rbComputed"
        Me.rbComputed.Size = New System.Drawing.Size(100, 17)
        Me.rbComputed.TabIndex = 0
        Me.rbComputed.TabStop = True
        Me.rbComputed.Text = "Computed Parts"
        Me.rbComputed.UseVisualStyleBackColor = True
        '
        'frmNewBOM
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(281, 108)
        Me.Controls.Add(Me.pnlImportSource)
        Me.Controls.Add(Me.txtInitials)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNewBOM"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "New Template"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.pnlImportSource.ResumeLayout(False)
        Me.pnlImportSource.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtInitials As System.Windows.Forms.TextBox
    Friend WithEvents pnlImportSource As System.Windows.Forms.Panel
    Friend WithEvents rbImported As System.Windows.Forms.RadioButton
    Friend WithEvents rbComputed As System.Windows.Forms.RadioButton

End Class
