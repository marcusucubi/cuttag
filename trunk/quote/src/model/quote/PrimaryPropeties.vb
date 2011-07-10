Imports System.ComponentModel
Imports DCS.Quote.Model
Imports System.Reflection

Namespace Model.Quote

    Public Class PrimaryPropeties
        Inherits SaveableProperties
        Implements INotifyPropertyChanged

        Private _QuoteHeader As Header
        Private _QuoteNumnber As Integer

        Public Sub New(ByVal QuoteHeader As Header, ByVal id As Long)
            _QuoteHeader = QuoteHeader
            Me._QuoteNumnber = id
            CustomerName = "Caterpillar Inc."
        End Sub

        Public Event PropertyChanged(ByVal sender As Object, _
                                     ByVal e As PropertyChangedEventArgs) _
            Implements INotifyPropertyChanged.PropertyChanged

        <CategoryAttribute("Quote"), _
        DisplayName("Customer"), _
        DescriptionAttribute("The customer name")> _
        Public Property CustomerName As String = ""

        <CategoryAttribute("Quote"), _
        DisplayName("RFQ"), _
        DescriptionAttribute("Request For Quote")> _
        Public Property RequestForQuoteNumber As String = ""

        <CategoryAttribute("Quote"), _
        DisplayName("Part Number"), _
        DescriptionAttribute("Part Number")> _
        Public Property PartNumber As String = ""

        <CategoryAttribute("Quote"), _
        DisplayName("QuoteNumnber"), _
        DescriptionAttribute("Quote Numnber")> _
        Public ReadOnly Property QuoteNumnber As Integer
            Get
                Return Me._QuoteNumnber
            End Get
        End Property

        Public Sub SetID(ByVal id As Integer)
            Me._QuoteNumnber = id
            SendEvents()
        End Sub

        Friend Sub SendEvents()
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(""))
            MyBase.MakeDirty()
        End Sub

    End Class
End Namespace
