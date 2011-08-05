Imports System.ComponentModel
Imports System.Reflection

Namespace Model.BOM

    Public Class Detail
        Inherits Common.Detail

        Private _WireProperties As WireProperties
        Private _ComponentProperties As ComponentProperties

        Friend Sub New(ByVal header As Header, ByVal product As Product)
            Me.Header = header
            Me._Product = product
            Me._WireProperties = New WireProperties(Me)
            Me._ComponentProperties = New ComponentProperties(Me)
            Me._Quantity = 1
        End Sub

        <BrowsableAttribute(False)>
        Property Header As Header

        <BrowsableAttribute(False)>
        Public Overrides ReadOnly Property QuoteDetailProperties As Object
            Get
                If Me._Product.UnitOfMeasure = UnitOfMeasure.BY_LENGTH Then
                    Return _WireProperties
                End If
                Return _ComponentProperties
            End Get
        End Property

    End Class
End Namespace
