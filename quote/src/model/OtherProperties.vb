Imports System.ComponentModel
Imports DCS.Quote.Model
Imports System.Reflection

Namespace Model

    Public Class OtherProperties
        Implements INotifyPropertyChanged

        Private _QuoteHeader As QuoteHeader

        Public Sub New(ByVal QuoteHeader As QuoteHeader)
            _QuoteHeader = QuoteHeader
        End Sub

        Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

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

        Friend Sub SendEvents()
            Dim info() As PropertyInfo
            info = GetType(QuoteHeader).GetProperties()
            For Each i As PropertyInfo In info
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(i.Name))
            Next
        End Sub

    End Class
End Namespace
