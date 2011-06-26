Imports System.ComponentModel
Imports System.Reflection

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

        Public ReadOnly Property TotalLength() As Decimal
            Get
                Return SumQty(UnitOfMeasure.LENGTH)
            End Get
        End Property

        Public ReadOnly Property TotalQty() As Decimal
            Get
                Return SumQty(UnitOfMeasure.PIECES)
            End Get
        End Property

        Public ReadOnly Property Cost() As Integer
            Get
                Return SumCost()
            End Get
        End Property

#End Region

#Region "Public Members and Methods"

        Public Function NewEditableQuoteDetail() As EditableQuoteDetail

            Dim oo As New EditableQuoteDetail(Me)

            AddHandler oo.PropertyChanged, AddressOf ForwardEvent
            Me.QuoteDetails.Add(oo)
            SendEvents()

            Return oo
        End Function

        Public Sub Remove(ByVal detail As QuoteDetail)

            If detail IsNot Nothing Then
                Me.QuoteDetails.Remove(detail)

                RemoveHandler detail.PropertyChanged, AddressOf ForwardEvent
                SendEvents()
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

        Private Function SumCost() As Integer
            Dim result As Integer
            For Each detail As QuoteDetail In _col
                result += detail.TotalCost
            Next
            Return result
        End Function

        Private Function SumQty(ByVal measure As UnitOfMeasure) As Decimal
            Dim result As Decimal
            For Each detail As QuoteDetail In _col
                If detail.UnitOfMeasure = measure Then
                    result += detail.Qty
                End If
            Next
            Return result
        End Function

        Private Sub SendEvents()
            Dim info() As PropertyInfo
            info = GetType(QuoteHeader).GetProperties()
            For Each i As PropertyInfo In info
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(i.Name))
            Next
        End Sub

        Private Sub ForwardEvent(ByVal sender, ByVal e)
            RaiseEvent PropertyChanged(sender, e)
        End Sub

#End Region

    End Class
End Namespace
