Imports System.ComponentModel
Imports System.Reflection

Namespace Model

    Public Class QuoteDetail
        Implements INotifyPropertyChanged
        Implements IEditableObject

        Public Event PropertyChanged As PropertyChangedEventHandler _
            Implements INotifyPropertyChanged.PropertyChanged

        Private _Quantity As Decimal
        Private _Product As Product
        Private _WireProperties As New WireProperties(Me)
        Private _ComponentProperties As New ComponentProperties(Me)

#Region "Properties"

        <BrowsableAttribute(False)>
        Property QuoteHeader As QuoteHeader

        Public ReadOnly Property TotalCost As Decimal
            Get
                Return Me.UnitCost * Me.Qty
            End Get
        End Property

        <BrowsableAttribute(False)>
        Public ReadOnly Property Product As Product
            Get
                Return _Product
            End Get
        End Property

        <BrowsableAttribute(False)>
        Public ReadOnly Property QuoteDetailProperties As Object
            Get
                If Me._Product.UnitOfMeasure = UnitOfMeasure.BY_LENGTH Then
                    Return _WireProperties
                End If
                Return _ComponentProperties
            End Get
        End Property

        Public ReadOnly Property ProductCode As String
            Get
                Return Product.Code.Trim
            End Get
        End Property

        Public ReadOnly Property UnitCost As Decimal
            Get
                Return Product.UnitCost
            End Get
        End Property

        <BrowsableAttribute(True), DisplayName("Type")>
        Public ReadOnly Property DisplayableProductClass As String
            Get
                Return IIf(Product.UnitOfMeasure = UnitOfMeasure.BY_EACH, "Component", "Wire")
            End Get
        End Property

        Public Property Qty() As Decimal
            Get
                Return Me._Quantity
            End Get

            Set(ByVal value As Decimal)
                If Not (value = _Quantity) Then
                    Me._Quantity = value
                    SendEvents()
                End If
            End Set
        End Property

#End Region

#Region "Methods"

        Friend Sub New(ByVal header As QuoteHeader, ByVal product As Product)
            Me.QuoteHeader = header
            Me._Product = product
            Me._Quantity = 1
        End Sub

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

        Public Sub BeginEdit() Implements System.ComponentModel.IEditableObject.BeginEdit
        End Sub

        Public Sub CancelEdit() Implements System.ComponentModel.IEditableObject.CancelEdit
        End Sub

        Public Sub EndEdit() Implements System.ComponentModel.IEditableObject.EndEdit
        End Sub

#End Region

    End Class
End Namespace
