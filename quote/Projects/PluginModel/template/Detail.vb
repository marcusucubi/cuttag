Imports System.ComponentModel
Imports System.Reflection

Namespace Template

    Public Class Detail
        Inherits Common.Detail

        Private _WireProperties As Common.WireProperties
        Private _ComponentProperties As Common.ComponentProperties

        Public Sub New(ByVal header As Header, ByVal product As Product)
            MyBase.New(product, product.UnitOfMeasure)
    
            Me.Header = header
            Me.IsWire = product.IsWire
            Me._ComponentProperties = PropertyFactory.CreateComponentProperties(Me)
            Me._WireProperties = PropertyFactory.CreateWireProperties(Me)
            Me.SequenceNumber = Me.Header.NextSequenceNumber
        End Sub

        <BrowsableAttribute(False)>
        Property Header As Header

        Public Sub UpdateComponentProperties(ByVal product As Product)
    
            MyBase.SetProduct(product)
            MyBase.SetPrivateUnitOfMeasure(product.UnitOfMeasure)
            
            Me._ComponentProperties = PropertyFactory.CreateComponentProperties(Me)
            
            If TypeOf Me._WireProperties Is DefaultWireProperties Then
                Dim w As DefaultWireProperties = Me._WireProperties
                w.PoundsPer1000Feet = product.CopperWeightPer1000Feet
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
