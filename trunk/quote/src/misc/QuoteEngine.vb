Imports DCS.Quote.Model

Public Class QuoteEngine

    Private _QuoteHeader As QuoteHeader

    Public Property WireUnitTime As Integer = 30
    Public Property WireUnitCutTime As Integer = 120
    Public Property NumberOfCuts As Integer = 1

    Public Sub New(ByVal header As QuoteHeader)
        Me._QuoteHeader = header
    End Sub

    Public ReadOnly Property WireTime As Integer
        Get
            Dim x As Decimal
            Dim min = _QuoteHeader.QuoteProperties.MinimumOrderQuantity

            If min > 0 Then
                Dim prop As QuoteProperties = _QuoteHeader.QuoteProperties
                Dim time1 As Decimal = (prop.WireLengthFeet * WireUnitTime)
                Dim time2 As Decimal = (NumberOfCuts * WireUnitCutTime)
                x += (time1 + time2)
                x = Math.Round(x)
            End If

            Return x
        End Get
    End Property

End Class
