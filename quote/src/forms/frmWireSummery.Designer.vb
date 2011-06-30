<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWireSummery
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
        Me.CtrWires31 = New DCS.Quote.ctrWireSummery()
        Me.SuspendLayout()
        '
        'CtrWires31
        '
        Me.CtrWires31.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrWires31.Location = New System.Drawing.Point(0, 0)
        Me.CtrWires31.Name = "CtrWires31"
        Me.CtrWires31.QuoteHeader = Nothing
        Me.CtrWires31.Size = New System.Drawing.Size(584, 225)
        Me.CtrWires31.TabIndex = 0
        '
        'frmQuoteHeader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 225)
        Me.Controls.Add(Me.CtrWires31)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmQuoteHeader"
        Me.Text = "New Quote - Wire Summary"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrWires31 As DCS.Quote.ctrWireSummery
End Class
