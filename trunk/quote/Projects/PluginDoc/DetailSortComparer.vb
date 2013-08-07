Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Globalization
Imports System.Linq

Imports Model.Common

Public Class DetailSortComparer
    Implements IComparer(Of Model.Common.Detail)
    
    Private propDesc As PropertyDescriptor = Nothing

    Private direction As ListSortDirection = ListSortDirection.Ascending

    Public Sub New(propDescriptor As PropertyDescriptor, direction As ListSortDirection)
        Me.propDesc = propDescriptor
        Me.direction = direction
    End Sub

    Public Function Compare(x As Model.Common.Detail, y As Model.Common.Detail) As Integer _
        Implements IComparer(Of Detail).Compare

        Dim retValue As Integer = 0
        Dim leftValue As Object = Me.propDesc.GetValue(x)
        Dim rightValue As Object = Me.propDesc.GetValue(y)
        retValue = CompareValues(leftValue, rightValue, Me.direction)
        
        Return retValue
    End Function

    Private Shared Function IsNumeric(value As String) As Boolean

        Dim result As decimal
        If decimal.TryParse(value, result) Then
            Return True
        End If

        Return False

    End Function

    Private Shared Function CompareValues( _ 
            x As Object, _ 
            y As Object, _
            direction As ListSortDirection) _ 
            As Integer

        Dim retValue As Integer = 0

        If x Is Nothing Then
            retValue = 0
        ElseIf y Is Nothing Then
            retValue = 0
        ElseIf IsNumeric(x.ToString()) And IsNumeric(y.ToString()) Then
            Dim left As Double = Convert.ToDouble(x, CultureInfo.CurrentCulture)
            Dim right As Double = Convert.ToDouble(y, CultureInfo.CurrentCulture)

            retValue = left.CompareTo(right)
        Else
            Dim left As String = x.ToString().ToLower()
            Dim right As String = y.ToString().ToLower()
            
            retValue = String.Compare(left, right, True, CultureInfo.CurrentCulture)
        End If

        If direction = ListSortDirection.Descending Then
            retValue *= -1
        End If

        Return retValue
    End Function
End Class
