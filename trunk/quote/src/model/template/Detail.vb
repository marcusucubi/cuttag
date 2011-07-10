Imports System.ComponentModel
Imports System.Reflection

Namespace Model.Template

    Public Class Detail
        Inherits Common.Detail

        Private _Quantity As Decimal
        Private _WireProperties As New WireProperties(Me)
        Private _ComponentProperties As New ComponentProperties(Me)

        <BrowsableAttribute(False)>
        Property Header As Header

        Public Shadows Property Qty() As Decimal
            Get
                Return Me._Quantity
            End Get

            Set(ByVal value As Decimal)
                If Not (value = _Quantity) Then
                    Me._Quantity = value
                    SendEvents()
                End If
            End Set
        End Property

        Public ReadOnly Property TotalCost As Decimal
            Get
                Return Me.UnitCost * Me.Qty
            End Get
        End Property

        <BrowsableAttribute(False)>
        Public Overrides ReadOnly Property QuoteDetailProperties As Object
            Get
                If Me._Product.UnitOfMeasure = UnitOfMeasure.BY_LENGTH Then
                    Return _WireProperties
                End If
                Return _ComponentProperties
            End Get
        End Property

        Friend Sub New(ByVal header As Header, ByVal product As Product)
            Me.Header = header
            Me._Product = product
            Me._Quantity = 1
        End Sub

    End Class
End Namespace
