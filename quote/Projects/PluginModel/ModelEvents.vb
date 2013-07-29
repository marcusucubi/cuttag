Imports System.ComponentModel

Imports Model.Common

Public Class ModelEventArgs 
    Inherits EventArgs 
    
    Public Property Id
End Class

Public NotInheritable Class ModelEvents

    Private Shared _ActiveEvents As ModelEvents = New ModelEvents

    Public Shared Event TemplateCreated As EventHandler(Of ModelEventArgs)

    Public Shared Event TemplateViewed As EventHandler

    Public Shared Event QuoteViewed As EventHandler

    Private Sub New
        
    End Sub
    
    Public Shared ReadOnly Property ActiveEvents As ModelEvents 
        Get
            Return _ActiveEvents
        End Get
    End Property
    
    Public Shared Sub NotifyTemplateCreated(id As Integer)
        Dim args As New ModelEventArgs
        args.Id = id
        RaiseEvent TemplateCreated(Nothing, args)
    End Sub

    Public Shared Sub NotifyTemplateViewed()
        RaiseEvent TemplateViewed(Nothing, New EventArgs())
    End Sub

    Public Shared Sub NotifyQuoteViewed()
        RaiseEvent QuoteViewed(Nothing, New EventArgs())
    End Sub

End Class

