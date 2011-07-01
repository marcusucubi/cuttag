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

        <CategoryAttribute("Input")> _
        Public Property Minimum As Integer
        <CategoryAttribute("Input")> _
        Public Property LeadTimeInitial As Integer
        <CategoryAttribute("Input")> _
        Public Property LeadTimeStandard As Integer
        <CategoryAttribute("Input")> _
        Public Property EstimatedAnnualUnits As Integer
        <CategoryAttribute("Input")> _
        Public Property MaterialMarkUp As Decimal
        <CategoryAttribute("Input")> _
        Public Property CU_Scrap As Decimal
        <CategoryAttribute("Input")> _
        Public Property LaborMinutes As Integer

        <DescriptionAttribute("(WireLengthFeet * WireUnitTime) + (NumberOfCuts * WireUnitCutTime)"), _
        CategoryAttribute("Wires")> _
        Public ReadOnly Property WireTime As Decimal
            Get
                Return Me._QuoteHeader.QuoteEngine.WireTime
            End Get
        End Property

        <CategoryAttribute("Wires")> _
        Public Property WireUnitCutTime As Integer
            Get
                Return Me._QuoteHeader.QuoteEngine.WireUnitCutTime
            End Get
            Set(ByVal value As Integer)
                Me._QuoteHeader.QuoteEngine.WireUnitCutTime = value
                Me.SendEvents()
            End Set
        End Property

        <CategoryAttribute("Wires")> _
        Public Property WireUnitTime As Decimal
            Get
                Return Me._QuoteHeader.QuoteEngine.WireUnitTime
            End Get
            Set(ByVal value As Decimal)
                Me._QuoteHeader.QuoteEngine.WireUnitTime = value
                Me.SendEvents()
            End Set
        End Property

        <CategoryAttribute("Wires")> _
        Public Property NumberOfCuts As Decimal
            Get
                Return Me._QuoteHeader.QuoteEngine.NumberOfCuts
            End Get
            Set(ByVal value As Decimal)
                Me._QuoteHeader.QuoteEngine.NumberOfCuts = value
                Me.SendEvents()
            End Set
        End Property

        <CategoryAttribute("Wires")> _
        Public ReadOnly Property WireLengthDecemeter() As Decimal
            Get
                Return SumQty(UnitOfMeasure.BY_LENGTH)
            End Get
        End Property

        <CategoryAttribute("Wires")> _
        Public ReadOnly Property WireLengthFeet() As Decimal
            Get
                Return Math.Round(SumQty(UnitOfMeasure.BY_LENGTH) / 3.048, 2)
            End Get
        End Property

        <CategoryAttribute("Wires")> _
        Public ReadOnly Property WireCost() As Decimal
            Get
                Return SumCost(UnitOfMeasure.BY_LENGTH)
            End Get
        End Property

        <CategoryAttribute("Wires")> _
        Public ReadOnly Property WireCount() As Integer
            Get
                Return Count(UnitOfMeasure.BY_LENGTH)
            End Get
        End Property

        <CategoryAttribute("Parts")> _
        Public ReadOnly Property PartCount() As Integer
            Get
                Return Count(UnitOfMeasure.BY_EACH)
            End Get
        End Property

        <CategoryAttribute("Parts")> _
        Public ReadOnly Property PartCost() As Decimal
            Get
                Return SumCost(UnitOfMeasure.BY_EACH)
            End Get
        End Property

        <CategoryAttribute("Parts")> _
        Public ReadOnly Property PartQty() As Decimal
            Get
                Return SumQty(UnitOfMeasure.BY_EACH)
            End Get
        End Property

        <CategoryAttribute("Total")> _
        Public ReadOnly Property TotalCost() As Decimal
            Get
                Return SumCost(UnitOfMeasure.BY_EACH) + SumCost(UnitOfMeasure.BY_LENGTH)
            End Get
        End Property

        <CategoryAttribute("Total")> _
        Public ReadOnly Property TotalCount() As Integer
            Get
                Return Count(UnitOfMeasure.BY_EACH) + Count(UnitOfMeasure.BY_LENGTH)
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
