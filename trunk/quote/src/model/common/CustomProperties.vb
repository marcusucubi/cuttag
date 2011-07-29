Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing.Design

Namespace Common

    Public Class CustomProperties
        Inherits SaveableProperties

        Private _Parent As Common.Header

        Public Sub New(ByVal Parent As Common.Header)
            _Parent = Parent
        End Sub

        Public ReadOnly Property Test As String
            Get
                Return ""
            End Get
        End Property

    End Class

End Namespace
