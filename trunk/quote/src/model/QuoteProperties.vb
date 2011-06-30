Imports System.ComponentModel
Imports DCS.Quote.Model
Imports System.Reflection

Namespace Model

    Public Class QuoteProperties
        Implements INotifyPropertyChanged

        Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        Private _QuoteHeader As QuoteHeader

        Public Property Minimum As Integer
        Public Property LeadTimeInitial As Integer
        Public Property LeadTimeStandard As Integer
        Public Property EstimatedAnnualUnits As Integer
        Public Property MaterialMarkUp As Decimal
        Public Property CU_Scrap As Decimal
        Public Property LaborMinutes As Integer

        Public ReadOnly Property TotalLength() As Decimal
            Get
                Return SumQty(UnitOfMeasure.BY_LENGTH)
            End Get
        End Property

        Public ReadOnly Property TotalLengthFeet() As Decimal
            Get
                Return Math.Round(SumQty(UnitOfMeasure.BY_LENGTH) / 3.048, 2)
            End Get
        End Property

        Public ReadOnly Property TotalQty() As Decimal
            Get
                Return SumQty(UnitOfMeasure.BY_EACH)
            End Get
        End Property

        Public ReadOnly Property TotalCost() As Decimal
            Get
                Return SumCost()
            End Get
        End Property

        Public ReadOnly Property WireCount() As Integer
            Get
                Return Count(UnitOfMeasure.BY_LENGTH)
            End Get
        End Property

        Public ReadOnly Property PartCount() As Integer
            Get
                Return Count(UnitOfMeasure.BY_EACH)
            End Get
        End Property

        Public ReadOnly Property PartAndWireCount() As Integer
            Get
                Return Count(UnitOfMeasure.BY_EACH) + Count(UnitOfMeasure.BY_LENGTH)
            End Get
        End Property

        Public Sub New(ByVal QuoteHeader As QuoteHeader)
            _QuoteHeader = QuoteHeader
        End Sub

        Private Function SumCost() As Decimal
            Dim result As Decimal
            For Each detail As QuoteDetail In _QuoteHeader.QuoteDetails
                result += detail.TotalCost
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
