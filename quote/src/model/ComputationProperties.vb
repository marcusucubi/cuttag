Imports System.ComponentModel
Imports DCS.Quote.Model
Imports System.Reflection

Namespace Model

    Public Class ComputationProperties
        Implements INotifyPropertyChanged

        Public Sub New(ByVal QuoteHeader As QuoteHeader)
            _QuoteHeader = QuoteHeader
        End Sub

        Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        Private _QuoteHeader As QuoteHeader
        Private _ShippingContainerCost As Decimal
        Private _ShippingCost As Decimal
        Private _ShippingBox As String = "NoBox"
        Private _TimeMultipler As Decimal = 1
        Private _LaborRate As Decimal = 18
        Private _WireUnitCutTime As Integer = 120
        Private _WireUnitTime As Decimal = 30
        Private _NumberOfCuts As Decimal = 0
        Private _MinimumOrderQuantity As Integer = 25
        Private _CopperScrap As Decimal = 0.03
        Private _CopperPrice As Decimal = 4.69
        Private _MaterialMarkup As Decimal = 1.15

        <CategoryAttribute("Copper"), _
        DisplayName("Copper Weight"), _
        DescriptionAttribute("Total Weight of Copper. " + Chr(10) + "(Pounds)")> _
        Public ReadOnly Property CopperWeight As Decimal
            Get
                Return Me._QuoteHeader.WeightProperties.Weight
            End Get
        End Property

        <CategoryAttribute("Copper"), _
        DisplayName("Copper Scrap"), _
        DescriptionAttribute("Weight of Scrap Copper. " + Chr(10) + "(Pounds)")> _
        Public Property CopperScrap As Decimal
            Get
                Return Me._CopperScrap
            End Get
            Set(ByVal value As Decimal)
                Me._CopperScrap = value
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
                Me._CopperPrice = value
                Me.SendEvents()
            End Set
        End Property

        <CategoryAttribute("Copper"), _
        DisplayName("Copper Cost"), _
        DescriptionAttribute("Copper Weight + Copper Scrap) * Copper Price. " _
            + Chr(10) + "(Dollars Per Pounds)")> _
        Public ReadOnly Property CopperCost As Decimal
            Get
                Return Math.Round((Me.CopperWeight + Me.CopperScrap) * Me.CopperPrice, 2)
            End Get
        End Property

        <CategoryAttribute("Labor"), _
        DisplayName("Labor Rate"), _
        DescriptionAttribute("Used to Computer Labor Costs. " + Chr(10) + "(Dollars Per Hour)")> _
        Public Property LaborRate As Decimal
            Get
                Return _LaborRate
            End Get
            Set(ByVal Value As Decimal)
                _LaborRate = Value
                Me.SendEvents()
            End Set
        End Property

        <CategoryAttribute("Labor"), _
        DisplayName("Labor Cost"), _
        DescriptionAttribute("TotalTimeHours * LaborRate" + Chr(10) + "(Dollars)")> _
        Public ReadOnly Property LaborCost As Decimal
            Get
                Return Math.Round(TotalTimeHours * LaborRate, 2)
            End Get
        End Property

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

        <DescriptionAttribute("Sum(ComponentTime) " + Chr(10) + "(Seconds)"), _
        DisplayName("Component Time"), _
        CategoryAttribute("Time")> _
        Public ReadOnly Property ComponentTime As Decimal
            Get
                Return Me.SumTime
            End Get
        End Property

        <DescriptionAttribute("(WireLengthFeet * WireUnitTime) + (NumberOfCuts * WireUnitCutTime)"), _
        DisplayName("Wire Time"), _
        CategoryAttribute("Time")> _
        Public ReadOnly Property WireTime As Integer
            Get
                Dim x As Decimal
                Dim prop As ComputationProperties = _QuoteHeader.ComputationProperties
                Dim time1 As Decimal = (prop.WireLengthFeet * WireUnitTime)
                Dim time2 As Decimal = (NumberOfCuts * WireUnitCutTime)
                x += (time1 + time2)
                x = Math.Round(x)
                Return x
            End Get
        End Property

        <DescriptionAttribute("Time to preform one cut. " + Chr(10) + "(Seconds Per Cut)"), _
        DisplayName("Wire Unit Cut Time"), _
        CategoryAttribute("Time")> _
        Public Property WireUnitCutTime As Integer
            Get
                Return _WireUnitCutTime
            End Get
            Set(ByVal value As Integer)
                _WireUnitCutTime = value
                Me.SendEvents()
            End Set
        End Property

        <DescriptionAttribute("Time to Process One Foot. " + Chr(10) + "(Seconds Per Foot)"), _
        DisplayName("Wire Unit Time"), _
        CategoryAttribute("Time")> _
        Public Property WireUnitTime As Decimal
            Get
                Return _WireUnitTime
            End Get
            Set(ByVal value As Decimal)
                _WireUnitTime = value
                Me.SendEvents()
            End Set
        End Property

        <DescriptionAttribute("WireTime + ComponentTime" + Chr(10) + "(Seconds)"), _
        DisplayName("Total Time"), _
        CategoryAttribute("Total")> _
        Public ReadOnly Property TotalTime() As Integer
            Get
                Return WireTime + ComponentTime
            End Get
        End Property

        <DescriptionAttribute("TotalTime / (60 * 60)" + Chr(10) + "(Hours)"), _
        DisplayName("Total Time Hours"), _
        CategoryAttribute("Total")> _
        Public ReadOnly Property TotalTimeHours() As Decimal
            Get
                Return Math.Round(CDec(TotalTime) / (60 * 60), 4)
            End Get
        End Property

        <DescriptionAttribute("Number of Cuts"), _
        DisplayName("Number Of Cuts"), _
        CategoryAttribute("Wires")> _
        Public Property NumberOfCuts As Decimal
            Get
                Return _NumberOfCuts
            End Get
            Set(ByVal value As Decimal)
                _NumberOfCuts = value
                Me.SendEvents()
            End Set
        End Property

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

        <DescriptionAttribute("ComponentCost + WireCost + ShippingContainerCostPerOrder" + Chr(10) + "(Dollar)"), _
        DisplayName("Total Material Cost"), _
        CategoryAttribute("Material Cost")> _
        Public ReadOnly Property TotalMaterialCost() As Decimal
            Get
                Return Math.Round(Me.ComponentMaterialCost + _
                    Me.WireMaterialCost + Me.ShippingContainerCostPerOrder, 2)
            End Get
        End Property

        <CategoryAttribute("Material Cost"), _
        DisplayName("Material Markup" + Chr(10) + ""), _
        DescriptionAttribute("Material Markup")> _
        Public Property MaterialMarkUp As Decimal
            Get
                Return _MaterialMarkup
            End Get
            Set(ByVal value As Decimal)
                Me._MaterialMarkup = value
            End Set
        End Property

        <DescriptionAttribute("(TotalMaterialCost * MaterialMarkup)" + _
            " + CopperCost + ShippingCost" + Chr(10) + "(Dollar)"), _
        DisplayName("Total Variable Material Cost"), _
        CategoryAttribute("Material Cost")> _
        Public ReadOnly Property TotalVariableMaterialCost() As Decimal
            Get
                Return Math.Round((Me.TotalMaterialCost * Me._MaterialMarkup) + _
                    Me.CopperCost + Me.ShippingCost, 2)
            End Get
        End Property

        <DescriptionAttribute("Sum(UnitCost * Quantity)" + Chr(10) + "(Dollar)"), _
        DisplayName("Wire Material Cost"), _
        CategoryAttribute("Wires")> _
        Public ReadOnly Property WireMaterialCost() As Decimal
            Get
                Return Math.Round(SumCost(UnitOfMeasure.BY_LENGTH), 2)
            End Get
        End Property

        <DescriptionAttribute("Sum(UnitCost * Quantity)" + Chr(10) + "(Dollar)"), _
        DisplayName("Component Material Cost"), _
        CategoryAttribute("Components")> _
        Public ReadOnly Property ComponentMaterialCost() As Decimal
            Get
                Return Math.Round(SumCost(UnitOfMeasure.BY_EACH), 2)
            End Get
        End Property

        <DescriptionAttribute("Sum(Quantity)"), _
        DisplayName("Component Qty"), _
        CategoryAttribute("Components")> _
        Public ReadOnly Property ComponentQty() As Decimal
            Get
                Return SumQty(UnitOfMeasure.BY_EACH)
            End Get
        End Property

        <DescriptionAttribute("Wire Cost + Component Cost + Shipping Cost + Copper Cost + Labor Cost" _
            + Chr(10) + "(Dollars)"), _
        DisplayName("Total Cost"), _
        CategoryAttribute("Total")> _
        Public ReadOnly Property TotalCost() As Decimal
            Get
                Return Me.ComponentMaterialCost + Me.WireMaterialCost + _
                    Me.ShippingContainerCostPerOrder + Me.CopperCost + Me.LaborCost
            End Get
        End Property

        Private Function SumCost(ByVal measure As UnitOfMeasure) As Decimal
            Dim result As Decimal
            For Each detail As QuoteDetail In _QuoteHeader.QuoteDetails
                If detail.Product.UnitOfMeasure = measure Then
                    result += detail.TotalCost
                End If
            Next
            Return result
        End Function

        Private Function SumTime() As Integer
            Dim result As Integer
            For Each detail As QuoteDetail In _QuoteHeader.QuoteDetails
                If detail.Product.UnitOfMeasure = UnitOfMeasure.BY_EACH Then
                    result += detail.TotalComponentTime
                End If
            Next
            Return result
        End Function

        Private Function SumQty(ByVal measure As UnitOfMeasure) As Decimal
            Dim result As Decimal
            For Each detail As QuoteDetail In _QuoteHeader.QuoteDetails
                If detail.Product.UnitOfMeasure = measure Then
                    result += detail.Qty
                End If
            Next
            Return result
        End Function

        Friend Sub SendEvents()
            Dim info() As PropertyInfo
            info = GetType(QuoteHeader).GetProperties()
            For Each i As PropertyInfo In info
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(i.Name))
            Next
        End Sub

    End Class
End Namespace
