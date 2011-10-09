Imports System.ComponentModel

Namespace Common

	Public Class DetailCollection(Of T)
		Inherits System.ComponentModel.BindingList(Of T)
		'dd_Added 10/8/11
		Private _Sorted As Boolean = False
		Private _SortDirection As ListSortDirection = ListSortDirection.Ascending
		Private _SortProperty As PropertyDescriptor = Nothing
		Protected Overrides ReadOnly Property SupportsSearchingCore As Boolean
			Get
				Return True
			End Get
		End Property
		Protected Overrides ReadOnly Property SupportsSortingCore As Boolean
			Get
				Return True
			End Get
		End Property
		Protected Overrides ReadOnly Property SortDirectionCore As System.ComponentModel.ListSortDirection
			Get
				Return Me._SortDirection
			End Get
		End Property
		Protected Overrides ReadOnly Property SortPropertyCore As System.ComponentModel.PropertyDescriptor
			Get
				Return Me._SortProperty
			End Get
		End Property
		Protected Overrides Sub ApplySortCore(ByVal prop As System.ComponentModel.PropertyDescriptor, ByVal direction As System.ComponentModel.ListSortDirection)
			Me._SortDirection = direction
			Me._SortProperty = prop
			Dim oComparer As New DetailSortComparer(prop, direction)
			ApplySort2Detail(oComparer)
		End Sub
		Private Sub ApplySort2Detail(ByVal oComparer As DetailSortComparer)
			Dim listRef As List(Of Detail) = MyBase.Items
			If Not IsNothing(listRef) Then
				listRef.Sort(oComparer)
				_Sorted = True
				OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
			End If
		End Sub
		'dd_Added end

	End Class

End Namespace
