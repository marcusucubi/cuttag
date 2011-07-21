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
                If (_QuoteDetail.Product.UnitOfMeasure = UnitOfMeasure.BY_EACH) Then
                    Me.MachineTime = 10
                End If
            End If
        End Sub

        <DisplayName("Total Machine Time")>
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
            End Set
        End Property

        <DisplayName("Description"), _
        CategoryAttribute("Vendor")> _
        Public Overloads ReadOnly Property Description() As String
            Get
                If _QuoteDetail.Product.PartRow.IsDescriptionNull Then
                    Return ""
                End If
                Return _QuoteDetail.Product.PartRow.Description
            End Get
        End Property

        <DisplayName("Lead Time"), _
        CategoryAttribute("Vendor")> _
        Public Overloads ReadOnly Property LeadTime() As Integer
            Get
                If _QuoteDetail.Product.PartRow.IsLeadTimeNull Then
                    Return 0
                End If
                Return _QuoteDetail.Product.PartRow.LeadTime
            End Get
        End Property

        <DisplayName("Vendor"), _
        CategoryAttribute("Vendor")> _
        Public Overloads ReadOnly Property Vendor() As String
            Get
                If _QuoteDetail.Product.PartRow.IsVendorNull Then
                    Return ""
                End If
                Return _QuoteDetail.Product.PartRow.Vendor
            End Get
        End Property

        Private Overloads Sub SendEvents()
            MyBase.SendEvents()
            Me._QuoteDetail.Header.ComputationProperties.SendEvents()
        End Sub

    End Class
End Namespace
