Imports System.ComponentModel

Namespace Model
    Public Class QuoteHeader
        Implements INotifyPropertyChanged

        Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

#Region "Private Data"

        Private _qty As Integer
        Protected _col As New List(Of QuoteDetail)

#End Region

#Region "Properties"

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

#End Region

        Public Sub Remove(ByVal detail As QuoteDetail)
            _col.Remove(detail)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Qty"))
        End Sub

#Region "Private Methods"

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
                result += System.Math.Round(detail.Price)
            Next
            Return result
        End Function

#End Region

    End Class
End Namespace
