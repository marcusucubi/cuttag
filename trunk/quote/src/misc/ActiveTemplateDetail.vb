Imports System.ComponentModel
Imports DCS.Quote.Model
Imports DCS.Quote.Model.Quote

Public Class ActiveTemplateDetail
    Implements INotifyPropertyChanged

    Private _QuoteDetail As Detail

    Public Shared ReadOnly ActiveTemplateDetail As ActiveTemplateDetail = New ActiveTemplateDetail

    Public Event PropertyChanged As PropertyChangedEventHandler _
        Implements INotifyPropertyChanged.PropertyChanged

    Public Property QuoteDetail As Detail
        Get
            Return _QuoteDetail
        End Get
        Friend Set(ByVal value As Detail)
            If (_QuoteDetail IsNot value) Then
                _QuoteDetail = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("QuoteDetail"))
            End If
        End Set
    End Property

End Class
