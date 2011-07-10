Imports System.ComponentModel
Imports System.Reflection

Namespace Model.Template
    Public Class ComponentProperties
        Inherits Common.ComponentProperties

        Private _QuoteDetail As Detail
        Private _ComponentTime As Integer

        Public Sub New(ByVal QuoteDetail As Detail)
            _QuoteDetail = QuoteDetail
            If _QuoteDetail.Product IsNot Nothing Then
                If (_QuoteDetail.Product.UnitOfMeasure = UnitOfMeasure.BY_EACH) Then
                    Me.ComponentTime = 10
                End If
            End If
        End Sub

        <DisplayName("Total Component Time")>
        Public Overloads ReadOnly Property TotalComponentTime() As Integer
            Get
                Return (_ComponentTime * _QuoteDetail.Qty)
            End Get
        End Property

        <DisplayName("Component Time")>
        Public Overloads Property ComponentTime() As Integer
            Get
                Return Me._ComponentTime
            End Get
            Set(ByVal value As Integer)
                If Not (value = _ComponentTime) Then
                    Me._ComponentTime = value
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

        Private Overloads Sub SendEvents()
            MyBase.SendEvents()
            Me._QuoteDetail.Header.ComputationProperties.SendEvents()
        End Sub

    End Class
End Namespace
