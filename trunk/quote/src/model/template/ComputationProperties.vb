Imports System.ComponentModel
Imports System.Reflection

Namespace Model.Template

    Public Class ComputationProperties
        Inherits Common.ComputationProperties

        Public Sub New(ByVal Header As Header)
            _Header = Header
        End Sub

#Region " Variables "

        Private _Header As Header
        Private _ShippingContainerCost As Decimal
        Private _ShippingCost As Decimal
        Private _ShippingBox As String = "NoBox"
        Private _TimeMultiplier As Decimal = 1
        Private _ManufacturingMarkup As Decimal = 1
        Private _LaborRate As Decimal = 10
        Private _WireSetupTime As Integer = 120
        Private _WireMachineTime As Decimal = 30
        Private _NumberOfCuts As Decimal = 0
        Private _MinimumOrderQuantity As Integer = 10
        Private _PercentCopperScrap As Decimal = 3
        Private _CopperPrice As Decimal = 1
        Private _MaterialMarkup As Decimal = 1
        Private _ComponentSetupTime As Decimal

#End Region

#Region " Copper "

        <CategoryAttribute("Copper"), _
        DisplayName("Copper Scrap Weight"), _
        DescriptionAttribute("CopperWeight * (PercentCopperScrap / 100)" + Chr(10) + "(Pounds)")> _
        Public ReadOnly Property CopperScrapWeight As Decimal
            Get
                Dim percent As Decimal = (Me._PercentCopperScrap / 100)
                Return Math.Round(Me.CopperWeight * percent, 4)
            End Get
        End Property

        <CategoryAttribute("Copper"), _
        DisplayName("Copper Weight"), _
        DescriptionAttribute("Weight of Copper Scrap. " + Chr(10) + "(Pounds)")> _
        Public ReadOnly Property CopperWeight As Decimal
            Get
                Return Me._Header.WeightProperties.Weight
            End Get
        End Property

        <CategoryAttribute("Copper"), _
        DisplayName("Percent Copper Scrap"), _
        DescriptionAttribute("Percent of Scrap Copper. " + Chr(10) + "(Percent)")> _
        Public Property PercentCopperScrap As Integer
            Get
                Return Me._PercentCopperScrap
            End Get
            Set(ByVal value As Integer)
                Me._PercentCopperScrap = value
                Me.SendEvents()
            End Set
        End Property

        <CategoryAttribute("Copper"), _
        DisplayName("Copper Price"), _
        DescriptionAttribute("Copper Price" + Chr(10) + "(Dollars Per Pounds)")> _
        Public Property CopperPrice As Decimal
            Get
                Return Me._CopperPrice
            End Get
            Set(ByVal value As Decimal)
                Me._CopperPrice = Math.Round(value, 2)
                Me.SendEvents()
            End Set
        End Property

        <CategoryAttribute("Copper"), _
        DisplayName("Copper Scrap Cost"), _
        DescriptionAttribute("CopperScrapWeight * CopperPrice. " _
            + Chr(10) + "(Dollars Per Pounds)")> _
        Public ReadOnly Property CopperScrapCost As Decimal
            Get
                Return Math.Round(Me.CopperScrapWeight * Me.CopperPrice, 2)
            End Get
        End Property

#End Region
#Region " Labor "

        <CategoryAttribute("Labor"), _
        DisplayName("Labor Rate"), _
        DescriptionAttribute("Used to Computer Labor Costs. " + Chr(10) + "(Dollars Per Hour)")> _
        Public Property LaborRate As Decimal
            Get
                Return _LaborRate
            End Get
            Set(ByVal Value As Decimal)
                _LaborRate = Math.Round(Value, 2)
                Me.SendEvents()
            End Set
        End Property

        <CategoryAttribute("Labor"), _
        DisplayName("Labor Cost"), _
        DescriptionAttribute("AdjustedTotalLaborTimeHours * LaborRate" + Chr(10) + "(Dollars)")> _
        Public ReadOnly Property LaborCost As Decimal
            Get
                Return Math.Round(AdjustedTotalLaborTimeHours * LaborRate, 2)
            End Get
        End Property

