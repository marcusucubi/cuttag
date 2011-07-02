Imports System.ComponentModel
Imports DCS.Quote.Model
Imports System.Reflection

Namespace Model

    Public Class QuoteProperties
        Implements INotifyPropertyChanged

        Public Sub New(ByVal QuoteHeader As QuoteHeader)
            _QuoteHeader = QuoteHeader
        End Sub

        Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        Private _QuoteHeader As QuoteHeader
        Private _ShippingCost As Decimal
        Private _ShippingBox As String
        Private _TimeMultipler As Decimal = 1
        Private _LaborMultiplier As Decimal = 0.01

        <CategoryAttribute("Input")> _
        Public Property MinimumOrderQuantity As Integer = 10
        <CategoryAttribute("Input")> _
        Public Property LeadTimeInitial As Integer
        <CategoryAttribute("Input")> _
        Public Property LeadTimeStandard As Integer
        <CategoryAttribute("Input")> _
        Public Property EstimatedAnnualUnits As Integer
        <CategoryAttribute("Input")> _
        Public Property MaterialMarkUp As Decimal
        <CategoryAttribute("Input")> _
        Public Property CopperScrap As Decimal

        <CategoryAttribute("Time"), _
        DescriptionAttribute("Used to computer labor costs. " + Chr(10) + "(Dollars Per Seconds)")> _
        Public Property LaborMultiplier As Decimal
            Get
                Return _LaborMultiplier
            End Get
            Set(ByVal Value As Decimal)
                _LaborMultiplier = Value
                Me.SendEvents()
            End Set
        End Property

        <CategoryAttribute("Labor"), _
        DescriptionAttribute("TotalTimeSeconds * LaborMultiplier")> _
        Public ReadOnly Property LaborCost As Decimal
            Get
                Dim y As Decimal
                y = TotalTimeSeconds * LaborMultiplier
                Return Math.Round(y, 2)
            End Get
        End Property

        <CategoryAttribute("Shipping")> _
        Public ReadOnly Property ShippingCost As Decimal
            Get
                If (_ShippingBox Is Nothing) Then
                    Return 0
                End If
                Return Math.Round(Shipping.Shipping.Lookup(Me._ShippingBox), 2)
            End Get
        End Property

        <TypeConverter(GetType(ShippingList)), _
            CategoryAttribute("Shipping"), _
            DescriptionAttribute("Shipping Container")> _
        Public Property ShippingBox() As String
            Get
                Return _ShippingBox
            End Get
            Set(ByVal Value As String)
                _ShippingBox = Value
                Me.SendEvents()
            End Set
        End Property

        <DescriptionAttribute("Sum(PartTime) " + Chr(10) + " (seconds)"), _
        CategoryAttribute("Time")> _
        Public ReadOnly Property PartTime As Decimal
            Get
                Return Me.SumTime
            End Get
        End Property

        <DescriptionAttribute("(WireLengthFeet * WireUnitTime) + (NumberOfCuts * WireUnitCutTime)"), _
        CategoryAttribute("Time")> _
        Public ReadOnly Property WireTime As Integer
            Get
                Return Me._QuoteHeader.QuoteEngine.WireTime
            End Get
        End Property

        <DescriptionAttribute("Time to preform one cut. " + Chr(10) + "(Seconds Per Cut)"), _
        CategoryAttribute("Time")> _
        Public Property WireUnitCutTime As Integer
            Get
                Return Me._QuoteHeader.QuoteEngine.WireUnitCutTime
            End Get
            Set(ByVal value As Integer)
                Me._QuoteHeader.QuoteEngine.WireUnitCutTime = value
                Me.SendEvents()
            End Set
        End Property

        <DescriptionAttribute("Time to process one foot. " + Chr(10) + "(Seconds Per Foot)"), _
        CategoryAttribute("Time")> _
        Public Property WireUnitTime As Decimal
            Get
                Return Me._QuoteHeader.QuoteEngine.WireUnitTime
            End Get
            Set(ByVal value As Decimal)
                Me._QuoteHeader.QuoteEngine.WireUnitTime = value
                Me.SendEvents()
            End Set
        End Property

        <DescriptionAttribute("WireTime + PartTime"), _
        CategoryAttribute("Time")> _
        Public ReadOnly Property TotalTimeSeconds() As Integer
            Get
                Return WireTime + PartTime
            End Get
        End Property

        <DescriptionAttribute("PartTimeTotalSeconds / 60"), _
        CategoryAttribute("Time")> _
        Public ReadOnly Property TotalTimeMinutes() As Decimal
            Get
                Return Math.Round(CDec(TotalTimeSeconds) / 60, 4)
            End Get
        End Property

        <DescriptionAttribute("Number of Cuts"), _
        CategoryAttribute("Wires")> _
        Public Property NumberOfCuts As Decimal
            Get
                Return Me._QuoteHeader.QuoteEngine.NumberOfCuts
            End Get
            Set(ByVal value As Decimal)
                Me._QuoteHeader.QuoteEngine.NumberOfCuts = value
                Me.SendEvents()
            End Set
        End Property

        <DescriptionAttribute("Wire Length" + Chr(10) + "(dm)"), _
        CategoryAttribute("Wires")> _
        Public ReadOnly Property WireLengthDecameter() As Decimal
            Get
                Return SumQty(UnitOfMeasure.BY_LENGTH)
            End Get
        End Property

        <DescriptionAttribute("WireLengthDecameter / 3.048"), _
        CategoryAttribute("Wires")> _
        Public ReadOnly Property WireLengthFeet() As Decimal
            Get
                Return Math.Round(SumQty(UnitOfMeasure.BY_LENGTH) / 3.048, 4)
            End Get
        End Property

        <DescriptionAttribute("Sum(UnitCost * Quantity)"), _
        CategoryAttribute("Wires")> _
        Public ReadOnly Property WireCost() As Decimal
            Get
                Return Math.Round(SumCost(UnitOfMeasure.BY_LENGTH), 2)
            End Get
        End Property

        <DescriptionAttribute("Number of kinds of wires"), _
        CategoryAttribute("Wires")> _
        Public ReadOnly Property WireCount() As Integer
            Get
                Return Count(UnitOfMeasure.BY_LENGTH)
            End Get
        End Property

        <DescriptionAttribute("Number of kinds of parts"), _
        CategoryAttribute("Parts")> _
        Public ReadOnly Property PartCount() As Integer
            Get
                Return Count(UnitOfMeasure.BY_EACH)
            End Get
        End Property

        <DescriptionAttribute("Sum(UnitCost * Quantity)"), _
        CategoryAttribute("Parts")> _
        Public ReadOnly Property PartCost() As Decimal
            Get
                Return Math.Round(SumCost(UnitOfMeasure.BY_EACH), 2)
            End Get
        End Property

        <DescriptionAttribute("Sum(Quantity)"), _
        CategoryAttribute("Parts")> _
        Public ReadOnly Property PartQty() As Decimal
            Get
                Return SumQty(UnitOfMeasure.BY_EACH)
            End Get
        End Property

        <DescriptionAttribute("WireCost + PartCost"), _
        CategoryAttribute("Total")> _
        Public ReadOnly Property TotalCost() As Decimal
            Get
                Return PartCost + WireCost
            End Get
        End Property

        <DescriptionAttribute("WireCount + PartCount"), _
        CategoryAttribute("Total")> _
        Public ReadOnly Property TotalCount() As Integer
            Get
                Return WireCount + PartCount
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

        Private Function Count(ByVal measure As UnitOfMeasure) As Integer
            Dim result As Integer
            For Each detail As QuoteDetail In _QuoteHeader.QuoteDetails
                If detail.Product.UnitOfMeasure = measure Then
                    result += 1
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
