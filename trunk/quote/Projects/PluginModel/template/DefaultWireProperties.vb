Imports System.ComponentModel
Imports System.Reflection

Imports Model.Quote

Namespace Template

    Public Class DefaultWireProperties
        Inherits Template.WireProperties

        Public Sub New(ByVal quoteDetail As Model.Template.Detail)
            MyBase.New(quoteDetail)
        End Sub

    End Class
End Namespace

