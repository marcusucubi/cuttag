Imports System.ComponentModel
Imports DCS.Quote.Model
Imports DCS.Quote.Common

Public Class ActiveDetail
    Implements INotifyPropertyChanged

    Private _Detail As Detail

    Public Shared ReadOnly ActiveDetail As ActiveDetail = New ActiveDetail

    Public Event PropertyChanged As PropertyChangedEventHandler _
        Implements INotifyPropertyChanged.PropertyChanged

    Public Property Detail As Detail
        Get
            Return _Detail
        End Get
        Friend Set(ByVal value As Detail)
            If (_Detail IsNot value) Then
                _Detail = value
                RaiseEvent PropertyChanged(Me, _
                    New PropertyChangedEventArgs("Detail"))
            End If
        End Set
    End Property

End Class
