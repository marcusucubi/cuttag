Imports System.ComponentModel
Imports DCS.Quote.Model

Public Class ActiveQuote
    Implements INotifyPropertyChanged

    Private _QuoteHeader As QuoteHeader

    Public Shared ReadOnly ActiveQuote As ActiveQuote = New ActiveQuote

    Public Event PropertyChanged As PropertyChangedEventHandler _
        Implements INotifyPropertyChanged.PropertyChanged

    Public Property QuoteHeader As QuoteHeader
        Get
            Return _QuoteHeader
        End Get
        Friend Set(ByVal value As QuoteHeader)
            If (_QuoteHeader IsNot value) Then
                _QuoteHeader = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("QuoteHeader"))
            End If
        End Set
    End Property

End Class

