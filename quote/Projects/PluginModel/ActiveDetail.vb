Imports System.ComponentModel

Imports Model
Imports Model.Common

Public Class ActiveDetail
    Implements INotifyPropertyChanged

    Private _Detail As Detail
    
    private Shared ReadOnly _ActiveDetail As ActiveDetail = New ActiveDetail
    
    Public Shared ReadOnly Property ActiveDetail
        Get 
            Return _ActiveDetail
        End Get
    End Property

    Public Event PropertyChanged As PropertyChangedEventHandler _
        Implements INotifyPropertyChanged.PropertyChanged

    Public Property Detail As Detail
        Get
            Return _Detail
        End Get
        Set(ByVal value As Detail)
            If (_Detail IsNot value) Then
                _Detail = value
                RaiseEvent PropertyChanged(Me, _
                    New PropertyChangedEventArgs("Detail"))
            End If
        End Set
    End Property

End Class
