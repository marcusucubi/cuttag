Imports System.ComponentModel
Imports System.Reflection

Imports Model

Namespace Template

    Public NotInheritable Class DefaultOtherProperties
        Inherits OtherProperties

        Public Sub New(ByVal QuoteHeader As Header)
            MyBase.New(QuoteHeader)
        End Sub

    End Class

End Namespace
