Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.ComponentModel.Design

Namespace Common

    Public MustInherit Class CustomPropertiesGenerator
        Inherits SaveableProperties

        Private WithEvents _PropInfos As New List(Of PropInfo)

        Public Class PropInfo
            Implements ICloneable

            Private Shared _Count As Integer
            Private _Name As String = "Property"

            Public Property Name As String
                Get
                    Return _Name
                End Get
                Set(ByVal value As String)
                    _Name = value
                    _Name = _Name.Replace(" ", "")
                    _Name = _Name.Replace(".", "")
                    _Name = _Name.Replace("(", "")
                    _Name = _Name.Replace(")", "")
                    _Name = _Name.Replace("$", "")
                    _Name = _Name.Replace("!", "")
                    _Name = _Name.Replace("-", "")
                End Set
            End Property

            Public Property Expression As String

            Public Sub New()
                _Count = _Count + 1
                _Name = _Name & _Count
            End Sub

            Public Function Clone() As Object Implements System.ICloneable.Clone
                Return MemberwiseClone()
            End Function

        End Class

        Public ReadOnly Property Properties As List(Of PropInfo)
            Get
                Return _PropInfos
            End Get
        End Property

        Public MustOverride Function Generate() As SaveableProperties

    End Class

End Namespace
