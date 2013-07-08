
Imports System.ComponentModel

Imports Model.Common

Public Class ModelEventArgs
    Public Property ID
End Class

Public Class ModelEvents

    Public Shared ReadOnly ActiveEvents As ModelEvents = New ModelEvents

    Public Delegate Sub ModelEventHandler(source As Object, args As ModelEventArgs)

    Public Shared Event TemplateCreated As ModelEventHandler

    Public Shared Event TemplateViewed As EventHandler

    Public Shared Event QuoteViewed As EventHandler

    Public Shared Sub NotifyTemplateCreated(id As Integer)
        Dim args As New ModelEventArgs
        args.ID = id
        RaiseEvent TemplateCreated(Nothing, args)
    End Sub

    Public Shared Sub NotifyTemplateViewed()
        RaiseEvent TemplateViewed(Nothing, New EventArgs())
    End Sub

    Public Shared Sub NotifyQuoteViewed()
        RaiseEvent QuoteViewed(Nothing, New EventArgs())
    End Sub

End Class

