Imports System.ComponentModel

Namespace Model

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
            Set(ByVal value As QuoteHeader)
                _QuoteHeader = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("QuoteHeader"))
            End Set
        End Property

    End Class

End Namespace
