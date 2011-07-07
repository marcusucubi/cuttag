﻿Imports System.ComponentModel
Imports DCS.Quote.Model

Public Class ActiveQuoteDetail
    Implements INotifyPropertyChanged

    Private _QuoteDetail As QuoteDetail

    Public Shared ReadOnly ActiveQuoteDetail As ActiveQuoteDetail = New ActiveQuoteDetail

    Public Event PropertyChanged As PropertyChangedEventHandler _
        Implements INotifyPropertyChanged.PropertyChanged

    Public Property QuoteDetail As QuoteDetail
        Get
            Return _QuoteDetail
        End Get
        Friend Set(ByVal value As QuoteDetail)
            If (_QuoteDetail IsNot value) Then
                _QuoteDetail = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("QuoteDetail"))
            End If
        End Set
    End Property

End Class