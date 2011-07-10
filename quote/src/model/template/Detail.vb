Imports System.ComponentModel
Imports System.Reflection

Namespace Model.Template

    Public Class Detail
        Inherits Common.Detail

        Private _Quantity As Decimal
        Private _Product As Product
        Private _WireProperties As New WireProperties(Me)
        Private _ComponentProperties As New ComponentProperties(Me)

        <BrowsableAttribute(False)>
        Property Header As Header

        Public Overloads ReadOnly Property TotalCost As Decimal
            Get
                Return Me.UnitCost * Me.Qty
            End Get
        End Property

        <BrowsableAttribute(False)>
        Public Overloads ReadOnly Property Product As Product
            Get
                Return _Product
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

        Public Overloads ReadOnly Property ProductCode As String
            Get
                Return Product.Code.Trim
            End Get
        End Property

        Public Overloads ReadOnly Property UnitCost As Decimal
            Get
                Return Product.UnitCost
            End Get
        End Property

        <BrowsableAttribute(True), DisplayName("Type")>
        Public Overloads ReadOnly Property DisplayableProductClass As String
            Get
                Return IIf(Product.UnitOfMeasure = UnitOfMeasure.BY_EACH, "Component", "Wire")
            End Get
        End Property

        Public Overloads Property Qty() As Decimal
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

#Region "Methods"

        Friend Sub New(ByVal header As Header, ByVal product As Product)
            Me.Header = header
            Me._Product = product
            Me._Quantity = 1
        End Sub

#End Region

    End Class
End Namespace
