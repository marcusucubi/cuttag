Imports DCS.Quote.Model

Public Class QuoteComputer

    Private _QuoteHeader As QuoteHeader

    Public Property WireTime As Integer
    Public Property CutTime As Integer

    Public Sub New(ByVal header As QuoteHeader)
        Me._QuoteHeader = header
    End Sub

End Class
