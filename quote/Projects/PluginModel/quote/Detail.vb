Imports System.ComponentModel
Imports System.Reflection

Imports Model

Namespace Quote

    Public Class Detail
        Inherits Common.Detail

        Private _Properties As New Common.SavableProperties

        Friend Sub New( _ 
            ByVal header As Common.Header, _
            ByVal product As Product)
            
            MyBase.New(product, "", 1)
            
            Me.QuoteHeader = header
        End Sub

        <BrowsableAttribute(False)>
        Property QuoteHeader As Header

        <BrowsableAttribute(False)>
        Public Overrides ReadOnly Property QuoteDetailProperties As Object
            Get
                Return _Properties
            End Get
        End Property

        Public Sub SetProperties(ByVal props As Common.SavableProperties)
            Me._Properties = props
        End Sub

        Public Overloads Property Qty() As Decimal
            Get
                Return MyBase.PrivateQty()
            End Get

            Set(ByVal value As Decimal)
                If Not (value = MyBase.PrivateQty()) Then
                    MyBase.SetPrivateQty(value)
                    SendEvents()
                End If
            End Set
        End Property

    End Class

End Namespace
