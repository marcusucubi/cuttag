Imports System.ComponentModel
Imports System.Reflection

Public Class DekalbWireProperties
    Inherits Model.Template.DefaultWireProperties

    Private _QuoteDetail As Model.Template.Detail

    Public Sub New(ByVal QuoteDetail As Model.Template.Detail)
        MyBase.New(QuoteDetail)
        _QuoteDetail = QuoteDetail
    End Sub

End Class

