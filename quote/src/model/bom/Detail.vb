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
			'dd_added 10/8/11
			Me.SequenceNumber = Me.Header.NextSequenceNumber
			'dd_added end

		End Sub

		<BrowsableAttribute(False)>
		Property Header As Header
		'dd_added 10/8/11
		Public Sub UpdateComponentProperties(ByVal pProduct As Product)
			Me._Product = pProduct
			Me._ComponentProperties = New ComponentProperties(Me)
		End Sub
		'dd_added end

		<BrowsableAttribute(False)>
		Public Overrides ReadOnly Property QuoteDetailProperties As Object
			Get
				'dd_changed 10/3/11
				'	If Me._Product.UnitOfMeasure = UnitOfMeasure.BY_LENGTH Then
				'		Return _WireProperties
				'	End If
				'	Return _ComponentProperties
				If Me.IsWire Then
					Return _WireProperties
				Else
					Return _ComponentProperties
				End If
				'dd_changed End
			End Get
		End Property

	End Class
End Namespace
