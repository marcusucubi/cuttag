Imports System.ComponentModel
Imports System.Reflection
Imports System.Math

Imports Model
Imports Model.Template

''' <summary>
''' The main computation properties.
''' </summary>
''' <remarks>
''' This class should contain computation related
''' code.  Any display related code should
''' should be in DisplayableComputationProperties.
''' </remarks>
Public NotInheritable Class DekalbComputationProperties
    Inherits Model.Template.ComputationProperties

    Public Sub New(ByVal Header As Model.Template.Header)
        MyBase.New(Header)
        _Header = Header
    End Sub
#Region " Variables "

    Private _Header As Header
    Private _ShippingContainerCost As Decimal
    Private _ShippingCost As Decimal
    Private _ShippingBox As String = "NoBox"
    Private _TimeMultiplier As Decimal = 1.15 'Read From DefaultValues
    Private _ManufacturingMarkup As Decimal = 1.25 'Read From DefaultValues
    Private _LaborRate As Decimal = 21.5 'Read From DefaultValues
    Private _WireSetupTime As Decimal = 120 '300 'Read From DefaultValues
    Private _WireMachineTime As Decimal = 25 '30 'Read From DefaultValues
    Private _NumberOfCuts As Decimal = 0
    Private _MinimumOrderQuantity As Integer = 0
    Private _OrderQuantity As Integer = 0
    Private _SingleDefQuantity As Integer = 0
    Private _PercentCopperScrap As Decimal = 10 '3 'Read From DefaultValues
    Private _CopperPrice As Decimal = 4.09 '3.57 'Read From DefaultValues
    Private _MaterialMarkup As Decimal = 1.075 'Read From DefaultValues
    Private _ComponentSetupTime As Decimal

    Private _QuoteType As String = "Production"

    Private _NumberOfTwistedPairs As Integer
    Private _TimePerTwistedPairs As Decimal = 300 'Read From DefaultValues

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

    Private _SummaryAdjustmentMultiplyer As Decimal = 0.08 'Read From DefaultValues
#End Region
#Region "1 Copper "
    Public ReadOnly Property CopperWeight As Decimal
        Get
            Return Weights.CalcWeight(_Header)
        End Get
    End Property
    Public Property PercentCopperScrap As Decimal
        Get
            Return Me._PercentCopperScrap
        End Get
        Set(ByVal value As Decimal)
            Me._PercentCopperScrap = value
            Me.SendEvents()
        End Set
    End Property
    Public ReadOnly Property CopperScrapWeight As Decimal
        Get
            Dim percent As Decimal = (Me._PercentCopperScrap / 100)
            Return Me.CopperWeight * percent
        End Get
    End Property
    Public Property CopperPrice As Decimal
        Get
            Return Me._CopperPrice
        End Get
        Set(ByVal value As Decimal)
            Me._CopperPrice = value
            Me.SendEvents()
        End Set
    End Property
    Public ReadOnly Property CopperScrapCost As Decimal
        Get
            Return Me.CopperScrapWeight * Me.CopperPrice
        End Get
    End Property

#End Region
#Region "2 Wires "
    Public ReadOnly Property NumberOfWires() As Decimal
        Get
            Return Count(True)
        End Get
    End Property

    Public ReadOnly Property WireLength() As Decimal
        Get
            Return SumQty(True)
        End Get
    End Property

    Public ReadOnly Property WireLengthFeet() As Decimal
        Get
            Return SumQty(True) / 3.048
        End Get
    End Property

#End Region
#Region "3 Material Cost "
    Public ReadOnly Property ComponentMaterialCost() As Decimal
        Get
            Return SumCost(False)
        End Get
    End Property
    Public ReadOnly Property WireMaterialCost() As Decimal
        Get
            Return SumCost(True)
        End Get
    End Property
    Public ReadOnly Property TotalMaterialCost() As Decimal
        Get
            Return _
                Me.ComponentMaterialCost + _
                Me.WireMaterialCost + _
                Me.ShippingContainerCostPerOrder
        End Get
    End Property
    Public Property MaterialMarkUp As Decimal
        Get
            Return _MaterialMarkup
        End Get
        Set(ByVal value As Decimal)
            Me._MaterialMarkup = value
            Me.SendEvents()
        End Set
    End Property
    Public ReadOnly Property AdjustedTotalMaterialCost As Decimal
        Get
            Return TotalMaterialCost * Me._MaterialMarkup
        End Get
    End Property
    Public ReadOnly Property TotalVariableMaterialCost() As Decimal
        Get
            Return _
                (Me.TotalMaterialCost * Me._MaterialMarkup) + _
                 Me.CopperScrapCost + _
                 Me.ShippingCost
        End Get
    End Property
