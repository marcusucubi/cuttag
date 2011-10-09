﻿Imports System.ComponentModel
Imports System.Reflection

Namespace Common

	Public MustInherit Class Detail
		Inherits SaveableProperties

		Protected _Quantity As Decimal = 1
		Protected _Product As Model.Product
		Protected _QuoteDetailProperties As Object
		'dd_added 10/2/11
		Protected _SequenceNumber As Integer = 1
		Protected _SourceID As Guid
		Protected _IsWire As Boolean
		'dd_added end

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

		'dd_added Set 9/26/11
		Public Property ProductCode As String
			Get
				Return Product.Code.Trim
			End Get
			Set(ByVal value As String)
				If Not (value = Product.Code) Then
					Product.Code = value
					SendEvents()
				End If
			End Set
		End Property
		'dd_added Property 10/8/11
		Public Property SequenceNumber As Integer
			Get
				Return _SequenceNumber
			End Get
			Set(ByVal value As Integer)
				If Not (value = _SequenceNumber) Then
					Me._SequenceNumber = value
					SendEvents()
				End If
			End Set
		End Property
		'dd_added 10/3/11
		Public Property SourceID As Guid
			Get
				Return _SourceID
			End Get
			Set(ByVal value As Guid)
				_SourceID = value
			End Set
		End Property
		'dd_added Property 10/2/11
		Public Property IsWire As Boolean
			Get
				Return _IsWire
			End Get
			Set(ByVal value As Boolean)
				If Not (value = Me._IsWire) Then
					_IsWire = value
				End If
			End Set
		End Property
		'dd_added End

		Public Property UnitCost As Decimal
			Get
				Return Product.UnitCost
			End Get
			Set(ByVal value As Decimal)
				Product.UnitCost = value
				SendEvents()
			End Set
		End Property

		<Browsable(False)> _
		Public ReadOnly Property LengthFeet As Decimal
			Get
				Return Math.Round(Qty / 3.048, 4)
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

		Public ReadOnly Property TotalCost As Decimal
			Get
				If Product.UnitOfMeasure = Model.UnitOfMeasure.BY_EACH Then
					Return Math.Round(Me.UnitCost * Me.Qty, 2)
				Else
					Return Math.Round(Me.UnitCost * Me.LengthFeet, 2)
				End If
			End Get
		End Property

	End Class
End Namespace
