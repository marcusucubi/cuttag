Imports System.ComponentModel
Imports System.Reflection

Imports Model
Imports Model.Template

Namespace Template

    Public Class DisplayableComponenetProperties
        Inherits Common.ComponentProperties

        Private WithEvents _Options As Common.GlobalOptions = Common.GlobalOptions.Instance

        Private Sub _Options_Changed() Handles _Options.Changed
            Me.SendEvents()
        End Sub

        Private _Subject As ComponentProperties

        Public Sub New(ByVal subject As ComponentProperties)
            _Subject = subject
            MyBase.Subject = subject
        End Sub

        <DisplayName("Total Machine Time"), _
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
        TypeConverter(GetType(UomConverter)), _
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

        <DisplayName("Unit Cost"), _
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
