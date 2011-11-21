Imports System.ComponentModel
Imports System.Reflection
Imports System.Math

Namespace Model.BOM

    Public Class ComputationProperties
        Inherits Common.ComputationProperties
        'dd_Changed 11/13/11 Added numbers to CategoryAttributes
        '  rearranged sub items 
        '  turn off alph sub item sorting in grid properties
        Public Sub New(ByVal Header As Object)
            _Header = Header
        End Sub
#Region " Variables "

        Private _Header As Header
        Private _ShippingContainerCost As Decimal
        Private _ShippingCost As Decimal
        Private _ShippingBox As String = "NoBox"
        Private _TimeMultiplier As Decimal = 1.15
        Private _ManufacturingMarkup As Decimal = 1.25
        Private _LaborRate As Decimal = 21.5
        Private _WireSetupTime As Integer = 300
        Private _WireMachineTime As Decimal = 30
        Private _NumberOfCuts As Decimal = 0
        Private _MinimumOrderQuantity As Integer = 0
        Private _OrderQuantity As Integer = 0
        Private _SingleDefQuantity As Integer = 0
        Private _PercentCopperScrap As Decimal = 3
        Private _CopperPrice As Decimal = 3.57
        Private _MaterialMarkup As Decimal = 1.075
        Private _ComponentSetupTime As Decimal

        Private _QuoteType As String = "Production"

        Private _NumberOfTwistedPairs As Integer
        Private _TimePerTwistedPairs As Decimal = 300

        Private _LengthOfTwistedWiresA As Decimal
        Private _LengthOfTwistedWiresB As Decimal
        Private _LengthOfTwistedWiresC As Decimal
        Private _RunTimeWireRateTW2 As Decimal = 300
        Private _RunTimeWireRateTW3 As Decimal = 300
        Private _RunTimeWireRateTW4 As Decimal = 300
        Private _SetupTW2 As Decimal = 100
        Private _SetupTW3 As Decimal = 100
        Private _SetupTW4 As Decimal = 100
        Private _NumberOf2Wires As Integer
        Private _NumberOf3Wires As Integer
        Private _NumberOf4Wires As Integer

        Private _SummaryMaterial As Decimal
        Private _SummaryTVMCIncrement As Decimal
        Private _SummaryDirectLabor As Decimal
        Private _SummaryOverhead As Decimal
        Private _SummaryAdjustmentMultiplyer As Decimal = 0.08
        Private _SummaryCostAdjustment As Decimal
        Private _SummaryProfit As Decimal
        Const SortedSpaces1 = "          "
        Const SortedSpaces2 = "          "
        Const SortedSpaces3 = "          "
        Const SortedSpaces4 = "          "
        Const SortedSpaces5 = "          "
        Const SortedSpaces6 = "          "
        Const SortedSpaces7 = "          "
        Const SortedSpaces8 = "          "
        Const SortedSpaces9 = "          "
        Const SortedSpaces10 = "          "
        Const SortedSpaces11 = "          "
#End Region
        '        Public Interface ICustomTypeDescriptor

        '        End Interface


#Region "1 Copper "
        <DescriptionAttribute("Weight of Copper. " + Chr(10) + "(Pounds)"), _
        DisplayName("Copper Weight"), _
        CategoryAttribute(SortedSpaces1 + "Copper")> _
        Public ReadOnly Property CopperWeight As Decimal
            Get
                Return Me._Header.WeightProperties.Weight
            End Get
        End Property
        <DescriptionAttribute("Percent of Scrap Copper. " + Chr(10) + "(Percent)"), _
        DisplayName("Percent Copper Scrap"), _
        CategoryAttribute(SortedSpaces1 + "Copper")> _
        Public Property PercentCopperScrap As Integer
            Get
                Return Me._PercentCopperScrap
            End Get
            Set(ByVal value As Integer)
                Me._PercentCopperScrap = value
                Me.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("CopperWeight * (PercentCopperScrap / 100)" + Chr(10) + "(Pounds)"), _
        DisplayName("Copper Scrap Weight"), _
        CategoryAttribute(SortedSpaces1 + "Copper")> _
        Public ReadOnly Property CopperScrapWeight As Decimal
            Get
                Dim percent As Decimal = (Me._PercentCopperScrap / 100)
                Return Math.Round(Me.CopperWeight * percent, 4)
            End Get
        End Property
        <DescriptionAttribute("Copper Price" + Chr(10) + "(Dollars Per Pounds)"), _
        DisplayName("Copper Price"), _
        CategoryAttribute(SortedSpaces1 + "Copper")> _
        Public Property CopperPrice As Decimal
            Get
                Return Me._CopperPrice
            End Get
            Set(ByVal value As Decimal)
                Me._CopperPrice = Math.Round(value, 4)
                Me.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("CopperScrapWeight * CopperPrice. " _
            + Chr(10) + "(Dollars Per Pounds)"), _
        DisplayName("Copper Scrap Cost"), _
        CategoryAttribute(SortedSpaces1 + "Copper")> _
        Public ReadOnly Property CopperScrapCost As Decimal
            Get
                Return Math.Round(Me.CopperScrapWeight * Me.CopperPrice, 4)
            End Get
        End Property

