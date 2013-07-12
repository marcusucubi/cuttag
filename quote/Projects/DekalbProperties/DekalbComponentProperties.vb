Imports System.ComponentModel
Imports System.Reflection

Imports Model

Public Class DekalbComponentProperties
    Inherits Model.Template.ComponentProperties

    Private _QuoteDetail As Model.Template.Detail
    Private _MinimumQty As Decimal
    Private _MinimumDollar As Decimal

    Public Sub New(ByVal QuoteDetail As Model.Template.Detail)
        MyBase.New(QuoteDetail)
        _QuoteDetail = QuoteDetail
        If _QuoteDetail.Product IsNot Nothing Then
            Me._MinimumQty = _QuoteDetail.Product.MinimumQty
            Me._MinimumDollar = _QuoteDetail.Product.MinimumDollar
        End If
    End Sub

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

    Public Property LeadTime() As Integer
        Get
            Return _QuoteDetail.Product.LeadTime
        End Get
        Set(ByVal value As Integer)
            _QuoteDetail.Product.LeadTime = value
            SendEvents()
        End Set
    End Property

End Class
