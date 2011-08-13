Imports System.ComponentModel

Namespace Model.BOM

    Public Class NoteProperties
        Inherits Common.NoteProperties

        Private _QuoteHeader As Header

        Public Sub New(ByVal QuoteHeader As Header)
            _QuoteHeader = QuoteHeader
        End Sub

    End Class
End Namespace

