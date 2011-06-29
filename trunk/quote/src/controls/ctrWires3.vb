Imports DCS.Quote.Model

Public Class ctrWires3

    Dim WithEvents _TotalLengthBinding As Binding
    Dim WithEvents _TotalLengthFeetBinding As Binding
    Dim WithEvents _WireCountBinding As Binding
    Private _QuoteHeader As QuoteHeader

    Public Property QuoteHeader As QuoteHeader
        Get
            Return _QuoteHeader
        End Get
        Set(ByVal value As QuoteHeader)
            _QuoteHeader = value
            If value IsNot Nothing Then
                _TotalLengthBinding = New Binding("Text", QuoteHeader, "TotalLength")
                Me.txtTotalLengthDm.DataBindings.Add(_TotalLengthBinding)

                _TotalLengthFeetBinding = New Binding("Text", QuoteHeader, "TotalLengthFeet")
                Me.txtTotalLengthFeet.DataBindings.Add(_TotalLengthFeetBinding)

                _WireCountBinding = New Binding("Text", QuoteHeader, "WireCount")
                Me.txtWireCount.DataBindings.Add(_WireCountBinding)
            End If
        End Set
    End Property

End Class
