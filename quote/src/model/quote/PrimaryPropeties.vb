﻿Imports System.ComponentModel
Imports System.Reflection
Imports DCS.Quote.Model.BOM

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
                       ByVal LastModified As DateTime
)
            _QuoteHeader = QuoteHeader
            Me.SetID(id)
            Me._CustomerName = CustomerName
            Me._RequestForQuoteNumber = RequestForQuoteNumber
            Me._PartNumber = PartNumber
            Me._Initials = Initials
            Me._CreatedDate = CreatedDate
            Me._LastModified = LastModified
        End Sub
        <FilterAttribute(True), CategoryAttribute(SortedSpaces1 + "Date"), _
         DisplayName("CreatedDate"), _
         DescriptionAttribute("Created Date")> _
        Public ReadOnly Property CreatedDate As DateTime
            Get
                Return _CreatedDate
            End Get
        End Property
        <CategoryAttribute(SortedSpaces1 + "Date"), _
         DisplayName("LastModified"), _
            DescriptionAttribute("Last Modified Date")> _
        Public ReadOnly Property LastModified As DateTime
            Get
                Return _LastModified
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
                Return _Initials
            End Get
        End Property
        <CategoryAttribute(SortedSpaces3 + "Quote"), _
        DisplayName("Customer"), _
        DescriptionAttribute("The customer"),
        TypeConverter(GetType(CustomerConverter))> _
        Public ReadOnly Property Customer As Customer
            Get
                Return Me.CommonCustomer
            End Get
        End Property

        <CategoryAttribute(SortedSpaces3 + "Quote"), _
        DisplayName("Part Number"), _
        DescriptionAttribute("Part Number")> _
        Public ReadOnly Property PartNumber As String
            Get
                Return _PartNumber
            End Get
        End Property
        <CategoryAttribute(SortedSpaces3 + "Quote"), _
        DisplayName("RFQ"), _
        DescriptionAttribute("Request For Quote")> _
        Public ReadOnly Property RequestForQuoteNumber As String
            Get
                Return _RequestForQuoteNumber
            End Get
        End Property

        <FilterAttribute(False), CategoryAttribute(SortedSpaces3 + "Quote"), _
        DisplayName("TemplateNumber"), _
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
