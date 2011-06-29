Imports DCS.Quote.Model

Public Class ctrPartsAndWires

    Dim WithEvents _CountBinding As Binding
    Private _QuoteHeader As QuoteHeader

    Public Property QuoteHeader As QuoteHeader
        Get
            Return _QuoteHeader
        End Get
        Set(ByVal value As QuoteHeader)
            _QuoteHeader = value
            If value IsNot Nothing Then
                _CountBinding = New Binding("Text", QuoteHeader, "PartAndWireCount")
                Me.txtCount.DataBindings.Add(_CountBinding)
            End If
        End Set
    End Property

End Class
