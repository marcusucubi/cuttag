Imports DCS.Quote.Model

Public Class ctrPartSummery

    Dim WithEvents _PartCountBinding As Binding
    Private _QuoteHeader As QuoteHeader

    Public Property QuoteHeader As QuoteHeader
        Get
            Return _QuoteHeader
        End Get
        Set(ByVal value As QuoteHeader)
            _QuoteHeader = value
            If value IsNot Nothing Then
                _PartCountBinding = New Binding("Text", QuoteHeader, "PartCount")
                Me.txtCount.DataBindings.Add(_PartCountBinding)
            End If
        End Set
    End Property

End Class
