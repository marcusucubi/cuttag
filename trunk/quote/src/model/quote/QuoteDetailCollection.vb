Imports System.ComponentModel

Namespace Model.Quote

    Public Class QuoteDetailCollection
        Inherits System.ComponentModel.BindingList(Of QuoteDetail)

#Region " Types "

        Private Class QuoteHeaderComparer
            Implements System.Collections.Generic.IComparer(Of QuoteDetail)

            Private prop As PropertyDescriptor
            Private direction As ListSortDirection

            Public Function Compare(ByVal x As QuoteDetail, _
            ByVal y As QuoteDetail) As Integer Implements IComparer(Of QuoteDetail).Compare

                Dim result As Integer = DirectCast(Me.prop.GetValue(x), IComparable).CompareTo(Me.prop.GetValue(y))

                If Me.direction = ListSortDirection.Descending Then
                    result = -result
                End If

                Return result
            End Function

            Public Sub New(ByVal prop As PropertyDescriptor, ByVal direction As ListSortDirection)
                Me.prop = prop
                Me.direction = direction
            End Sub

        End Class

#End Region 'Types

#Region " Variables "

        Private _sort As String
        Private _sortProperty As PropertyDescriptor
        Private _sortDirection As ListSortDirection

#End Region 'Variables

#Region " Properties "

        Public Property Sort() As String
            Get
                Return Me._sort
            End Get
            Set(ByVal value As String)
                Dim prop As PropertyDescriptor = Nothing
                Dim direction As ListSortDirection

                Me.ParseSortClause(value, prop, direction)
                Me._sort = value

                If prop Is Nothing Then
                    Me.RemoveSortCore()
                Else
                    Me.ApplySortCore(prop, direction)
                End If
            End Set
        End Property

        Protected Overrides ReadOnly Property SortDirectionCore() As System.ComponentModel.ListSortDirection
            Get
                Return Me._sortDirection
            End Get
        End Property

        Protected Overrides ReadOnly Property SortPropertyCore() As System.ComponentModel.PropertyDescriptor
            Get
                Return Me._sortProperty
            End Get
        End Property

        Protected Overrides ReadOnly Property SupportsSortingCore() As Boolean
            Get
                Return True
            End Get
        End Property

#End Region 'Properties

#Region " Methods "

        Protected Overrides Sub ApplySortCore(ByVal prop As PropertyDescriptor, ByVal direction As ListSortDirection)
            Me._sortProperty = prop
            Me._sortDirection = direction

            Dim upperBound As Integer = Me.Items.Count - 1
            Dim items(upperBound) As QuoteDetail

            Me.Items.CopyTo(items, 0)

            Array.Sort(items, _
            New QuoteHeaderComparer(prop, _
            direction))

            For index As Integer = 0 To upperBound
                Me.Items(index) = items(index)
            Next

            Me.OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, 0))
        End Sub

        Protected Overrides Sub RemoveSortCore()
            Me._sortProperty = Nothing
            Me._sortDirection = Nothing
        End Sub

        Private Sub ParseSortClause(ByVal clause As String, _
                                    ByRef prop As PropertyDescriptor, _
                                    ByRef direction As ListSortDirection)

            If clause IsNot Nothing AndAlso clause.Trim() <> String.Empty Then
                Dim parts As String() = clause.Split(" "c)

                If parts.Length > 2 Then
                    Throw New ArgumentException("Invalid sort clause")
                End If

                For index As Integer = 0 To parts.GetUpperBound(0)
                    parts(index) = parts(index).Trim()
                Next

                prop = TypeDescriptor.GetProperties(GetType(QuoteDetail))(parts(0))

                If prop Is Nothing Then
                    Throw New ArgumentException("Invalid property name")
                End If

                If parts.Length = 1 OrElse _
                parts(1) = String.Empty OrElse _
                String.Compare(parts(1), "ASC", True) = 0 Then
                    direction = ListSortDirection.Ascending
                ElseIf String.Compare(parts(1), "DESC", True) = 0 Then
                    direction = ListSortDirection.Descending
                Else
                    Throw New ArgumentException("Invalid sort direction")
                End If
            End If
        End Sub

#End Region 'Methods

    End Class

End Namespace
