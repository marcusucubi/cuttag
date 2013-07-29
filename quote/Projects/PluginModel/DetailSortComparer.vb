Imports System.ComponentModel
Imports System.Globalization

Namespace Common
    Public Class DetailSortComparer
        Implements IComparer(Of Detail)
        Private _PropDesc As PropertyDescriptor = Nothing
        Private _Direction As ListSortDirection = ListSortDirection.Ascending
        Public Sub New(ByVal propDescriptor As PropertyDescriptor, ByVal direction As ListSortDirection)
            Me._PropDesc = propDescriptor
            Me._Direction = direction
        End Sub
        Public Function Compare(ByVal x As Detail, ByVal y As Detail) As Integer Implements IComparer(Of Detail).Compare
            Dim retValue As Integer
            Dim xValue As Object = _PropDesc.GetValue(x)
            Dim yValue As Object = _PropDesc.GetValue(y)
            retValue = CompareValues(xValue, yValue, _Direction)
            Return retValue
        End Function

        Private Shared Function CompareValues(ByVal x As Object, ByVal y As Object, ByVal direction As ListSortDirection) As Integer
            Dim retValue As Integer = 0
            If IsNumeric(x) And IsNumeric(y) Then
                retValue = CType(x, Double).CompareTo(CType(y, Double))
            ElseIf x Is Nothing Then
                retValue = 0
            ElseIf y Is Nothing Then
                retValue = 0
            Else
                retValue = String.Compare(x.ToString(), y.ToString(), True, CultureInfo.CurrentCulture)
            End If
            If direction = ListSortDirection.Descending Then
                retValue *= -1
            End If
            Return retValue
        End Function

    End Class


End Namespace

