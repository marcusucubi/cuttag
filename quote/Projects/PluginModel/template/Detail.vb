Imports System.ComponentModel
Imports System.Reflection

Namespace Template

    Public Class Detail
        Inherits Common.Detail

        Private _WireProperties As Common.WireProperties
        Private _ComponentProperties As Common.ComponentProperties

        Public Sub New(ByVal header As Header, ByVal product As Product)
            Me.Header = header
            Me._Product = product
            Me.IsWire = product.IsWire
            Me._ComponentProperties = PropertyFactory.Instance.CreateComponentProperties(Me)
            Me._WireProperties = PropertyFactory.Instance.CreateWireProperties(Me)
            Me._Quantity = 1
            Me.SequenceNumber = Me.Header.NextSequenceNumber
            Me._UOM = product.UnitOfMeasure
        End Sub

        <BrowsableAttribute(False)>
        Property Header As Header

        Public Sub UpdateComponentProperties(ByVal pProduct As Product)
            Me._Product = pProduct
            Me._ComponentProperties = PropertyFactory.Instance.CreateComponentProperties(Me)
            Me._UOM = pProduct.UnitOfMeasure
            If TypeOf Me._WireProperties Is DefaultWireProperties Then
                Dim w As DefaultWireProperties = Me._WireProperties
                w.PoundsPer1000Feet = pProduct.CopperWeightPer1000Ft
            End If
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
