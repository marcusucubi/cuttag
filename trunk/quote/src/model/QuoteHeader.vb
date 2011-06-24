Imports System.ComponentModel

Public Class QuoteHeader
    Implements INotifyPropertyChanged

    Private _qty As Integer
    Protected _col As New List(Of QuoteDetail)

    Public Sub Remove(ByVal detail As QuoteDetail)
        _col.Remove(detail)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Qty"))
    End Sub

    Public ReadOnly Property Price() As Decimal
        Get
            Return SumPrice()
        End Get
    End Property

    Public ReadOnly Property Qty() As Integer
        Get
            Return Sum()
        End Get
    End Property

    Private Function Sum() As Integer
        Dim result As Integer
        For Each detail As QuoteDetail In _col
            result += detail.Qty
        Next
        Return result
    End Function

    Private Function SumPrice() As Decimal
        Dim result As Decimal
        For Each detail As QuoteDetail In _col
            result += detail.Price
        Next
        Return result
    End Function

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

End Class
