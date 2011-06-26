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

        Public Function NewEditableQuoteDetail() As EditableQuoteDetail

            Dim oo As New EditableQuoteDetail(Me)

            RaiseEvent PropertyChanged(oo, New PropertyChangedEventArgs("Qty"))
            RaiseEvent PropertyChanged(oo, New PropertyChangedEventArgs("Price"))

            AddHandler oo.PropertyChanged, AddressOf ForwardEvent
            Me.QuoteDetails.Add(oo)

            Return oo
        End Function

        Public Sub Remove(ByVal detail As QuoteDetail)

            If detail IsNot Nothing Then
                Me.QuoteDetails.Remove(detail)

                RemoveHandler detail.PropertyChanged, AddressOf ForwardEvent
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Qty"))
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Price"))
            End If
        End Sub

#End Region

#Region "Protected Members and Methods"

        Protected Overridable Function FactoryMethod() As QuoteDetail
            Return New QuoteDetail(Me)
        End Function

#End Region

#Region "Private Members and Methods"

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
                result += System.Math.Round(detail.Price, 2)
            Next
            Return result
        End Function

        Private Sub ForwardEvent(ByVal sender, ByVal e)
            RaiseEvent PropertyChanged(sender, e)
        End Sub

#End Region

    End Class
End Namespace
