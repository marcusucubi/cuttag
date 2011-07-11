Imports System.ComponentModel
Imports DCS.Quote.Model
Imports System.Reflection

Namespace Model.Quote

    Public Class OtherProperties
        Inherits Common.OtherProperties

        Private _QuoteHeader As Header
        Public _LeadTimeInitial As Integer
        Public _LeadTimeStandard As Integer
        Public _EstimatedAnnualUnits As Integer
        Public _StartDate As DateTime
        Public _CompletedDate As DateTime
        Public _VerifiedDate As DateTime
        Public _DueDate As DateTime

        Public Sub New(ByVal QuoteHeader As Header)
            _QuoteHeader = QuoteHeader
        End Sub

        <CategoryAttribute("Supply Chain"), _
        DisplayName("Initial Lead Time"), _
        DescriptionAttribute("Minimum number of days between the first purchase order and delivery")> _
        Public ReadOnly Property LeadTimeInitial As Integer
            Get
                Return _LeadTimeInitial
            End Get
        End Property

        <CategoryAttribute("Supply Chain"), _
        DisplayName("Standard Lead Time"), _
        DescriptionAttribute("Minimum number of days between the purchase order and delivery")> _
        Public ReadOnly Property LeadTimeStandard As Integer
            Get
                Return _LeadTimeStandard
            End Get
        End Property

        <CategoryAttribute("Supply Chain"), _
        DisplayName("Estimated Annual Units"), _
        DescriptionAttribute("Estimated Annual Units")> _
        Public ReadOnly Property EstimatedAnnualUnits As Integer
            Get
                Return _EstimatedAnnualUnits
            End Get
        End Property

        <CategoryAttribute("Date"), _
        DisplayName("Start Date"), _
        DescriptionAttribute("Date the quote is started")> _
        Public ReadOnly Property StartDate As DateTime
            Get
                Return (_StartDate)
            End Get
        End Property

        <CategoryAttribute("Date"), _
        DisplayName("Completed Date"), _
        DescriptionAttribute("Date the quote is completed")> _
        Public ReadOnly Property CompletedDate As DateTime
            Get
                Return _CompletedDate
            End Get
        End Property

        <CategoryAttribute("Date"), _
        DisplayName("Verified Date"), _
        DescriptionAttribute("Date the quote is verified")> _
        Public ReadOnly Property VerifiedDate As DateTime
            Get
                Return _VerifiedDate
            End Get
        End Property

        <CategoryAttribute("Date"), _
        DisplayName("Due Date"), _
        DescriptionAttribute("Date the quote is to be given to the customer")> _
        Public ReadOnly Property DueDate As DateTime
            Get
                Return _DueDate
            End Get
        End Property

    End Class
End Namespace
