Imports DCS.Quote.Model

Public Class ctrWires3

    Dim WithEvents _TotalLengthBinding As Binding
    Dim WithEvents _TotalLengthFeetBinding As Binding
    Dim WithEvents _WireCountBinding As Binding

    Public Property QuoteHeader As QuoteHeader

    Private Sub Wires_Load(ByVal sender As Object, _
                           ByVal e As System.EventArgs) _
                Handles MyBase.Load

        _TotalLengthBinding = New Binding("Text", QuoteHeader, "TotalLength")
        Me.txtTotalLengthDm.DataBindings.Add(_TotalLengthBinding)

        _TotalLengthFeetBinding = New Binding("Text", QuoteHeader, "TotalLengthFeet")
        Me.txtTotalLengthFeet.DataBindings.Add(_TotalLengthFeetBinding)

        _WireCountBinding = New Binding("Text", QuoteHeader, "WireCount")
        Me.txtWireCount.DataBindings.Add(_WireCountBinding)
    End Sub

End Class