#End Region
#Region " Shipping "

        <CategoryAttribute("Shipping"), _
        DisplayName("Minimum Order Quantity")> _
        Public Property MinimumOrderQuantity As Integer
            Get
                Return _MinimumOrderQuantity
            End Get
            Set(ByVal Value As Integer)
                _MinimumOrderQuantity = Value
                Me.SendEvents()
            End Set
        End Property

        <CategoryAttribute("Shipping"), _
        DisplayName("Shipping Container Cost"), _
        DescriptionAttribute("Cost of the Shipping Container" + Chr(10) + "(Dollars)")> _
        Public ReadOnly Property ShippingContainerCost As Decimal
            Get
                If (_ShippingBox Is Nothing) Then
                    Return 0
                End If
                Return Math.Round(Shipping.Shipping.Lookup(Me._ShippingBox), 2)
            End Get
        End Property

        <CategoryAttribute("Shipping"), _
        DisplayName("Shipping Container Cost Per Order"), _
        DescriptionAttribute("ShippingContainerCost / MinimumOrderQuantity" + Chr(10) + "(Dollars)")> _
        Public ReadOnly Property ShippingContainerCostPerOrder As Decimal
            Get
                If (Me.MinimumOrderQuantity = 0) Then
                    Return 0
                End If
                Return Math.Round(Me.ShippingContainerCost / Me.MinimumOrderQuantity, 2)
            End Get
        End Property

        <TypeConverter(GetType(ShippingList)), _
        DisplayName("Shipping Container"), _
        CategoryAttribute("Shipping"), _
            DescriptionAttribute("Description of the Shipping Container")> _
        Public Property ShippingContainer() As String
            Get
                Return _ShippingBox
            End Get
            Set(ByVal Value As String)
                _ShippingBox = Value
                Me.SendEvents()
            End Set
        End Property

        <DisplayName("Shipping Cost"), _
        CategoryAttribute("Shipping"), _
        DescriptionAttribute("Shipping Cost" + Chr(10) + "(Dollars)")> _
        Public Property ShippingCost() As String
            Get
                Return _ShippingCost
            End Get
            Set(ByVal Value As String)
                _ShippingCost = Value
                Me.SendEvents()
            End Set
        End Property

#End Region
#Region " Machine Time "

        <DescriptionAttribute("Used with TotalWireMachineTime " + Chr(10) + "(Seconds)"), _
        DisplayName("Wire Machine Time"), _
        CategoryAttribute("Machine Time")> _
        Public Property WireMachineTime As Integer
            Get
                Return Me._WireMachineTime
            End Get
            Set(ByVal value As Integer)
                Me._WireMachineTime = value
                Me.SendEvents()
            End Set
        End Property

        <DescriptionAttribute("TotalWireMachineTime + " + _
            "TotalComponentMachineTime" + Chr(10) + "(Seconds)"), _
        DisplayName("Total Machine Time"), _
        CategoryAttribute("Machine Time")> _
        Public ReadOnly Property TotalMachineTime As Decimal
            Get
                Return Math.Round(Me.TotalComponentMachineTime + _
                    Me.TotalWireMachineTime, 4)
            End Get
        End Property

        <DescriptionAttribute("Sum(ComponentMachineTime) " + Chr(10) + "(Seconds)"), _
        DisplayName("Total Component Machine Time"), _
        CategoryAttribute("Machine Time")> _
        Public ReadOnly Property TotalComponentMachineTime As Decimal
            Get
                Return Me.SumTime(UnitOfMeasure.BY_EACH)
            End Get
        End Property

        <DescriptionAttribute("WireLengthFeet * WireMachineTime " + Chr(10) + "(Seconds)"), _
        DisplayName("Total Wire Machine Time"), _
        CategoryAttribute("Machine Time")> _
        Public ReadOnly Property TotalWireMachineTime As Decimal
            Get
                Return Math.Round(Me.WireLengthFeet * Me.WireMachineTime, 4)
            End Get
        End Property

#End Region
#Region " Setup Time "

        <DescriptionAttribute("Setup time to cut a particular length of wire. (Cut time)" + Chr(10) + "(Seconds Per Cut)"), _
        DisplayName("Wire Setup Time"), _
        CategoryAttribute("Setup Time")> _
        Public Property WireSetupTime As Integer
            Get
                Return _WireSetupTime
            End Get
            Set(ByVal value As Integer)
                _WireSetupTime = value
                Me.SendEvents()
            End Set
        End Property


        <DescriptionAttribute("NumberOfCuts * WireSetupTime" + Chr(10) + "(Seconds)"), _
        DisplayName("Total Wire Setup Time"), _
        CategoryAttribute("Setup Time")> _
        Public ReadOnly Property TotalWireSetupTime As Decimal
            Get
                Return Math.Round(Me.NumberOfCuts * Me.WireSetupTime, 4)
            End Get
        End Property

        <DescriptionAttribute("(TotalWireSetupTime + ComponentSetupTime) " + _
            "/ MinimumOrderQuantity" + _
            Chr(10) + "(Seconds)"), _
        DisplayName("Total Setup Time"), _
        CategoryAttribute("Setup Time")> _
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

        <DescriptionAttribute("Component Setup Time" + Chr(10) + "(Seconds)"), _
        DisplayName("Component Setup Time"), _
        CategoryAttribute("Setup Time")> _
        Public Property ComponentSetupTime() As Integer
            Get
                Return _ComponentSetupTime
            End Get
            Set(ByVal value As Integer)
                _ComponentSetupTime = value
                Me.SendEvents()
            End Set
        End Property

