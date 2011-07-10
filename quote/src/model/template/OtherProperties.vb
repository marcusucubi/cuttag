Imports System.ComponentModel
Imports System.Reflection

Namespace Model.Template

    Public Class OtherProperties
        Inherits Common.OtherProperties

        Private _QuoteHeader As Header

        Public Sub New(ByVal QuoteHeader As Header)
            _QuoteHeader = QuoteHeader
        End Sub

        <CategoryAttribute("Supply Chain"), _
        DisplayName("Initial Lead Time"), _
        DescriptionAttribute("Minimum number of days between the first purchase order and delivery")> _
        Public Overloads Property LeadTimeInitial As Integer

        <CategoryAttribute("Supply Chain"), _
        DisplayName("Standard Lead Time"), _
        DescriptionAttribute("Minimum number of days between the purchase order and delivery")> _
        Public Overloads Property LeadTimeStandard As Integer

        <CategoryAttribute("Supply Chain"), _
        DisplayName("Estimated Annual Units"), _
        DescriptionAttribute("Estimated Annual Units")> _
        Public Overloads Property EstimatedAnnualUnits As Integer

        <CategoryAttribute("Date"), _
        DisplayName("Start Date"), _
        DescriptionAttribute("Date the quote is started")> _
        Public Overloads Property StartDate As DateTime

        <CategoryAttribute("Date"), _
        DisplayName("Completed Date"), _
        DescriptionAttribute("Date the quote is completed")> _
        Public Overloads Property CompletedDate As DateTime

        <CategoryAttribute("Date"), _
        DisplayName("Verified Date"), _
        DescriptionAttribute("Date the quote is verified")> _
        Public Overloads Property VerifiedDate As DateTime

        <CategoryAttribute("Date"), _
        DisplayName("Due Date"), _
        DescriptionAttribute("Date the quote is to be given to the customer")> _
        Public Overloads Property DueDate As DateTime

    End Class

End Namespace
