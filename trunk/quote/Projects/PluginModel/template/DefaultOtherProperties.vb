Imports System.ComponentModel
Imports System.Reflection

Imports Model

Namespace Template

    Public NotInheritable Class DefaultOtherProperties
        Inherits OtherProperties

        Public Sub New(ByVal quoteHeader As Header)
            MyBase.New(quoteHeader)
        End Sub

    End Class

End Namespace
