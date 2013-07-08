Imports System.ComponentModel
Imports System.Reflection

Imports Model

Namespace BOM

    Public Class PrimaryPropeties
        Inherits Common.PrimaryPropeties

        Private _QuoteHeader As Header

        Public Sub New(ByVal QuoteHeader As Header, ByVal id As Long)
            _QuoteHeader = QuoteHeader
            Me.SetID(id)
        End Sub

        <FilterAttribute(True), CategoryAttribute(SortedSpaces1 + "Date"), _
        DisplayName("CreatedDate"), _
        DescriptionAttribute("Created Date")> _
        Public ReadOnly Property CreatedDate As DateTime
            Get
                Return Me.CommonCreatedDate
            End Get
        End Property

        <CategoryAttribute(SortedSpaces1 + "Date"), _
        DisplayName("LastModified"), _
        DescriptionAttribute("Last Modified Date")> _
        Public ReadOnly Property LastModified As DateTime
            Get
                Return Me.CommonLastModified
            End Get
        End Property
        <CategoryAttribute(SortedSpaces2 + "Misc"), _
        DisplayName("Quote Number"), _
        DescriptionAttribute("Quote Number")> _
        Public Overloads ReadOnly Property QuoteNumber As Integer
            Get
                Return MyBase.CommonID
            End Get
        End Property
        <CategoryAttribute(SortedSpaces2 + "Misc"), _
        DisplayName("Initials"), _
        DescriptionAttribute("Initials of creator")> _
        Public ReadOnly Property Initials As String
            Get
                Return Me.CommonInitials
            End Get
        End Property

        <CategoryAttribute(SortedSpaces3 + "Quote"), _
        DisplayName("Customer"), _
        DescriptionAttribute("The customer"),
        TypeConverter(GetType(CustomerConverter))> _
        Public Property Customer As Customer
            Get
                Return Me.CommonCustomer
            End Get
            Set(ByVal value As Customer)
                Me.CommonCustomer = value
                Me.SendEvents()
            End Set
        End Property

        <CategoryAttribute(SortedSpaces3 + "Quote"), _
        DisplayName("Part Number"), _
        DescriptionAttribute("Part Number")> _
        Public Property PartNumber As String
            Get
                Return Me.CommonPartNumber
            End Get
            Set(ByVal value As String)
                Me.CommonPartNumber = value
                Me.SendEvents()
            End Set
        End Property
        <FilterAttribute(False), CategoryAttribute(SortedSpaces3 + "Quote"), _
        DisplayName("RFQ"), _
        DescriptionAttribute("Request For Quote")> _
        Public Property RequestForQuoteNumber As String
            Get
                Return Me.CommonRequestForQuoteNumber
            End Get
            Set(ByVal value As String)
                Me.CommonRequestForQuoteNumber = value
                Me.SendEvents()
            End Set
        End Property

    End Class
End Namespace