#End Region
#Region " Time "

        <DescriptionAttribute("(TotalSetupTime + TotalMachineTime)" + _
            Chr(10) + "(Seconds)"), _
        DisplayName("Total Labor Time"), _
        CategoryAttribute("Time")> _
        Public ReadOnly Property TotalLaborTime() As Integer
            Get
                Return (Me.TotalSetupTime + Me.TotalMachineTime)
            End Get
        End Property

        <DescriptionAttribute("AdjustedTotalLaborTime / (60 * 60)" + Chr(10) + "(Hours)"), _
        DisplayName("Adjusted Total Labor Time Hours"), _
        CategoryAttribute("Time")> _
        Public ReadOnly Property AdjustedTotalLaborTimeHours() As Decimal
            Get
                Return Math.Round(CDec(Me.AdjustedTotalLaborTime) / (60 * 60), 4)
            End Get
        End Property

        <DescriptionAttribute("Number of lines in 'CIRCUIT DATA TABLE'" + _
            " in Engineering Print." + Chr(10) + "(Count)"), _
        DisplayName("Number of Cuts"), _
        CategoryAttribute("Time")> _
        Public Property NumberOfCuts() As Integer
            Get
                Return _NumberOfCuts
            End Get
            Set(ByVal value As Integer)
                _NumberOfCuts = value
                Me.SendEvents()
            End Set
        End Property

        <DescriptionAttribute("Number of Wires" + Chr(10) + "(Count)"), _
        DisplayName("Number of Wires"), _
        CategoryAttribute("Time")> _
        Public ReadOnly Property NumberOfWires() As Decimal
            Get
                Return Count(UnitOfMeasure.BY_LENGTH)
            End Get
        End Property

        <DescriptionAttribute("Number of Components" + Chr(10) + "(Count)"), _
        DisplayName("Number of Components"), _
        CategoryAttribute("Time")> _
        Public ReadOnly Property NumberOfComponents() As Decimal
            Get
                Return Count(UnitOfMeasure.BY_EACH)
            End Get
        End Property

        <CategoryAttribute("Time"), _
        DisplayName("Time Multiplier"), _
        DescriptionAttribute("Time Multiplier")> _
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
        CategoryAttribute("Time")> _
        Public ReadOnly Property AdjustedTotalLaborTime() As Decimal
            Get
                Return Math.Round(Me._TimeMultiplier * Me.TotalLaborTime)
            End Get
        End Property

#End Region
#Region " Wires "

        <DescriptionAttribute("Wire Length" + Chr(10) + "(Decameter)"), _
        DisplayName("Wire Length"), _
        CategoryAttribute("Wires")> _
        Public ReadOnly Property WireLength() As Decimal
            Get
                Return SumQty(UnitOfMeasure.BY_LENGTH)
            End Get
        End Property

        <DescriptionAttribute("WireLength / 3.048" + Chr(10) + "(Feet)"), _
        DisplayName("Wire Length Feet"), _
        CategoryAttribute("Wires")> _
        Public ReadOnly Property WireLengthFeet() As Decimal
            Get
                Return Math.Round(SumQty(UnitOfMeasure.BY_LENGTH) / 3.048, 4)
            End Get
        End Property

