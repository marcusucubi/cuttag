Imports System.ComponentModel
Imports DCS.Quote.Model
Imports DCS.Quote.Model.Quote

Public Class ActiveTemplate
    Implements INotifyPropertyChanged

    Private _QuoteHeader As QuoteHeader

    Public Shared ReadOnly ActiveTemplate As ActiveTemplate = New ActiveTemplate

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