#End Region
#Region "2 Wires "
        <DescriptionAttribute("Number of Wires" + Chr(10) + "(Count)"), _
        DisplayName("Number of Wires"), _
        CategoryAttribute(SortedSpaces2 + "Wire")> _
        Public ReadOnly Property NumberOfWires() As Decimal
            Get
                Return Count(True)
            End Get
        End Property

        <DescriptionAttribute("Wire Length" + Chr(10) + "(Decimeter)"), _
        DisplayName("Wire Length"), _
        CategoryAttribute(SortedSpaces2 + "Wire")> _
        Public ReadOnly Property WireLength() As Decimal
            Get
                Return SumQty(True)
            End Get
        End Property

        <DescriptionAttribute("WireLength / 3.048" + Chr(10) + "(Feet)"), _
        DisplayName("Wire Length Feet"), _
        CategoryAttribute(SortedSpaces2 + "Wire")> _
        Public ReadOnly Property WireLengthFeet() As Decimal
            Get
                Return Math.Round(SumQty(True) / 3.048, 4)
            End Get
        End Property

#End Region
#Region "3 Material Cost "
        <DescriptionAttribute("Sum(UnitCost * Quantity)" + Chr(10) + "(Dollar)"), _
        DisplayName("Component Material Cost"), _
        CategoryAttribute(SortedSpaces3 + "Material Cost")> _
        Public ReadOnly Property ComponentMaterialCost() As Decimal
            Get
                Return Math.Round(SumCost(False), 4)
            End Get
        End Property
        <DescriptionAttribute("Sum(UnitCost * Quantity)" + Chr(10) + "(Dollar)"), _
        DisplayName("Wire Material Cost"), _
        CategoryAttribute(SortedSpaces3 + "Material Cost")> _
        Public ReadOnly Property WireMaterialCost() As Decimal
            Get
                Return Math.Round(SumCost(True), 4)
            End Get
        End Property
        <DescriptionAttribute("ComponentMaterialCost + WireMaterialCost + ShippingContainerCostPerOrder" + Chr(10) + "(Dollar)"), _
        DisplayName("Total Material Cost"), _
        CategoryAttribute(SortedSpaces3 + "Material Cost")> _
        Public ReadOnly Property TotalMaterialCost() As Decimal
            Get
                Return Math.Round( _
                    Me.ComponentMaterialCost + _
                    Me.WireMaterialCost + _
                    Me.ShippingContainerCostPerOrder, 4)
            End Get
        End Property
        <DescriptionAttribute("Material Markup"), _
        DisplayName("Material Markup"), _
        CategoryAttribute(SortedSpaces3 + "Material Cost")> _
        Public Property MaterialMarkUp As Decimal
            Get
                Return _MaterialMarkup
            End Get
            Set(ByVal value As Decimal)
                Me._MaterialMarkup = value
                Me.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("TotalMaterialCost * MaterialMarkup" + Chr(10) + "(Dollars)"), _
        DisplayName("Adjusted Total Material Cost"), _
        CategoryAttribute(SortedSpaces3 + "Material Cost")> _
        Public ReadOnly Property AdjustedTotalMaterialCost As Decimal
            Get
                Return Math.Round(TotalMaterialCost * Me._MaterialMarkup, 4)
            End Get
        End Property
        <DescriptionAttribute("(TotalMaterialCost * MaterialMarkup)" + _
            " + CopperCost + ShippingCost" + Chr(10) + "(Dollar)"), _
        DisplayName("Total Variable Material Cost"), _
        CategoryAttribute(SortedSpaces3 + "Material Cost")> _
        Public ReadOnly Property TotalVariableMaterialCost() As Decimal
            Get
                Return Math.Round( _
                    (Me.TotalMaterialCost * Me._MaterialMarkup) + _
                     Me.CopperScrapCost + _
                     Me.ShippingCost, 4)
            End Get
        End Property
