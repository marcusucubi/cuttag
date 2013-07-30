Imports System.ComponentModel
Imports System.Reflection

Namespace Template

    Public Class DefaultComponentProperties
        Inherits Template.ComponentProperties

        Private _QuoteDetail As Template.Detail

        Public Sub New(ByVal quoteDetail As Template.Detail)
            MyBase.New(quoteDetail)
            _QuoteDetail = quoteDetail
        End Sub

        Protected Overloads Sub SendEvents()
            MyBase.SendEvents()
            Me._QuoteDetail.Header.ComputationProperties.SendEvents()
        End Sub

    End Class
End Namespace
