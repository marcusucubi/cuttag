Imports System.ComponentModel
Imports System.Reflection

Namespace Model.Template

    Public Class PrimaryPropeties
        Inherits Common.PrimaryPropeties

        Private _QuoteHeader As Header
        Private _CustomerName As String
        Private _RequestForQuoteNumber As String
        Private _PartNumber As String

        Public Sub New(ByVal QuoteHeader As Header, ByVal id As Long)
            _QuoteHeader = QuoteHeader
            Me.SetID(id)
            CustomerName = "Caterpillar Inc."
        End Sub

        <CategoryAttribute("Quote"), _
        DisplayName("Customer"), _
        DescriptionAttribute("The customer name")> _
        Public Property CustomerName As String
            Get
                Return _CustomerName
            End Get
            Set(ByVal value As String)
                _CustomerName = value
                Me.SendEvents()
            End Set
        End Property

        <CategoryAttribute("Quote"), _
        DisplayName("RFQ"), _
        DescriptionAttribute("Request For Quote")> _
        Public Property RequestForQuoteNumber As String
            Get
                Return _RequestForQuoteNumber
            End Get
            Set(ByVal value As String)
                _RequestForQuoteNumber = value
                Me.SendEvents()
            End Set
        End Property

        <CategoryAttribute("Quote"), _
        DisplayName("Part Number"), _
        DescriptionAttribute("Part Number")> _
        Public Property PartNumber As String
            Get
                Return _PartNumber
            End Get
            Set(ByVal value As String)
                _PartNumber = value
                Me.SendEvents()
            End Set
        End Property

        <CategoryAttribute("Quote"), _
        DisplayName("QuoteNumnber"), _
        DescriptionAttribute("Quote Numnber")> _
        Public Overloads ReadOnly Property QuoteNumber As Integer
            Get
                Return MyBase.CommonID
            End Get
        End Property

    End Class
End Namespace
