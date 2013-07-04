Imports System.ComponentModel

Namespace BOM

    Public Class NoteProperties
        Inherits Common.NoteProperties

        Private _QuoteHeader As Header
        Private _Note As String
        Private _Note2Customer As String

        Public Sub New(ByVal QuoteHeader As Header)
            _QuoteHeader = QuoteHeader
        End Sub
        <DisplayName("Note-Internal"), _
         CategoryAttribute("Notes")> _
        Public Property Note As String
            Get
                Return _Note
            End Get
            Set(ByVal value As String)
                If _Note <> value Then
                    _Note = value
                    Me.SendEvents()
                End If
            End Set
        End Property
        <DisplayName("Note-To Customer"), _
         CategoryAttribute("Notes")> _
        Public Property Note2Customer As String
            Get
                Return _Note2Customer
            End Get
            Set(ByVal value As String)
                If _Note2Customer <> value Then
                    _Note2Customer = value
                    Me.SendEvents()
                End If
            End Set
        End Property

    End Class
End Namespace