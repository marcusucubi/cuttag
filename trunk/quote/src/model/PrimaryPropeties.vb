Imports System.ComponentModel
Imports DCS.Quote.Model
Imports System.Reflection

Namespace Model

    Public Class PrimaryPropeties
        Implements INotifyPropertyChanged

        Private _QuoteHeader As QuoteHeader
        Private _QuoteNumnber As Integer

        Public Sub New(ByVal QuoteHeader As QuoteHeader)
            _QuoteHeader = QuoteHeader
            CustomerName = "Caterpillar Inc."
        End Sub

        Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        <CategoryAttribute("Quote"), _
        DisplayName("Customer"), _
        DescriptionAttribute("The customer name")> _
        Public Property CustomerName As String

        <CategoryAttribute("Quote"), _
        DisplayName("RFQ"), _
        DescriptionAttribute("Request For Quote")> _
        Public Property RequestForQuoteNumber As String

        <CategoryAttribute("Quote"), _
        DisplayName("Part Number"), _
        DescriptionAttribute("Part Number")> _
        Public Property PartNumber As String

        <CategoryAttribute("Quote"), _
        DisplayName("QuoteNumnber"), _
        DescriptionAttribute("Quote Numnber")> _
        Public ReadOnly Property QuoteNumnber As Integer
            Get
                Return Me._QuoteNumnber
            End Get
        End Property

    End Class
End Namespace
