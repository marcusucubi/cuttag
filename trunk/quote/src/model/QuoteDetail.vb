Imports System.ComponentModel

Namespace Model

    Public Class QuoteDetail
        Implements INotifyPropertyChanged

#Region "Private Members"
        Private _qty As Integer
        Private _product As Product
#End Region

#Region "Properties"

        Property Type As String
        Property QtyUnit As String
        Property QuoteHeader As QuoteHeader

        ReadOnly Property Price As Double
            Get
                Return System.Math.Round(Me.UnitPrice * Me.Qty, 2)
            End Get
        End Property

        Property Product As Product
            Get
                Return _product
            End Get
            Set(ByVal value As Product)
                _product = value
            End Set
        End Property

        ReadOnly Property ProductCode As String
            Get
                Return Product.Code.Trim
            End Get
        End Property

        ReadOnly Property UnitPrice As Double
            Get
                Return Product.UnitPrice
            End Get
        End Property

        Public Property Qty() As Integer
            Get
                Return Me._qty
            End Get

            Set(ByVal value As Integer)
                If Not (value = _qty) Then
                    Me._qty = value
                    NotifyPropertyChanged("Qty")
                    NotifyPropertyChanged("Price")
                End If
            End Set
        End Property

#End Region

        Friend Sub New()
        End Sub

        Friend Sub New(ByVal QuoteHeader As QuoteHeader)
            Me.QuoteHeader = QuoteHeader
            Me._qty = 1
        End Sub

        Public Event PropertyChanged As PropertyChangedEventHandler _
            Implements INotifyPropertyChanged.PropertyChanged

        Private Sub NotifyPropertyChanged(ByVal name As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
        End Sub

    End Class
End Namespace
