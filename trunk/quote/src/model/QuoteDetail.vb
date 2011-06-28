Imports System.ComponentModel
Imports System.Reflection

Namespace Model

    Public Class QuoteDetail
        Implements INotifyPropertyChanged

        Public Event PropertyChanged As PropertyChangedEventHandler _
            Implements INotifyPropertyChanged.PropertyChanged

#Region "Properties"

        Property QuoteHeader As QuoteHeader

        ReadOnly Property TotalCost As Decimal
            Get
                Return Me.UnitCost * Me.Qty
            End Get
        End Property

        ReadOnly Property Product As Product
            Get
                Return _product
            End Get
        End Property

        ReadOnly Property ProductCode As String
            Get
                Return Product.Code.Trim
            End Get
        End Property

        ReadOnly Property UnitCost As Decimal
            Get
                Return Product.UnitCost
            End Get
        End Property

        ReadOnly Property DisplayableProductClass As String
            Get
                Return IIf(Product.UnitOfMeasure = UnitOfMeasure.BY_EACH, "Wire", "Component")
            End Get
        End Property

        ReadOnly Property DisplayableUnitOfMeasure As String
            Get
                Return Product.UnitOfMeasure.ToString
            End Get
        End Property

        Public Property Qty() As Decimal
            Get
                Return Me._qty
            End Get

            Set(ByVal value As Decimal)
                If Not (value = _qty) Then
                    Me._qty = value
                    SendEvents()
                End If
            End Set
        End Property

#End Region

#Region "Public Members and Methods"

        Friend Sub New(ByVal header As QuoteHeader, ByVal product As Product)
            Me.QuoteHeader = header
            Me._product = product
            Me._qty = 1
        End Sub

#End Region

#Region "Private Members and Methods"

        Private _qty As Decimal
        Private _product As Product

        Private Sub NotifyPropertyChanged(ByVal name As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
        End Sub

        Private Sub SendEvents()
            Dim info() As PropertyInfo
            info = GetType(QuoteDetail).GetProperties()
            For Each i As PropertyInfo In info
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(i.Name))
            Next
        End Sub

#End Region

    End Class
End Namespace