#End Region
#Region "4 Twisted Pairs "
        <DescriptionAttribute("Number Of Twisted Pairs " + Chr(10) + "(Number)"), _
        DisplayName("Twisted Pairs"), _
        CategoryAttribute(SortedSpaces4 + "Twisted Wire")> _
        Public Property NumberOfTwistedPairs As Integer
            Get
                Return _NumberOfTwistedPairs
            End Get
            Set(ByVal value As Integer)
                Me._NumberOfTwistedPairs = value
                Me.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("Twisted Pairs Machine Time " + Chr(10) + "(Number)"), _
        DisplayName("Twisted Pairs Machine Time"), _
        CategoryAttribute(SortedSpaces4 + "Twisted Wire")> _
        Public ReadOnly Property TwistedPairsMachineTime As Decimal
            Get
                Return (Me._NumberOfTwistedPairs * 300)
            End Get
        End Property
#End Region
#Region " Twisted Pairs Computation "

        '<DescriptionAttribute("Total Twisting Time " + Chr(10) + "(Seconds)"), _
        'DisplayName("Total Twisting time"), _
        'CategoryAttribute("Experimental")> _
        'Public ReadOnly Property TotalTwistingTime As Decimal
        '    Get
        '        Return (Me.TwistingRuntime + Me.TwistingSetupTime)
        '    End Get
        'End Property

        '<DescriptionAttribute("(_LengthOfTwistedWiresA * _RunTimeWireRateTW2) + " + Chr(10) + _
        '    "(_LengthOfTwistedWiresB * _RunTimeWireRateTW3) + " + Chr(10) + _
        '    "(_LengthOfTwistedWiresC * _RunTimeWireRateTW4)" + Chr(10)), _
        'DisplayName("Twisting Runtime"), _
        'CategoryAttribute("Experimental")> _
        'Public ReadOnly Property TwistingRuntime As Decimal
        '    Get
        '        Return _
        '            (_LengthOfTwistedWiresA * _RunTimeWireRateTW2) + _
        '            (_LengthOfTwistedWiresB * _RunTimeWireRateTW3) + _
        '            (_LengthOfTwistedWiresC * _RunTimeWireRateTW4)
        '    End Get
        'End Property

        '<DescriptionAttribute("((SetupTW2 * NumberOf2Wires) + " + Chr(10) + _
        '    "(SetupTW3 * NumberOf3Wires) + " + Chr(10) + _
        '    "(SetupTW4 * NumberOf4Wires)) / MinimumOrderQuantity " + Chr(10) + _
        '    Chr(10) + "(Seconds)"), _
        'DisplayName("Twisting Setup Time"), _
        'CategoryAttribute("Experimental")> _
        'Public ReadOnly Property TwistingSetupTime As Decimal
        '    Get
        '        Return _
        '            (_SetupTW2 * _NumberOf2Wires) + _
        '            (_SetupTW3 * _NumberOf3Wires) + _
        '            (_SetupTW4 * _NumberOf4Wires)
        '    End Get
        'End Property

        '<DescriptionAttribute("Length of Twisted Wires (A)" + Chr(10) + "(Number)"), _
        'DisplayName("Length of Twisted Wires (A)"), _
        'CategoryAttribute("Experimental")> _
        'Public Property LengthOfTwistedWiresA As Decimal
        '    Get
        '        Return Me._LengthOfTwistedWiresA
        '    End Get
        '    Set(ByVal value As Decimal)
        '        Me._LengthOfTwistedWiresA = value
        '        Me.SendEvents()
        '    End Set
        'End Property

        '<DescriptionAttribute("Length of Twisted Wires (B)" + Chr(10) + "(Number)"), _
        'DisplayName("Length of Twisted Wires (B)"), _
        'CategoryAttribute("Experimental")> _
        'Public Property LengthOfTwistedWiresB As Decimal
        '    Get
        '        Return Me._LengthOfTwistedWiresB
        '    End Get
        '    Set(ByVal value As Decimal)
        '        Me._LengthOfTwistedWiresB = value
        '        Me.SendEvents()
        '    End Set
        'End Property

        '<DescriptionAttribute("Length of Twisted Wires (C)" + Chr(10) + "(Number)"), _
        'DisplayName("Length of Twisted Wires (C)"), _
        'CategoryAttribute("Experimental")> _
        'Public Property LengthOfTwistedWiresC As Decimal
        '    Get
        '        Return Me._LengthOfTwistedWiresC
        '    End Get
        '    Set(ByVal value As Decimal)
        '        Me._LengthOfTwistedWiresC = value
        '        Me.SendEvents()
        '    End Set
        'End Property

        '<DescriptionAttribute("Run Time Wire Rate TW2" + Chr(10) + "(Number)"), _
        'DisplayName("Run Time Wire Rate TW2"), _
        'CategoryAttribute("Experimental")> _
        'Public Property RunTimeWireRateTW2 As Decimal
        '    Get
        '        Return Me._RunTimeWireRateTW2
        '    End Get
        '    Set(ByVal value As Decimal)
        '        Me._RunTimeWireRateTW2 = value
        '        Me.SendEvents()
        '    End Set
        'End Property

        '<DescriptionAttribute("Run Time Wire Rate TW3" + Chr(10) + "(Number)"), _
        'DisplayName("Run Time Wire Rate TW3"), _
        'CategoryAttribute("Experimental")> _
        'Public Property RunTimeWireRateTW3 As Decimal
        '    Get
        '        Return Me._RunTimeWireRateTW3
        '    End Get
        '    Set(ByVal value As Decimal)
        '        Me._RunTimeWireRateTW3 = value
        '        Me.SendEvents()
        '    End Set
        'End Property

        '<DescriptionAttribute("Run Time Wire Rate TW4" + Chr(10) + "(Number)"), _
        'DisplayName("Run Time Wire Rate TW4"), _
        'CategoryAttribute("Experimental")> _
        'Public Property RunTimeWireRateTW4 As Decimal
        '    Get
        '        Return Me._RunTimeWireRateTW4
        '    End Get
        '    Set(ByVal value As Decimal)
        '        Me._RunTimeWireRateTW4 = value
        '        Me.SendEvents()
        '    End Set
        'End Property

        '<DescriptionAttribute("Setup TW2" + Chr(10) + "(Number)"), _
        'DisplayName("Setup TW2"), _
        'CategoryAttribute("Experimental")> _
        'Public Property SetupTW2 As Decimal
        '    Get
        '        Return Me._SetupTW2
        '    End Get
        '    Set(ByVal value As Decimal)
        '        Me._SetupTW2 = value
        '        Me.SendEvents()
        '    End Set
        'End Property

        '<DescriptionAttribute("Setup TW3" + Chr(10) + "(Number)"), _
        'DisplayName("Setup TW3"), _
        'CategoryAttribute("Experimental")> _
        'Public Property SetupTW3 As Decimal
        '    Get
        '        Return Me._SetupTW3
        '    End Get
        '    Set(ByVal value As Decimal)
        '        Me._SetupTW3 = value
        '        Me.SendEvents()
        '    End Set
        'End Property

        '<DescriptionAttribute("Setup TW4" + Chr(10) + "(Number)"), _
        'DisplayName("Setup TW4"), _
        'CategoryAttribute("Experimental")> _
        'Public Property SetupTW4 As Decimal
        '    Get
        '        Return Me._SetupTW4
        '    End Get
        '    Set(ByVal value As Decimal)
        '        Me._SetupTW4 = value
        '        Me.SendEvents()
        '    End Set
        'End Property

        '<DescriptionAttribute("Number Of 2 Wires" + Chr(10) + "(Number)"), _
        'DisplayName("Number Of 2 Wires"), _
        'CategoryAttribute("Experimental")> _
        'Public Property NumberOf2Wires As Integer
        '    Get
        '        Return Me._NumberOf2Wires
        '    End Get
        '    Set(ByVal value As Integer)
        '        Me._NumberOf2Wires = value
        '        Me.SendEvents()
        '    End Set
        'End Property

        '<DescriptionAttribute("Number Of 3 Wires" + Chr(10) + "(Number)"), _
        'DisplayName("Number Of 3 Wires"), _
        'CategoryAttribute("Experimental")> _
        'Public Property NumberOf3Wires As Integer
        '    Get
        '        Return Me._NumberOf3Wires
        '    End Get
        '    Set(ByVal value As Integer)
        '        Me._NumberOf3Wires = value
        '        Me.SendEvents()
        '    End Set
        'End Property

        '<DescriptionAttribute("Number Of 4 Wires" + Chr(10) + "(Number)"), _
        'DisplayName("Number Of 4 Wires"), _
        'CategoryAttribute("Experimental")> _
        'Public Property NumberOf4Wires As Integer
        '    Get
        '        Return Me._NumberOf4Wires
        '    End Get
        '    Set(ByVal value As Integer)
        '        Me._NumberOf4Wires = value
        '        Me.SendEvents()
        '    End Set
        'End Property

