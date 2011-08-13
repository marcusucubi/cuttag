Namespace Common

    Public Class NoteProperties
        Inherits SaveableProperties

        Private _Note As String

        Public Property Note As String
            Get
                Return _Note
            End Get
            Set(ByVal value As String)
                If _Note <> value Then
                    _Note = value
                    Console.WriteLine(" -- " + value)
                    Me.SendEvents()
                End If
            End Set
        End Property

    End Class

End Namespace
