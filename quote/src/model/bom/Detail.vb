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
            Me.IsWire = product.IsWire
            Me._WireProperties = New WireProperties(Me)
            'dd_Added 12/18/11

            'dd_Added End
            Me._ComponentProperties = New ComponentProperties(Me)
			Me._Quantity = 1
            Me.SequenceNumber = Me.Header.NextSequenceNumber
            Me._UOM = product.UnitOfMeasure 'dd_Added 12/16/11
            '    SetupUOM()
        End Sub

        'Private Sub SetupUOM()
        '    If (_Product.IsWire) Then
        '        _UOM = "Decimeter"
        '    Else
        '        _UOM = "Each"
        '        _UOM = "Test"
        '    End If
        'End Sub

        <BrowsableAttribute(False)>
        Property Header As Header

        Public Sub UpdateComponentProperties(ByVal pProduct As Product)
            Me._Product = pProduct
            Me._ComponentProperties = New ComponentProperties(Me)
            Me._UOM = pProduct.UnitOfMeasure 'dd_Added 12/16/11
            Me._WireProperties.PoundsPer1000Feet = pProduct.CopperWeightPer1000Ft 'dd_Added Wt 12/30/11
            ' SetupUOM() 'dd_remmed 12/16/11
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
