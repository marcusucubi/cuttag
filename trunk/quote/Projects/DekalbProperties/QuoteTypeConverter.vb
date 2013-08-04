Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq

Public Class QuoteTypeConverter
	Inherits System.ComponentModel.StringConverter
	Public Const Production As String = "Production"

	Public Const Pilot As String = "Pilot"

	Public Const Prove As String = "Prove"

	Public Const SingleDefinite As String = "Single Definate"

	Public Overrides Function GetStandardValues(context As System.ComponentModel.ITypeDescriptorContext) As TypeConverter.StandardValuesCollection
		Dim l As New List(Of String)()
		l.AddRange(New String() {Production, Pilot, Prove, SingleDefinite})

		Return New StandardValuesCollection(l)
	End Function

	Public Overrides Function GetStandardValuesExclusive(context As System.ComponentModel.ITypeDescriptorContext) As Boolean
		Return True
	End Function

	Public Overrides Function GetStandardValuesSupported(context As ITypeDescriptorContext) As Boolean
		Return True
	End Function
End Class
