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
        Private _ShippingCost As Decimal
        Private _ShippingBox As String = "NoBox"
        Private _TimeMultipler As Decimal = 1
        Private _LaborRate As Decimal = 18
        Private _WireUnitCutTime As Integer = 120
        Private _WireUnitTime As Decimal = 30
        Private _NumberOfCuts As Decimal = 0
        Private _MinimumOrderQuantity As Integer = 25

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
        DisplayName("Shipping Cost"), _
        DescriptionAttribute("ShippingContainerCost / MinimumOrderQuantity" + Chr(10) + "(Dollars)")> _
        Public ReadOnly Property ShippingCost As Decimal
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

        <DescriptionAttribute("Sum(PartTime) " + Chr(10) + "(Seconds)"), _
        DisplayName("Part Time"), _
        CategoryAttribute("Time")> _
        Public ReadOnly Property PartTime As Decimal
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

        <DescriptionAttribute("WireTime + PartTime" + Chr(10) + "(Seconds)"), _
        DisplayName("Total Time"), _
        CategoryAttribute("Total")> _
        Public ReadOnly Property TotalTime() As Integer
            Get
                Return WireTime + PartTime
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

        <DescriptionAttribute("Sum(UnitCost * Quantity)" + Chr(10) + "(Dollar)"), _
        DisplayName("Wire Cost"), _
        CategoryAttribute("Wires")> _
        Public ReadOnly Property WireCost() As Decimal
            Get
                Return Math.Round(SumCost(UnitOfMeasure.BY_LENGTH), 2)
            End Get
        End Property

        <DescriptionAttribute("Sum(UnitCost * Quantity)" + Chr(10) + "(Dollar)"), _
        DisplayName("Part Cost"), _
        CategoryAttribute("Parts")> _
        Public ReadOnly Property PartCost() As Decimal
            Get
                Return Math.Round(SumCost(UnitOfMeasure.BY_EACH), 2)
            End Get
        End Property

        <DescriptionAttribute("Sum(Quantity)"), _
        DisplayName("Part Qty"), _
        CategoryAttribute("Parts")> _
        Public ReadOnly Property PartQty() As Decimal
            Get
                Return SumQty(UnitOfMeasure.BY_EACH)
            End Get
        End Property

        <DescriptionAttribute("WireCost + PartCost + ShippingCost" + Chr(10) + "(Dollars)"), _
        DisplayName("Total Cost"), _
        CategoryAttribute("Total")> _
        Public ReadOnly Property TotalCost() As Decimal
            Get
                Return Me.PartCost + Me.WireCost + Me.ShippingCost
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
                    result += detail.TotalPartTime
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
