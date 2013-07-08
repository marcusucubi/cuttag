Imports System.ComponentModel

Public Class DisplaySettings
    Implements System.ComponentModel.INotifyPropertyChanged

    Private Shared m_Instance As New DisplaySettings

    Private m_HideReadOnlyProperties As Boolean = False

    Public Event PropertyChanged As PropertyChangedEventHandler _
            Implements INotifyPropertyChanged.PropertyChanged

    Public Shared ReadOnly Property Instance As DisplaySettings
        Get
            Return m_Instance
        End Get
    End Property

    Public Property HideReadOnlyProperties As Boolean
        Get
            Return m_HideReadOnlyProperties
        End Get
        Set(value As Boolean)
            If (m_HideReadOnlyProperties <> value) Then
                m_HideReadOnlyProperties = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("HideReadOnlyProperties"))
            End If
        End Set
    End Property

End Class
