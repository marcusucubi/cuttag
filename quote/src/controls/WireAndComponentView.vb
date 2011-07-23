Public Class WireAndComponentView

    Private WithEvents _DetailCollection As Common.DetailCollection(Of Common.Detail)

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

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, _
                                               ByVal e As System.EventArgs) _
                                           Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count > 0 Then
            Dim i As Common.Detail
            i = ListView1.SelectedItems(0).Tag
            ActiveDetail.ActiveDetail.Detail = i
        Else
            ActiveDetail.ActiveDetail.Detail = Nothing
        End If
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

    Private Sub Sync()

        Dim addList As New List(Of Common.Detail)

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
            ListView1.Refresh()
        Next

    End Sub

End Class
