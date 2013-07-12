Imports System.ComponentModel
Imports System.Reflection

Imports Model.Quote

Namespace Template

    Public Class DefaultWireProperties
        Inherits Template.WireProperties

        Public Sub New(ByVal QuoteDetail As Model.Template.Detail)
            MyBase.New(QuoteDetail)
        End Sub

    End Class
End Namespace

