Imports System.ComponentModel
Imports System.Reflection
Imports System.Math

Namespace Template

    Public NotInheritable Class DefaultComputationProperties
        Inherits Template.ComputationProperties

        Public Sub New(ByVal Header As Template.Header)
            MyBase.New(Header)
        End Sub

    End Class
End Namespace
