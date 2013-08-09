Imports System.ComponentModel
Imports System.Reflection

Imports Model
Imports Model.Quote

Public Class DisplayableWireProperties
    Inherits Common.WireProperties

    Private WithEvents _Options As Common.GlobalOptions = Common.GlobalOptions.Instance

    Private Sub _Options_Changed() Handles _Options.Changed
        Me.SendEvents()
    End Sub

    Private ReadOnly _Subject As DekalbWireProperties

    Public Sub New(ByVal subject As DekalbWireProperties)
        _Subject = subject
        MyBase.Subject = subject
    End Sub
    <CategoryAttribute("Detail")> _
    Public ReadOnly Property Gage As String
        Get
            Return _Subject.Gage
        End Get
    End Property

    <DisplayName("Description"), _
    CategoryAttribute("Detail")> _
    Public Property Description As String
        Get
            Return _Subject.Description
        End Get
        Set(ByVal value As String)
            _Subject.Description = value
            SendEvents()
        End Set
    End Property

    <DescriptionAttribute("Length in Decimeters"), _
    DisplayName("Length in Decimeters"), _
    CategoryAttribute("Detail")> _
    Public ReadOnly Property Length As Decimal
        Get
            Return Math.Round(_Subject.Length, Common.GlobalOptions.Instance.DecimalPointsToDisplay)
        End Get
    End Property

    <DescriptionAttribute("Length / 3.048"), _
    DisplayName("Length in Feet"), _
    CategoryAttribute("Detail")> _
    Public ReadOnly Property LengthFeet As Decimal
        Get
            Return Math.Round(_Subject.LengthFeet, Common.GlobalOptions.Instance.DecimalPointsToDisplay)
        End Get
    End Property

    <DescriptionAttribute("Pounds per 1000 feet"), _
    CategoryAttribute("Copper Weight")> _
    Public Property PoundsPer1000Feet As Decimal
        Get
            Return Math.Round(_Subject.PoundsPer1000Feet, Common.GlobalOptions.Instance.DecimalPointsToDisplay)
        End Get
        Set(ByVal value As Decimal)
            _Subject.PoundsPer1000Feet = value
            SendEvents()
        End Set
    End Property

    <DescriptionAttribute("WeightPerFoot * Length" + Chr(10) + "(Pounds)"), _
    CategoryAttribute("Copper Weight")> _
    Public ReadOnly Property TotalWeight As Decimal
        Get
            Return Math.Round(_Subject.TotalWeight, Common.GlobalOptions.Instance.DecimalPointsToDisplay)
        End Get
    End Property

    <DescriptionAttribute("Number of Decimeters"), _
    DisplayName("Quantity"), _
    CategoryAttribute("Detail")> _
    Public Property Quantity As Decimal
        Get
            Return Math.Round(_Subject.Quantity, Common.GlobalOptions.Instance.DecimalPointsToDisplay)
        End Get
        Set(ByVal value As Decimal)
            _Subject.Quantity = value
            SendEvents()
        End Set
    End Property

    <DisplayName("Unit Cost"), _
    DescriptionAttribute("Dollars per Decimeter"), _
    CategoryAttribute("Detail")> _
    Public Property UnitCost As Decimal
        Get
            Return Math.Round(_Subject.UnitCost, Common.GlobalOptions.Instance.DecimalPointsToDisplay)
        End Get
        Set(ByVal value As Decimal)
            _Subject.UnitCost = value
            SendEvents()
        End Set
    End Property

    <DisplayName("Unit Of Measure"), _
    TypeConverter(GetType(UnitOfMeasureConverter)), _
    CategoryAttribute("Detail")> _
    Public Property UnitOfMeasure As String
        Get
            Return _Subject.UnitOfMeasure
        End Get
        Set(ByVal value As String)
            _Subject.UnitOfMeasure = value
            SendEvents()
        End Set
    End Property

End Class

