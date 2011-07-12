﻿Imports System.ComponentModel
Imports System.Reflection

Namespace Model.Quote

    Public Class Detail
        Inherits Common.Detail

        Private _WireProperties As New SaveableProperties
        Private _ComponentProperties As New SaveableProperties

        Friend Sub New(ByVal header As Common.Header, ByVal product As Product)
            Me.QuoteHeader = header
            Me._Product = product
            Me._Quantity = 1
        End Sub

        <BrowsableAttribute(False)>
        Property QuoteHeader As Header

        <BrowsableAttribute(False)>
        Public Overrides ReadOnly Property QuoteDetailProperties As Object
            Get
                If Me._Product.UnitOfMeasure = UnitOfMeasure.BY_LENGTH Then
                    Return _WireProperties
                End If
                Return _ComponentProperties
            End Get
        End Property

        Public Overloads Property Qty() As Decimal
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

    End Class
End Namespace
