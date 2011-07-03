Imports System.ComponentModel
Imports DCS.Quote.Model
Imports System.Reflection

Namespace Model
    Public Class GageProperties
        Implements INotifyPropertyChanged

        Public Sub New(ByVal QuoteHeader As QuoteHeader)
            _QuoteHeader = QuoteHeader
        End Sub

        Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        Private _QuoteHeader As QuoteHeader

        <CategoryAttribute("Gage"), _
        DescriptionAttribute("")> _
        Public ReadOnly Property Gage18 As Decimal
            Get
                Return 0
            End Get
        End Property

    End Class
End Namespace
