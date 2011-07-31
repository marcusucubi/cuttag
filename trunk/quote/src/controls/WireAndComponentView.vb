Public Class WireAndComponentView

    Private WithEvents _DetailCollection As Common.DetailCollection(Of Common.Detail)
    Private _Sorter As New ListViewColumnSorter

    Public Property DetailCollection As Common.DetailCollection(Of Common.Detail)
        Get
            Return _DetailCollection
        End Get
        Set(ByVal value As Common.DetailCollection(Of Common.Detail))
            _DetailCollection = value
            Sync()
        End Set
    End Property

    Public Sub New()
        InitializeComponent()
        InitComponent()
    End Sub

    Private Sub InitComponent()

    End Sub

    Private Sub ListView1_ColumnClick(ByVal sender As Object, _
                                      ByVal e As System.Windows.Forms.ColumnClickEventArgs) _
                                    Handles ListView1.ColumnClick
        Dim c As ColumnHeader = Me.ListView1.Columns(e.Column)

        If (e.Column = _Sorter.SortColumn) Then
            If (_Sorter.Order = SortOrder.Ascending) Then
                _Sorter.Order = SortOrder.Descending
            Else
                _Sorter.Order = SortOrder.Ascending
            End If
        Else
            _Sorter.SortColumn = e.Column
            _Sorter.Order = SortOrder.Ascending
        End If
        ListView1.ListViewItemSorter = _Sorter
        ListView1.Sort()

    End Sub

    Private Sub ListView1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.GotFocus
        SelectDetail()
    End Sub

    Private Sub ListView1_ItemActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.ItemActivate
        SelectDetail()
    End Sub

    Private Sub ListView1_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles ListView1.ItemSelectionChanged
        SelectDetail()
    End Sub

    Private Sub _DetailCollection_ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles _DetailCollection.ListChanged
        Sync()
    End Sub

    Private Sub WireAndComponentView_Resize(ByVal sender As Object, _
                                            ByVal e As System.EventArgs) _
                                        Handles Me.Resize
        Dim size As Integer
        For Each col As ColumnHeader In Me.ListView1.Columns
            If col.DisplayIndex > 0 Then
                size = size + col.Width
            End If
        Next
        Dim left As ColumnHeader = Me.ListView1.Columns(0)
        left.Width = Me.ListView1.Width - size
    End Sub

    Private Sub SelectDetail()
        If ListView1.SelectedItems.Count > 0 Then
            Dim i As Common.Detail
            i = ListView1.SelectedItems(0).Tag
            ActiveDetail.ActiveDetail.Detail = i
        Else
            ActiveDetail.ActiveDetail.Detail = Nothing
        End If
    End Sub

    Private Sub Sync()

        If Me._DetailCollection Is Nothing Then
            Return
        End If

        Dim addList As New List(Of Common.Detail)
        Dim removeList As New List(Of WireAndComponentItem)

        For Each o As Common.Detail In Me._DetailCollection
            Dim test As Common.Detail = Nothing
            For Each item As ListViewItem In Me.ListView1.Items
                If item.Tag Is o Then
                    test = item.Tag
                End If
            Next
            If test Is Nothing Then
                addList.Add(o)
            End If
        Next

        For Each o As Common.Detail In addList
            Dim i As New WireAndComponentItem(o)
            ListView1.BeginUpdate()
            ListView1.Items.Add(i)
            ListView1.SelectedItems.Clear()
            ListView1.SelectedIndices.Add(ListView1.Items.IndexOf(i))
            ListView1.EndUpdate()
        Next

        For Each item As ListViewItem In Me.ListView1.Items
            Dim test As Common.Detail = Nothing
            For Each o As Common.Detail In Me._DetailCollection
                If item.Tag Is o Then
                    test = item.Tag
                End If
            Next
            If test Is Nothing Then
                removeList.Add(item)
            End If
        Next

        For Each o As WireAndComponentItem In removeList
            ListView1.BeginUpdate()
            ListView1.Items.Remove(o)
            ListView1.EndUpdate()
        Next
        ListView1.Refresh()

    End Sub

End Class
