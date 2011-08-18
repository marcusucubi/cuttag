Imports System.ComponentModel
Imports System.Reflection

Namespace Model.BOM
    Public Class ComponentProperties
        Inherits Common.ComponentProperties

        Private _QuoteDetail As Detail
        Private _MachineTime As Decimal

        Public Sub New(ByVal QuoteDetail As Detail)
            _QuoteDetail = QuoteDetail
            If _QuoteDetail.Product IsNot Nothing Then
                Me.MachineTime = _QuoteDetail.Product.MachineTime
            End If
        End Sub

        <DisplayName("Total Machine Time"), _
        Browsable(False)>
        Public Overloads ReadOnly Property TotalMachineTime() As Decimal
            Get
                Return (_MachineTime * _QuoteDetail.Qty)
            End Get
        End Property

        <DisplayName("Machine Time")>
        Public Overloads Property MachineTime() As Decimal
            Get
                Return Me._MachineTime
            End Get
            Set(ByVal value As Decimal)
                If Not (value = _MachineTime) Then
                    Me._MachineTime = value
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

        <DisplayName("Description"), _
        CategoryAttribute("Vendor")> _
        Public Property Description() As String
            Get
                Return _QuoteDetail.Product.Description
            End Get
            Set(ByVal value As String)
                _QuoteDetail.Product.Description = value
                SendEvents()
            End Set
        End Property

        <DisplayName("Lead Time"), _
        CategoryAttribute("Vendor")> _
        Public Property LeadTime() As Integer
            Get
                Return _QuoteDetail.Product.LeadTime
            End Get
            Set(ByVal value As Integer)
                _QuoteDetail.Product.LeadTime = value
                SendEvents()
            End Set
        End Property

        <DisplayName("Vendor"), _
        CategoryAttribute("Vendor")> _
        Public Property Vendor() As String
            Get
                Return _QuoteDetail.Product.Vendor
            End Get
            Set(ByVal value As String)
                _QuoteDetail.Product.Vendor = value
                SendEvents()
            End Set
        End Property

        <DisplayName("Unit Of Measure"), _
        CategoryAttribute("Vendor")> _
        Public ReadOnly Property UnitOfMeasure() As String
            Get
                Return _QuoteDetail.Product.UnitOfMeasure.value
            End Get
        End Property

        <DisplayName("Unit Cost")> _
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