#End Region
#Region "5 Setup Time "
        <DescriptionAttribute("Number of Components" + Chr(10) + "(Count)"), _
        DisplayName("Number of Components"), _
        CategoryAttribute(SortedSpaces5 + "Setup Time")> _
        Public ReadOnly Property NumberOfComponents() As Decimal
            Get
                Return Count(False)
            End Get
        End Property
        <DescriptionAttribute("Component Setup Time" + Chr(10) + "(Seconds)"), _
        DisplayName("Component Setup Time"), _
        CategoryAttribute(SortedSpaces5 + "Setup Time")> _
        Public Property ComponentSetupTime() As Integer
            Get
                Return _ComponentSetupTime
            End Get
            Set(ByVal value As Integer)
                _ComponentSetupTime = value
                Me.SendEvents()
            End Set
        End Property

        <DescriptionAttribute("Setup time to cut a particular length of wire. (Cut time)" + Chr(10) + "(Seconds Per Cut)"), _
        DisplayName("Wire Setup Time Multiplier"), _
        CategoryAttribute(SortedSpaces5 + "Setup Time")> _
        Public Property WireSetupTime As Integer
            Get
                Return _WireSetupTime
            End Get
            Set(ByVal value As Integer)
                _WireSetupTime = value
                Me.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("Number of lines in 'CIRCUIT DATA TABLE'" + _
            " in Engineering Print." + Chr(10) + "(Count)"), _
        DisplayName("Number of Cuts"), _
        CategoryAttribute(SortedSpaces5 + "Setup Time")> _
        Public Property NumberOfCuts() As Integer
            Get
                Return _NumberOfCuts
            End Get
            Set(ByVal value As Integer)
                _NumberOfCuts = value
                Me.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("NumberOfCuts * WireSetupTime" + Chr(10) + "(Seconds)"), _
        DisplayName("Total Wire Setup Time"), _
        CategoryAttribute(SortedSpaces5 + "Setup Time")> _
        Public ReadOnly Property TotalWireSetupTime As Decimal
            Get
                Return Math.Round(Me.NumberOfCuts * Me.WireSetupTime, 4)
            End Get
        End Property
        <DescriptionAttribute("(TotalWireSetupTime + ComponentSetupTime) " + _
            "/ MinimumOrderQuantity" + _
            Chr(10) + "(Seconds)"), _
        DisplayName("Adjusted Total Setup Time"), _
        CategoryAttribute(SortedSpaces5 + "Setup Time")> _
        Public ReadOnly Property TotalSetupTime() As Decimal
            Get
                If Me.MinimumOrderQuantity = 0 Then
                    Return 0
                End If
                Return Math.Round( _
                    (Me.TotalWireSetupTime + Me.ComponentSetupTime) _
                    / Me.MinimumOrderQuantity, 4)
            End Get
        End Property
