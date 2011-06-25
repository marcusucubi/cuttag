Imports System.ComponentModel

Namespace Model

    ''' <summary>
    ''' Represents the quote header
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Public Class QuoteHeader
        Implements INotifyPropertyChanged

        Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

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

#Region "Public Members and Methods"

        Public Sub Remove(ByVal detail As QuoteDetail)
            _col.Remove(detail)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Qty"))
        End Sub

#End Region

#Region "Private Members and Methods"

        Private _qty As Integer
        Private _col As New List(Of QuoteDetail)

        Protected ReadOnly Property QuoteDetails As List(Of QuoteDetail)
            Get
                Return _col
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
                result += System.Math.Round(detail.Price)
            Next
            Return result
        End Function

#End Region

    End Class
End Namespace
