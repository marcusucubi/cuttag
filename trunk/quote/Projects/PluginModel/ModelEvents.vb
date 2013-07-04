
Imports System.ComponentModel

Imports Model.Common

Public Class ModelEventArgs
    Public Property ID
End Class

Public Class ModelEvents

    Public Shared ReadOnly ActiveEvents As ModelEvents = New ModelEvents

    Public Delegate Sub ModelEventHandler(source As Object, args As ModelEventArgs)

    Public Shared Event TemplateCreated As ModelEventHandler

    Public Shared Sub NotifyTemplateCreated(id As Integer)
        Dim args As New ModelEventArgs
        args.ID = id
        RaiseEvent TemplateCreated(Nothing, args)
    End Sub

End Class

