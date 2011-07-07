Imports System.ComponentModel
Imports System.Reflection

Namespace Model

    Public Class QuoteDetail
        Implements INotifyPropertyChanged
        Implements IEditableObject

        Public Event PropertyChanged As PropertyChangedEventHandler _
            Implements INotifyPropertyChanged.PropertyChanged

        Private _Quantity As Decimal
        Private _ComponentTime As Integer
        Private _Product As Product

#Region "Properties"

        <BrowsableAttribute(False)>
        Property QuoteHeader As QuoteHeader

        <BrowsableAttribute(False)>
        ReadOnly Property TotalCost As Decimal
            Get
                Return Me.UnitCost * Me.Qty
            End Get
        End Property

        <BrowsableAttribute(False)>
        ReadOnly Property Product As Product
            Get
                Return _Product
            End Get
        End Property

        <BrowsableAttribute(False)>
        ReadOnly Property ProductCode As String
            Get
                Return Product.Code.Trim
            End Get
        End Property

        <BrowsableAttribute(True)>
        ReadOnly Property Gage As String
            Get
                Return Product.Gage.Trim
            End Get
        End Property

        <BrowsableAttribute(False)>
        ReadOnly Property UnitCost As Decimal
            Get
                Return Product.UnitCost
            End Get
        End Property

        <BrowsableAttribute(True), DisplayName("Type")>
        ReadOnly Property DisplayableProductClass As String
            Get
                Return IIf(Product.UnitOfMeasure = UnitOfMeasure.BY_EACH, "Wire", "Component")
            End Get
        End Property

        <BrowsableAttribute(False)>
        ReadOnly Property DisplayableUnitOfMeasure As String
            Get
                Return Product.UnitOfMeasure.ToString
            End Get
        End Property

        <BrowsableAttribute(True), DisplayName("Total Component Time")>
        Public ReadOnly Property TotalComponentTime() As Integer
            Get
                Return (Me._ComponentTime * Me._Quantity)
            End Get
        End Property

        <BrowsableAttribute(False)>
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

        <BrowsableAttribute(True), DisplayName("Component Time")>
        Public Property ComponentTime() As Integer
            Get
                If Me._Product.UnitOfMeasure = UnitOfMeasure.BY_LENGTH Then
                    Return Nothing
                End If
                Return Me._ComponentTime
            End Get
            Set(ByVal value As Integer)
                If Me._Product.UnitOfMeasure = UnitOfMeasure.BY_LENGTH Then
                    MsgBox("ComponentTime can only be set for Component")
                Else
                    If Not (value = _ComponentTime) Then
                        Me._ComponentTime = value
                        SendEvents()
                    End If
                End If
            End Set
        End Property

#End Region

#Region "Methods"

        Friend Sub New(ByVal header As QuoteHeader, ByVal product As Product)
            Me.QuoteHeader = header
            Me._Product = product
            Me._Quantity = 1
            If (product.UnitOfMeasure = UnitOfMeasure.BY_EACH) Then
                Me.ComponentTime = 10
            End If
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
