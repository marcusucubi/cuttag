Imports System.ComponentModel

Namespace Model.BOM

    Public Class NoteProperties
        Inherits Common.NoteProperties

        Private _QuoteHeader As Header
        Private _Note As String

        Public Sub New(ByVal QuoteHeader As Header)
            _QuoteHeader = QuoteHeader
        End Sub

        Public Property Note As String
            Get
                Return _Note
            End Get
            Set(ByVal value As String)
                _Note = value
                Me.SendEvents()
            End Set
        End Property

    End Class
End Namespace

