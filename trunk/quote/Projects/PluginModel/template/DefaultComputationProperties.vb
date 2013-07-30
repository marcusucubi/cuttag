Imports System.ComponentModel
Imports System.Reflection
Imports System.Math

Namespace Template

    Public NotInheritable Class DefaultComputationProperties
        Inherits Template.ComputationProperties

        Public Sub New(ByVal header As Template.Header)
            MyBase.New(header)
        End Sub

    End Class
End Namespace