#End Region
#Region "6 Machine Time "
        <DescriptionAttribute("Sum(ComponentMachineTime) " + Chr(10) + "(Seconds)"), _
        DisplayName("Total Component Run Time"), _
        CategoryAttribute(SortedSpaces6 + "Run Time")> _
        Public ReadOnly Property TotalComponentMachineTime As Decimal
            Get
                Return Math.Round(Me.SumTime(False), 4)
            End Get
        End Property
        <DescriptionAttribute("WireLengthFeet * WireMachineTime " + Chr(10) + "(Seconds)"), _
        DisplayName("Total Wire Run Time"), _
        CategoryAttribute(SortedSpaces6 + "Run Time")> _
        Public ReadOnly Property TotalWireMachineTime As Decimal
            Get
                Return Math.Round(Me.WireLengthFeet * Me.WireMachineTime, 4)
            End Get
        End Property
        <DescriptionAttribute("Used with TotalWireMachineTime " + Chr(10) + "(Seconds)"), _
        DisplayName("Wire Run Time Multiplier"), _
        CategoryAttribute(SortedSpaces6 + "Run Time")> _
        Public Property WireMachineTime As Decimal
            Get
                Return Me._WireMachineTime
            End Get
            Set(ByVal value As Decimal)
                Me._WireMachineTime = value
                Me.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("TotalWireMachineTime + " + _
            "TotalComponentMachineTime + TwistedPairsMachineTime" + Chr(10) + "(Seconds)"), _
        DisplayName("Total Run Time"), _
        CategoryAttribute(SortedSpaces6 + "Run Time")> _
        Public ReadOnly Property TotalMachineTime As Decimal
            Get
                Return Math.Round(Me.TotalComponentMachineTime + _
                    Me.TotalWireMachineTime + TwistedPairsMachineTime, 4
                    )
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
                Return (Me.TotalSetupTime + Me.TotalMachineTime)
            End Get
        End Property
        <DescriptionAttribute("Time Multiplier"),
         DisplayName("Time Multiplier"), _
         CategoryAttribute(SortedSpaces7 + "Time Summary")> _
        Public Property TimeMultiplier As Decimal
            Get
                Return _TimeMultiplier
            End Get
            Set(ByVal value As Decimal)
                Me._TimeMultiplier = value
                Me.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("TimeMultiplier * TotalLaborTime" + Chr(10) + "(Seconds)"), _
        DisplayName("Adjusted Total Labor Time"), _
        CategoryAttribute(SortedSpaces7 + "Time Summary")> _
        Public ReadOnly Property AdjustedTotalLaborTime() As Decimal
            Get
                Return Math.Round(Me._TimeMultiplier * Me.TotalLaborTime, 4)
            End Get
        End Property

        <DescriptionAttribute("AdjustedTotalLaborTime / (60 * 60)" + Chr(10) + "(Hours)"), _
        DisplayName("Adjusted Total Labor Time Hours"), _
        CategoryAttribute(SortedSpaces7 + "Time Summary")> _
        Public ReadOnly Property AdjustedTotalLaborTimeHours() As Decimal
            Get
                Return Math.Round(CDec(Me.AdjustedTotalLaborTime) / (60 * 60), 4)
            End Get
        End Property
