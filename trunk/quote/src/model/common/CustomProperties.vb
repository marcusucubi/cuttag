Imports System.ComponentModel

Namespace Common

    Public Class CustomProperties
        Inherits SaveableProperties

        <DisplayName("Property")> _
        Public Class PropInfo
            Public Property Name As String
            Public Property Expression As String
        End Class

        Public Class CustomPropertyFactory

            Public Property Parent As CustomProperties

            Public Sub New(ByVal parent As CustomProperties)
                Me.Parent = parent
            End Sub

            Public Function GenerateProperties() As SaveableProperties
                Return Parent
            End Function

        End Class

        Private _PropInfos As New List(Of PropInfo)
        Private _CustomPropertyFactory As New CustomPropertyFactory(Me)

        Public ReadOnly Property Properties As List(Of PropInfo)
            Get
                Return _PropInfos
            End Get
        End Property

        <Browsable(False)> _
        Public Property MyPropertyFactory As CustomPropertyFactory
            Get
                Return _CustomPropertyFactory
            End Get
            Set(ByVal value As CustomPropertyFactory)
                _CustomPropertyFactory = value
            End Set
        End Property

        <Browsable(False)> _
        Public ReadOnly Property MyObject As SaveableProperties
            Get
                Return _CustomPropertyFactory.GenerateProperties
            End Get
        End Property

        Public Sub Add(ByVal Name As String)
            Dim info As New PropInfo
            info.Name = Name
            _PropInfos.Add(info)
        End Sub

    End Class

End Namespace
