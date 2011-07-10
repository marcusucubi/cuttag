Imports System.ComponentModel
Imports System.Reflection

Namespace Common

    Public MustInherit Class Detail
        Inherits SaveableProperties

        Protected _Product As Model.Product
        Protected _QuoteDetailProperties As Object

        Public Property Qty() As Decimal

        Public ReadOnly Property ProductCode As String
            Get
                Return Product.Code.Trim
            End Get
        End Property

        Public ReadOnly Property UnitCost As Decimal
            Get
                Return Product.UnitCost
            End Get
        End Property

        <BrowsableAttribute(False)>
        Public ReadOnly Property Product As Model.Product
            Get
                Return _Product
            End Get
        End Property

        Public Overridable ReadOnly Property QuoteDetailProperties As Object
            Get
                Return _QuoteDetailProperties
            End Get
        End Property

        <BrowsableAttribute(True), DisplayName("Type")>
        Public ReadOnly Property DisplayableProductClass As String
            Get
                Return IIf(Product.UnitOfMeasure = Model.UnitOfMeasure.BY_EACH, _
                           "Component", "Wire")
            End Get
        End Property

    End Class
End Namespace