#End Region
#Region " Material Cost "

        <DescriptionAttribute("ComponentMaterialCost + WireMaterialCost + ShippingContainerCostPerOrder" + Chr(10) + "(Dollar)"), _
        DisplayName("Total Material Cost"), _
        CategoryAttribute("Material Cost")> _
        Public ReadOnly Property TotalMaterialCost() As Decimal
            Get
                Return Math.Round( _
                    Me.ComponentMaterialCost + _
                    Me.WireMaterialCost + _
                    Me.ShippingContainerCostPerOrder, 2)
            End Get
        End Property

        <CategoryAttribute("Material Cost"), _
        DisplayName("Adjusted Total Material Cost"), _
        DescriptionAttribute("TotalMaterialCost * LaborRate" + Chr(10) + "(Dollars)")> _
        Public ReadOnly Property AdjustedTotalMaterialCost As Decimal
            Get
                Return Math.Round(TotalMaterialCost * Me._MaterialMarkup, 2)
            End Get
        End Property

        <CategoryAttribute("Material Cost"), _
        DisplayName("Material Markup"), _
        DescriptionAttribute("Material Markup")> _
        Public Property MaterialMarkUp As Decimal
            Get
                Return _MaterialMarkup
            End Get
            Set(ByVal value As Decimal)
                Me._MaterialMarkup = value
                Me.SendEvents()
            End Set
        End Property

        <DescriptionAttribute("(TotalMaterialCost * MaterialMarkup)" + _
            " + CopperCost + ShippingCost" + Chr(10) + "(Dollar)"), _
        DisplayName("Total Variable Material Cost"), _
        CategoryAttribute("Material Cost")> _
        Public ReadOnly Property TotalVariableMaterialCost() As Decimal
            Get
                Return Math.Round( _
                    (Me.TotalMaterialCost * Me._MaterialMarkup) + _
                     Me.CopperScrapCost + _
                     Me.ShippingCost, 2)
            End Get
        End Property

        <DescriptionAttribute("Sum(UnitCost * Quantity)" + Chr(10) + "(Dollar)"), _
        DisplayName("Wire Material Cost"), _
        CategoryAttribute("Material Cost")> _
        Public ReadOnly Property WireMaterialCost() As Decimal
            Get
                Return Math.Round(SumCost(UnitOfMeasure.BY_LENGTH), 2)
            End Get
        End Property

        <DescriptionAttribute("Sum(UnitCost * Quantity)" + Chr(10) + "(Dollar)"), _
        DisplayName("Component Material Cost"), _
        CategoryAttribute("Material Cost")> _
        Public ReadOnly Property ComponentMaterialCost() As Decimal
            Get
                Return Math.Round(SumCost(UnitOfMeasure.BY_EACH), 2)
            End Get
        End Property

#End Region
#Region " Total "

        <CategoryAttribute("Total"), _
        DisplayName("Manufacturing Markup"), _
        DescriptionAttribute("Manufacturing Markup")> _
        Public Property ManufacturingMarkup As Decimal
            Get
                Return _ManufacturingMarkup
            End Get
            Set(ByVal value As Decimal)
                Me._ManufacturingMarkup = value
                Me.SendEvents()
            End Set
        End Property

        <DescriptionAttribute("TotalVariableMaterialCost + LaborCost" _
            + Chr(10) + "(Dollars)"), _
        DisplayName("Total Unit Cost"), _
        CategoryAttribute("Total")> _
        Public ReadOnly Property TotalUnitCost() As Decimal
            Get
                Return _
                    Me.TotalVariableMaterialCost + _
                    Me.LaborCost
            End Get
        End Property

        <DescriptionAttribute("TotalUnitCost * ManufacturingMarkup" + Chr(10) + "(Dollars)"), _
        DisplayName("Adjusted Total Unit Cost"), _
        CategoryAttribute("Total")> _
        Public ReadOnly Property AdjustedTotalUnitCost() As Decimal
            Get
                Return Math.Round(Me._ManufacturingMarkup * Me.TotalUnitCost, 2)
            End Get
        End Property

#End Region

#Region " Methods "

        Private Function SumCost(ByVal measure As UnitOfMeasure) As Decimal
            Dim result As Decimal
            For Each detail As Detail In _Header.Details
                If detail.Product.UnitOfMeasure = measure Then
                    result += detail.TotalCost
                End If
            Next
            Return result
        End Function

        Private Function SumTime(ByVal uom As UnitOfMeasure) As Integer
            Dim result As Integer
            For Each detail As Detail In _Header.Details
                If detail.Product.UnitOfMeasure = uom Then
                    result += detail.QuoteDetailProperties.TotalMachineTime
                End If
            Next
            Return result
        End Function

        Private Function SumQty(ByVal measure As UnitOfMeasure) As Decimal
            Dim result As Decimal
            For Each detail As Detail In _Header.Details
                If detail.Product.UnitOfMeasure = measure Then
                    result += detail.Qty
                End If
            Next
            Return result
        End Function

        Private Function Count(ByVal measure As UnitOfMeasure) As Decimal
            Dim result As Integer
            For Each detail As Detail In _Header.Details
                If detail.Product.UnitOfMeasure = measure Then
                    result += 1
                End If
            Next
            Return result
        End Function
#End Region

    End Class
End Namespace
