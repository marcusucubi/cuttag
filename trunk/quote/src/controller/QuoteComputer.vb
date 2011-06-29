Imports DCS.Quote.Model

Public Class QuoteComputer

    Private _QuoteHeader As QuoteHeader

    Public Property WireTime As Integer
    Public Property CutTime As Integer
    Public Property NumberOfCuts As Integer

    Public Sub New(ByVal header As QuoteHeader)
        Me._QuoteHeader = header
    End Sub

    Public ReadOnly Property TotalWireTime As Decimal
        Get
            Dim time As Decimal = _QuoteHeader.TotalLengthFeet * WireTime

            Dim x As Decimal
            x += time
            Return x
        End Get
    End Property

End Class
