Imports System.ComponentModel
Imports System.Reflection

Namespace Model.Quote

    Public Class PrimaryPropeties
        Inherits Common.PrimaryPropeties

        Private _QuoteHeader As Header
        Private _CustomerName As String
        Private _RequestForQuoteNumber As String
        Private _PartNumber As String

        Public Sub New(ByVal QuoteHeader As Header, _
                       ByVal id As Long, _
                       ByVal CustomerName As String, _
                       ByVal RequestForQuoteNumber As String, _
                       ByVal PartNumber As String)
            _QuoteHeader = QuoteHeader
            Me.SetID(id)
            Me._CustomerName = CustomerName
            Me._RequestForQuoteNumber = RequestForQuoteNumber
            Me._PartNumber = PartNumber
        End Sub

        <CategoryAttribute("Quote"), _
        DisplayName("Customer"), _
        DescriptionAttribute("The customer name")> _
        Public ReadOnly Property CustomerName As String
            Get
                Return _CustomerName
            End Get
        End Property

        <CategoryAttribute("Quote"), _
        DisplayName("RFQ"), _
        DescriptionAttribute("Request For Quote")> _
        Public ReadOnly Property RequestForQuoteNumber As String
            Get
                Return _RequestForQuoteNumber
            End Get
        End Property

        <CategoryAttribute("Quote"), _
        DisplayName("Part Number"), _
        DescriptionAttribute("Part Number")> _
        Public ReadOnly Property PartNumber As String
            Get
                Return _PartNumber
            End Get
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