#End Region
#Region "4 Twisted Pairs "
    Public Property NumberOfTwistedPairs As Integer
        Get
            Return _NumberOfTwistedPairs
        End Get
        Set(ByVal value As Integer)
            Me._NumberOfTwistedPairs = value
            Me.SendEvents()
        End Set
    End Property
    Public ReadOnly Property TwistedPairsMachineTime As Decimal
        Get
            Return (Me._NumberOfTwistedPairs * 300)
        End Get
    End Property
#End Region
#Region "5 Setup Time "
    Public ReadOnly Property NumberOfComponents() As Decimal
        Get
            Return Count(False)
        End Get
    End Property
    Public Property ComponentSetupTime() As Decimal
        Get
            Return _ComponentSetupTime
        End Get
        Set(ByVal value As Decimal)
            _ComponentSetupTime = value
            Me.SendEvents()
        End Set
    End Property
    Public Property WireSetupTime As Decimal
        Get
            Return _WireSetupTime
        End Get
        Set(ByVal value As Decimal)
            _WireSetupTime = value
            Me.SendEvents()
        End Set
    End Property
    Public Property NumberOfCuts() As Integer
        Get
            Return _NumberOfCuts
        End Get
        Set(ByVal value As Integer)
            _NumberOfCuts = value
            Me.SendEvents()
        End Set
    End Property
    Public ReadOnly Property TotalWireSetupTime As Decimal
        Get
            Return Me.NumberOfCuts * Me.WireSetupTime
        End Get
    End Property
    Public ReadOnly Property TotalSetupTime() As Decimal
        Get
            If Me.MinimumOrderQuantity = 0 Then
                Return 0
            End If
            Return _
                (Me.TotalWireSetupTime + Me.ComponentSetupTime) _
                / Me.MinimumOrderQuantity
        End Get
    End Property
#End Region
#Region "6 Machine Time "
    Public ReadOnly Property TotalComponentMachineTime As Decimal
        Get
            Return Me.SumTime(False)
        End Get
    End Property
    Public ReadOnly Property TotalWireMachineTime As Decimal
        Get
            Return Me.WireLengthFeet * Me.WireMachineTime
        End Get
    End Property
    Public Property WireMachineTime As Decimal
        Get
            Return Me._WireMachineTime
        End Get
        Set(ByVal value As Decimal)
            Me._WireMachineTime = value
            Me.SendEvents()
        End Set
    End Property
    Public ReadOnly Property TotalMachineTime As Decimal
        Get
            Return Me.TotalComponentMachineTime + _
                Me.TotalWireMachineTime + TwistedPairsMachineTime
        End Get
    End Property
#End Region
#Region "7 Time "
    Public ReadOnly Property TotalLaborTime() As Decimal
        Get
            Return (Me.TotalSetupTime + Me.TotalMachineTime)
        End Get
    End Property
    Public Property TimeMultiplier As Decimal
        Get
            Return _TimeMultiplier
        End Get
        Set(ByVal value As Decimal)
            Me._TimeMultiplier = value
            Me.SendEvents()
        End Set
    End Property
    Public ReadOnly Property AdjustedTotalLaborTime() As Decimal
        Get
            Return Me._TimeMultiplier * Me.TotalLaborTime
        End Get
    End Property

    Public ReadOnly Property AdjustedTotalLaborTimeHours() As Decimal
        Get
            Return CDec(Me.AdjustedTotalLaborTime) / (60 * 60)
        End Get
    End Property
#End Region
#Region "8 Labor "
    Public Property LaborRate As Decimal
        Get
            Return _LaborRate
        End Get
        Set(ByVal Value As Decimal)
            _LaborRate = Value
            Me.SendEvents()
        End Set
    End Property
    Public ReadOnly Property LaborCost As Decimal
        Get
            Return AdjustedTotalLaborTimeHours * LaborRate
        End Get
    End Property
