Imports System.ComponentModel

Imports Model.Common

Public Class ActiveHeader
    Implements INotifyPropertyChanged

    Private _Header As Header
    Public Shared ReadOnly ActiveHeader As ActiveHeader = New ActiveHeader

    Public Event PropertyChanged As PropertyChangedEventHandler _
        Implements INotifyPropertyChanged.PropertyChanged

    Public Property Header As Header
        Get
            Return _Header
        End Get
        Set(ByVal value As Header)
            If (_Header IsNot value) Then
                _Header = value
                RaiseEvent PropertyChanged(Me, _
                    New PropertyChangedEventArgs("Header"))
            End If
        End Set
    End Property

End Class

