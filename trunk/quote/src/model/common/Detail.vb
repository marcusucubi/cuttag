Imports System.ComponentModel
Imports System.Reflection

Namespace Common

    Public MustInherit Class Detail
        Inherits SaveableProperties

        Protected _QuoteDetailProperties As Object

        Public Property TotalCost As Decimal
        Public Property ProductCode As String
        Public Property UnitCost As Decimal
        <BrowsableAttribute(True), DisplayName("Type")>
        Public Property DisplayableProductClass As String
        Public Property Qty() As Decimal
        <BrowsableAttribute(False)>
        Public Property Product As Model.Product

        Public Overridable ReadOnly Property QuoteDetailProperties As Object
            Get
                Return _QuoteDetailProperties
            End Get
        End Property

    End Class
End Namespace
