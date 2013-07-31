Imports System.ComponentModel
Imports System.Reflection

Namespace Template
    Public Class ComponentProperties
        Inherits Common.ComponentProperties

        Private _QuoteDetail As Model.Template.Detail

        Public Sub New(ByVal quoteDetail As Model.Template.Detail)
            MyBase.New()
            _QuoteDetail = quoteDetail
            If _QuoteDetail.Product IsNot Nothing Then
            End If
        End Sub

        Public Overloads ReadOnly Property TotalMachineTime() As Decimal
            Get
                Return (_QuoteDetail.MachineTime * _QuoteDetail.Qty)
            End Get
        End Property

        Public Overloads Property MachineTime() As Decimal
            Get
                Return _QuoteDetail.MachineTime
            End Get
            Set(ByVal value As Decimal)
                If Not (value = _QuoteDetail.MachineTime) Then
                    _QuoteDetail.MachineTime = value
                    SendEvents()
                End If
            End Set
        End Property

        Public Overloads Property Quantity() As Decimal
            Get
                Return Me._QuoteDetail.Qty
            End Get
            Set(ByVal value As Decimal)
                Me._QuoteDetail.Qty = value
                SendEvents()
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _QuoteDetail.Product.Description
            End Get
            Set(ByVal value As String)
                _QuoteDetail.Product.Description = value
                SendEvents()
            End Set
        End Property

        Public Property Vendor() As String
            Get
                Return _QuoteDetail.Product.Vendor
            End Get
            Set(ByVal value As String)
                _QuoteDetail.Product.Vendor = value
                SendEvents()
            End Set
        End Property

        Public Property UnitOfMeasure() As String
            Get
                Return _QuoteDetail.UnitOfMeasure
            End Get
            Set(ByVal value As String)
                _QuoteDetail.UnitOfMeasure = value
            End Set
        End Property

        Public Property UnitCost() As Decimal
            Get
                Return _QuoteDetail.UnitCost
            End Get
            Set(ByVal value As Decimal)
                _QuoteDetail.UnitCost = value
                SendEvents()
            End Set
        End Property

    End Class
End Namespace
