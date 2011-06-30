<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmQuoteSummary
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
        Me.CtrPartsAndWires1 = New DCS.Quote.ctrPartsAndWires()
        Me.SuspendLayout()
        '
        'CtrPartsAndWires1
        '
        Me.CtrPartsAndWires1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrPartsAndWires1.Location = New System.Drawing.Point(0, 0)
        Me.CtrPartsAndWires1.Name = "CtrPartsAndWires1"
        Me.CtrPartsAndWires1.QuoteHeader = Nothing
        Me.CtrPartsAndWires1.Size = New System.Drawing.Size(284, 112)
        Me.CtrPartsAndWires1.TabIndex = 0
        '
        'frmQuoteSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 112)
        Me.Controls.Add(Me.CtrPartsAndWires1)
        Me.Name = "frmQuoteSummary"
        Me.Text = "New Quote - Summary"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrPartsAndWires1 As DCS.Quote.ctrPartsAndWires
End Class
