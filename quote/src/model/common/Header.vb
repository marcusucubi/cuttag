﻿Imports DCS.Quote.Common

Namespace Common

    Public MustInherit Class Header
        Inherits SaveableProperties

        Protected _PrimaryProperties As Common.PrimaryPropeties
        Protected _OtherProperties As Common.OtherProperties
        Protected _ComputationProperties As Common.ComputationProperties
        Protected _CustomProperties As New SaveableProperties()
        Protected _CustomPropertiesFactory As Common.CustomPropertiesGenerator
        Protected WithEvents _Details As New DetailCollection(Of Common.Detail)

        Public Property ID As Integer
        Public Property WeightProperties As New Common.Weights(Me)
        Public Property IsQuote As Boolean

        Public ReadOnly Property ComputationProperties As Common.ComputationProperties
            Get
                Return _ComputationProperties
            End Get
        End Property

        Public ReadOnly Property OtherProperties As Common.OtherProperties
            Get
                Return _OtherProperties
            End Get
        End Property

        Public ReadOnly Property PrimaryProperties As Common.PrimaryPropeties
            Get
                Return _PrimaryProperties
            End Get
        End Property

        Public ReadOnly Property CustomProperties As SaveableProperties
            Get
                Return _CustomProperties
            End Get
        End Property

        Public ReadOnly Property CustomPropertiesFactory As Common.CustomPropertiesGenerator
            Get
                Return _CustomPropertiesFactory
            End Get
        End Property

        Public ReadOnly Property Details As DetailCollection(Of Common.Detail)
            Get
                Return _Details
            End Get
        End Property

        Public ReadOnly Property DisplayName As String
            Get
                Dim s As String
                If IsQuote Then
                    s = "Quote"
                Else
                    s = "Template"
                End If
                If Me.PrimaryProperties.CommonID = 0 Then
                    Return "New " + s
                End If
                Return s & " " & Me.PrimaryProperties.CommonID
            End Get
        End Property

        Public Sub GenerateCustomProperties()
            Me._CustomProperties = Me._CustomPropertiesFactory.Generate
        End Sub

        Public MustOverride Function NewDetail(ByVal product As Model.Product) As Detail

    End Class

End Namespace