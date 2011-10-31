Imports System.ComponentModel
Imports DCS.Quote.Model

Namespace Common

    Public Class DetailCollection(Of T)
        Inherits System.ComponentModel.BindingList(Of T)

        Private _Header As Common.Header

        Public Sub New(ByVal pHeader As Common.Header)
            _Header = pHeader
        End Sub

        'dd_Added 10/8/11
        Private _IsSorted As Boolean = False
        Private _SortDirection As ListSortDirection = ListSortDirection.Ascending
        Private _SortProperty As PropertyDescriptor = Nothing

        Protected Overrides Function AddNewCore() As Object
            Dim p As New Product("", 0, "", False, Nothing, Nothing) '("100", 2, "10", 1, True, "test", 0, "", 0, 0)
            'Dim p As New Product("", 1, "10", False, Nothing, Nothing)
            Dim o As Object = New Quote.Model.BOM.Detail(_Header, p) 'ddAdded
            ' Return New Quote.Model.BOM.Detail(_Header, p) 'ddRemmed
            Add(o) 'ddAdded
            Return o 'ddAdded

            'Return MyBase.AddNewCore()
        End Function

        Protected Overrides Sub OnAddingNew(ByVal e As System.ComponentModel.AddingNewEventArgs)
            MyBase.OnAddingNew(e)
        End Sub

        Public Overrides Sub CancelNew(ByVal itemIndex As Integer)
            MyBase.CancelNew(itemIndex)
        End Sub



        Protected Overrides Function FindCore(ByVal prop As System.ComponentModel.PropertyDescriptor, ByVal key As Object) As Integer
            Return MyBase.FindCore(prop, key)
        End Function

        Protected Overrides Sub InsertItem(ByVal index As Integer, ByVal item As T)
            MyBase.InsertItem(index, item)
        End Sub

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
        Protected Overrides ReadOnly Property IsSortedCore As Boolean
            Get
                Return Me._IsSorted
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
                _IsSorted = True
                OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
            End If
        End Sub
        'dd_Added end

    End Class

End Namespace
