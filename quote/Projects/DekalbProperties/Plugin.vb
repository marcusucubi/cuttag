Public Class Plugin
    Implements PluginHost.IPluginInit

    Public Sub Init() Implements PluginHost.IPluginInit.Init

        Dim s As New StatusBarUpdater
        s.Init()

    End Sub

End Class
