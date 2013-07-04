Imports System.ComponentModel
Imports System.Reflection

Imports Model.Quote

Namespace BOM

    ''' <summary>
    ''' Computation properties for wires.
    ''' </summary>
    ''' <remarks>
    ''' This class should contain computation related
    ''' code.  Any display related code should
    ''' should be in DisplayableWireProperties.
    ''' </remarks>
    Public Class WireProperties
        Inherits Common.WireProperties

        Private _CopperWeightPer1000Ft As Decimal
        Private WithEvents _QuoteDetail As Detail

        Public Sub New(ByVal QuoteDetail As Detail)
            _QuoteDetail = QuoteDetail
        End Sub

        Protected Overrides Sub Finalize()
            _QuoteDetail = Nothing
        End Sub

        Public Overridable ReadOnly Property Gage As String
            Get
                Return _QuoteDetail.Product.Gage.Trim
            End Get
        End Property

        Public Overridable ReadOnly Property Length As Decimal
            Get
                Return _QuoteDetail.Qty
            End Get
        End Property

        Public Property Description() As String
            Get
                Return _QuoteDetail.Product.Description
            End Get
            Set(ByVal value As String)
                _QuoteDetail.Product.Description = value
                SendEvents()
            End Set
        End Property

        Public Overridable ReadOnly Property LengthFeet As Decimal
            Get
                Return _QuoteDetail.Qty / 3.048
            End Get
        End Property

        Public Overridable Property PoundsPer1000Feet As Decimal
            Get
                Return Me._CopperWeightPer1000Ft
            End Get
            Set(ByVal value As Decimal)
                Me._CopperWeightPer1000Ft = value
                SendEvents()
            End Set
        End Property

        Public Overridable ReadOnly Property TotalWeight As Decimal
            Get
                Return PoundsPer1000Feet / 1000 * Me.LengthFeet
            End Get
        End Property

        Private Sub _QuoteDetail_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _QuoteDetail.PropertyChanged
            SendEvents()
        End Sub

        Public Overridable Property Quantity() As Decimal
            Get
                Return Me._QuoteDetail.Qty
            End Get
            Set(ByVal value As Decimal)
                Me._QuoteDetail.Qty = value
                Me.SendEvents()
            End Set
        End Property

        Public Overridable Property UnitCost() As Decimal
            Get
                Return _QuoteDetail.UnitCost
            End Get
            Set(ByVal value As Decimal)
                _QuoteDetail.UnitCost = value
                Me.SendEvents()
            End Set
        End Property

        Public Overridable Property UnitOfMeasure() As String
            Get
                Return _QuoteDetail.UOM
            End Get
            Set(ByVal value As String)
                _QuoteDetail.UOM = value
            End Set
        End Property

        Private Overloads Sub SendEvents()
            MyBase.SendEvents()
            Me._QuoteDetail.Header.ComputationProperties.SendEvents()
        End Sub

    End Class
End Namespace