#End Region
#Region "9 Shipping "
    Public Property QuoteType As String
        Get
            Return _QuoteType
        End Get
        Set(ByVal value As String)
            _QuoteType = value
            Me.SendEvents()
        End Set
    End Property
    Public Property MinimumOrderQuantity As Integer
        Get
            Return _MinimumOrderQuantity
        End Get
        Set(ByVal Value As Integer)
            _MinimumOrderQuantity = Value
            Me.SendEvents()
        End Set
    End Property
    Public Property SingleDefiniteQuantity As Integer
        Get
            Return _SingleDefQuantity
        End Get
        Set(ByVal Value As Integer)
            _SingleDefQuantity = Value
            Me.SendEvents()
        End Set
    End Property
    Public Property OrderQuantity As Integer
        Get
            Return _OrderQuantity
        End Get
        Set(ByVal Value As Integer)
            _OrderQuantity = Value
            Me.SendEvents()
        End Set
    End Property
    Public ReadOnly Property FunctionalQuantity As Integer
        Get
            Dim result As Integer
            If _QuoteType = QuoteTypeList.Production Then
                result = Me._MinimumOrderQuantity
            ElseIf _QuoteType = QuoteTypeList.SingleDefinite Then
                result = Me._SingleDefQuantity
            Else
                result = Me._OrderQuantity
            End If
            Return result
        End Get
    End Property
    Public Property ShippingContainer() As String
        Get
            Return _ShippingBox
        End Get
        Set(ByVal Value As String)
            _ShippingBox = Value
            Me.SendEvents()
        End Set
    End Property
    Public ReadOnly Property ShippingContainerCost As Decimal
        Get
            If (_ShippingBox Is Nothing) Then
                Return 0
            End If
            Return Shipping.Lookup(Me._ShippingBox)
        End Get
    End Property
    Public ReadOnly Property ShippingContainerCostPerOrder As Decimal
        Get
            If (Me.FunctionalQuantity = 0) Then
                Return 0
            End If
            Return Me.ShippingContainerCost / Me.FunctionalQuantity
        End Get
    End Property
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
#Region "10 Summary "
    Public ReadOnly Property SummaryMaterial As Decimal
        Get
            Return TotalMaterialCost
        End Get
    End Property
    Public ReadOnly Property SummaryTVMCIncrement As Decimal
        Get
            Dim result As Decimal = TotalVariableMaterialCost - TotalMaterialCost
            Return (result)
        End Get
    End Property
    Public ReadOnly Property SummaryDirectLabor As Decimal
        Get
            Return AdjustedTotalLaborTimeHours * 9.5
        End Get
    End Property
    Public ReadOnly Property SummaryOverhead As Decimal
        Get
            Return SummaryDirectLabor * 1.465
        End Get
    End Property
    Public Property SummaryAdjustment As Decimal
        Get
            Return _SummaryAdjustmentMultiplyer
        End Get
        Set(ByVal value As Decimal)
            _SummaryAdjustmentMultiplyer = value
            Me.SendEvents()
        End Set
    End Property
    Public ReadOnly Property SummaryCostAdjustment As Decimal
        Get
            Dim result As Decimal = 
                (
                SummaryMaterial + 
                SummaryTVMCIncrement + 
                SummaryDirectLabor + 
                SummaryOverhead
                ) * SummaryAdjustment
            Return result
        End Get
    End Property
    Public ReadOnly Property SummaryProfit As Decimal
        Get
            Dim result As Decimal = 
                AdjustedTotalUnitCost - 
                (
                SummaryMaterial + 
                SummaryTVMCIncrement + 
                SummaryDirectLabor + 
                SummaryOverhead + 
                SummaryCostAdjustment
                )
            Return result
        End Get
    End Property
#End Region
#Region "11 Total "
    Public ReadOnly Property TotalUnitCost() As Decimal
        Get
            Return _
                Me.TotalVariableMaterialCost + _
                Me.LaborCost
        End Get
    End Property
    Public Property ManufacturingMarkup As Decimal
        Get
            Return _ManufacturingMarkup
        End Get
        Set(ByVal value As Decimal)
            Me._ManufacturingMarkup = value
            Me.SendEvents()
        End Set
    End Property
    Public ReadOnly Property AdjustedTotalUnitCost() As Decimal
        Get
            Return (Me._ManufacturingMarkup * Me.TotalUnitCost) + Me.SummaryCostAdjustment
        End Get
    End Property

#End Region

#Region " Methods "
    Private Function SumCost(ByVal IsWire As Boolean) As Decimal
        Dim result As Decimal
        For Each detail As Model.Template.Detail In _Header.Details
            If detail.Product.IsWire = IsWire Then
                result += detail.TotalCost
            End If
        Next
        Return result
    End Function

    Private Function SumTime(ByVal IsWire As Boolean) As Decimal
        Dim result As Decimal
        For Each detail As Model.Template.Detail In _Header.Details
            If detail.Product.IsWire = IsWire Then
                Dim c As Model.Common.IHasTotalMachineTime = detail.QuoteDetailProperties
                result += c.TotalMachineTime
            End If
        Next
        Return result
    End Function

    Private Function SumQty(ByVal IsWire As Boolean) As Decimal
        Dim result As Decimal
        For Each detail As Model.Template.Detail In _Header.Details
            If detail.Product.IsWire = IsWire Then
                result += detail.Qty
            End If
        Next
        Return result
    End Function

    Private Function Count(ByVal IsWire As Boolean) As Decimal
        Dim result As Integer
        For Each detail As Model.Template.Detail In _Header.Details
            If detail.Product.IsWire = IsWire Then
                result += 1
            End If
        Next
        Return result
    End Function
#End Region

End Class
