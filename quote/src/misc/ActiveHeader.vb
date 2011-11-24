Imports System.ComponentModel
Imports DCS.Quote.Common

Public Class ActiveHeader
    Implements INotifyPropertyChanged

    Private _Header As Header

    Public Shared ReadOnly ActiveHeader As ActiveHeader = New ActiveHeader
    'dd_added 11/21/11
    Public Shared HideReadOnlyProperties As Boolean
    Public Event PropertyChanged As PropertyChangedEventHandler _
        Implements INotifyPropertyChanged.PropertyChanged

    Public Property Header As Header
        Get
            Return _Header
        End Get
        Friend Set(ByVal value As Header)
            If (_Header IsNot value) Then
                _Header = value
                RaiseEvent PropertyChanged(Me, _
                    New PropertyChangedEventArgs("Header"))
            End If
        End Set
    End Property

End Class

