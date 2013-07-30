Imports System.ComponentModel
Imports System.Reflection

Imports Model.Template
Imports Model

Namespace Quote

    Public Class PrimaryProperties
        Inherits Common.PrimaryProperties

        Private _RequestForQuoteNumber As String
        Private _PartNumber As String
        Private _TemplateID As Long
        Private _Initials As String
        Private _CreatedDate As DateTime
        Private _LastModified As DateTime

        Public Sub New(ByVal id As Long, _
                       ByVal requestForQuoteNumber As String, _
                       ByVal partNumber As String, _
                       ByVal initials As String, _
                       ByVal createdDate As DateTime, _
                       ByVal lastModified As DateTime
)
            Me.SetId(id)
            Me._RequestForQuoteNumber = requestForQuoteNumber
            Me._PartNumber = partNumber
            Me._Initials = initials
            Me._CreatedDate = createdDate
            Me._LastModified = lastModified
        End Sub

        <CategoryAttribute(Spaces.SortedSpaces1 + "Date"), _
         DisplayName("CreatedDate"), _
         DescriptionAttribute("Created Date")> _
        Public ReadOnly Property CreatedDate As DateTime
            Get
                Return _CreatedDate
            End Get
        End Property

        <CategoryAttribute(Spaces.SortedSpaces1 + "Date"), _
         DisplayName("LastModified"), _
            DescriptionAttribute("Last Modified Date")> _
        Public ReadOnly Property LastModified As DateTime
            Get
                Return _LastModified
            End Get
        End Property

        <CategoryAttribute(Spaces.SortedSpaces2 + "Misc"), _
        DisplayName("Quote Number"), _
        DescriptionAttribute("Quote Number")> _
        Public Overloads ReadOnly Property QuoteNumber As Integer
            Get
                Return MyBase.CommonId
            End Get
        End Property

        <CategoryAttribute(Spaces.SortedSpaces2 + "Misc"), _
        DisplayName("Initials"), _
        DescriptionAttribute("Initials of creator")> _
        Public ReadOnly Property Initials As String
            Get
                Return _Initials
            End Get
        End Property

        <CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), _
        DisplayName("Customer"), _
        DescriptionAttribute("The customer"),
        TypeConverter(GetType(CustomerConverter))> _
        Public ReadOnly Property Customer As Customer
            Get
                Return Me.CommonCustomer
            End Get
        End Property

        <CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), _
        DisplayName("Part Number"), _
        DescriptionAttribute("Part Number")> _
        Public ReadOnly Property PartNumber As String
            Get
                Return _PartNumber
            End Get
        End Property

        <CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), _
        DisplayName("RFQ"), _
        DescriptionAttribute("Request For Quote")> _
        Public ReadOnly Property RequestForQuoteNumber As String
            Get
                Return _RequestForQuoteNumber
            End Get
        End Property

        <CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), _
        DisplayName("TemplateNumber"), _
        DescriptionAttribute("Created from template")> _
        Public ReadOnly Property TemplateNumber As Integer
            Get
                Return Me._TemplateID
            End Get
        End Property

        Public Sub SetTemplateId(ByVal id As Long)
            Me._TemplateID = id
        End Sub

    End Class
End Namespace
