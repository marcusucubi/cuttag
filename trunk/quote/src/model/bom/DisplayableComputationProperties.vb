﻿Imports System.ComponentModel
Imports System.Reflection
Imports System.Math

Namespace Model.BOM
    ''' <summary>
    ''' Adds display attributes and rounding to ComputationProperties.
    ''' </summary>
    ''' <remarks>
    ''' This class should contain display releated code,
    ''' and ComputationProperties should contain computation
    ''' related code.
    ''' </remarks>
    Public Class DisplayableComputationProperties
        Inherits Common.ComputationProperties
        Implements ICustomTypeDescriptor

        Private WithEvents _Options As Common.GlobalOptions = Common.GlobalOptions.Instance

        Public Sub New(ByVal subject As ComputationProperties)
            _Subject = subject
            MyBase.Subject = subject '''''''''''''''dddddddd
            '  MyBase.NonDisplayableProperties = subject 'dd_Added 2/1/2012
        End Sub

        Private Sub _Options_Changed() Handles _Options.Changed
            Me.SendEvents()
        End Sub

#Region " Variables "
        Private _Subject As ComputationProperties
#End Region
#Region "1 Copper "
        <FilterAttribute(True), _
        DescriptionAttribute("Weight of Copper. " + Chr(10) + "(Pounds)"), _
        DisplayName("Copper Weight"), _
        CategoryAttribute(SortedSpaces1 + "Copper")> _
        Public ReadOnly Property CopperWeight As Decimal
            Get
                Return Math.Round(_Subject.CopperWeight, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property
        <DescriptionAttribute("Percent of Scrap Copper. " + Chr(10) + "(Percent)"), _
        DisplayName("Percent Copper Scrap"), _
        CategoryAttribute(SortedSpaces1 + "Copper")> _
        Public Property PercentCopperScrap As Decimal
            Get
                Return Math.Round(_Subject.PercentCopperScrap, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Decimal)
                _Subject.PercentCopperScrap = value
                MyBase.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("CopperWeight * (PercentCopperScrap / 100)" + Chr(10) + "(Pounds)"), _
        DisplayName("Copper Scrap Weight"), _
        CategoryAttribute(SortedSpaces1 + "Copper")> _
        Public ReadOnly Property CopperScrapWeight As Decimal
            Get
                Return Math.Round(_Subject.CopperScrapWeight, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property
        <DescriptionAttribute("Copper Price" + Chr(10) + "(Dollars Per Pounds)"), _
        DisplayName("Copper Price"), _
        CategoryAttribute(SortedSpaces1 + "Copper")> _
        Public Property CopperPrice As Decimal
            Get
                Return Math.Round(_Subject.CopperPrice, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Decimal)
                _Subject.CopperPrice = value
                MyBase.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("CopperScrapWeight * CopperPrice. " _
            + Chr(10) + "(Dollars Per Pounds)"), _
        DisplayName("Copper Scrap Cost"), _
        CategoryAttribute(SortedSpaces1 + "Copper")> _
        Public ReadOnly Property CopperScrapCost As Decimal
            Get
                Return Math.Round(_Subject.CopperScrapCost, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property

#End Region
#Region "2 Wires "
        <DescriptionAttribute("Number of Wires" + Chr(10) + "(Count)"), _
        DisplayName("Number of Wires"), _
        CategoryAttribute(SortedSpaces2 + "Wire")> _
        Public ReadOnly Property NumberOfWires() As Decimal
            Get
                Return _Subject.NumberOfWires
            End Get
        End Property

        <DescriptionAttribute("Wire Length" + Chr(10) + "(Decimeter)"), _
        DisplayName("Wire Length"), _
        CategoryAttribute(SortedSpaces2 + "Wire")> _
        Public ReadOnly Property WireLength() As Decimal
            Get
                Return Math.Round(_Subject.WireLength, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property

        <DescriptionAttribute("WireLength / 3.0Common.GlobalOptions.DecimalPointsToDisplay8" + Chr(10) + "(Feet)"), _
        DisplayName("Wire Length Feet"), _
        CategoryAttribute(SortedSpaces2 + "Wire")> _
        Public ReadOnly Property WireLengthFeet() As Decimal
            Get
                Return Math.Round(_Subject.WireLengthFeet, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property

#End Region
#Region "3 Material Cost "
        <DescriptionAttribute("Sum(UnitCost * Quantity)" + Chr(10) + "(Dollar)"), _
        DisplayName("Component Material Cost"), _
        CategoryAttribute(SortedSpaces3 + "Material Cost")> _
        Public ReadOnly Property ComponentMaterialCost() As Decimal
            Get
                Return Math.Round(_Subject.ComponentMaterialCost, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property
        <DescriptionAttribute("Sum(UnitCost * Quantity)" + Chr(10) + "(Dollar)"), _
        DisplayName("Wire Material Cost"), _
        CategoryAttribute(SortedSpaces3 + "Material Cost")> _
        Public ReadOnly Property WireMaterialCost() As Decimal
            Get
                Return Math.Round(_Subject.WireMaterialCost, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property
        <DescriptionAttribute("ComponentMaterialCost + WireMaterialCost + ShippingContainerCostPerOrder" + Chr(10) + "(Dollar)"), _
        DisplayName("Total Material Cost"), _
        CategoryAttribute(SortedSpaces3 + "Material Cost")> _
        Public ReadOnly Property TotalMaterialCost() As Decimal
            Get
                Return Math.Round(_Subject.TotalMaterialCost, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property
        <DescriptionAttribute("Material Markup"), _
        DisplayName("Material Markup"), _
        CategoryAttribute(SortedSpaces3 + "Material Cost")> _
        Public Property MaterialMarkUp As Decimal
            Get
                Return Math.Round(_Subject.MaterialMarkUp, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Decimal)
                _Subject.MaterialMarkUp = value
                MyBase.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("TotalMaterialCost * MaterialMarkup" + Chr(10) + "(Dollars)"), _
        DisplayName("Adjusted Total Material Cost"), _
        CategoryAttribute(SortedSpaces3 + "Material Cost")> _
        Public ReadOnly Property AdjustedTotalMaterialCost As Decimal
            Get
                Return Math.Round(_Subject.AdjustedTotalMaterialCost, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property
        <DescriptionAttribute("(TotalMaterialCost * MaterialMarkup)" + _
            " + CopperCost + ShippingCost" + Chr(10) + "(Dollar)"), _
        DisplayName("Total Variable Material Cost"), _
        CategoryAttribute(SortedSpaces3 + "Material Cost")> _
        Public ReadOnly Property TotalVariableMaterialCost() As Decimal
            Get
                Return Math.Round(_Subject.TotalVariableMaterialCost, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property
#End Region
#Region "Common.GlobalOptions.DecimalPointsToDisplay Twisted Pairs "
        <DescriptionAttribute("Number Of Twisted Pairs " + Chr(10) + "(Number)"), _
        DisplayName("Twisted Pairs"), _
        CategoryAttribute(SortedSpaces4 + "Twisted Wire")> _
        Public Property NumberOfTwistedPairs As Integer
            Get
                Return _Subject.NumberOfTwistedPairs
            End Get
            Set(ByVal value As Integer)
                _Subject.NumberOfTwistedPairs = value
                MyBase.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("Twisted Pairs Machine Time " + Chr(10) + "(Number)"), _
        DisplayName("Twisted Pairs Machine Time"), _
        CategoryAttribute(SortedSpaces4 + "Twisted Wire")> _
        Public ReadOnly Property TwistedPairsMachineTime As Decimal
            Get
                Return Math.Round(_Subject.TwistedPairsMachineTime, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property
#End Region

#Region "5 Setup Time "
        <DescriptionAttribute("Number of Components" + Chr(10) + "(Count)"), _
        DisplayName("Number of Components"), _
        CategoryAttribute(SortedSpaces5 + "Setup Time")> _
        Public ReadOnly Property NumberOfComponents() As Decimal
            Get
                Return _Subject.NumberOfComponents
            End Get
        End Property
        <DescriptionAttribute("Component Setup Time" + Chr(10) + "(Seconds)"), _
        DisplayName("Component Setup Time"), _
        CategoryAttribute(SortedSpaces5 + "Setup Time")> _
        Public Property ComponentSetupTime() As Decimal
            Get
                Return Math.Round(_Subject.ComponentSetupTime, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Decimal)
                _Subject.ComponentSetupTime = value
                MyBase.SendEvents()
            End Set
        End Property

        <DescriptionAttribute("Setup time to cut a particular length of wire. (Cut time)" + Chr(10) + "(Seconds Per Cut)"), _
        DisplayName("Wire Setup Time Multiplier"), _
        CategoryAttribute(SortedSpaces5 + "Setup Time")> _
        Public Property WireSetupTime As Decimal
            Get
                Return Math.Round(_Subject.WireSetupTime, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Decimal)
                _Subject.WireSetupTime = value
                MyBase.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("Number of lines in 'CIRCUIT DATA TABLE'" + _
            " in Engineering Print." + Chr(10) + "(Count)"), _
        DisplayName("Number of Cuts"), _
        CategoryAttribute(SortedSpaces5 + "Setup Time")> _
        Public Property NumberOfCuts() As Integer
            Get
                Return _Subject.NumberOfCuts
            End Get
            Set(ByVal value As Integer)
                _Subject.NumberOfCuts = value
                MyBase.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("NumberOfCuts * WireSetupTime" + Chr(10) + "(Seconds)"), _
        DisplayName("Total Wire Setup Time"), _
        CategoryAttribute(SortedSpaces5 + "Setup Time")> _
        Public ReadOnly Property TotalWireSetupTime As Decimal
            Get
                Return Math.Round(_Subject.TotalWireSetupTime, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property
        <DescriptionAttribute("(TotalWireSetupTime + ComponentSetupTime) " + _
            "/ MinimumOrderQuantity" + _
            Chr(10) + "(Seconds)"), _
        DisplayName("Adjusted Total Setup Time"), _
        CategoryAttribute(SortedSpaces5 + "Setup Time")> _
        Public ReadOnly Property TotalSetupTime() As Decimal
            Get
                Return Math.Round(_Subject.TotalSetupTime, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property
#End Region
#Region "6 Machine Time "
        <DescriptionAttribute("Sum(ComponentMachineTime) " + Chr(10) + "(Seconds)"), _
        DisplayName("Total Component Run Time"), _
        CategoryAttribute(SortedSpaces6 + "Run Time")> _
        Public ReadOnly Property TotalComponentMachineTime As Decimal
            Get
                Return Math.Round(_Subject.TotalComponentMachineTime, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property
        <DescriptionAttribute("WireLengthFeet * WireMachineTime " + Chr(10) + "(Seconds)"), _
        DisplayName("Total Wire Run Time"), _
        CategoryAttribute(SortedSpaces6 + "Run Time")> _
        Public ReadOnly Property TotalWireMachineTime As Decimal
            Get
                Return Math.Round(_Subject.TotalWireMachineTime, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property
        <DescriptionAttribute("Used with TotalWireMachineTime " + Chr(10) + "(Seconds)"), _
        DisplayName("Wire Run Time Multiplier"), _
        CategoryAttribute(SortedSpaces6 + "Run Time")> _
        Public Property WireMachineTime As Decimal
            Get
                Return Math.Round(_Subject.WireMachineTime, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Decimal)
                Me._Subject.WireMachineTime = value
                MyBase.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("TotalWireMachineTime + " + _
            "TotalComponentMachineTime + TwistedPairsMachineTime" + Chr(10) + "(Seconds)"), _
        DisplayName("Total Run Time"), _
        CategoryAttribute(SortedSpaces6 + "Run Time")> _
        Public ReadOnly Property TotalMachineTime As Decimal
            Get
                Return Math.Round(_Subject.TotalMachineTime, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property
#End Region
#Region "7 Time "
        <DescriptionAttribute("(TotalSetupTime + TotalMachineTime)" + _
            Chr(10) + "(Seconds)"), _
        DisplayName("Total Labor Time"), _
        CategoryAttribute(SortedSpaces7 + "Time Summary")> _
        Public ReadOnly Property TotalLaborTime() As Decimal
            Get
                Return Math.Round(_Subject.TotalLaborTime, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property
        <DescriptionAttribute("Time Multiplier"),
         DisplayName("Time Multiplier"), _
         CategoryAttribute(SortedSpaces7 + "Time Summary")> _
        Public Property TimeMultiplier As Decimal
            Get
                Return Math.Round(_Subject.TimeMultiplier, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Decimal)
                Me._Subject.TimeMultiplier = value
                MyBase.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("TimeMultiplier * TotalLaborTime" + Chr(10) + "(Seconds)"), _
        DisplayName("Adjusted Total Labor Time"), _
        CategoryAttribute(SortedSpaces7 + "Time Summary")> _
        Public ReadOnly Property AdjustedTotalLaborTime() As Decimal
            Get
                Return Math.Round(_Subject.AdjustedTotalLaborTime, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property

        <DescriptionAttribute("AdjustedTotalLaborTime / (60 * 60)" + Chr(10) + "(Hours)"), _
        DisplayName("Adjusted Total Labor Time Hours"), _
        CategoryAttribute(SortedSpaces7 + "Time Summary")> _
        Public ReadOnly Property AdjustedTotalLaborTimeHours() As Decimal
            Get
                Return Math.Round(_Subject.AdjustedTotalLaborTimeHours, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property
#End Region
#Region "8 Labor "
        <DescriptionAttribute("Used to Computer Labor Costs. " + Chr(10) + "(Dollars Per Hour)"), _
        DisplayName("Labor Rate"), _
        CategoryAttribute(SortedSpaces8 + "Labor")> _
        Public Property LaborRate As Decimal
            Get
                Return Math.Round(_Subject.LaborRate, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal Value As Decimal)
                _Subject.LaborRate = Value
                MyBase.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("AdjustedTotalLaborTimeHours * LaborRate" + Chr(10) + "(Dollars)"), _
        DisplayName("Labor Cost"), _
        CategoryAttribute(SortedSpaces8 + "Labor")> _
        Public ReadOnly Property LaborCost As Decimal
            Get
                Return Math.Round(_Subject.LaborCost, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property
#End Region
#Region "9 Shipping "
        <DescriptionAttribute("The type of quote"), _
        DisplayName("Quote Type"), _
        CategoryAttribute(SortedSpaces9 + "Shipping"), _
        TypeConverter(GetType(QuoteTypeList))> _
        Public Property QuoteType As String
            Get
                Return _Subject.QuoteType
            End Get
            Set(ByVal value As String)
                _Subject.QuoteType = value
                MyBase.SendEvents()
            End Set
        End Property
        <DisplayName("Minimum Order Quantity"),
        CategoryAttribute(SortedSpaces9 + "Shipping")> _
        Public Property MinimumOrderQuantity As Integer
            Get
                Return _Subject.MinimumOrderQuantity
            End Get
            Set(ByVal Value As Integer)
                _Subject.MinimumOrderQuantity = Value
                MyBase.SendEvents()
            End Set
        End Property
        <DisplayName("Single Definite Quantity"), _
        CategoryAttribute(SortedSpaces9 + "Shipping")> _
        Public Property SingleDefiniteQuantity As Integer
            Get
                Return _Subject.SingleDefiniteQuantity
            End Get
            Set(ByVal Value As Integer)
                _Subject.SingleDefiniteQuantity = Value
                MyBase.SendEvents()
            End Set
        End Property
        <DisplayName("Order Quantity"), _
        CategoryAttribute(SortedSpaces9 + "Shipping")> _
        Public Property OrderQuantity As Integer
            Get
                Return _Subject.OrderQuantity
            End Get
            Set(ByVal Value As Integer)
                _Subject.OrderQuantity = Value
                MyBase.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("If(QuoteType = PRODUCTION) then " + Chr(10) + _
            "     MinimumOrderQuantity" + Chr(10) + _
            "ElseIf(QuoteType = SINGLE_DEFINATE) then " + Chr(10) + _
            "     SingleDefinateQuantity" + Chr(10) + _
            "Else OrderQuantity" + Chr(10) _
            ), _
        DisplayName("Functional Quantity"), _
        CategoryAttribute(SortedSpaces9 + "Shipping")> _
        Public ReadOnly Property FunctionalQuantity As Integer
            Get
                Return _Subject.FunctionalQuantity
            End Get
        End Property
        <DescriptionAttribute("Description of the Shipping Container"), _
        DisplayName("Shipping Container"), _
        CategoryAttribute(SortedSpaces9 + "Shipping"), _
        TypeConverter(GetType(ShippingList))> _
        Public Property ShippingContainer() As String
            Get
                Return _Subject.ShippingContainer
            End Get
            Set(ByVal Value As String)
                _Subject.ShippingContainer = Value
                MyBase.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("Cost of the Shipping Container" + Chr(10) + "(Dollars)"), _
        DisplayName("Shipping Container Cost"), _
        CategoryAttribute(SortedSpaces9 + "Shipping")> _
        Public ReadOnly Property ShippingContainerCost As Decimal
            Get
                Return Math.Round(_Subject.ShippingContainerCost, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property
        <DescriptionAttribute("ShippingContainerCost / FunctionalQuantity" + Chr(10) + "(Dollars)"), _
        DisplayName("Shipping Container Cost Per Order"), _
        CategoryAttribute(SortedSpaces9 + "Shipping")> _
        Public ReadOnly Property ShippingContainerCostPerOrder As Decimal
            Get
                Return Math.Round(_Subject.ShippingContainerCostPerOrder, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property
        <DescriptionAttribute("Shipping Cost" + Chr(10) + "(Dollars)"), _
        DisplayName("Shipping Cost"), _
        CategoryAttribute(SortedSpaces9 + "Shipping")> _
        Public Property ShippingCost() As Decimal
            Get
                Return Math.Round(_Subject.ShippingCost, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal Value As Decimal)
                _Subject.ShippingCost = Value
                MyBase.SendEvents()
            End Set
        End Property
#End Region
#Region "10 Summary "
        <DescriptionAttribute("Mat'l"), _
        DisplayName("Mat'l"), _
        CategoryAttribute(SortedSpaces10 + "ExecutiveSummary")> _
        Public ReadOnly Property SummaryMaterial As String
            Get
                Return _Subject.SummaryMaterial
            End Get
        End Property
        <DescriptionAttribute("MU"), _
        DisplayName("MU"), _
        CategoryAttribute(SortedSpaces10 + "ExecutiveSummary")> _
        Public ReadOnly Property SummaryTVMCIncrement As String
            Get
                Return _Subject.SummaryTVMCIncrement
            End Get
        End Property
        <DescriptionAttribute("DL * 9.5"), _
        DisplayName("DL"), _
        CategoryAttribute(SortedSpaces10 + "ExecutiveSummary")> _
        Public ReadOnly Property SummaryDirectLabor As String
            Get
                Return _Subject.SummaryDirectLabor
            End Get
        End Property
        <DescriptionAttribute("DL * 1.Common.GlobalOptions.DecimalPointsToDisplay65"), _
        DisplayName("OH"), _
        CategoryAttribute(SortedSpaces10 + "ExecutiveSummary")> _
        Public ReadOnly Property SummaryOverhead As String
            Get
                Return _Subject.SummaryOverhead
            End Get
        End Property
        <DescriptionAttribute("Adjustment Multiplier"), _
        DisplayName("F/B-Test Board Multiplier"), _
        CategoryAttribute(SortedSpaces10 + "ExecutiveSummary")> _
        Public Property SummaryAdjustment As Decimal
            Get
                Return Math.Round(_Subject.SummaryAdjustment, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Decimal)
                _Subject.SummaryAdjustment = value
                MyBase.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("Mat'l + MU + DL + OH) * Adjustment Multiplier"), _
        DisplayName("F/B-Test Board"), _
        CategoryAttribute(SortedSpaces10 + "ExecutiveSummary")> _
        Public ReadOnly Property SummaryCostAdjustment As String
            Get
                Return _Subject.SummaryCostAdjustment
            End Get
        End Property
        <DescriptionAttribute("UnitCost - (Mat'l + MU + DL + OH + F/B-Test Board)"), _
        DisplayName("Profit"), _
        CategoryAttribute(SortedSpaces10 + "ExecutiveSummary")> _
        Public ReadOnly Property SummaryProfit As String
            Get
                Return _Subject.SummaryProfit
            End Get
        End Property
#End Region
#Region "11 Total "
        <DescriptionAttribute("TotalVariableMaterialCost + LaborCost" _
            + Chr(10) + "(Dollars)"), _
        DisplayName("Total Unit Cost"), _
        CategoryAttribute(SortedSpaces11 + "Total")> _
        Public ReadOnly Property TotalUnitCost() As Decimal
            Get
                Return Math.Round(_Subject.TotalUnitCost, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property
        <DescriptionAttribute("Manufacturing Markup"), _
        DisplayName("Manufacturing Markup"), _
        CategoryAttribute(SortedSpaces11 + "Total")> _
        Public Property ManufacturingMarkup As Decimal
            Get
                Return Math.Round(_Subject.ManufacturingMarkup, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Decimal)
                _Subject.ManufacturingMarkup = value
                MyBase.SendEvents()
            End Set
        End Property
        <FilterAttribute(False), DescriptionAttribute("(TotalUnitCost * ManufacturingMarkup)+F/B-Test Board" + Chr(10) + "(Dollars)"), _
        DisplayName("Adjusted Total Unit Cost"), _
        CategoryAttribute(SortedSpaces11 + "Total")> _
        Public ReadOnly Property AdjustedTotalUnitCost() As Decimal
            Get
                Return Math.Round(_Subject.AdjustedTotalUnitCost, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property

#End Region

#Region "TypeDescriptor Implementation"
        Public Function GetClassName() As String Implements ICustomTypeDescriptor.GetClassName
            Return TypeDescriptor.GetClassName(Me, True)
        End Function
        Public Function GetAttributes() As AttributeCollection Implements ICustomTypeDescriptor.GetAttributes
            Return TypeDescriptor.GetAttributes(Me, True)
        End Function
        Public Function GetComponentName() As String Implements ICustomTypeDescriptor.GetComponentName
            Return TypeDescriptor.GetComponentName(Me, True)
        End Function
        Public Function GetConverter() As TypeConverter Implements ICustomTypeDescriptor.GetConverter
            Return TypeDescriptor.GetConverter(Me, True)
        End Function
        Public Function GetDefaultEvent() As EventDescriptor Implements ICustomTypeDescriptor.GetDefaultEvent
            Return TypeDescriptor.GetDefaultEvent(Me, True)
        End Function
        Public Function GetDefaultProperty() As PropertyDescriptor Implements ICustomTypeDescriptor.GetDefaultProperty
            Return TypeDescriptor.GetDefaultProperty(Me, True)
        End Function
        Public Function GetEditor(ByVal editorBaseType As Type) As Object Implements ICustomTypeDescriptor.GetEditor
            Return TypeDescriptor.GetEditor(Me, editorBaseType, True)
        End Function
        Public Function GetEvents(ByVal attributes As Attribute()) As EventDescriptorCollection Implements ICustomTypeDescriptor.GetEvents
            Return TypeDescriptor.GetEvents(Me, attributes, True)
        End Function
        Public Function GetEvents() As EventDescriptorCollection Implements ICustomTypeDescriptor.GetEvents
            Return TypeDescriptor.GetEvents(Me, True)
        End Function
        Public Function GetProperties(ByVal attributes As Attribute()) As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
            Return _Subject.FilterProperties(TypeDescriptor.GetProperties(Me, attributes, True))
        End Function
        Public Function GetProperties() As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
            Return TypeDescriptor.GetProperties(Me, True)
        End Function
        Public Function GetPropertyOwner(ByVal pd As PropertyDescriptor) As Object Implements ICustomTypeDescriptor.GetPropertyOwner
            Return Me
        End Function
#End Region

    End Class
End Namespace