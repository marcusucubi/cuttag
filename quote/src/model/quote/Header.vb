Imports System.ComponentModel
Imports System.Reflection

Namespace Model.Quote

    Public Class Header
        Inherits Common.Header

        Public Sub New()
            Me.New(0, "", "", "")
        End Sub

        Public Sub New(ByVal id As Long, _
                       ByVal CustomerName As String, _
                       ByVal RequestForQuoteNumber As String, _
                       ByVal PartNumber As String)
            Me._PrimaryProperties = New Quote.PrimaryPropeties(Me, id, _
                CustomerName, RequestForQuoteNumber, PartNumber)
            Me._ComputationProperties = New Common.ComputationProperties
            Me._OtherProperties = New Common.OtherProperties
            MyBase.ID = id
            MyBase.IsQuote = True
        End Sub

        Public Sub SetComputationProperties(ByVal o As Object)
            Me._ComputationProperties = o
        End Sub

        Public Sub SetOtherProperties(ByVal o As Object)
            Me._OtherProperties = o
        End Sub

        Public Shadows ReadOnly Property ID As Integer
            Get
                Return PrimaryProperties.CommonID
            End Get
        End Property

        Public Overrides Function NewDetail(ByVal product As Product) As Common.Detail

            Dim oo As Detail = New Detail(Me, product)

            'AddHandler oo.PropertyChanged, AddressOf ForwardEvent
            MyBase.Details.Add(oo)

            Return oo
        End Function

    End Class
End Namespace
