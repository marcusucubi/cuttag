Imports System.ComponentModel
Imports System.Reflection

Namespace Quote

    Public Class Header
        Inherits Common.Header

        Public Sub New()
            Me.New(0, "", "", 0, "", Date.Now, Date.Now)
        End Sub

        Public Sub New(ByVal id As Long, _
                       ByVal requestForQuoteNumber As String, _
                       ByVal partNumber As String, _
                       ByVal templateId As Long, _
                       ByVal initials As String, _
                       ByVal createdDate As DateTime, _
                       ByVal lastModifiedDate As DateTime)

            Dim p As Quote.PrimaryProperties = New Quote.PrimaryProperties(id, _
                requestForQuoteNumber, partNumber, _
                initials, createdDate, lastModifiedDate)
            MyBase.SetPrimaryProperties(p)
            p.SetTemplateID(templateId)
            MyBase.SetComputationProperties(New Common.ComputationProperties)
            MyBase.SetOtherProperties(New Common.OtherProperties)
            MyBase.ID = id
            MyBase.IsQuote = True
        End Sub

        Public Shadows ReadOnly Property ID As Integer
            Get
                Return PrimaryProperties.CommonId
            End Get
        End Property

        Public Overrides Function NewDetail(ByVal product As Model.Product) As Common.Detail
            Dim oo As Detail = New Detail(Me, product)
            MyBase.Details.Add(oo)
            Return oo
        End Function

        Public Sub SetPublicPrimaryProperties(ByVal value As Object)
            MyBase.SetPrimaryProperties(value)
        End Sub
        
        Public Sub SetPublicComputationProperties(ByVal value As Object)
            MyBase.SetComputationProperties(value)
        End Sub

        Public Sub SetPublicOtherProperties(ByVal value As Object)
            MyBase.SetOtherProperties(value)
        End Sub

        Public Sub SetPublicCustomProperties(ByVal value As Object)
            MyBase.SetCustomProperties(value)
        End Sub

        Public Sub SetPublicNoteProperties(ByVal value As Object)
            MyBase.SetNoteProperties(value)
        End Sub
        
    End Class
End Namespace
