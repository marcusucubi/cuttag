Imports System.ComponentModel
Imports System.Reflection

Namespace Model.Template
    Public Class ComponentProperties
        Inherits Common.ComponentProperties

        Private _QuoteDetail As Detail
        Private _MachineTime As Integer

        Public Sub New(ByVal QuoteDetail As Detail)
            _QuoteDetail = QuoteDetail
            If _QuoteDetail.Product IsNot Nothing Then
                If (_QuoteDetail.Product.UnitOfMeasure = _
                    Model.UnitOfMeasure.BY_EACH) Then

                    Me.MachineTime = 10
                End If
            End If
        End Sub

        <DisplayName("Total Machine Time"), _
        Browsable(False)>
        Public Overloads ReadOnly Property TotalMachineTime() As Integer
            Get
                Return (_MachineTime * _QuoteDetail.Qty)
            End Get
        End Property

        <DisplayName("Machine Time")>
        Public Overloads Property MachineTime() As Integer
            Get
                Return Me._MachineTime
            End Get
            Set(ByVal value As Integer)
                If Not (value = _MachineTime) Then
                    Me._MachineTime = value
                    SendEvents()
                End If
            End Set
        End Property

        Public Overloads Property Quantity() As Integer
            Get
                Return Me._QuoteDetail.Qty
            End Get
            Set(ByVal value As Integer)
                Me._QuoteDetail.Qty = value
                SendEvents()
            End Set
        End Property

        <DisplayName("Description"), _
        CategoryAttribute("Vendor")> _
        Public Overloads ReadOnly Property Description() As String
            Get
                Return _QuoteDetail.Product.Description
            End Get
        End Property

        <DisplayName("Lead Time"), _
        CategoryAttribute("Vendor")> _
        Public Overloads ReadOnly Property LeadTime() As Integer
            Get
                Return _QuoteDetail.Product.LeadTime
            End Get
        End Property

        <DisplayName("Vendor"), _
        CategoryAttribute("Vendor")> _
        Public Overloads ReadOnly Property Vendor() As String
            Get
                Return _QuoteDetail.Product.Vendor
            End Get
        End Property

        <DisplayName("Unit Of Measure"), _
        CategoryAttribute("Vendor")> _
        Public Overloads ReadOnly Property UnitOfMeasure() As String
            Get
                Return _QuoteDetail.Product.UnitOfMeasure.value
              End Get
        End Property

        <DisplayName("Unit Cost")> _
        Public Overloads ReadOnly Property UnitCost() As Decimal
            Get
                Return _QuoteDetail.UnitCost
            End Get
        End Property

        Private Overloads Sub SendEvents()
            MyBase.SendEvents()
            Me._QuoteDetail.Header.ComputationProperties.SendEvents()
        End Sub

    End Class
End Namespace
