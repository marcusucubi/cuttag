Imports System.ComponentModel
Imports System.Reflection

Namespace Model.Template

    Public Class OtherProperties
        Inherits Common.OtherProperties

        Private _QuoteHeader As Header
        Private _LeadTimeInitial As Integer
        Private _LeadTimeStandard As Integer
        Private _EstimatedAnnualUnits As Integer
        Private _DueDate As DateTime

        Public Sub New(ByVal QuoteHeader As Header)
            _QuoteHeader = QuoteHeader
        End Sub

        <CategoryAttribute("Supply Chain"), _
        DisplayName("Initial Lead Time"), _
        DescriptionAttribute("Minimum number of days between the first purchase order and delivery")> _
        Public Property LeadTimeInitial As Integer
            Get
                Return _LeadTimeInitial
            End Get
            Set(ByVal value As Integer)
                _LeadTimeInitial = value
                Me.SendEvents()
            End Set
        End Property

        <CategoryAttribute("Supply Chain"), _
        DisplayName("Standard Lead Time"), _
        DescriptionAttribute("Minimum number of days between the purchase order and delivery")> _
        Public Property LeadTimeStandard As Integer
            Get
                Return _LeadTimeStandard
            End Get
            Set(ByVal value As Integer)
                _LeadTimeStandard = value
                Me.SendEvents()
            End Set
        End Property

        <CategoryAttribute("Supply Chain"), _
        DisplayName("Estimated Annual Units"), _
        DescriptionAttribute("Estimated Annual Units")> _
        Public Property EstimatedAnnualUnits As Integer
            Get
                Return _EstimatedAnnualUnits
            End Get
            Set(ByVal value As Integer)
                _EstimatedAnnualUnits = value
                Me.SendEvents()
            End Set
        End Property

        <CategoryAttribute("Date"), _
        DisplayName("Due Date"), _
        DescriptionAttribute("Date the quote is to be given to the customer")> _
        Public Property DueDate As DateTime
            Get
                Return _DueDate
            End Get
            Set(ByVal value As DateTime)
                _DueDate = value
                Me.SendEvents()
            End Set
        End Property

    End Class

End Namespace
