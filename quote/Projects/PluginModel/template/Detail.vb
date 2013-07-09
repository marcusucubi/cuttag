Imports System.ComponentModel
Imports System.Reflection

Namespace Template

    Public Class Detail
        Inherits Common.Detail

        Private _WireProperties As DisplayableWireProperties
        Private _ComponentProperties As DisplayableComponentProperties

        Public Sub New(ByVal header As Header, ByVal product As Product)
            Me.Header = header
            Me._Product = product
            Me.IsWire = product.IsWire
            Me._WireProperties = New DisplayableWireProperties(New WireProperties(Me))
            Me._ComponentProperties = New DisplayableComponentProperties(New ComponentProperties(Me))
            Me._Quantity = 1
            Me.SequenceNumber = Me.Header.NextSequenceNumber
            Me._UOM = product.UnitOfMeasure
        End Sub

        <BrowsableAttribute(False)>
        Property Header As Header

        Public Sub UpdateComponentProperties(ByVal pProduct As Product)
            Me._Product = pProduct
            Me._ComponentProperties = New DisplayableComponentProperties(New ComponentProperties(Me))
            Me._UOM = pProduct.UnitOfMeasure
            Me._WireProperties.PoundsPer1000Feet = pProduct.CopperWeightPer1000Ft
        End Sub

        <BrowsableAttribute(False)>
        Public Overrides ReadOnly Property QuoteDetailProperties As Object
            Get
                If Me.IsWire Then
                    Return _WireProperties
                Else
                    Return _ComponentProperties
                End If
            End Get
        End Property

    End Class
End Namespace
