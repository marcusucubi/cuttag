﻿Imports System.ComponentModel
Imports DCS.Quote.Model
Imports System.Reflection

Namespace Model.Quote

    Public Class WireProperties
        Inherits Common.WireProperties

        Private WithEvents _QuoteDetail As Detail

        Public Sub New(ByVal QuoteDetail As Detail)
            _QuoteDetail = QuoteDetail
        End Sub

        Protected Overrides Sub Finalize()
            _QuoteDetail = Nothing
        End Sub

        Public ReadOnly Property Gage As String
            Get
                Return _QuoteDetail.Product.Gage.Trim
            End Get
        End Property

        <DescriptionAttribute("Length in Decameters")> _
        Public ReadOnly Property Length As String
            Get
                Return _QuoteDetail.Qty
            End Get
        End Property

        <DescriptionAttribute("Length / 3.048")> _
        Public ReadOnly Property LengthFeet As Decimal
            Get
                Return Math.Round(_QuoteDetail.Qty / 3.048, 4)
            End Get
        End Property

        <DescriptionAttribute("Pounds per foot")> _
        Public ReadOnly Property WeightPerDecameter As Decimal
            Get
                Return Common.Weights.FindWeight(Me.Gage)
            End Get
        End Property

        <DescriptionAttribute("WeightPerFoot * LengthFeet" + Chr(10) + "(Pounds)")> _
        Public ReadOnly Property TotalWeight As Decimal
            Get
                Return Math.Round(WeightPerDecameter * Length, 4)
            End Get
        End Property

        Private Sub _QuoteDetail_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _QuoteDetail.PropertyChanged
            SendEvents()
        End Sub

        Public Property Quantity() As Integer
            Get
                Return Me._QuoteDetail.Qty
            End Get
            Set(ByVal value As Integer)
                Me._QuoteDetail.Qty = value
            End Set
        End Property

    End Class
End Namespace
