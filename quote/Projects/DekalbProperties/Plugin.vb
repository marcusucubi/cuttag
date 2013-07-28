Public Class Plugin
    Implements Host.IInit

    Public Sub Init() Implements Host.IInit.Init

        Dim s As New StatusBarUpdater
        s.Init()

    End Sub

End Class
