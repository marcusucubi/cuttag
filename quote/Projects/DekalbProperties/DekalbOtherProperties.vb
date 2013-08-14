Imports System.ComponentModel
Imports System.Reflection

Imports Model
Imports Model.Template

Public NotInheritable Class DekalbOtherProperties
    Inherits OtherProperties

    Private _QuoteHeader As Header
    Private _LeadTimeInitial As Integer
    Private _LeadTimeStandard As Integer
    Private _EstimatedAnnualUnits As Integer
    Private _Tooling As Decimal
    Private _FormBoardCost As Decimal
    Private _DueDate As DateTime
    Private _QuoteDate As DateTime
    Private _QuoteType As String = "Production"
    Private _ImportedUnitCost As Decimal
    Private _ImportedCuWeight As Decimal
    Private _ImportedLaborMinutes As Decimal
    Private _IsNew As Boolean = True

    Public Sub New(ByVal QuoteHeader As Header)
        _QuoteHeader = QuoteHeader
    End Sub

    <CategoryAttribute(Spaces.SortedSpaces1 + "Date"), _
    DisplayName("Due Date"), _
    DescriptionAttribute("Date the quote is to be given to the customer")> _
    Public Property DueDate As DateTime
        Get
            Return _DueDate
        End Get
        Set(ByVal value As DateTime)
            _DueDate = value
            Me.OnPropertyChanged()
        End Set
    End Property

    <CategoryAttribute(Spaces.SortedSpaces1 + "Date"), _
    DisplayName("Quote Date"), _
    DescriptionAttribute("Quote Date to be displayed to the customer")> _
    Public Property QuoteDate As DateTime
        Get
            Return _QuoteDate
        End Get
        Set(ByVal value As DateTime)
            _QuoteDate = value
            Me.OnPropertyChanged()
        End Set
    End Property

    Public Sub SetImportedUnitCost(ByVal value As Decimal)
        _ImportedUnitCost = value
    End Sub

    <CategoryAttribute(Spaces.SortedSpaces2 + "Import"), _
    DisplayName("ImportedCuWeight"), _
    DescriptionAttribute("Imported CuWeight")> _
    Public ReadOnly Property ImportedCuWeight As Decimal
        Get
            Return _ImportedCuWeight
        End Get
    End Property

    Public Sub SetImportedCuWeight(ByVal value As Decimal)
        _ImportedCuWeight = value
    End Sub

    <CategoryAttribute(Spaces.SortedSpaces2 + "Import"), _
    DisplayName("ImportedLaborMinutes"), _
    DescriptionAttribute("Imported Labor Minutes")> _
    Public ReadOnly Property ImportedLaborMinutes As Decimal
        Get
            Return _ImportedLaborMinutes
        End Get
    End Property

    <CategoryAttribute(Spaces.SortedSpaces2 + "Import"), _
    DisplayName("ImportedUnitCost"), _
    DescriptionAttribute("Imported Unit Cost")> _
    Public ReadOnly Property ImportedUnitCost As Decimal
        Get
            Return _ImportedUnitCost
        End Get
    End Property

    <CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), _
    DisplayName("Estimated Annual Units"), _
    DescriptionAttribute("Estimated Annual Units")> _
    Public Property EstimatedAnnualUnits As Integer
        Get
            Return _EstimatedAnnualUnits
        End Get
        Set(ByVal value As Integer)
            _EstimatedAnnualUnits = value
            Me.OnPropertyChanged()
        End Set
    End Property

    <CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), _
    DisplayName("Initial Lead Time"), _
    DescriptionAttribute("Minimum number of days between the first purchase order and delivery")> _
    Public Property LeadTimeInitial As Integer
        Get
            Return _LeadTimeInitial
        End Get
        Set(ByVal value As Integer)
            _LeadTimeInitial = value
            Me.OnPropertyChanged()
        End Set
    End Property

    <CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), _
    DisplayName("Standard Lead Time"), _
    DescriptionAttribute("Minimum number of days between the purchase order and delivery")> _
    Public Property LeadTimeStandard As Integer
        Get
            Return _LeadTimeStandard
        End Get
        Set(ByVal value As Integer)
            _LeadTimeStandard = value
            Me.OnPropertyChanged()
        End Set
    End Property

    <CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), _
    DisplayName("Tooling"), _
    DescriptionAttribute("Tooling Cost")> _
    Public Property Tooling As Decimal
        Get
            Return _Tooling
        End Get
        Set(ByVal value As Decimal)
            _Tooling = value
            Me.OnPropertyChanged()
        End Set
    End Property

    <CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), _
    DisplayName("Form Board Cost"), _
    DescriptionAttribute("Form Board Cost")> _
    Public Property FormBoardCost As Decimal
        Get
            Return _FormBoardCost
        End Get
        Set(ByVal value As Decimal)
            _FormBoardCost = value
            Me.OnPropertyChanged()
        End Set
    End Property

    <CategoryAttribute(Spaces.SortedSpaces3 + "Quote"), _
    DisplayName("Is New"), _
    DescriptionAttribute("Is New")> _
    Public Property IsNew As Boolean
        Get
            Return _IsNew
        End Get
        Set(ByVal value As Boolean)
            _IsNew = value
            Me.OnPropertyChanged()
        End Set
    End Property

    Public Sub SetImportedLaborMinutes(ByVal value As Decimal)
        _ImportedLaborMinutes = value
    End Sub

End Class