#End Region
#Region "8 Labor "
        <DescriptionAttribute("Used to Computer Labor Costs. " + Chr(10) + "(Dollars Per Hour)"), _
        DisplayName("Labor Rate"), _
        CategoryAttribute(SortedSpaces8 + "Labor")> _
        Public Property LaborRate As Decimal
            Get
                Return _LaborRate
            End Get
            Set(ByVal Value As Decimal)
                _LaborRate = Math.Round(Value, 4)
                Me.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("AdjustedTotalLaborTimeHours * LaborRate" + Chr(10) + "(Dollars)"), _
        DisplayName("Labor Cost"), _
        CategoryAttribute(SortedSpaces8 + "Labor")> _
        Public ReadOnly Property LaborCost As Decimal
            Get
                Return Math.Round(AdjustedTotalLaborTimeHours * LaborRate, 4)
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
                Return _QuoteType
            End Get
            Set(ByVal value As String)
                _QuoteType = value
                Me.SendEvents()
            End Set
        End Property
        <DisplayName("Minimum Order Quantity"),
        CategoryAttribute(SortedSpaces9 + "Shipping")> _
        Public Property MinimumOrderQuantity As Integer
            Get
                Return _MinimumOrderQuantity
            End Get
            Set(ByVal Value As Integer)
                _MinimumOrderQuantity = Value
                Me.SendEvents()
            End Set
        End Property
        <DisplayName("Single Definite Quantity"), _
        CategoryAttribute(SortedSpaces9 + "Shipping")> _
        Public Property SingleDefiniteQuantity As Integer
            Get
                Return _SingleDefQuantity
            End Get
            Set(ByVal Value As Integer)
                _SingleDefQuantity = Value
                Me.SendEvents()
            End Set
        End Property
        <DisplayName("Order Quantity"), _
        CategoryAttribute(SortedSpaces9 + "Shipping")> _
        Public Property OrderQuantity As Integer
            Get
                Return _OrderQuantity
            End Get
            Set(ByVal Value As Integer)
                _OrderQuantity = Value
                Me.SendEvents()
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
                Dim result As Integer
                If _QuoteType = QuoteTypeList.PRODUCTION Then
                    result = Me._MinimumOrderQuantity
                ElseIf _QuoteType = QuoteTypeList.SINGLE_DEFINATE Then
                    result = Me._SingleDefQuantity
                Else
                    result = Me._OrderQuantity
                End If
                Return result
            End Get
        End Property
        <DescriptionAttribute("Description of the Shipping Container"), _
        DisplayName("Shipping Container"), _
        CategoryAttribute(SortedSpaces9 + "Shipping"), _
        TypeConverter(GetType(ShippingList))> _
        Public Property ShippingContainer() As String
            Get
                Return _ShippingBox
            End Get
            Set(ByVal Value As String)
                _ShippingBox = Value
                Me.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("Cost of the Shipping Container" + Chr(10) + "(Dollars)"), _
        DisplayName("Shipping Container Cost"), _
        CategoryAttribute(SortedSpaces9 + "Shipping")> _
        Public ReadOnly Property ShippingContainerCost As Decimal
            Get
                If (_ShippingBox Is Nothing) Then
                    Return 0
                End If
                Return Math.Round(Shipping.Shipping.Lookup(Me._ShippingBox), 4)
            End Get
        End Property
        <DescriptionAttribute("ShippingContainerCost / FunctionalQuantity" + Chr(10) + "(Dollars)"), _
        DisplayName("Shipping Container Cost Per Order"), _
        CategoryAttribute(SortedSpaces9 + "Shipping")> _
        Public ReadOnly Property ShippingContainerCostPerOrder As Decimal
            Get
                If (Me.FunctionalQuantity = 0) Then
                    Return 0
                End If
                Return Math.Round(Me.ShippingContainerCost / Me.FunctionalQuantity, 4)
            End Get
        End Property
        <DescriptionAttribute("Shipping Cost" + Chr(10) + "(Dollars)"), _
        DisplayName("Shipping Cost"), _
        CategoryAttribute(SortedSpaces9 + "Shipping")> _
        Public Property ShippingCost() As Decimal
            Get
                Return _ShippingCost
            End Get
            Set(ByVal Value As Decimal)
                _ShippingCost = Value
                Me.SendEvents()
            End Set
        End Property
