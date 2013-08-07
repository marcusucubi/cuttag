Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Linq

Imports Model
Imports Model.Common

Public Class DetailBindingList
    Inherits System.ComponentModel.BindingList(Of Model.Common.Detail)
    Private header As Model.Common.Header

    Private collection As DetailCollection(Of Model.Common.Detail)

    Private sorted As Boolean = False

    Private syncronizing As Boolean = False

    Private sortDirection As ListSortDirection = ListSortDirection.Ascending

    Private sortProperty As PropertyDescriptor = Nothing

    Public Sub New(collection As DetailCollection(Of Model.Common.Detail))
        Me.collection = collection
        Me.header = collection.Header

        Me.syncronizing = True
        For Each detail As Model.Common.Detail In collection
            Me.Add(detail)
        Next
        Me.syncronizing = False
    End Sub

    Protected Overrides ReadOnly Property SupportsSearchingCore() As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overrides ReadOnly Property SupportsSortingCore() As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overrides ReadOnly Property SortDirectionCore() As ListSortDirection
        Get
            Return Me.sortDirection
        End Get
    End Property

    Protected Overrides ReadOnly Property SortPropertyCore() As PropertyDescriptor
        Get
            Return Me.sortProperty
        End Get
    End Property

    Protected Overrides ReadOnly Property IsSortedCore() As Boolean
        Get
            Return Me.sorted
        End Get
    End Property

    Public Overrides Sub CancelNew(itemIndex As Integer)
        MyBase.CancelNew(itemIndex)
        Sync()
    End Sub

    Protected Overrides Function AddNewCore() As Object
        System.Diagnostics.Debug.WriteLine("object AddNewCore()")

        Dim p As New Product()
        Dim o As Model.Common.Detail = New Model.Template.Detail(DirectCast(Me.header, Model.Template.Header), p)
        Me.Add(o)
        Return o
    End Function

    Protected Overrides Sub OnAddingNew(e As System.ComponentModel.AddingNewEventArgs)
        MyBase.OnAddingNew(e)
    End Sub

    Protected Overrides Function FindCore(prop As System.ComponentModel.PropertyDescriptor, key As Object) As Integer
        Return MyBase.FindCore(prop, key)
    End Function

    Protected Overrides Sub InsertItem(index As Integer, item As Model.Common.Detail)
        MyBase.InsertItem(index, item)
        Sync()
    End Sub

    Public Overrides Sub EndNew(itemIndex As Integer)
        MyBase.EndNew(itemIndex)
        Sync()
    End Sub

    Protected Overrides Sub RemoveItem(index As Integer)
        MyBase.RemoveItem(index)
        Sync()
    End Sub

    Protected Overrides Sub ApplySortCore(prop As PropertyDescriptor, direction As ListSortDirection)
        Me.sortDirection = direction
        Me.sortProperty = prop
        Dim comparer As New DetailSortComparer(prop, direction)
        Me.ApplySort2Detail(comparer)
    End Sub

    Private Sub ApplySort2Detail(comparer As DetailSortComparer)

        Dim listRef As List(Of Model.Common.Detail) = TryCast(Me.Items, List(Of Model.Common.Detail))
        
        If listRef IsNot Nothing Then
            listRef.Sort(comparer)
            Me.sorted = True
            Me.OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
        End If
        
    End Sub

    Private Sub Sync()
        If syncronizing Then
            Return
        End If
        syncronizing = True

        Dim addList As List(Of Model.Common.Detail) = New List(Of Detail)()
        Dim removeList As List(Of Model.Common.Detail) = New List(Of Detail)()

        For Each d As Model.Common.Detail In Me
    
            If d.Product.Code Is Nothing Then
                'Continue For
            End If

            If d.Product.Code.Length = 0 Then
                'Continue For
            End If

            If Not Me.collection.Contains(d) Then
                addList.Add(d)
            End If
        Next

        For Each d As Model.Common.Detail In Me.collection
            If Not Me.Contains(d) Then
                removeList.Add(d)
            End If
        Next

        For Each d As Model.Common.Detail In addList
            Me.collection.Add(d)
        Next

        For Each d As Model.Common.Detail In removeList
            Me.collection.Remove(d)
        Next

        syncronizing = False
    End Sub
    
End Class