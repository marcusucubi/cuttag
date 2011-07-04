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

        <CategoryAttribute("Supply Chain"), _
        DisplayName("Initial Lead Time"), _
        DescriptionAttribute("Minimum number of days between the first purchase order and delivery")> _
        Public Property LeadTimeInitial As Integer

        <CategoryAttribute("Supply Chain"), _
        DisplayName("Standard Lead Time"), _
        DescriptionAttribute("Minimum number of days between the purchase order and delivery")> _
        Public Property LeadTimeStandard As Integer

        <CategoryAttribute("Supply Chain"), _
        DisplayName("Estimated Annual Units"), _
        DescriptionAttribute("Estimated Annual Units")> _
        Public Property EstimatedAnnualUnits As Integer

        <CategoryAttribute("Misc"), _
        DisplayName("Material MarkUp"), _
        DescriptionAttribute("")> _
        Public Property MaterialMarkUp As Decimal

        <CategoryAttribute("Misc"), _
        DisplayName("Copper Scrap"), _
        DescriptionAttribute("")> _
        Public Property CopperScrap As Decimal

        <CategoryAttribute("Misc"), _
        DisplayName("Start Date"), _
        DescriptionAttribute("Date the quote is started")> _
        Public Property StartDate As DateTime

        <CategoryAttribute("Misc"), _
        DisplayName("Completed Date"), _
        DescriptionAttribute("Date the quote is completed")> _
        Public Property CompletedDate As DateTime

        <CategoryAttribute("Misc"), _
        DisplayName("Verified Date"), _
        DescriptionAttribute("Date the quote is verified")> _
        Public Property VerifiedDate As DateTime

        Friend Sub SendEvents()
            Dim info() As PropertyInfo
            info = GetType(QuoteHeader).GetProperties()
            For Each i As PropertyInfo In info
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(i.Name))
            Next
        End Sub

    End Class
End Namespace
