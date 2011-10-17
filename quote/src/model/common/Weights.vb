Imports System.ComponentModel
Imports DCS.Quote.Model
Imports System.Reflection
Imports DCS.Quote.Model.Quote

Namespace Common
	Public Class Weights

		Class WireWeight
			Sub New(ByVal Gage As String, ByVal Value As Decimal)
				Me.Gage = Gage
				Me.Value = Value
			End Sub
			Property Gage As String
			Property Value As Decimal
		End Class

		Private Shared WeightArray() = New WireWeight() { _
				New WireWeight("18", 0.00159 * 3.048), _
				New WireWeight("16", 0.00242 * 3.048), _
				New WireWeight("14", 0.00387 * 3.048), _
				New WireWeight("12", 0.00602 * 3.048), _
				New WireWeight("10", 0.00959 * 3.048), _
				New WireWeight("8", 0.01541 * 3.048), _
				New WireWeight("6", 0.02657 * 3.048), _
				New WireWeight("4", 0.04206 * 3.048), _
				New WireWeight("2", 0.39), _
				New WireWeight("1", 0.198), _
				New WireWeight("1/0", 0.244), _
				New WireWeight("2/0", 0.311), _
				New WireWeight("3/0", 0.492), _
				New WireWeight("4/0", 0.673), _
				New WireWeight("373 MCM", 1.176), _
				New WireWeight("444 MCM", 1.413), _
				New WireWeight("777 MCM", 2.435) _
		}

		Private Const CATAGORY_1 As String = "Wire Length (1)"
		Private Const CATAGORY_2 As String = "Wire Length (2)"
		Private Const CATAGORY_3 As String = "Wire Length (3)"

		Public Sub New(ByVal Header As Common.Header)
			_Header = Header
		End Sub

		Private WithEvents _Header As Header

		<CategoryAttribute(CATAGORY_1), _
		DisplayName("18"), _
		DescriptionAttribute("Length of Gage 18")> _
		Public ReadOnly Property Gage18 As Decimal
			Get
				Dim gage As Integer = CalcQty("18")
				Return gage
			End Get
		End Property

		<CategoryAttribute(CATAGORY_1), _
		DisplayName("16"), _
		DescriptionAttribute("Length of Gage 16")> _
		Public ReadOnly Property Gage16 As Decimal
			Get
				Dim gage As Integer = CalcQty("16")
				Return gage
			End Get
		End Property

		<CategoryAttribute(CATAGORY_1), _
		DisplayName("14"), _
		DescriptionAttribute("Length of Gage 14")> _
		Public ReadOnly Property Gage14 As Decimal
			Get
				Dim gage As Integer = CalcQty("14")
				Return gage
			End Get
		End Property

		<CategoryAttribute(CATAGORY_1), _
		DisplayName("12"), _
		DescriptionAttribute("Length of Gage 12")> _
		Public ReadOnly Property Gage12 As Decimal
			Get
				Dim gage As Integer = CalcQty("12")
				Return gage
			End Get
		End Property

		<CategoryAttribute(CATAGORY_1), _
		DisplayName("10"), _
		DescriptionAttribute("Length of Gage 10")> _
		Public ReadOnly Property Gage10 As Decimal
			Get
				Dim gage As Integer = CalcQty("10")
				Return gage
			End Get
		End Property

		<CategoryAttribute(CATAGORY_1), _
		DisplayName("08"), _
		DescriptionAttribute("Length of Gage 8")> _
		Public ReadOnly Property Gage08 As Decimal
			Get
				Dim gage As Integer = CalcQty("8")
				Return gage
			End Get
		End Property

		<CategoryAttribute(CATAGORY_1), _
		DisplayName("06"), _
		DescriptionAttribute("Length of Gage 6")> _
		Public ReadOnly Property Gage06 As Decimal
			Get
				Dim gage As Integer = CalcQty("6")
				Return gage
			End Get
		End Property

		<CategoryAttribute(CATAGORY_1), _
		DisplayName("04"), _
		DescriptionAttribute("Length of Gage 4")> _
		Public ReadOnly Property Gage04 As Decimal
			Get
				Dim gage As Integer = CalcQty("4")
				Return gage
			End Get
		End Property

		<CategoryAttribute(CATAGORY_1), _
		DisplayName("02"), _
		DescriptionAttribute("Length of Gage 2")> _
		Public ReadOnly Property Gage02 As Decimal
			Get
				Dim gage As Integer = CalcQty("2")
				Return gage
			End Get
		End Property

		<CategoryAttribute(CATAGORY_1), _
		DisplayName("01"), _
		DescriptionAttribute("Length of Gage 1")> _
		Public ReadOnly Property Gage01 As Decimal
			Get
				Dim gage As Integer = CalcQty("1")
				Return gage
			End Get
		End Property

		<CategoryAttribute(CATAGORY_2), _
		DisplayName("1/0"), _
		DescriptionAttribute("Length of Gage 1/0")> _
		Public ReadOnly Property Gage00_1 As Decimal
			Get
				Dim gage As Integer = CalcQty("1/0")
				Return gage
			End Get
		End Property

		<CategoryAttribute(CATAGORY_2), _
		DisplayName("2/0"), _
		DescriptionAttribute("Length of Gage 2/0")> _
		Public ReadOnly Property Gage00_2 As Decimal
			Get
				Dim gage As Integer = CalcQty("2/0")
				Return gage
			End Get
		End Property

		<CategoryAttribute(CATAGORY_2), _
		DisplayName("3/0"), _
		DescriptionAttribute("Length of Gage 3/0")> _
		Public ReadOnly Property Gage00_3 As Decimal
			Get
				Dim gage As Integer = CalcQty("3/0")
				Return gage
			End Get
		End Property

		<CategoryAttribute(CATAGORY_2), _
		DisplayName("4/0"), _
		DescriptionAttribute("Length of Gage 4/0")> _
		Public ReadOnly Property Gage00_4 As Decimal
			Get
				Dim gage As Integer = CalcQty("4/0")
				Return gage
			End Get
		End Property

		<CategoryAttribute(CATAGORY_3), _
		DisplayName("373 MCM"), _
		DescriptionAttribute("Length of Gage 373 MCM")> _
		Public ReadOnly Property Gage373MCM As Decimal
			Get
				Dim gage As Integer = CalcQty("373 MCM")
				Return gage
			End Get
		End Property

		<CategoryAttribute(CATAGORY_3), _
		DisplayName("444 MCM"), _
		DescriptionAttribute("Length of Gage 444 MCM")> _
		Public ReadOnly Property Gage444MCM As Decimal
			Get
				Dim gage As Integer = CalcQty("444 MCM")
				Return gage
			End Get
		End Property

		<CategoryAttribute(CATAGORY_3), _
		DisplayName("777 MCM"), _
		DescriptionAttribute("Length of Gage 777 MCM")> _
		Public ReadOnly Property Gage777MCM As Decimal
			Get
				Dim gage As Integer = CalcQty("777 MCM")
				Return gage
			End Get
		End Property

		<CategoryAttribute("Total"), _
		DescriptionAttribute("Total Length" + Chr(10) + "(Decimeters)")> _
		Public ReadOnly Property Length As Decimal
			Get
				Return CalcQty(Nothing)
			End Get
		End Property

		<CategoryAttribute("Total"), _
		DescriptionAttribute("Total Weight" + Chr(10) + "(Pounds)")> _
		Public ReadOnly Property Weight As Decimal
			Get
				Return Math.Round(CalcWeight(), 4)
			End Get
		End Property

		Private Function CalcQty(ByVal gage As String) As Decimal
			Dim qty As Decimal
			For Each q As Detail In _Header.Details
				'dd_changed IsWire 10/3/2011
				'If q.Product.UnitOfMeasure = UnitOfMeasure.BY_LENGTH Then
				If q.IsWire Then
					'dd_changed end
					If q.QuoteDetailProperties.Gage = gage Then
						qty += q.LengthFeet
					ElseIf q.QuoteDetailProperties.Gage.Contains("-" & gage) Then
						qty += q.LengthFeet
					ElseIf gage Is Nothing Then
						qty += q.LengthFeet
					End If
				End If
			Next
			Return qty
		End Function

		Private Function CalcWeight() As Decimal
			Dim r As Decimal
			For Each w As WireWeight In WeightArray
				r += FindWeight(w.Gage) * CalcQty(w.Gage)
			Next
			Return r
		End Function

		Public Shared Function FindWeight(ByVal gage As String) As Decimal
			Dim r As Decimal
			For Each w As WireWeight In WeightArray
				If w.Gage = gage Then
					r = w.Value
				ElseIf w.Gage.Contains("-" & gage) Then
					r = w.Value
				ElseIf gage Is Nothing Then
					r = w.Value
				End If
			Next
			Return r
		End Function

	End Class
End Namespace
