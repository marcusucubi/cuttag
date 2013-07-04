Imports System.ComponentModel
Imports System.Reflection

Namespace BOM
    ''' <summary>
    ''' Computation properties for componeents.
    ''' </summary>
    ''' <remarks>
    ''' This class should contain computation related
    ''' code.  Any display related code should
    ''' should be in DisplayableComponentProperties.
    ''' </remarks>
    Public Class ComponentProperties
        Inherits Common.ComponentProperties

        Private _QuoteDetail As BOM.Detail
        Private _MinimumQty As Decimal
        Private _MinimumDollar As Decimal

        Public Sub New(ByVal QuoteDetail As BOM.Detail)
            _QuoteDetail = QuoteDetail
            If _QuoteDetail.Product IsNot Nothing Then
                Me._MinimumQty = _QuoteDetail.Product.MinimumQty
                Me._MinimumDollar = _QuoteDetail.Product.MinimumDollar
            End If
        End Sub

        Public Overloads ReadOnly Property TotalMachineTime() As Decimal
            Get
                Return (_QuoteDetail.MachineTime * _QuoteDetail.Qty)
            End Get
        End Property

        Public Overloads Property MachineTime() As Decimal
            Get
                Return _QuoteDetail.MachineTime 'Me._MachineTime
            End Get
            Set(ByVal value As Decimal)
                If Not (value = _QuoteDetail.MachineTime) Then
                    _QuoteDetail.MachineTime = value
                    SendEvents()
                End If
            End Set
        End Property

        Public Overloads Property MinimumQty() As Decimal
            Get
                Return Me._MinimumQty
            End Get
            Set(ByVal value As Decimal)
                If Not (value = _MinimumQty) Then
                    Me._MinimumQty = value
                    SendEvents()
                End If
            End Set
        End Property

        Public Overloads Property MinimumDollar() As Decimal
            Get
                Return Me._MinimumDollar
            End Get
            Set(ByVal value As Decimal)
                If Not (value = _MinimumDollar) Then
                    Me._MinimumDollar = value
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

        Public Property LeadTime() As Integer
            Get
                Return _QuoteDetail.Product.LeadTime
            End Get
            Set(ByVal value As Integer)
                _QuoteDetail.Product.LeadTime = value
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
                Return _QuoteDetail.UOM
            End Get
            Set(ByVal value As String)
                _QuoteDetail.UOM = value
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

        Private Overloads Sub SendEvents()
            MyBase.SendEvents()
            Me._QuoteDetail.Header.ComputationProperties.SendEvents()
        End Sub

    End Class
End Namespace