#End Region
        'dd_Added 11/14/11 
#Region "10 Summary "
        <DescriptionAttribute("Mat'l"), _
        DisplayName("Mat'l"), _
        CategoryAttribute(SortedSpaces10 + "ExecutiveSummary")> _
        Public ReadOnly Property SummaryMaterial As String
            Get
                _SummaryMaterial = TotalMaterialCost
                Return FormatExecutiveSummaryLine(_SummaryMaterial)
            End Get
        End Property
        <DescriptionAttribute("MU"), _
        DisplayName("MU"), _
        CategoryAttribute(SortedSpaces10 + "ExecutiveSummary")> _
        Public ReadOnly Property SummaryTVMCIncrement As String
            Get
                _SummaryTVMCIncrement = TotalVariableMaterialCost - TotalMaterialCost
                Return FormatExecutiveSummaryLine(_SummaryTVMCIncrement)
            End Get
        End Property
        <DescriptionAttribute("DL * 9.5"), _
        DisplayName("DL"), _
        CategoryAttribute(SortedSpaces10 + "ExecutiveSummary")> _
        Public ReadOnly Property SummaryDirectLabor As String
            Get
                _SummaryDirectLabor = AdjustedTotalLaborTimeHours * 9.5
                Return FormatExecutiveSummaryLine(_SummaryDirectLabor)
            End Get
        End Property
        <DescriptionAttribute("DL * 1.465"), _
        DisplayName("OH"), _
        CategoryAttribute(SortedSpaces10 + "ExecutiveSummary")> _
        Public ReadOnly Property SummaryOverhead As String
            Get
                _SummaryOverhead = _SummaryDirectLabor * 1.465
                Return FormatExecutiveSummaryLine(_SummaryOverhead)
            End Get
        End Property
        <DescriptionAttribute("Adjustment Multiplier"), _
        DisplayName("F/B-Test Board Multiplier"), _
        CategoryAttribute(SortedSpaces10 + "ExecutiveSummary")> _
        Public Property SummaryAdjustment As Decimal
            Get
                Return _SummaryAdjustmentMultiplyer
            End Get
            Set(ByVal value As Decimal)
                _SummaryAdjustmentMultiplyer = value
                Me.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("Mat'l + MU + DL + OH) * Adjustment Multiplier"), _
        DisplayName("F/B-Test Board"), _
        CategoryAttribute(SortedSpaces10 + "ExecutiveSummary")> _
        Public ReadOnly Property SummaryCostAdjustment As String
            Get
                _SummaryCostAdjustment = (_SummaryMaterial + _SummaryTVMCIncrement + _SummaryDirectLabor + _SummaryOverhead) * SummaryAdjustment
                Return Round(_SummaryCostAdjustment, 2).ToString
            End Get
        End Property
        <DescriptionAttribute("UnitCost - (Mat'l + MU + DL + OH + F/B-Test Board)"), _
        DisplayName("Profit"), _
        CategoryAttribute(SortedSpaces10 + "ExecutiveSummary")> _
        Public ReadOnly Property SummaryProfit As String
            Get
                _SummaryProfit = AdjustedTotalUnitCost - (_SummaryMaterial + _SummaryTVMCIncrement + _SummaryDirectLabor + _SummaryOverhead + _SummaryCostAdjustment)
                Return FormatExecutiveSummaryLine(_SummaryProfit)
            End Get
        End Property
