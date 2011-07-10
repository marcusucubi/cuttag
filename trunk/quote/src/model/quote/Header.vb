Imports System.ComponentModel
Imports System.Reflection

Namespace Model.Quote

    Public Class Header
        Inherits Common.Header

        Public Sub New()
            Me.New(0)
        End Sub

        Public Sub New(ByVal id As Long)
            Me._PrimaryProperties = New PrimaryPropeties(Me, id)
            Me._ComputationProperties = New ComputationProperties(Me)
            Me._OtherProperties = New OtherProperties(Me)
        End Sub

        Public Shadows ReadOnly Property IsQuote As Boolean
            Get
                Return True
            End Get
        End Property

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
