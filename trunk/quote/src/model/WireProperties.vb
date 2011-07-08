Imports System.ComponentModel
Imports DCS.Quote.Model
Imports System.Reflection

Namespace Model
    Public Class WireProperties
        Implements INotifyPropertyChanged

        Private WithEvents _QuoteDetail As QuoteDetail

        Public Event PropertyChanged As PropertyChangedEventHandler _
            Implements INotifyPropertyChanged.PropertyChanged

        Public Sub New(ByVal QuoteDetail As QuoteDetail)
            _QuoteDetail = QuoteDetail
        End Sub

        Protected Overrides Sub Finalize()
            _QuoteDetail = Nothing
        End Sub

        Public ReadOnly Property Gage As String
            Get
                Return _QuoteDetail.Product.Gage.Trim
            End Get
        End Property

        <DescriptionAttribute("Length in Decameters")> _
        Public ReadOnly Property Length As String
            Get
                Return _QuoteDetail.Qty
            End Get
        End Property

        <DescriptionAttribute("Length / 3.048")> _
        Public ReadOnly Property LengthFeet As Decimal
            Get
                Return Math.Round(_QuoteDetail.Qty / 3.048, 4)
            End Get
        End Property

        <DescriptionAttribute("Pounds per foot")> _
        Public ReadOnly Property WeightPerDecameter As Decimal
            Get
                Return Weights.FindWeight(Me.Gage)
            End Get
        End Property

        <DescriptionAttribute("WeightPerFoot * LengthFeet" + Chr(10) + "(Pounds)")> _
        Public ReadOnly Property TotalWeight As Decimal
            Get
                Return Math.Round(WeightPerDecameter * Length, 4)
            End Get
        End Property

        Private Sub _QuoteDetail_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _QuoteDetail.PropertyChanged
            SendEvents()
        End Sub

        Private Sub SendEvents()
            Dim info() As PropertyInfo
            info = GetType(WireProperties).GetProperties()
            For Each i As PropertyInfo In info
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(i.Name))
            Next
            Me._QuoteDetail.QuoteHeader.ComputationProperties.SendEvents()
        End Sub

    End Class
End Namespace

