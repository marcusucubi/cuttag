Imports DCS.Quote.Model

Public Class ctrParts

    Dim WithEvents _PartCountBinding As Binding

    Public Property QuoteHeader As QuoteHeader

    Private Sub Form2_Load(ByVal sender As Object, _
                           ByVal e As System.EventArgs) _
                Handles MyBase.Load

        _PartCountBinding = New Binding("Text", QuoteHeader, "PartCount")
        Me.txtPartCount.DataBindings.Add(_PartCountBinding)

    End Sub

End Class