#End Region
        'dd_Added End 
#Region "11 Total "
        <DescriptionAttribute("TotalVariableMaterialCost + LaborCost" _
            + Chr(10) + "(Dollars)"), _
        DisplayName("Total Unit Cost"), _
        CategoryAttribute(SortedSpaces11 + "Total")> _
        Public ReadOnly Property TotalUnitCost() As Decimal
            Get
                Return _
                    Me.TotalVariableMaterialCost + _
                    Me.LaborCost
            End Get
        End Property
        <DescriptionAttribute("Manufacturing Markup"), _
        DisplayName("Manufacturing Markup"), _
        CategoryAttribute(SortedSpaces11 + "Total")> _
        Public Property ManufacturingMarkup As Decimal
            Get
                Return _ManufacturingMarkup
            End Get
            Set(ByVal value As Decimal)
                Me._ManufacturingMarkup = value
                Me.SendEvents()
            End Set
        End Property
        <DescriptionAttribute("TotalUnitCost * ManufacturingMarkup" + Chr(10) + "(Dollars)"), _
        DisplayName("Adjusted Total Unit Cost"), _
        CategoryAttribute(SortedSpaces11 + "Total")> _
        Public ReadOnly Property AdjustedTotalUnitCost() As Decimal
            Get
                Return Math.Round(Me._ManufacturingMarkup * Me.TotalUnitCost, 2)
            End Get
        End Property

#End Region

#Region " Methods "
        Private Function FormatExecutiveSummaryLine(ByVal Value As Decimal) As String
            Dim retValue As String
            retValue = Round(Value, 2).ToString + " ("
            If AdjustedTotalUnitCost = 0 Then
                retValue += "N/A"
            Else
                retValue += Round((Value / AdjustedTotalUnitCost * 100), 1).ToString + "%"
            End If
            retValue += ")"
            '   Return Round(Value, 2).ToString + " (" + Round((Value / AdjustedTotalUnitCost * 100), 1).ToString + "%)"
            Return retValue
        End Function
        Private Function SumCost(ByVal IsWire As Boolean) As Decimal
            Dim result As Decimal
            For Each detail As Detail In _Header.Details
                If detail.Product.IsWire = IsWire Then
                    result += detail.TotalCost
                End If
            Next
            Return result
        End Function

        Private Function SumTime(ByVal IsWire As Boolean) As Decimal
            Dim result As Decimal
            For Each detail As Detail In _Header.Details
                If detail.Product.IsWire = IsWire Then
                    result += detail.QuoteDetailProperties.TotalMachineTime
                End If
            Next
            Return result
        End Function

        Private Function SumQty(ByVal IsWire As Boolean) As Decimal
            Dim result As Decimal
            For Each detail As Detail In _Header.Details
                If detail.Product.IsWire = IsWire Then
                    result += detail.Qty
                End If
            Next
            Return result
        End Function

        Private Function Count(ByVal IsWire As Boolean) As Decimal
            Dim result As Integer
            For Each detail As Detail In _Header.Details
                If detail.Product.IsWire = IsWire Then
                    result += 1
                End If
            Next
            Return result
        End Function
#End Region

    End Class
End Namespace
