Imports System.ComponentModel
Imports System.Reflection

Namespace Model.Quote

    Public Class PrimaryPropeties
        Inherits Common.PrimaryPropeties

        Private _QuoteHeader As Header
        Private _CustomerName As String
        Private _RequestForQuoteNumber As String
        Private _PartNumber As String
        Private _TemplateID As Long
        Private _Initials As String
        Private _CreatedDate As DateTime
        Private _LastModified As DateTime

        Public Sub New(ByVal QuoteHeader As Header, _
                       ByVal id As Long, _
                       ByVal CustomerName As String, _
                       ByVal RequestForQuoteNumber As String, _
                       ByVal PartNumber As String, _
                       ByVal Initials As String, _
                       ByVal CreatedDate As DateTime, _
                       ByVal LastModified As DateTime)
            _QuoteHeader = QuoteHeader
            Me.SetID(id)
            Me._CustomerName = CustomerName
            Me._RequestForQuoteNumber = RequestForQuoteNumber
            Me._PartNumber = PartNumber
            Me._Initials = Initials
            Me._CreatedDate = CreatedDate
            Me._LastModified = LastModified
        End Sub

        <CategoryAttribute("Quote"), _
        DisplayName("CreatedDate"), _
        DescriptionAttribute("Created Date")> _
        Public ReadOnly Property CreatedDate As DateTime
            Get
                Return _CreatedDate
            End Get
        End Property

        <CategoryAttribute("Quote"), _
        DisplayName("LastModified"), _
        DescriptionAttribute("Last Modified Date")> _
        Public ReadOnly Property LastModified As DateTime
            Get
                Return _LastModified
            End Get
        End Property

        <CategoryAttribute("Quote"), _
        DisplayName("Initials"), _
        DescriptionAttribute("Initials of creator")> _
        Public ReadOnly Property Initials As String
            Get
                Return _Initials
            End Get
        End Property

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

        <CategoryAttribute("Quote"), _
        DisplayName("TemplateNumnber"), _
        DescriptionAttribute("Created from template")> _
        Public ReadOnly Property TemplateNumber As Integer
            Get
                Return Me._TemplateID
            End Get
        End Property

        Public Sub SetTemplateID(ByVal id As Long)
            Me._TemplateID = id
        End Sub

    End Class
End Namespace
