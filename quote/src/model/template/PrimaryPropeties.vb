Imports System.ComponentModel
Imports System.Reflection

Namespace Model.Template

    Public Class PrimaryPropeties
        Inherits Common.PrimaryPropeties

        Private _QuoteHeader As Header
        Private _QuoteNumnber As Integer

        Public Sub New(ByVal QuoteHeader As Header, ByVal id As Long)
            _QuoteHeader = QuoteHeader
            Me._QuoteNumnber = id
            CustomerName = "Caterpillar Inc."
        End Sub

        <CategoryAttribute("Quote"), _
        DisplayName("Customer"), _
        DescriptionAttribute("The customer name")> _
        Public Overloads Property CustomerName As String = ""

        <CategoryAttribute("Quote"), _
        DisplayName("RFQ"), _
        DescriptionAttribute("Request For Quote")> _
        Public Overloads Property RequestForQuoteNumber As String = ""

        <CategoryAttribute("Quote"), _
        DisplayName("Part Number"), _
        DescriptionAttribute("Part Number")> _
        Public Overloads Property PartNumber As String = ""

        <CategoryAttribute("Quote"), _
        DisplayName("QuoteNumnber"), _
        DescriptionAttribute("Quote Numnber")> _
        Public Overloads ReadOnly Property QuoteNumnber As Integer
            Get
                Return Me._QuoteNumnber
            End Get
        End Property

        Public Overrides Sub SetID(ByVal id As Integer)
            Me._QuoteNumnber = id
            SendEvents()
        End Sub

    End Class
End Namespace
