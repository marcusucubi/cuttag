Imports DCS.Quote.Model

Public Class QuoteEngine

    Private _QuoteHeader As QuoteHeader

    Public Property WireUnitTime As Integer
    Public Property CutTime As Integer
    Public Property NumberOfCuts As Integer

    Public Sub New(ByVal header As QuoteHeader)
        Me._QuoteHeader = header
    End Sub

    Public ReadOnly Property WireTime As Decimal
        Get
            Dim prop As QuoteProperties = _QuoteHeader.QuoteProperties
            Dim time1 As Decimal = prop.WireLengthFeet * WireUnitTime
            Dim time2 As Decimal = (NumberOfCuts * CutTime)

            Dim x As Decimal
            x += (time1 + time2)
            Return x
        End Get
    End Property

End Class
