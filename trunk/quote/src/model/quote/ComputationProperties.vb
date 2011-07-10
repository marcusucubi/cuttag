Imports System.ComponentModel
Imports DCS.Quote.Model
Imports System.Reflection

Namespace Model.Quote

    Public Class ComputationProperties
        Inherits Common.ComputationProperties

        Public Sub New(ByVal QuoteHeader As Header)
            _QuoteHeader = QuoteHeader
        End Sub

        Private _QuoteHeader As Header

        <CategoryAttribute("Copper"), _
        DisplayName("Copper Scrap Weight"), _
        DescriptionAttribute("CopperWeight * PercentCopperScrap" + Chr(10) + "(Pounds)")> _
        Public Overloads Property CopperScrapWeight As Decimal

        <CategoryAttribute("Copper"), _
        DisplayName("Copper Weight"), _
        DescriptionAttribute("Weight of Copper Scrap. " + Chr(10) + "(Pounds)")> _
        Public Overloads Property CopperWeight As Decimal

        <CategoryAttribute("Copper"), _
        DisplayName("Percent Copper Scrap"), _
        DescriptionAttribute("Percent of Scrap Copper. " + Chr(10) + "(Percent)")> _
        Public Overloads Property PercentCopperScrap As Decimal

        <CategoryAttribute("Copper"), _
        DisplayName("Copper Price"), _
        DescriptionAttribute("Copper Price" + Chr(10) + "(Dollars Per Pounds)")> _
        Public Overloads Property CopperPrice As Decimal

        <CategoryAttribute("Copper"), _
        DisplayName("Copper Cost"), _
        DescriptionAttribute("(CopperWeight + CopperScrapWeight) * CopperPrice. " _
            + Chr(10) + "(Dollars Per Pounds)")> _
        Public Overloads Property CopperCost As Decimal

        <CategoryAttribute("Labor"), _
        DisplayName("Labor Rate"), _
        DescriptionAttribute("Used to Computer Labor Costs. " + Chr(10) + "(Dollars Per Hour)")> _
        Public Overloads Property LaborRate As Decimal

        <CategoryAttribute("Labor"), _
        DisplayName("Labor Cost"), _
        DescriptionAttribute("AdjustedTotalLaborTimeHours * LaborRate" + Chr(10) + "(Dollars)")> _
        Public Overloads Property LaborCost As Decimal

        <CategoryAttribute("Shipping"), _
        DisplayName("Minimum Order Quantity")> _
        Public Overloads Property MinimumOrderQuantity As Integer

        <CategoryAttribute("Shipping"), _
        DisplayName("Shipping Container Cost"), _
        DescriptionAttribute("Cost of the Shipping Container" + Chr(10) + "(Dollars)")> _
        Public Overloads Property ShippingContainerCost As Decimal

        <CategoryAttribute("Shipping"), _
        DisplayName("Shipping Container Cost Per Order"), _
        DescriptionAttribute("ShippingContainerCost / MinimumOrderQuantity" + Chr(10) + "(Dollars)")> _
        Public Overloads Property ShippingContainerCostPerOrder As Decimal

        <TypeConverter(GetType(ShippingList)), _
        DisplayName("Shipping Container"), _
        CategoryAttribute("Shipping"), _
            DescriptionAttribute("Description of the Shipping Container")> _
        Public Overloads Property ShippingContainer() As String

        <DisplayName("Shipping Cost"), _
        CategoryAttribute("Shipping"), _
        DescriptionAttribute("Shipping Cost" + Chr(10) + "(Dollars)")> _
        Public Overloads Property ShippingCost() As String

        <DescriptionAttribute("Setup time to cut a particular length of wire." + Chr(10) + "(Seconds Per Cut)"), _
        DisplayName("Cut Setup Time"), _
        CategoryAttribute("Time")> _
        Public Overloads Property CutSetupTime As Integer

        <DescriptionAttribute("((ComponentSetupTime * NumberOfComponents)" + _
            " + (WireSetupTime * NumberOfWires)) / MinimumOrderQuantity" + _
            Chr(10) + "(Seconds)"), _
        DisplayName("Total Labor Time"), _
        CategoryAttribute("Time")> _
        Public Overloads Property TotalLaborTime() As Integer

        <DescriptionAttribute("AdjustedTotalLaborTime / (60 * 60)" + Chr(10) + "(Hours)"), _
        DisplayName("Adjusted Total Labor Time Hours"), _
        CategoryAttribute("Time")> _
        Public Overloads Property AdjustedTotalLaborTimeHours() As Decimal

        <DescriptionAttribute("Wire Length" + Chr(10) + "(Decameter)"), _
        CategoryAttribute("Wires")> _
        Public Shadows WireLength() As Decimal

        <DescriptionAttribute("Number of Wires" + Chr(10) + "(Count)"), _
        DisplayName("Number of Wires"), _
        CategoryAttribute("Time")> _
        Public Shadows Property NumberOfWires() As Decimal

        <DescriptionAttribute("Number of Components" + Chr(10) + "(Count)"), _
        DisplayName("Number of Components"), _
        CategoryAttribute("Time")> _
        Public Shadows Property NumberOfComponents() As Decimal

        <DescriptionAttribute("Component Setup Time" + Chr(10) + "(Seconds)"), _
        DisplayName("Component Setup Time"), _
        CategoryAttribute("Time")> _
        Public Shadows Property ComponentSetupTime() As Decimal

        <DescriptionAttribute("Wire Setup Time" + Chr(10) + "(Seconds)"), _
        DisplayName("Wire Setup Time"), _
        CategoryAttribute("Time")> _
        Public Shadows Property WireSetupTime() As Decimal

        <DescriptionAttribute("WireLength / 3.048" + Chr(10) + "(Feet)"), _
        DisplayName("Wire Length Feet"), _
        CategoryAttribute("Wires")> _
        Public Shadows Property WireLengthFeet() As Decimal

        <DescriptionAttribute("ComponentMaterialCost + WireMaterialCost + ShippingContainerCostPerOrder" + Chr(10) + "(Dollar)"), _
        DisplayName("Total Material Cost"), _
        CategoryAttribute("Material Cost")> _
        Public Shadows Property TotalMaterialCost() As Decimal

        <CategoryAttribute("Material Cost"), _
        DisplayName("Adjusted Total Material Cost"), _
        DescriptionAttribute("TotalMaterialCost * LaborRate" + Chr(10) + "(Dollars)")> _
        Public Shadows Property AdjustedTotalMaterialCost As Decimal

        <CategoryAttribute("Total"), _
        DisplayName("Manufacturing Markup"), _
        DescriptionAttribute("Manufacturing Markup")> _
        Public Shadows Property ManufacturingMarkup As Decimal

        <CategoryAttribute("Time"), _
        DisplayName("Time Multiplier"), _
        DescriptionAttribute("Time Multiplier")> _
        Public Shadows Property TimeMultiplier As Decimal

        <CategoryAttribute("Material Cost"), _
        DisplayName("Material Markup"), _
        DescriptionAttribute("Material Markup")> _
        Public Shadows Property MaterialMarkUp As Decimal

        <DescriptionAttribute("(TotalMaterialCost * MaterialMarkup)" + _
            " + CopperCost + ShippingCost" + Chr(10) + "(Dollar)"), _
        DisplayName("Total Variable Material Cost"), _
        CategoryAttribute("Material Cost")> _
        Public Shadows Property TotalVariableMaterialCost() As Decimal

        <DescriptionAttribute("Sum(UnitCost * Quantity)" + Chr(10) + "(Dollar)"), _
        DisplayName("Wire Material Cost"), _
        CategoryAttribute("Material Cost")> _
        Public Shadows Property WireMaterialCost() As Decimal

        <DescriptionAttribute("Sum(UnitCost * Quantity)" + Chr(10) + "(Dollar)"), _
        DisplayName("Component Material Cost"), _
        CategoryAttribute("Material Cost")> _
        Public Shadows Property ComponentMaterialCost() As Decimal

        <DescriptionAttribute("TotalVariableMaterialCost + TotalLaborTime" _
            + Chr(10) + "(Dollars)"), _
        DisplayName("Total Unit Cost"), _
        CategoryAttribute("Total")> _
        Public Shadows Property TotalUnitCost() As Decimal

        <DescriptionAttribute("TotalUnitCost * ManufacturingMarkup" + Chr(10) + "(Dollars)"), _
        DisplayName("Adjusted Total Unit Cost"), _
        CategoryAttribute("Total")> _
        Public Shadows Property AdjustedTotalUnitCost() As Decimal

        <DescriptionAttribute("TimeMultiplier * TotalLaborTime" + Chr(10) + "(Seconds)"), _
        DisplayName("Adjusted Total Labor Time"), _
        CategoryAttribute("Time")> _
        Public Shadows Property AdjustedTotalLaborTime() As Decimal

    End Class
End Namespace
