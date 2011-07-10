Imports System.ComponentModel
Imports DCS.Quote.Model
Imports System.Reflection

Namespace Model
    Public Class ComponentProperties
        Inherits SaveableProperties
        Implements INotifyPropertyChanged

        Private _QuoteDetail As QuoteDetail
        Private _ComponentTime As Integer

        Public Event PropertyChanged As PropertyChangedEventHandler _
            Implements INotifyPropertyChanged.PropertyChanged

        Public Sub New(ByVal QuoteDetail As QuoteDetail)
            _QuoteDetail = QuoteDetail
            If _QuoteDetail.Product IsNot Nothing Then
                If (_QuoteDetail.Product.UnitOfMeasure = UnitOfMeasure.BY_EACH) Then
                    Me.ComponentTime = 10
                End If
            End If
        End Sub

        <DisplayName("Total Component Time")>
        Public ReadOnly Property TotalComponentTime() As Integer
            Get
                Return (_ComponentTime * _QuoteDetail.Qty)
            End Get
        End Property

        <DisplayName("Component Time")>
        Public Property ComponentTime() As Integer
            Get
                Return Me._ComponentTime
            End Get
            Set(ByVal value As Integer)
                If Not (value = _ComponentTime) Then
                    Me._ComponentTime = value
                    SendEvents()
                End If
            End Set
        End Property

        Public Property Quantity() As Integer
            Get
                Return Me._QuoteDetail.Qty
            End Get
            Set(ByVal value As Integer)
                Me._QuoteDetail.Qty = value
            End Set
        End Property

        Private Sub SendEvents()
            Dim info() As PropertyInfo
            info = GetType(QuoteDetail).GetProperties()
            For Each i As PropertyInfo In info
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(i.Name))
            Next
            Me._QuoteDetail.QuoteHeader.ComputationProperties.SendEvents()
            MyBase.MakeDirty()
        End Sub

    End Class
End Namespace
