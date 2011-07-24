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

        If IsInteger(s1) And IsInteger(s2) Then
            Dim i1 As Integer = CInt(s1)
            Dim i2 As Integer = CInt(s2)
            Dim compareResult As Integer
            compareResult = (i1.CompareTo(i2))

            If Order = SortOrder.Ascending Then
                Return compareResult
            ElseIf (Order = SortOrder.Descending) Then
                Return (-compareResult)
            End If
        Else
            Dim compareResult As Integer
            compareResult = (s1.CompareTo(s2))

            If Order = SortOrder.Ascending Then
                Return compareResult
            ElseIf (Order = SortOrder.Descending) Then
                Return (-compareResult)
            End If
        End If

        Return 0

    End Function

    Private Function IsInteger(ByVal s As String) As Boolean
        Return IsNumeric(s)
    End Function

End Class
