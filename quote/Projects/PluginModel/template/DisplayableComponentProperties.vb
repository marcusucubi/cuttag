Imports System.ComponentModel
Imports System.Reflection

Imports Model

Namespace Template
    ''' <summary>
    ''' Adds display attributes and rounding to ComponentProperties.
    ''' </summary>
    ''' <remarks>
    ''' This class should contain display releated code,
    ''' and ComponentProperties should contain computation
    ''' related code.
    ''' </remarks>
    Public Class DisplayableComponentProperties
        Inherits Common.ComponentProperties
        Private WithEvents _Options As Common.GlobalOptions = Common.GlobalOptions.Instance

        Private Sub _Options_Changed() Handles _Options.Changed
            Me.SendEvents()
        End Sub

        Private _Subject As DefaultComponentProperties

        Public Sub New(ByVal subject As DefaultComponentProperties)
            _Subject = subject
            MyBase.Subject = subject
        End Sub
        <FilterAttribute(True), _
        DisplayName("Total Machine Time"), _
        Browsable(False)>
        Public Overloads ReadOnly Property TotalMachineTime As Decimal
            Get
                Return Math.Round(_Subject.TotalMachineTime, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property

        <DisplayName("Machine Time"), _
        CategoryAttribute("Detail")> _
        Public Overloads Property MachineTime As Decimal
            Get
                Return Math.Round(_Subject.MachineTime, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Decimal)
                _Subject.MachineTime = value
                SendEvents()
            End Set
        End Property

        <DisplayName("Minimum Qty"), _
        CategoryAttribute("Vendor")> _
        Public Overloads Property MinimumQty As Decimal
            Get
                Return Math.Round(_Subject.MinimumQty, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Decimal)
                _Subject.MinimumQty = value
                SendEvents()
            End Set
        End Property

        <DisplayName("Minimum Dollar"), _
        CategoryAttribute("Vendor")> _
        Public Overloads Property MinimumDollar As Decimal
            Get
                Return Math.Round(_Subject.MinimumDollar, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Decimal)
                _Subject.MinimumDollar = value
                SendEvents()
            End Set
        End Property
        <CategoryAttribute("Detail")> _
        Public Overloads Property Quantity As Decimal
            Get
                Return Math.Round(_Subject.Quantity, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Decimal)
                _Subject.Quantity = value
                SendEvents()
            End Set
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

        <DisplayName("Lead Time"), _
        CategoryAttribute("Vendor")> _
        Public Property LeadTime As Integer
            Get
                Return Math.Round(_Subject.LeadTime, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Integer)
                _Subject.LeadTime = value
                SendEvents()
            End Set
        End Property

        <DisplayName("Vendor"), _
        CategoryAttribute("Vendor")> _
        Public Property Vendor As String
            Get
                Return _Subject.Vendor
            End Get
            Set(ByVal value As String)
                _Subject.Vendor = value
                SendEvents()
            End Set
        End Property

        <DisplayName("Unit Of Measure"), _
        TypeConverter(GetType(UOMConverter)), _
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

        <FilterAttribute(False), _
        DisplayName("Unit Cost"), _
        CategoryAttribute("Detail")> _
        Public Property UnitCost As Decimal
            Get
                Return Math.Round(_Subject.UnitCost, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Decimal)
                _Subject.UnitCost = value
                SendEvents()
            End Set
        End Property

    End Class
End Namespace
