Imports DCS.Quote.Model

Public Class ctrWires

    Private WithEvents _Binding As Binding

    Public Property QuoteHeader As QuoteHeader

    Public Sub New()
        '_Binding = New Binding("Text", oDt, "GetDateTime")
        'txt1.DataBindings.Add(oBinding)
        'txt2.DataBindings.Add("Text", oDt, "GetDateTime")    End Sub
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'ctrWires
        '
        Me.Name = "ctrWires"
        Me.Size = New System.Drawing.Size(192, 165)
        Me.ResumeLayout(False)

    End Sub
End Class
