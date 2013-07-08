Imports System.Windows.Forms

Public Class CustomPropertyGrid
    Inherits PropertyGrid

    Private WithEvents _Settings As DisplaySettings = DisplaySettings.Instance

    Public Shadows Property SelectedObject As Object
        Get
            Return MyBase.SelectedObject
        End Get
        Set(value As Object)
            If value Is Nothing Then
                MyBase.SelectedObject = Nothing
            Else
                Dim wrapper As New ObjectWrapper(value)
                MyBase.SelectedObject = wrapper
            End If
        End Set
    End Property

    Private Sub _Settings_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles _Settings.PropertyChanged
        Dim t As Object
        t = MyBase.SelectedObject
        MyBase.SelectedObject = Nothing
        MyBase.SelectedObject = t
    End Sub

End Class
