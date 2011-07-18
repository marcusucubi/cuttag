Imports System.ComponentModel
Imports System.Reflection

Namespace Model.Quote

    Public Class Header
        Inherits Common.Header

        Public Sub New()
            Me.New(0, "", "", "", 0, "", Date.Now, Date.Now)
        End Sub

        Public Sub New(ByVal id As Long, _
                       ByVal CustomerName As String, _
                       ByVal RequestForQuoteNumber As String, _
                       ByVal PartNumber As String, _
                       ByVal TemplateID As Long, _
                       ByVal Initials As String, _
                       ByVal CreatedDate As DateTime, _
                       ByVal LastModifiedDate As DateTime)

            Dim p As Quote.PrimaryPropeties = New Quote.PrimaryPropeties(Me, id, _
                CustomerName, RequestForQuoteNumber, PartNumber, _
                Initials, CreatedDate, LastModifiedDate)
            Me._PrimaryProperties = p
            p.SetTemplateID(TemplateID)
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
            MyBase.Details.Add(oo)
            Return oo
        End Function

    End Class
End Namespace
