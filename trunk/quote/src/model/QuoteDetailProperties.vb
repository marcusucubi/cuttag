Imports System.ComponentModel
Imports DCS.Quote.Model
Imports System.Reflection

Namespace Model
    Public Class QuoteDetailProperties
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

        Public ReadOnly Property Gage As String
            Get
                Return "" '_QuoteDetail.Product.Gage.Trim
            End Get
        End Property

        <DisplayName("Total Component Time")>
        Public ReadOnly Property TotalComponentTime() As Integer
            Get
                Return (_ComponentTime * _QuoteDetail.Qty)
            End Get
        End Property

        <DisplayName("Component Time")>
        Public Property ComponentTime() As Integer
            Get
                'If Me._QuoteDetail.Product.UnitOfMeasure = UnitOfMeasure.BY_LENGTH Then
                'Return Nothing
                'End If
                Return Me._ComponentTime
            End Get
            Set(ByVal value As Integer)
                If Me._QuoteDetail.Product.UnitOfMeasure = UnitOfMeasure.BY_LENGTH Then
                    MsgBox("ComponentTime can only be set for Component")
                Else
                    If Not (value = _ComponentTime) Then
                        Me._ComponentTime = value
                        SendEvents()
                    End If
                End If
            End Set
        End Property

        Private Sub SendEvents()
            Dim info() As PropertyInfo
            info = GetType(QuoteDetail).GetProperties()
            For Each i As PropertyInfo In info
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(i.Name))
            Next
        End Sub

    End Class
End Namespace

