Imports System.ComponentModel
Imports System.Reflection

Namespace Common

    Public Class Detail
        Inherits SaveableProperties

        Private _Quantity As Decimal = 1
        Private _Product As Model.Product
        Private _QuoteDetailProperties As Object
        Private _SequenceNumber As Integer = 1
        Private _SourceId As Guid
        Private _IsWire As Boolean
        Private _UOM As String
        
        Public Sub New( _
            Optional product As Model.Product = Nothing, _
            Optional unitOfMeasure As String = "", _
            Optional quantity As Decimal = 1)

            _Quantity = quantity
            _Product = product
            _UOM = unitOfMeasure
        End Sub
        
        Public Shadows Property Qty() As Decimal
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

        Public Property ProductCode As String
            Get
                Return Product.Code.Trim
            End Get
            Set(ByVal value As String)
                If Not (value = Product.Code) Then
                    Product.Code = value
                    SendEvents()
                End If
            End Set
        End Property

        Public Property UOM As String
            Get
                Return _UOM
            End Get
            Set(ByVal value As String)
                If Not (_UOM = value) Then
                    _UOM = value
                    SendEvents()
                End If
            End Set
        End Property

        Public Property SequenceNumber As Integer
            Get
                Return _SequenceNumber
            End Get
            Set(ByVal value As Integer)
                If Not (value = _SequenceNumber) Then
                    Me._SequenceNumber = value
                    SendEvents()
                End If
            End Set
        End Property
        
        Public Property SourceId As Guid
            Get
                Return _SourceId
            End Get
            Set(ByVal value As Guid)
                _SourceId = value
            End Set
        End Property
        
        Public Property IsWire As Boolean
            Get
                Return _IsWire
            End Get
            Set(ByVal value As Boolean)
                If Not (value = Me._IsWire) Then
                    _IsWire = value
                End If
            End Set
        End Property
        
        Public Property MachineTime As Decimal
            Get
                Return Product.MachineTime
            End Get
            Set(ByVal value As Decimal)
                Product.MachineTime = value
                SendEvents()
            End Set
        End Property

        Public Property UnitCost As Decimal
            Get
                Return Product.UnitCost
            End Get
            Set(ByVal value As Decimal)
                Product.UnitCost = value
                SendEvents()
            End Set
        End Property

        <Browsable(False)> _
        Public ReadOnly Property LengthFeet As Decimal
            Get
                Return Math.Round(Qty / 3.048, 4)
            End Get
        End Property

        <BrowsableAttribute(False)>
        Public ReadOnly Property Product As Model.Product
            Get
                Return _Product
            End Get
        End Property

        Public Overridable ReadOnly Property QuoteDetailProperties As Object
            Get
                Return _QuoteDetailProperties
            End Get
        End Property

        <BrowsableAttribute(True), DisplayName("Type")>
        Public ReadOnly Property DisplayableProductClass As String
            Get
                Return IIf(Product.IsWire, "Wire", "Component")
            End Get
        End Property

        Public ReadOnly Property TotalCost As Decimal
            Get
                If Product.IsWire Then
                    Return Math.Round(Me.UnitCost * Me.LengthFeet, 4)
                Else
                    Return Math.Round(Me.UnitCost * Me.Qty, 4)
                End If
            End Get
        End Property

        Protected Sub SetPrivateQty(value As Decimal)
            _Quantity = value
        End Sub
        
        Protected Function PrivateQty() As Decimal
            Return _Quantity
        End Function
        
        Protected Sub SetPrivateUnitOfMeasure(value As String)
            Me._UOM = value
        End Sub
        
        Protected Sub SetProduct(value As Model.Product)
            Me._Product = value
        End Sub
        
    End Class
End Namespace
