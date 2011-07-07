Imports System.ComponentModel
Imports DCS.Quote.Model
Imports System.Reflection

Namespace Model
    Public Class WireProperties
        Implements INotifyPropertyChanged

        Private _QuoteDetail As QuoteDetail

        Public Event PropertyChanged As PropertyChangedEventHandler _
            Implements INotifyPropertyChanged.PropertyChanged

        Public Sub New(ByVal QuoteDetail As QuoteDetail)
            _QuoteDetail = QuoteDetail
        End Sub

        Public ReadOnly Property Gage As String
            Get
                Return _QuoteDetail.Product.Gage.Trim
            End Get
        End Property

    End Class
End Namespace

