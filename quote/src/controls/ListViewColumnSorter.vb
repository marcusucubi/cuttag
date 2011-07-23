Public Class ListViewColumnSorter
    Implements IComparer

    Public Property SortColumn As Integer
    Public Property Order As SortOrder
    Public Property ObjectCompare As CaseInsensitiveComparer

    Public Sub New()
        SortColumn = 0
        Order = SortOrder.None
        ObjectCompare = New CaseInsensitiveComparer()
    End Sub

    Public Function Compare(ByVal x, ByVal y) As Integer _
        Implements IComparer.Compare

        Dim listviewX As ListViewItem = x
        Dim listviewY As ListViewItem = y

        Dim s1 As String = listviewX.SubItems(SortColumn).Text
        Dim s2 As String = listviewY.SubItems(SortColumn).Text

        Dim compareResult As Integer
        compareResult = (s1.CompareTo(s2))

        If Order = SortOrder.Ascending Then
            Return compareResult
        ElseIf (Order = SortOrder.Descending) Then
            Return (-compareResult)
        Else
            Return 0
        End If

    End Function

End Class
