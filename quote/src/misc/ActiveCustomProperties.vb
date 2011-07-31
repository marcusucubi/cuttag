Imports DCS.Quote.Common
Imports System.ComponentModel

Public Class ActiveCustomProperties
    Implements INotifyPropertyChanged

    Private _CustomProperties As New SaveableProperties
    Private _CustomPropertiesGenerator As New Model.BOM.CustomPropertiesGenerator

    Public Event PropertyChanged As PropertyChangedEventHandler _
        Implements INotifyPropertyChanged.PropertyChanged

    Public ReadOnly Property Properties As SaveableProperties
        Get
            Return _CustomProperties
        End Get
    End Property

    Public ReadOnly Property Generator As CustomPropertiesGenerator
        Get
            Return _CustomPropertiesGenerator
        End Get
    End Property

    Public Shared Property ActiveCustomProperties As New ActiveCustomProperties

    Public Sub GenerateCustomProperties()
        _CustomProperties = _CustomPropertiesGenerator.Generate
        RaiseEvent PropertyChanged(Me, _
            New PropertyChangedEventArgs("Properties"))
    End Sub

    Public Sub Save()
        CommonSaver.DeleteCustomProperties()
        CommonSaver.SaveCustomPropertiesGenerator(_CustomProperties)
    End Sub

    Public Sub Load()
        CommonLoader.LoadCustomPropertiesGenerator(_CustomPropertiesGenerator)
        GenerateCustomProperties()
    End Sub

End Class
