Imports System.ComponentModel
Imports System.Reflection

Imports Model

Namespace Quote

    Public Class Detail
        Inherits Common.Detail

        Private _Properties As New Common.SaveableProperties

        Friend Sub New(ByVal header As Common.Header, _
                       ByVal product As Product)
            Me.QuoteHeader = header
            Me._Product = product
            Me._Quantity = 1
        End Sub

        <BrowsableAttribute(False)>
        Property QuoteHeader As Header

        <BrowsableAttribute(False)>
        Public Overrides ReadOnly Property QuoteDetailProperties As Object
            Get
                Return _Properties
            End Get
        End Property

        Public Sub SetProperties(ByVal props As Common.SaveableProperties)
            Me._Properties = props
        End Sub

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
